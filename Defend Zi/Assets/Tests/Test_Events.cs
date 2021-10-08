using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class Test_Events : MonoBehaviourExt
{
    private Action InnerAction;

    private event Action<int> OuterEvent;

    protected override void AwakeExt()
    {
        SubscribeEvents();

        //call from client
        OuterEvent += LoggNumber;
        //call from itself
        InnerAction?.Invoke();
        //call from client
        OuterEvent -= LoggNumber;
        //call from itself
        InnerAction?.Invoke();
    }

    private void SubscribeEvents()
    {
        InnerAction += InvokeOuterEvent;
    }

    private void InvokeOuterEvent() => OuterEvent?.Invoke(2);

    private void LoggNumber(int number)
    {
        Debug.Log($"The number is {number}");
    }
}
