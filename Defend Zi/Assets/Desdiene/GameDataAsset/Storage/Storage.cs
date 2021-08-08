﻿using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.GameDataAsset.Combiner;
using Desdiene.GameDataAsset.Data;
using Desdiene.GameDataAsset.DataLoader;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.GameDataAsset.Storage
{
    public class Storage<T> : MonoBehaviourExtContainer, IStorage<T> where T : IData, new()
    {
        private T _data = new T();
        private readonly IDataCombiner<T> _combiner;
        private readonly IStorageDataLoader<T> _storageDataLoader;

        private readonly ICoroutine _chooseDataRoutine;

        public Storage(MonoBehaviourExt superMonoBehaviour,
                       IDataCombiner<T> combiner,
                       IStorageDataLoader<T> storageDataLoader)
            : base(superMonoBehaviour)
        {
            _combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
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
            Debug.Log($"Combining data!\nFirst data:\n{data1}\n\nSecond data:\n{data2}");
            return _combiner.Combine(data1, data2);
        }
    }
}
