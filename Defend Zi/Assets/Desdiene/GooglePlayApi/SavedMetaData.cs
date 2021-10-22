using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

namespace Desdiene.GooglePlayApi
{
    public class SavedMetaData
    {
        private readonly ISavedGameClient _savedGameClient;
        private readonly string _fileName;
        private readonly DataSource _dataSource;
        private readonly ConflictResolutionStrategy _conflictResolutionStrategy;
        private readonly SavedGameRequestStatus _successStatus = SavedGameRequestStatus.Success;
        private ISavedGameMetadata _metadata = null;

        public SavedMetaData(ISavedGameClient savedGameClient,
                             string fileName,
                             DataSource dataSource = DataSource.ReadCacheOrNetwork,
                             ConflictResolutionStrategy conflictResolutionStrategy = ConflictResolutionStrategy.UseLongestPlaytime)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} can't be null or empty");
            }

            _savedGameClient = savedGameClient ?? throw new ArgumentNullException(nameof(savedGameClient));
            _fileName = fileName;
            _dataSource = dataSource;
            _conflictResolutionStrategy = conflictResolutionStrategy;
        }

        public void Get(Action<SavedGameRequestStatus, ISavedGameMetadata> result)
        {
            if (_metadata != null)
            {
                result?.Invoke(_successStatus, _metadata);
                return;
            }

            Debug.Log("Начало открытия сохранения на облаке");
            _savedGameClient.OpenWithAutomaticConflictResolution(_fileName,
                                                                 _dataSource,
                                                                 _conflictResolutionStrategy,
                                                                 UpdateMetaData + result);

        }


        public void Update(SavedGameMetadataUpdate updateForMetadata,
                           byte[] dataToSave,
                           Action<SavedGameRequestStatus> statusCallback)
        {
            if (dataToSave == null) throw new ArgumentNullException(nameof(dataToSave));

            Get((openedStatus, metadata) =>
            {
                if (openedStatus != _successStatus)
                {
                    statusCallback?.Invoke(openedStatus);
                    return;
                }

                _savedGameClient.CommitUpdate(metadata,
                                              updateForMetadata,
                                              dataToSave,
                                              UpdateMetaData + OnUpdated(statusCallback));
            });
        }

        private Action<SavedGameRequestStatus, ISavedGameMetadata> OnUpdated(Action<SavedGameRequestStatus> statusCallback)
        {
            return (dataSavedStatus, gameMetadata) =>
            {
                Debug.Log($"Данные были записаны на облако со статусом " + dataSavedStatus);
                statusCallback?.Invoke(dataSavedStatus);
            };
        }

        private void UpdateMetaData(SavedGameRequestStatus status, ISavedGameMetadata metadata)
        {
            Debug.Log("Метаданные с облака получены со статусом " + status);
            if (status == _successStatus) _metadata = metadata;
        }
    }
}
