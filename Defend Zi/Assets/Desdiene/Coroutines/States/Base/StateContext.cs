using UnityEngine;

namespace Desdiene.Coroutines.States.Base
{
    public class StateContext
    {
        public StateContext(Coroutine coroutine)
        {
            Coroutine = coroutine;
        }

        public Coroutine Coroutine { get; }
    }
}
