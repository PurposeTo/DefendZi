using System;

public interface IDeath
{
    bool IsDeath { get; }
    event Action OnDied;
    event Action OnReborn;
}
