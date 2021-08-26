using Desdiene.MonoBehaviourExtension;

namespace Desdiene.Singletons.Unity.Base
{
    /// <summary> 
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class Singleton<T>
        : MonoBehaviourExt
        where T : Singleton<T>
    {
        public static T Instance { get; private protected set; }

        protected sealed override void AwakeExt()
        {
            AwakeInstance();
        }

        /// <summary>
        /// Используется после инициализации Singleton. Использовать вместо Awake/AwakeWrapped.
        /// </summary>
        protected virtual void AwakeSingleton() { }

        private protected abstract void AwakeInstance();
    }
}
