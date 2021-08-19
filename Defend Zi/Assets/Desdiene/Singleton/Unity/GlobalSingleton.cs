﻿using Desdiene.Singleton.Unity.Base;
using UnityEngine;

namespace Desdiene.Singleton.Unity
{
    /// <summary> 
    /// MonoBehaviourExt Singleton. Will NOT be destroyed with scene unloading.
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class GlobalSingleton<T>
        : Singleton<T>
        where T : Singleton<T>
    {
        private protected sealed override void AwakeInstance()
        {
            if (Instance == null)
            {
                Debug.Log($"Initialize Unity.GlobalSingleton {this}");
                DontDestroyOnLoad(gameObject);
                Instance = this as T;
                AwakeSingleton();
                onInitedAction.InvokeAndClear(Instance);
            }
            else Destroy(gameObject);
        }
    }
}
