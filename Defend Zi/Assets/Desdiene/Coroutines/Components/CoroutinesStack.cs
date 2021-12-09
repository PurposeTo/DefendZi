using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;

namespace Desdiene.Coroutines.Components
{
    /// <summary>
    /// Класс позволяет добавлять хранить корутины и выполнять их в порядке, обратном добавлению - т.е. всегда сначала выполнится последняя добавленная.
    /// </summary>
    public class CoroutinesStack : MonoBehaviourExtContainer, IEnumerator
    {
        private readonly Stack<IEnumerator> _allNestedCoroutines = new Stack<IEnumerator>();

        // need to use mono for debug
        public CoroutinesStack(MonoBehaviourExt mono) : base(mono) { }

        public object Current
        {
            get
            {
                if (TryGetLatestCoroutine(out IEnumerator latest))
                {
                    return latest.Current;
                }
                else return null;
            }
        }

        public void Clear() => _allNestedCoroutines.Clear();

        public void Add(IEnumerator enumerator)
        {
            _allNestedCoroutines.Push(enumerator);
        }

        public bool MoveNext()
        {
            if (!HasLatestCoroutine()) return false;

            int pastStackCount = _allNestedCoroutines.Count;
            if (GetLatestCoroutine().MoveNext()) return true;
            int nextStackCount = _allNestedCoroutines.Count;

            // кол-во корутин в стеке может измениться во время вызова LatestCoroutine.MoveNext()
            if (pastStackCount != nextStackCount) return MoveNext();

            _allNestedCoroutines.Pop();
            return MoveNext();

        }

        public void Reset() => throw new System.NotImplementedException();

        private bool TryGetLatestCoroutine(out IEnumerator latest)
        {
            if (HasLatestCoroutine())
            {
                latest = GetLatestCoroutine();
                return true;
            }
            else
            {
                latest = null;
                return false;
            }
        }

        private bool HasLatestCoroutine() => _allNestedCoroutines.Count != 0;

        private IEnumerator GetLatestCoroutine()
        {
            if (_allNestedCoroutines.Count == 0) throw new System.InvalidOperationException();
            return _allNestedCoroutines.Peek();
        }
    }
}
