using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public abstract class InterfaceComponent<T> : MonoBehaviourExt where T : class
{
    [SerializeField, NotNull] private MonoBehaviour _component;

    private T _implementation;

    public T Implementation
    {
        get
        {
            if (_implementation == null) _implementation = Init();
            return _implementation;
        }
    }

    private T Init()
    {
        if (_component is T component) return component;
        else throw new NotImplementedException($"The component {_component} does not implement the {typeof(T)} interface");
    }
}
