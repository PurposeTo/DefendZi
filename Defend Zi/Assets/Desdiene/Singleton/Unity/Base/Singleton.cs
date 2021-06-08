using System;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Types.EventContainers;

namespace Desdiene.Singleton.Unity
{
    /// <summary> 
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class Singleton<T>
        : MonoBehaviourExt
        where T : Singleton<T>
    {
        private protected static readonly ActionEvent<T> onInitedAction = new ActionEvent<T>();

        public static T Instance { get; private protected set; }
        public static IInitionEvent<T> OnInitedWrap => onInitedAction;

        /// <summary>
        /// Данное событие выполнится тогда, когда Instance будет инициализирован.
        /// Команды выполняются сразу в Awake() после метода AwakeSingleton(), если синглтон не был инициализирован.
        /// НЕЛЬЗЯ ожидать инициализацию сиглтонов внутри конструкторов классов!
        /// В противном случае не факт, что после вызова конструктора класс будет готов к использованию.
        /// </summary>
        public static event Action<T> OnInited
        {
            add
            {
                if (Instance != null) value?.Invoke(Instance);
                else onInitedAction.OnInited += value;
            }
            remove => onInitedAction.OnInited -= value;
        }

        protected sealed override void Constructor()
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
