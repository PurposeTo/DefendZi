using System;

public interface IStat<T>
{
    event Action OnValueChanged;
    T Value { get; }
}

