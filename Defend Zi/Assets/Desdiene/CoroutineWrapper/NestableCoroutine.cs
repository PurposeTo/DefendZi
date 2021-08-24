using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Desdiene.CoroutineWrapper
{
    public class NestableCoroutine : IEnumerator
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
        public NestableCoroutine(IEnumerator initialCoroutine)
        {
            Add(initialCoroutine);
        }

        public void Add(IEnumerator recent)
        {
            _allNestedCoroutines.Push(recent);
        }

        public bool IsCoroutineContains(IEnumerator coroutineToCheck)
        {
            return _allNestedCoroutines.Contains(coroutineToCheck);
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
            return _allNestedCoroutines.Count != 0;
        }

        public void Reset() => throw new System.NotImplementedException();
    }
}
