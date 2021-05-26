using System;
using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.DataLoader
{
    public interface IStorageDataLoader<T> where T : GameData
    {
        void Load(Action<T> dataCallback);
        void Save(T data);
    }
}
