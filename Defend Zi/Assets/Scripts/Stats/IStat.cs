using System;

public interface IStat<T>
{
    event Action OnStatChange;
    T Value { get; }

}

