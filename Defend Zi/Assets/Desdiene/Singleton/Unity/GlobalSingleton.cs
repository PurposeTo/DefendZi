using System;
using Desdiene.Types.EventContainers;
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
        private protected override T Create()
        {
            Debug.Log($"Initialize GlobalSingleton.Unity {this}");
            DontDestroyOnLoad(gameObject);
            return this as T;
        }
    }
}
