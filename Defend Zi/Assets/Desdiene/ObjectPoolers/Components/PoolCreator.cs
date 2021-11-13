using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.MonoBehaviourExtension;
using Desdiene.ObjectPoolers.Datas;
using UnityEngine;

namespace Desdiene.ObjectPoolers.Components
{
    internal class PoolCreator : MonoBehaviourExtContainer
    {
        private readonly ObjectToPoolCreator objectCreator;

        public PoolCreator(MonoBehaviourExt mono, ObjectToPoolCreator objectCreator)
            : base(mono)
        {
            this.objectCreator = objectCreator;
        }

        public Pools CreateAndInitializePools(List<PoolData> PoolDatas)
        {
            Pools pools = new Pools();

            foreach (var poolData in PoolDatas)
            {
                GameObject parent = CreateNewPoolParent(poolData.prefab.name);
                Queue<GameObject> objectPool = CreateNewPoolQueue(poolData, parent.transform);

                pools.Add(poolData.prefab, new Pool(poolData, objectPool, parent.transform));
                Debug.Log($"Pool with {poolData.prefab.name}s has been created!");
            }

            return pools;
        }


        private GameObject CreateNewPoolParent(string PrefabName)
        {
            GameObject parent = new GameObject(PrefabName + " Pool");
            parent.transform.SetParent(MonoBehaviourExt.gameObject.transform);
            return parent;
        }


        private Queue<GameObject> CreateNewPoolQueue(PoolData poolData, Transform parentTransform)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < poolData.size; i++)
            {
                GameObject NewObjectToPool = objectCreator.CreateNewObjectToPool(poolData.prefab, parentTransform);
                objectPool.Enqueue(NewObjectToPool);
            }

            return objectPool;
        }
    }
}
