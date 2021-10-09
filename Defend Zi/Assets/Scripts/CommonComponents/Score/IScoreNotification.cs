using System;

public interface IScoreNotification
{
    event Action<uint> OnReceived;
}
