using System;
using Desdiene.Types.AtomicReferences;

/// <summary>
/// Класс с данными, которые может ввести пользователь.
/// </summary>
public class UserInputData : IUserInput
{
    public bool IsActive => isActiveRef.Value;

    public event Action<IUserInput> OnInputChange
    {
        add => isActiveRef.OnChanged += () => value(this);
        remove => isActiveRef.OnChanged -= () => value(this);
    }

    private readonly IRef<bool> isActiveRef = new Ref<bool>();

    public void SetActive(bool isActive)
    {
        isActiveRef.Set(isActive);
    }
}
