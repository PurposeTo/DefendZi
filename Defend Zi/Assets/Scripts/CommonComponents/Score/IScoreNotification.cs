using System;

public interface IScoreNotification
{
    event Action<int> OnReceived;
}
