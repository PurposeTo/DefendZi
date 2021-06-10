using System;

public interface IPositionNotification
{
    event Action OnChanged;
}
