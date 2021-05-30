﻿using System;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.AtomicReference.Interfaces;

/// <summary>
/// Класс с данными, которые может ввести пользователь.
/// </summary>
public class UserInputData : IUserInput
{
    public bool IsActive => isActiveRef.Get();

    public event Action<bool> OnIsActiveChange
    {
        add => isActiveRef.OnValueChanged += () => value(IsActive);
        remove => isActiveRef.OnValueChanged -= () => value(IsActive);
    }

    private readonly IRef<bool> isActiveRef = new Ref<bool>();

    public void SetActive(bool isActive)
    {
        isActiveRef.Set(isActive);
    }
}
