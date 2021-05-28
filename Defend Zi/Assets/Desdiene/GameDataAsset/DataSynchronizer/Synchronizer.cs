using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.GameDataAsset.Model;
using Desdiene.MonoBehaviourExtention;

namespace Desdiene.GameDataAsset.DataSynchronizer
{
    public class Synchronizer<T> : MonoBehaviourExtContainer, ISynchronizer where T : GameData
    {
        private readonly IModelInteraction<T> model;
        private readonly IStorageDataLoader<T> storageDataLoader;

        private readonly ICoroutine ChooseDataInfo;

        public Synchronizer(MonoBehaviourExt superMonoBehaviour, 
            IModelInteraction<T> model, 
            IStorageDataLoader<T> storageDataLoader)
            : base(superMonoBehaviour)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.storageDataLoader = storageDataLoader ?? throw new ArgumentNullException(nameof(storageDataLoader));

            ChooseDataInfo = superMonoBehaviour.CreateCoroutine();
        }

        private T cashData = null;


        public void LoadData()
        {
            storageDataLoader.Load(loadedData =>
            {
                    if (cashData == null)
                    {
                        cashData = loadedData;
                        T combinedData = CombineData(model.GetData(), loadedData);
                        model.SetData(combinedData);
                        return;
                    }
                    else
                    {
                        if (cashData.Equals(loadedData)) return;
                        else
                        {
                            ChooseData(loadedData, choosedData => model.SetData(choosedData));
                            return;
                        }
                    }
            });
        }

        public void SaveData()
        {
            storageDataLoader.Save(model.GetData());
        }

        private void ChooseData(T loadedData, Action<T> choosedData)
        {
            T currentData = model.GetData();

            monoBehaviourExt.ExecuteCoroutineContinuously(ChooseDataInfo,
                ChooseDataEnumerator(currentData, loadedData, choosedData));
        }

        private IEnumerator ChooseDataEnumerator(T currentData, T loadedData, Action<T> choosedData)
        {
            //todo предложить игроку выбрать модель
            yield return null;
            choosedData?.Invoke(currentData);
        }

        private T CombineData(T data1, T data2)
        {
            //todo добавить смешение данных за прошлую игровую сессию с текущими данными
            return data1;
        }
    }
}
