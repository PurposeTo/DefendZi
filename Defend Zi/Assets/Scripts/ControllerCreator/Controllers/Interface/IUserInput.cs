using System;

public interface IUserInput
{
    public bool IsActive { get; }

    public event Action<bool> OnIsActiveChange;
}
