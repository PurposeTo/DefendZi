using System;
using System.Collections;
using System.Text;
using Desdiene.Coroutines;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.DataStorageFactories.DataLoaders.Json;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.JsonConvertorWrapper;
using Desdiene.MonoBehaviourExtension;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

namespace Desdiene.DataStorageFactories.ConcreteLoaders
{
    public class GooglePlayJsonDataLoader<T> : JsonDataLoader<T>, IDataLoader<T> where T : IData, new()
    {
        private readonly PlayGamesPlatform _platform;
        private readonly ICoroutine _loadingData;

        public GooglePlayJsonDataLoader(MonoBehaviourExt mono,
            string fileName,
            IJsonConvertor<T> jsonConvertor,
            PlayGamesPlatform platform)
            : base(mono,
                  "Облачное хранилище google play-я",
                  fileName,
                  jsonConvertor)
        {
            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _loadingData = new CoroutineWrap(mono);
        }

        private DateTime _startPlayingTime;
        private ISavedGameMetadata _currentGameMetadata;

        private ISavedGameClient SavedGameClient => ((PlayGamesPlatform)Social.Active).SavedGame;

        protected override void LoadJsonData(Action<string> jsonDataCallback)
        {
            _loadingData.StartContinuously(LoadingData(jsonDataCallback));
        }

        protected override void SaveJsonData(string jsonData)
        {
            if (_currentGameMetadata == null)
            {
                Debug.LogWarning("Невозможно записать данные на облако, не открыв сохранения! " +
                    "Сохранение откроется автоматически при чтении данных с облака.");
            }
            else SaveData(Encoding.UTF8.GetBytes(jsonData));
        }

        private IEnumerator LoadingData(Action<string> jsonDataCallback)
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

                // Данные считаются извлеченными только тогда, когда статус их чтения "Success"
                if (readingStatus == SavedGameRequestStatus.Success)
                {
                    string cloudDataAsStr = Encoding.UTF8.GetString(data);
                    Debug.Log($"Длина извлеченного массива байт = { data.Length }.\nДанные в виде строки: " + cloudDataAsStr);
                    jsonDataCallback?.Invoke(cloudDataAsStr);
                }
            });
        }

        private void LoadData(Action<SavedGameRequestStatus, byte[]> completedCallback)
        {
            OpenSavedGame((openingStatus, gameMetadata) =>
            {
                Debug.Log("Данные с облака были открыты со статусом " + openingStatus);

                // Данные начинают считываться только тогда, когда статус их открытия "Success"
                if (openingStatus == SavedGameRequestStatus.Success)
                {
                    _currentGameMetadata = gameMetadata;
                    //Чтение и открытие данных - разные операции
                    SavedGameClient.ReadBinaryData(gameMetadata, completedCallback);
                }
            });
        }

        private void OpenSavedGame(Action<SavedGameRequestStatus, ISavedGameMetadata> OnSavedGameOpened)
        {
            Debug.Log("Начало открытия сохранения на облаке");
            SavedGameClient.OpenWithAutomaticConflictResolution(FileNameWithExtension,
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                OnSavedGameOpened);
        }

        private void SaveData(byte[] dataToSave)
        {
            TimeSpan allPlayingTime = DateTime.Now - _startPlayingTime;
            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

            builder = builder.WithUpdatedPlayedTime(_currentGameMetadata.TotalTimePlayed + allPlayingTime)
                .WithUpdatedDescription("Saved game at " + DateTime.Now);
            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            SavedGameClient.CommitUpdate(_currentGameMetadata, updatedMetadata, dataToSave, OnSavedCreated);
        }


        private void OnSavedCreated(SavedGameRequestStatus dataSavedStatus, ISavedGameMetadata gameMetadata)
        {
            Debug.Log($"Данные были записаны на облако со статусом " + dataSavedStatus);

            if (dataSavedStatus == SavedGameRequestStatus.Success)
            {
                // Так как при сохранении метаданные обновляются, то после его завершения необходимо их перезаписать
                _currentGameMetadata = gameMetadata;

                // Заново считаем время игры с момента записи сохранения
                _startPlayingTime = DateTime.Now;
            }
        }
    }
}
