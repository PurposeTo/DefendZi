using System;
using System.Collections;
using System.Text;
using Desdiene.Coroutines;
using Desdiene.DataSaving.Datas;
using Desdiene.GooglePlayApi;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Сохранить файл в json формате на облако в google play-е.
    /// Не используется JsonStorageAsync, тк надо иметь доступ к объекту с данными.
    /// Прежде чем работать с данными (чтение и запись) их надо открыть.
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
    public sealed class JsonGooglePlayAsync<T> : FileStorageAsync<T>
        where T : IJsonSerializable, IValidData, IDataWithTotalInAppTime
    {
        private const string JsonFileExtension = "json";
        private const string EmptyJson = "{}";
        private readonly IJsonDeserializer<T> _jsonDeserializer;

        private readonly SavedGameRequestStatus _successStatus = SavedGameRequestStatus.Success;
        private readonly PlayGamesPlatform _platform;
        private readonly SavedMetaData _metaData;
        private readonly ICoroutine _loadingData;

        public JsonGooglePlayAsync(MonoBehaviourExt mono,
                                   string baseFileName,
                                   IJsonDeserializer<T> jsonDeserializer,
                                   PlayGamesPlatform platform)
            : base("Асинхронное облачное хранилище Json данных в google play-е",
                   baseFileName,
                   JsonFileExtension)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            _jsonDeserializer = jsonDeserializer ?? throw new ArgumentNullException(nameof(jsonDeserializer));

            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _metaData = new SavedMetaData(() => _platform.SavedGame, FileName);
            _loadingData = new CoroutineWrap(mono);
        }

        private ISavedGameClient SavedGameClient => _platform.SavedGame;

        protected override void ReadData(Action<bool, T> result)
        {
            _loadingData.StartContinuously(ReadingData(result));
        }

        protected override void UpdateData(T data, Action<bool> successResult)
        {
            _metaData.Get(Update(data, successResult));
        }

        protected override void DeleteData(Action<bool> successResult)
        {
            _metaData.Get(Delete(successResult));
        }

        private IEnumerator ReadingData(Action<bool, T> result)
        {
            Debug.Log("Начало операции загрузки данных с облака. Ожидание аутентификации пользователя.");
            yield return _loadingData.StartNested(new WaitUntil(() => _platform.IsAuthenticated()));
            Debug.Log("Операция загрузки данных с облака - пользователь аутентифицировался.");

            _metaData.Get(Read(result));
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> Read(Action<bool, T> result)
        {
            return (openingStatus, metaData) =>
            {
                if (openingStatus != _successStatus)
                {
                    result?.Invoke(false, default);
                    return;
                }

                // Загрузка данных из облака
                SavedGameClient.ReadBinaryData(metaData, OnReaded(result));
            };
        }

        private Action<SavedGameRequestStatus, byte[]> OnReaded(Action<bool, T> result)
        {
            return (readingStatus, dataAsBytes) =>
            {
                Debug.Log($"Данные с облака были извлечены со статусом " + readingStatus);
                bool success = readingStatus == SavedGameRequestStatus.Success;
                T data = success
                ? BytesToObject(dataAsBytes)
                : default;
                result?.Invoke(success, data);
            };
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> Update(T data, Action<bool> successResult)
        {
            return (openingStatus, metaData) =>
            {
                if (openingStatus != _successStatus)
                {
                    successResult?.Invoke(false);
                    return;
                }

                byte[] dataToSave = ObjectToBytes(data);
                SavedGameMetadataUpdate updatedMetadata = new SavedGameMetadataUpdate
                    .Builder()
                    .WithUpdatedPlayedTime(data.TotalInAppTime)
                    .Build();

                _metaData.Update(updatedMetadata, dataToSave, OnUpdated(successResult));
            };
        }

        private Action<SavedGameRequestStatus> OnUpdated(Action<bool> successCallback)
        {
            return (updatedStatus) =>
            {
                bool success = updatedStatus == SavedGameRequestStatus.Success;
                successCallback?.Invoke(success);
            };
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> Delete(Action<bool> successResult)
        {
            return (openingStatus, metaData) =>
            {
                if (openingStatus != _successStatus)
                {
                    successResult?.Invoke(false);
                    return;
                }

                SavedGameClient.Delete(metaData);
                successResult?.Invoke(true);
            };
        }

        private T BytesToObject(byte[] data)
        {
            string dataAsStr = Encoding.UTF8.GetString(data);
            Debug.Log($"Длина извлеченного массива байт = { data.Length }.\nДанные в виде строки: " + dataAsStr);
            try
            {
                return _jsonDeserializer.ToObject(dataAsStr);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Deserialization exception! Json:\n{dataAsStr}\n\n{exception}");
                return _jsonDeserializer.ToObject(EmptyJson);
            }
        }

        private byte[] ObjectToBytes(T data)
        {
            string dataAsStr = data.ToJson();
            return Encoding.UTF8.GetBytes(dataAsStr);
        }
    }
}
