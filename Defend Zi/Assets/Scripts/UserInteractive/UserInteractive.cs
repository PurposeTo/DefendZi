using System.Collections.Generic;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class UserInteractive : MonoBehaviourExt
{
    private static readonly List<IUserControlled> userControlled = new List<IUserControlled>();
    private IUserInput userInput;

    protected override void AwakeExt()
    {
        ComponentsProxy.OnInited += Init;
    }

    public static void AddControlled(IUserControlled controlled) => userControlled.Add(controlled);

    public static void RemoveControlled(IUserControlled controlled) => userControlled.Remove(controlled);

    private void Init(ComponentsProxy gameObjectsHolder)
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
        for (int i = 0; i < userControlled.Count; i++)
        {
            Debug.Log($"{GetType()}.Control[{i}] invoke.");
            userControlled[i].Control(userInpute);
        }
    }
}
