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
        A a = GetInitedComponent<A>();
        string name = a.Value.Name;
        Value = new C("into B. using " + name);
    }
}
