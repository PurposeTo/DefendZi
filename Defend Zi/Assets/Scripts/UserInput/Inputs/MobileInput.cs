﻿using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

/// <summary>
/// Описывает модель вводимых пользователем данных через мобильный ввод.
/// </summary>
public class MobileInput : MonoBehaviourExtContainer, IUserInput
{
    private readonly ICoroutine coroutine;
    private readonly UserInputData userInputData = new UserInputData();

    public MobileInput(MonoBehaviourExt mono) : base(mono)
    {
        coroutine = mono.CreateCoroutine();
        mono.ReStartCoroutineExecution(coroutine, Update());
    }

    bool IUserInput.IsActive => userInputData.IsActive;

    event Action<IUserInput> IUserInput.OnInputChange
    {
        add => userInputData.OnInputChange += value;
        remove => userInputData.OnInputChange += value;
    }

    private IEnumerator Update()
    {
        while (true)
        {
            userInputData.SetActive(Input.GetKey(KeyCode.Space));
            yield return null;
        }
    }
}