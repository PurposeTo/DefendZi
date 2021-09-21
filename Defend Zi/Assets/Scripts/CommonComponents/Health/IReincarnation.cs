using System;

public interface IReincarnation
{
    event Action OnRevived;
    void Revive();
}
