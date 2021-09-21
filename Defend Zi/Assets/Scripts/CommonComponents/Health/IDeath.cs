using System;

public interface IDeath
{
    event Action OnDied;
    bool IsDeath { get; }
}
