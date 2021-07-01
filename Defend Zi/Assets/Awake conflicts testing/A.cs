using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class A : MonoBehaviourExt
{
    public C Value { get; private set; }

    protected override void AwakeExt()
    {
        Debug.Log("AwakeExt call A");
        B b = GetComponent<B>();
        Debug.Log(b.Value.Name);
        Value = new C("into A");
    }
}
