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
    /// 
    /// Прежде чем работать с данными (чтение и запись) их надо открыть.
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
    public class JsonGooglePlayAsync<T> : JsonStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        private readonly SavedGameRequestStatus _successStatus = SavedGameRequestStatus.Success;
        private readonly PlayGamesPlatform _platform;
        private readonly SavedMetaData _metaData;
        private readonly ICoroutine _loadingData;
        private DateTime _startPlayingTime;

        public JsonGooglePlayAsync(MonoBehaviourExt mono,
                                   string baseFileName,
                                   IJsonDeserializer<T> jsonDeserializer,
                                   PlayGamesPlatform platform)
            : base("Асинхронное облачное хранилище Json данных в google play-е",
                   baseFileName,
                   jsonDeserializer)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));

            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _metaData = new SavedMetaData(() => _platform.SavedGame, FileName);
            _loadingData = new CoroutineWrap(mono);
        }

        private ISavedGameClient SavedGameClient => _platform.SavedGame;

        protected override void ReadJson(Action<bool, string> result)
        {
            _loadingData.StartContinuously(LoadingData(result));
        }

        protected override void UpdateJson(string jsonData, Action<bool> successResult)
        {
            _metaData.Get(Update(jsonData, successResult));
        }

        protected override void DeleteData(Action<bool> successResult)
        {
            _metaData.Get(Delete(successResult));
        }

        private IEnumerator LoadingData(Action<bool, string> result)
        {
            Debug.Log("Начало операции загрузки данных с облака. Ожидание аутентификации пользователя.");
            yield return _loadingData.StartNested(new WaitUntil(() => _platform.IsAuthenticated()));
            Debug.Log("Операция загрузки данных с облака - пользователь аутентифицировался.");

            _metaData.Get(Read(result));
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> Read(Action<bool, string> result)
        {
            return (openingStatus, metaData) =>
            {
                if (openingStatus != _successStatus)
                {
                    result?.Invoke(false, null);
                    return;
                }

                // Начать отсчет времени для текущей сессии игры
                _startPlayingTime = DateTime.Now;

                // Загрузка данных из облака
                SavedGameClient.ReadBinaryData(metaData, OnReaded(result));
            };
        }

        private Action<SavedGameRequestStatus, byte[]> OnReaded(Action<bool, string> result)
        {
            return (readingStatus, data) =>
            {
                Debug.Log($"Данные с облака были извлечены со статусом " + readingStatus);
                bool success = readingStatus == SavedGameRequestStatus.Success;
                string dataAsStr = success
                ? BytesToString(data)
                : null;
                result?.Invoke(success, dataAsStr);
            };
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> Update(string jsonData, Action<bool> successResult)
        {
            return (openingStatus, metaData) =>
            {
                if (openingStatus != _successStatus)
                {
                    successResult?.Invoke(false);
                    return;
                }

                byte[] dataToSave = StringToBytes(jsonData);
                TimeSpan allPlayingTime = DateTime.Now - _startPlayingTime;
                SavedGameMetadataUpdate updatedMetadata = new SavedGameMetadataUpdate
                    .Builder()
                    .WithUpdatedPlayedTime(metaData.TotalTimePlayed + allPlayingTime)
                    .Build();

                _metaData.Update(updatedMetadata, dataToSave, OnUpdated(successResult));
            };
        }

        private Action<SavedGameRequestStatus> OnUpdated(Action<bool> successCallback)
        {
            return (updatedStatus) =>
            {
                bool success = updatedStatus == SavedGameRequestStatus.Success;
                // Заново считаем время игры с момента записи сохранения
                if (success) _startPlayingTime = DateTime.Now;

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

        private string BytesToString(byte[] data)
        {
            string dataAsStr = Encoding.UTF8.GetString(data);
            Debug.Log($"Длина извлеченного массива байт = { data.Length }.\nДанные в виде строки: " + dataAsStr);
            return dataAsStr;
        }

        private byte[] StringToBytes(string data) => Encoding.UTF8.GetBytes(data);
    }
}
