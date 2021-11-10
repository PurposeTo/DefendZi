using System;

public interface IImmortalNotification
{
    public event Action WhenImmortal;
    public event Action WhenMortal;
}
