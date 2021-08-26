using System;
using System.Collections.Generic;
using Desdiene.ObjectPoolers.Datas;
using UnityEngine;

namespace Desdiene.ObjectPoolers.Components
{
    internal class Pool
    {
        public PoolData PoolData { get; }
        public Queue<GameObject> ObjectPoolQueue { get; }
        public Transform PoolParent { get; }

        public Pool(PoolData poolData, Queue<GameObject> objectPoolQueue, Transform poolParent)
        {
            PoolData = poolData != null ? poolData : throw new ArgumentNullException(nameof(poolData));
            ObjectPoolQueue = objectPoolQueue ?? throw new ArgumentNullException(nameof(objectPoolQueue));
            PoolParent = poolParent != null ? poolParent : throw new ArgumentNullException(nameof(poolParent));
        }

        public bool ShouldExpand => PoolData.shouldExpand;
    }
}
