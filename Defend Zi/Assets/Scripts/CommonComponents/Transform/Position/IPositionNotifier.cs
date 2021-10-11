using System;

public interface IPositionNotifier
{
    event Action OnChanged;
}
