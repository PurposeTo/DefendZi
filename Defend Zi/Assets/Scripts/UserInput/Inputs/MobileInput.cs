using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
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
        coroutine = new CoroutineWrap(mono);
        coroutine.StartContinuously(Update());
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
            userInputData.SetActive(Input.touchCount > 0);
            yield return null;
        }
    }
}
