using System;
using Desdiene.DataStorageFactories.Combiners;
using Desdiene.DataStorageFactories.DataLoaders;
using Desdiene.DataStorageFactories.Datas;
using UnityEngine;

namespace Desdiene.DataStorageFactories.Storages
{
    public sealed class DataContainer<T> : IDataContainer<T> where T : IData, IDataCombiner<T>, new()
    {
        private T _data = new T();
        private readonly IDataCombiner<T> _combiner;
        private readonly IStorageData<T> _storageDataLoader;

        public DataContainer(IStorageData<T> storageDataLoader)
        {
            _combiner = _data;
            _storageDataLoader = storageDataLoader ?? throw new ArgumentNullException(nameof(storageDataLoader));
        }

        // загруженные данные, полученные с хранилищ/а
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
                    if (loadedData.PlayingTime > _cashLoadedData.PlayingTime)
                    {
                        _cashLoadedData = loadedData;
                        _data = loadedData;
                    }
                }
            });
        }

        public void InvokeSavingData()
        {
            _storageDataLoader.Save(_data, null);
        }

        private T CombineData(T data1, T data2)
        {
            T combinedData = _combiner.Combine(data1, data2);
            Debug.Log($"Data was combined\nFirst data:\n{data1}\n\nSecond data:\n{data2}\n\nCombined data:\n{combinedData}");
            return combinedData;
        }
    }
}
