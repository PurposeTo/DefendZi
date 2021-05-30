using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

/// <summary>
/// Описывает модель вводимых пользователем данных через ввод в windows editor-е.
/// </summary>
public class EditorInput : MonoBehaviourExtContainer, IUserInput
{
    private readonly ICoroutine coroutine;
    private readonly UserInputData userInputData = new UserInputData();

    public EditorInput(MonoBehaviourExt mono) : base(mono) 
    {
        coroutine = mono.CreateCoroutine();
        mono.ReStartCoroutineExecution(coroutine, Update());
    }

    bool IUserInput.IsActive => userInputData.IsActive;

    event Action<bool> IUserInput.OnIsActiveChange
    {
        add => userInputData.OnIsActiveChange += value;

        remove => userInputData.OnIsActiveChange += value;
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
