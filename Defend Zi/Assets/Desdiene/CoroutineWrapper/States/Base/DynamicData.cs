using UnityEngine;

namespace Desdiene.CoroutineWrapper.States.Base
{
    public class DynamicData
    {
        private readonly Coroutine coroutine;

        public DynamicData(Coroutine coroutine)
        {
            this.coroutine = coroutine;
        }

        public Coroutine Coroutine => coroutine;
    }
}
