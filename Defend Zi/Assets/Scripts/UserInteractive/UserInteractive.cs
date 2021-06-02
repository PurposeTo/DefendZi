﻿using System.Collections.Generic;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class UserInteractive : MonoBehaviourExt
{
    private static readonly List<IUserControlled> userControlleds = new List<IUserControlled>();
    private IUserInput userInput;

    protected override void AwakeExt()
    {
        GameObjectsHolder.OnInited += Init;
    }

    public static void AddControlled(IUserControlled controlled) => userControlleds.Add(controlled);

    public static void RemoveControlled(IUserControlled controlled) => userControlleds.Remove(controlled);

    private void Init(GameObjectsHolder gameObjectsHolder)
    {
        userInput = new UserInputCreator(this).GetOrDefault();
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        userInput.OnInputChange += Control;
    }

    private void UnsubscribeEvents()
    {
        userInput.OnInputChange -= Control;
    }

    private void Control(IUserInput userInpute)
    {
        for (int i = 0; i < userControlleds.Count; i++)
        {
            Debug.Log($"{GetType()}.Control[{i}] invoke.");
            userControlleds[i].Control(userInpute);
        }
    }
}
