using System;

public interface IUserController
{
    public bool IsActive { get; }

    public event Action<bool> OnIsActiveChange;
}
