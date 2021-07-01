using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class B : MonoBehaviourExt
{
    public C Value { get; private set; }

    protected override void AwakeExt()
    {
        Debug.Log("AwakeExt call B");
        A a = GetComponent<A>();
        Debug.Log(a.Value.Name);
        Value = new C("into B");
    }
}
