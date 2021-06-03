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
        private protected override T Create()
        {
            Debug.Log($"Initialize SceneSingleton.Unity {this}");
            return this as T;
        }
    }
}
