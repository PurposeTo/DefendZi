using System.Collections;
using System.Collections.Generic;
using Desdiene.Container;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/*
 * При остановке корутины выполняться инструкции до следующего yield return. 
 * Если корутина останавливается внутри yield return _innerEnumerator, то дальше она выполняться не будет.
 * Пример неявной работы:
 * 
 * [_routine = запущенная корутина TestProcess()]
 * 
 * IEnumerator TestProcess() 
 * {
 *     while (true)
 *     {
 *         yield return null;
 *         _routine.Stop();
 *         Debug.Log("КРЯ");
 *     }
 * }
 *    
 * Лог будет выведен, т.к. yield return будет лишь в следующей итерации цикла while.
 */
public class StoppableCoroutine : MonoBehaviourExtContainer
{
    private readonly NestableCoroutine _nestableCoroutine;
    private Coroutine _routineWrapper;
    private bool _isTerminated = false;

    public StoppableCoroutine(MonoBehaviourExt mono, IEnumerator initialCoroutine) : base(mono)
    {
        _nestableCoroutine = new NestableCoroutine(initialCoroutine);
    }

    public void Start()
    {
        _routineWrapper = monoBehaviourExt.StartCoroutine(Run());
    }

    public void Stop()
    {
        _isTerminated = true;
        monoBehaviourExt.StopCoroutine(_routineWrapper);
        _routineWrapper = null;
    }

    public IEnumerator StartNested(IEnumerator newCoroutine)
    {
        _nestableCoroutine.Add(newCoroutine);
        while (_nestableCoroutine.IsCoroutineDone(newCoroutine) == false)
        {
            yield return null;
        }
    }

    private IEnumerator Run()
    {
        //Wait a frame so that user can safely access this instance inside 'initialCoroutine'
        //JUST AFTER constructor returns.
        //yield return null;
        while (!_isTerminated && _nestableCoroutine.MoveNext())
        {
            yield return _nestableCoroutine.Current;
        }
    }
}