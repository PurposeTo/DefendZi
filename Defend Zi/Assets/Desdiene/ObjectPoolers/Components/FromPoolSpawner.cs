using System;
using Desdiene.Containers;
using Desdiene.Extensions.UnityEngine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.ObjectPoolers.Components
{
    internal class FromPoolSpawner : MonoBehaviourContainer
    {
        private readonly Pools _pools;
        private readonly ObjectToPoolCreator _objectCreator;
        public FromPoolSpawner(
            MonoBehaviour monoBehaviour,
            Pools pools,
            ObjectToPoolCreator objectCreator)
            : base(monoBehaviour)
        {
            _pools = pools;
            _objectCreator = objectCreator;
        }

        public void ReturnToPool(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public GameObject SpawnFromPool(GameObject prefabKey)
        {
            Pool pool = _pools.Get(prefabKey);

            // Посмотреть на первый обьект в очереди.
            GameObject objectToSpawn = pool.ObjectPoolQueue.Peek();

            if (objectToSpawn.activeInHierarchy)
            {
                // Если объект включен (нельзя использовать)
                // И можно расширить пул
                if (pool.ShouldExpand)
                {
                    //То сделать новый объект
                    objectToSpawn = _objectCreator.CreateNewObjectToPool(prefabKey, pool.PoolParent);
                }
            }
            else
            {
                // Если он выключен, то можно использовать. 
                objectToSpawn = pool.ObjectPoolQueue.Dequeue();
            }

            objectToSpawn.transform.SetDefault();
            objectToSpawn.SetActive(true);
            OnObjectSpawn(objectToSpawn);

            pool.ObjectPoolQueue.Enqueue(objectToSpawn);

            return objectToSpawn;
        }


        private void OnObjectSpawn(GameObject objectToSpawn)
        {
            IPooledObject[] pooledComponents = objectToSpawn.GetComponents<IPooledObject>();

            Array.ForEach(pooledComponents, (pooled) =>
            {
                pooled.OnObjectSpawn();
            });
        }
    }
}
