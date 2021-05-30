using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class EditorController : MonoBehaviourExtContainer, IUserInput
{
    private readonly ICoroutine coroutine;
    private readonly UserInputData userController = new UserInputData();

    public EditorController(MonoBehaviourExt mono) : base(mono) 
    {
        coroutine = mono.CreateCoroutine();
        mono.ReStartCoroutineExecution(coroutine, Update());
    }

    bool IUserInput.IsActive => userController.IsActive;

    event Action<bool> IUserInput.OnIsActiveChange
    {
        add => userController.OnIsActiveChange += value;

        remove => userController.OnIsActiveChange += value;
    }

    private IEnumerator Update()
    {
        while (true)
        {
            userController.SetActive(Input.GetKey(KeyCode.Space));
            yield return null;
        }
    }
}
