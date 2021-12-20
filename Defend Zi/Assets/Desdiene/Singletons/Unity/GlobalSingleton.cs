using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.Singletons.Unity
{
    /// <summary> 
    /// MonoBehaviourExt Singleton. Will NOT be destroyed with scene unloading.
    /// To access the heir by a static field "Instance".
    /// </summary>
    public class GlobalSingleton<T> : MonoBehaviourExt where T : GlobalSingleton<T>
    {

        private static T _instance = null;

        private bool alive = true;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    T instance = null;

                    // Find T
                    T[] managers = Object.FindObjectsOfType<T>();
                    if (managers != null && managers.Length > 0)
                    {
                        instance = managers[0];
                    }
                    // Create T 
                    else
                    {
                        GameObject go = new GameObject(typeof(T).Name, typeof(T));
                        instance = go.GetComponent<T>();
                    }

                    MarkAsDontDestroy(instance.gameObject);
                    instance.TryAwake();
                    _instance = instance;
                    return _instance;
                }
            }

            //Can be initialized externally
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// Check flag if need work from OnDestroy or OnApplicationExit
        /// </summary>
        public static bool IsAlive
        {
            get
            {
                if (_instance == null)
                    return false;
                return _instance.alive;
            }
        }

        protected sealed override void AwakeExt()
        {
            if (_instance == null)
            {
                Debug.Log($"Initialize Unity.GlobalSingleton {this}");
                _instance = this as T;
                MarkAsDontDestroy(_instance.gameObject);
                AwakeSingleton();
            }
            else
            {
                if (!ReferenceEquals(this, _instance))
                {
                    DestroySafely(gameObject);
                }
                else
                {
                    Debug.LogError("Singleton, уже являющийся _instance, пытается удалить сам себя.");
                }
            }
        }

        protected void OnDestroy() { alive = false; }

        protected void OnApplicationQuit() { alive = false; }

        protected virtual void AwakeSingleton() { }

        private static void MarkAsDontDestroy(GameObject gameObject)
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private static void DestroySafely(GameObject gameObject)
        {
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}