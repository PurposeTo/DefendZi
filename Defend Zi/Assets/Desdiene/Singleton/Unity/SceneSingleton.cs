using System;
using Desdiene.Singleton.Unity.Base;
using UnityEngine;

namespace Desdiene.Singleton.Unity
{
    /// <summary> 
    /// MonoBehaviourExt Singleton. Will be destroyed with scene unloading.
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class SceneSingleton<T>
        : Singleton<T>
        where T : Singleton<T>
    {
        private protected sealed override void AwakeInstance()
        {
            if (Instance == null)
            {
                Debug.Log($"Initialize Unity.SceneSingleton {this}");
                Instance = this as T;
                AwakeSingleton();
                onInitedAction.InvokeAndClear(Instance);
            }
            else throw new OverflowException($"There is several {GetType()} on loaded scene/s! There must be only one.");
        }
    }
}
