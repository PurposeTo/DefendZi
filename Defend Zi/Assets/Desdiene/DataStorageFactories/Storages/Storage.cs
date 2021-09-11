using System;
using System.Collections;
using Desdiene.Containers;
using Desdiene.Coroutines;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.Datas;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages
{
    public class Storage<T> : MonoBehaviourExtContainer, IStorage<T> where T : IData, IDataCombiner<T>, new()
    {
        private T _data = new T();
        private readonly IDataCombiner<T> _combiner;
        private readonly IStorageDataLoader<T> _storageDataLoader;

        private readonly ICoroutine _chooseDataRoutine;

        public Storage(MonoBehaviourExt superMonoBehaviour,
                       IStorageDataLoader<T> storageDataLoader)
            : base(superMonoBehaviour)
        {
            _combiner = _data;
            _storageDataLoader = storageDataLoader ?? throw new ArgumentNullException(nameof(storageDataLoader));

            _chooseDataRoutine = new CoroutineWrap(superMonoBehaviour);
        }

        // данные, полученные с хранилищ/а
        private T _cashLoadedData = default;

        public T GetData() => _data;

        public void InvokeLoadingData()
        {
            _storageDataLoader.Load(loadedData =>
            {
                if (_cashLoadedData == null)
                {
                    _cashLoadedData = loadedData;
                    T combinedData = CombineData(_data, loadedData);
                    _data = combinedData;
                    return;
                }
                else
                {
                    if (_cashLoadedData.Equals(loadedData)) return;
                    else
                    {
                        ChooseData(loadedData, choosedData =>
                        {
                            _cashLoadedData = choosedData;
                            _data = choosedData;
                        });
                        return;
                    }
                }
            });
        }

        public void InvokeSavingData()
        {
            _storageDataLoader.Save(_data);
        }

        private void ChooseData(T loadedData, Action<T> choosedData)
        {
            T currentData = _data;

            _chooseDataRoutine.StartContinuously(ChooseDataEnumerator(currentData, loadedData, choosedData));
        }

        private IEnumerator ChooseDataEnumerator(T currentData, T loadedData, Action<T> choosedData)
        {
            Debug.LogWarning("NotImplementedException: выбор моделей");
            //todo предложить игроку выбрать модель
            yield return null;
            choosedData?.Invoke(currentData);
        }

        private T CombineData(T data1, T data2)
        {
            T combinedData = _combiner.Combine(data1, data2);
            Debug.Log($"Data was combined\nFirst data:\n{data1}\n\nSecond data:\n{data2}\n\nCombined data:\n{combinedData}");
            return combinedData;
        }
    }
}
