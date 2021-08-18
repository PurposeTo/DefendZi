using System;

public interface IDeath
{
    event Action OnDied;
    event Action OnReborn;
}
