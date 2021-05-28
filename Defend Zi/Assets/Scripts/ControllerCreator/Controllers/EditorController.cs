using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class EditorController : MonoBehaviourExtContainer, IUserController
{
    private readonly ICoroutine coroutine;
    private readonly UserController userController = new UserController();

    public EditorController(MonoBehaviourExt mono) : base(mono) 
    {
        coroutine = mono.CreateCoroutine();
        mono.ReStartCoroutineExecution(coroutine, Update());
    }

    bool IUserController.IsActive => userController.IsActive;

    event Action<bool> IUserController.OnIsActiveChange
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
