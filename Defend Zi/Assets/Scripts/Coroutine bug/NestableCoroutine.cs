using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class NestableCoroutine : IEnumerator
{
    private readonly List<IEnumerator> _all = new List<IEnumerator>();
    private IEnumerator LatestCoroutine
    {
        get
        {
            if (_all.Count == 0)
            {
                return null;
            }
            else
            {
                return _all[_all.Count - 1];
            }
        }
    }
    public NestableCoroutine(IEnumerator initialCoroutine)
    {
        _all.Add(initialCoroutine);
    }

    public void Add(IEnumerator recent)
    {
        _all.Add(recent);
    }

    public bool IsCoroutineDone(IEnumerator coroutineToCheck)
    {
        return _all.Contains(coroutineToCheck);
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

        _all.RemoveAt(_all.Count - 1);
        return _all.Count != 0;
    }

    public void Reset() => throw new System.NotImplementedException();
}
