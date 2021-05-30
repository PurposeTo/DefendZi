using System;

/// <summary>
/// Интерфейс считывания данных, вводимых пользователем.
/// </summary>
public interface IUserInput
{
    public bool IsActive { get; }

    public event Action<bool> OnIsActiveChange;
}
