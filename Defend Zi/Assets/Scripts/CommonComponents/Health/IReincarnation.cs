using System;

public interface IReincarnation
{
    event Action OnReviving;
    void Revive();
}
