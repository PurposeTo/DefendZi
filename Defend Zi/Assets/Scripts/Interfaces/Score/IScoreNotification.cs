using System;

public interface IScoreNotification
{
    event Action OnScoreChanged;
}
