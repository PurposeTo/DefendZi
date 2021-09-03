using UnityEngine;

namespace Desdiene.Coroutines.States.Base
{
    public class StateContext
    {
        public StateContext(Coroutine coroutine)
        {
            this.Coroutine = coroutine;
        }

        public Coroutine Coroutine { get; }
    }
}
