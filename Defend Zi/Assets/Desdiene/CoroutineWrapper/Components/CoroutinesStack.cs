using System.Collections;
using System.Collections.Generic;

namespace Desdiene.CoroutineWrapper.Components
{
    /// <summary>
    /// Класс позволяет добавлять хранить корутины и выполнять их в порядке, обратном добавлению - т.е. всегда сначала выполнится последняя добавленная.
    /// </summary>
    public class CoroutinesStack : IEnumerator
    {
        private readonly Stack<IEnumerator> _allNestedCoroutines = new Stack<IEnumerator>();
        private IEnumerator LatestCoroutine
        {
            get
            {
                if (_allNestedCoroutines.Count == 0)
                {
                    return null;
                }
                else
                {
                    return _allNestedCoroutines.Peek();
                }
            }
        }

        public void Add(IEnumerator enumerator)
        {
            _allNestedCoroutines.Push(enumerator);
        }

        public object Current
        {
            get
            {
                if (LatestCoroutine == null)
                {
                    return null;
                }
                else
                {
                    return LatestCoroutine.Current;
                }
            }
        }

        public bool MoveNext()
        {
            if (LatestCoroutine == null) return false;

            bool canMoveNext = LatestCoroutine.MoveNext();
            if (canMoveNext) return true;

            _allNestedCoroutines.Pop();
            return MoveNext();
            //return _allNestedCoroutines.Count != 0;
        }

        public void Reset() => throw new System.NotImplementedException();
    }
}
