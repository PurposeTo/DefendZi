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
        private protected override void AwakeInstance()
        {
            if (Instance == null)
            {
                Debug.Log($"Initialize GlobalSingleton.Unity {this}");
                DontDestroyOnLoad(gameObject);
                Instance = this as T;
                AwakeSingleton();
                onInitedAction.InvokeAndClear(Instance);
            }
            else Destroy(gameObject);
        }
    }
}
