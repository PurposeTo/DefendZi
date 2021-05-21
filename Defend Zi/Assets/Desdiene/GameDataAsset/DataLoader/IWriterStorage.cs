﻿using Desdiene.GameDataAsset.Data;

namespace Desdiene.GameDataAsset.DataLoader
{
    public interface IWriterStorage<T> where T : GameData
    {
        void Save(T data);
    }
}
