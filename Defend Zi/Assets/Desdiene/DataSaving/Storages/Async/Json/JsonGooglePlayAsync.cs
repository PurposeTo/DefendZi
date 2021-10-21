using System;
using System.Collections;
using System.Text;
using Desdiene.Coroutines;
using Desdiene.DataSaving.Datas;
using Desdiene.Json;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

namespace Desdiene.DataSaving.Storages
{
    /// <summary>
    /// Сохранить файл в json формате на облако в google play-е.
    /// 
    /// </summary>
    /// <typeparam name="T">Объект с данными, загружаемый/сохраняемый в хранилище.</typeparam>
    public class JsonGooglePlayAsync<T> : JsonStorageAsync<T> where T : IJsonSerializable, IValidData
    {
        private readonly PlayGamesPlatform _platform;
        private readonly ICoroutine _loadingData;
        private DateTime _startPlayingTime;
        private ISavedGameMetadata _currentGameMetadata; // todo зачем кешировать?

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
            _loadingData = new CoroutineWrap(mono);
        }

        private ISavedGameClient SavedGameClient => _platform.SavedGame;

        protected override void LoadJson(Action<bool, string> result)
        {
            _loadingData.StartContinuously(LoadingData(result));
        }

        protected override void SaveJson(string jsonData, Action<bool> successResult)
        {
            if (_currentGameMetadata == null)
            {
                Debug.LogWarning("Невозможно записать данные на облако, не открыв сохранения! " +
                    "Сохранение откроется автоматически при чтении данных с облака.");
            }
            else SaveData(Encoding.UTF8.GetBytes(jsonData), successResult);
        }

        protected override void CleanData(Action<bool> successResult)
        {
            throw new NotImplementedException();
        }

        private IEnumerator LoadingData(Action<bool, string> result)
        {
            Debug.Log("Начало операции загрузки данных с облака. Ожидание аутентификации пользователя.");
            yield return _loadingData.StartNested(new WaitUntil(() => _platform.IsAuthenticated()));
            Debug.Log("Операция загрузки данных с облака - пользователь аутентифицировался.");

            // Начать отсчет времени для текущей сессии игры
            _startPlayingTime = DateTime.Now;

            // Загрузка данных из облака
            LoadData((readingStatus, data) =>
            {
                Debug.Log($"Данные с облака были извлечены со статусом " + readingStatus);

                bool success = readingStatus == SavedGameRequestStatus.Success;
                string dataAsStr = success
                ? BytesToString(data)
                : null;
                result?.Invoke(success, dataAsStr);
            });
        }

        private void LoadData(Action<SavedGameRequestStatus, byte[]> completedCallback)
        {
            OpenData((openingStatus, gameMetadata) =>
            {
                Debug.Log("Данные с облака были открыты со статусом " + openingStatus);

                // Данные начинают считываться только тогда, когда статус их открытия "Success"
                //Чтение и открытие данных - разные операции
                if (openingStatus == SavedGameRequestStatus.Success)
                {
                    _currentGameMetadata = gameMetadata;
                }

                SavedGameClient.ReadBinaryData(gameMetadata, completedCallback);
            });
        }

        private void OpenData(Action<SavedGameRequestStatus, ISavedGameMetadata> OnSavedGameOpened)
        {
            Debug.Log("Начало открытия сохранения на облаке");
            SavedGameClient.OpenWithAutomaticConflictResolution(FileName,
                                                                DataSource.ReadCacheOrNetwork,
                                                                ConflictResolutionStrategy.UseLongestPlaytime,
                                                                OnSavedGameOpened);
        }

        private void SaveData(byte[] dataToSave, Action<bool> successCallback)
        {
            TimeSpan allPlayingTime = DateTime.Now - _startPlayingTime;
            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

            builder = builder.WithUpdatedPlayedTime(_currentGameMetadata.TotalTimePlayed + allPlayingTime)
                .WithUpdatedDescription("Saved game at " + DateTime.Now);
            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            SavedGameClient.CommitUpdate(_currentGameMetadata,
                                         updatedMetadata,
                                         dataToSave,
                                         (dataSavedStatus, gameMetadata) =>
                                         {
                                             OnSavedCreated(dataSavedStatus, gameMetadata, successCallback);
                                         });
        }

        private void OnSavedCreated(SavedGameRequestStatus dataSavedStatus,
                                    ISavedGameMetadata gameMetadata,
                                    Action<bool> successCallback)
        {
            Debug.Log($"Данные были записаны на облако со статусом " + dataSavedStatus);

            if (dataSavedStatus == SavedGameRequestStatus.Success)
            {
                // Так как при сохранении метаданные обновляются, то после его завершения необходимо их перезаписать
                _currentGameMetadata = gameMetadata;

                // Заново считаем время игры с момента записи сохранения
                _startPlayingTime = DateTime.Now;
            }

            successCallback?.Invoke(dataSavedStatus == SavedGameRequestStatus.Success);
        }

        private string BytesToString(byte[] data)
        {
            string dataAsStr = Encoding.UTF8.GetString(data);
            Debug.Log($"Длина извлеченного массива байт = { data.Length }.\nДанные в виде строки: " + dataAsStr);
            return dataAsStr;
        }
    }
}
