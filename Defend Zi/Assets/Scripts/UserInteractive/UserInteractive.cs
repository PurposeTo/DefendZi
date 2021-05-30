﻿using Desdiene.MonoBehaviourExtention;

public class UserInteractive : MonoBehaviourExt
{
    private IUserInput userInput;
    //private IUserControllable userControllable;

    protected override void AwakeExt()
    {
        GameObjectsHolder.OnInited += Init;
    }

    private void Init(GameObjectsHolder gameObjectsHolder)
    {
        userInput = new UserInputCreator(this).GetOrDefault();
        //userControllable = gameObjectsHolder.PlayerControl;
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
        //userControllable.Control(userInput);
    }
}
