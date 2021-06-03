using System;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Types.EventContainers;
using UnityEngine;

namespace Desdiene.Singleton
{

    /// <summary> 
    /// To access the heir by a static field "Instance".
    /// </summary>
    public abstract class SingletonMonoBehaviourExt<T>
        : MonoBehaviourExt
        where T : SingletonMonoBehaviourExt<T>
    {
        [SerializeField] private bool dontDestroyOnLoad = false;
        private static readonly ActionEvent<T> onInitedAction = new ActionEvent<T>();

        public static T Instance { get; private set; }
        public static IInitionEvent<T> OnInitedWrap => onInitedAction;

        /// <summary>
        /// Данное событие выполнится тогда, когда Instance будет инициализирован.
        /// Команды выполняются сразу в Awake() после метода AwakeSingleton(), если синглтон не был инициализирован.
        /// </summary>
        public static event Action<T> OnInited
        {
            add
            {
                if (Instance != null) value?.Invoke(Instance);
                else onInitedAction.OnInited += value;
            }
            remove
            {
                onInitedAction.OnInited -= value;
            }
        }

        protected sealed override void AwakeExt()
        {
            if (Instance == null) Init();
            else Destroy(gameObject);
        }

        /// <summary>
        /// Используется после инициализации Singleton. Использовать вместо Awake/AwakeWrapped.
        /// </summary>
        protected virtual void AwakeSingleton() { }

        private void Init()
        {
            Debug.Log($"Initialize SingletonSuperMonoBehaviour {this}");
            Instance = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            AwakeSingleton();
            onInitedAction.InvokeAndClear(Instance);
        }
    }

    /*Пример с GameManager
     * 
     *  public class GameManager : Singleton<GameManager>
    {
        protected override void AwakeSingleton() { }
    }
     */
}
