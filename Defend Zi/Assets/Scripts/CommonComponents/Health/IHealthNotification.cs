using System;

public interface IHealthNotification
{
    event Action WhenAlive;
    event Action OnDamaged;
    event Action OnDeath;
    event Action WhenDead;
}
