using UnityEngine;

namespace Desdiene.CoroutineWrapper.States.Base
{
    public class MutableData
    {
        private readonly Coroutine coroutine;

        public MutableData(Coroutine coroutine)
        {
            this.coroutine = coroutine;
        }

        public Coroutine Coroutine => coroutine;
    }
}
