using System;

public interface IInvulnerableNotification
{
    public event Action WhenInvulnerable;
    public event Action WhenVulnerable;
}
