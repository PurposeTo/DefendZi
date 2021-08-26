using UnityEngine;

namespace Desdiene.Coroutines.States.Base
{
    public class MutableData
    {
        public MutableData(Coroutine coroutine)
        {
            this.Coroutine = coroutine;
        }

        public Coroutine Coroutine { get; }
    }
}
