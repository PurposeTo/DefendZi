using System;
using Desdiene.Types.AtomicReference;

/// <summary>
/// Класс с данными, которые может ввести пользователь.
/// </summary>
public class UserInputData : IUserInput
{
    public bool IsActive => isActiveRef.Get();

    public event Action<IUserInput> OnInputChange
    {
        add => isActiveRef.OnValueChanged += () => value(this);
        remove => isActiveRef.OnValueChanged -= () => value(this);
    }

    private readonly IRef<bool> isActiveRef = new Ref<bool>();

    public void SetActive(bool isActive)
    {
        isActiveRef.Set(isActive);
    }
}
