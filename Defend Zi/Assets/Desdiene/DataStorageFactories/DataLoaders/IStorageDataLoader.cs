﻿using System;
using Desdiene.DataStorageFactories.Data;

namespace Desdiene.DataStorageFactories.DataLoaders
{
    public interface IStorageDataLoader<T> where T : IData
    {
        void Load(Action<T> dataCallback);
        void Save(T data);
    }
}