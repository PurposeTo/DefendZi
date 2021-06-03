using System.Collections.Generic;
using Desdiene.ObjectPoolerAsset.Base;
using Desdiene.Singleton.Unity;
using UnityEngine;

namespace Desdiene.ObjectPoolerAsset
{
    public class ObjectPooler : SceneSingleton<ObjectPooler>
    {
        public List<PoolData> PoolDatas; // Сетим через инспектор

        private FromPoolSpawner fromPoolSpawner;

        protected override void AwakeSingleton()
        {
            ObjectToPoolCreator objectCreator = new ObjectToPoolCreator();
            fromPoolSpawner = new FromPoolSpawner(
                this,
                new PoolCreator(this, objectCreator).CreateAndInitializePools(PoolDatas),
                objectCreator);
        }


        public void ReturnToPool(GameObject gameObject)
        {
            fromPoolSpawner.ReturnToPool(gameObject);
        }


        public GameObject SpawnFromPool(GameObject prefabKey)
        {
            return fromPoolSpawner.SpawnFromPool(prefabKey);
        }
    }
}
