using System;

namespace Desdiene.Types.EventContainers
{
    /// <summary>
    /// Обертка для event, чтобы с ним можно было работать как с объектом.
    /// </summary>
    public class ActionEvent<T> : IInitionEvent<T>
    {
        public event Action<T> OnInited { 
            add { container += value; } 
            remove { container -= value; } 
        }
        private Action<T> container;

        public void InvokeAndClear(T arg)
        {
            Invoke(arg);
            Clear();
        }

        public void Invoke(T arg) => container?.Invoke(arg);

        public void Clear() => container = null;
    }
}
