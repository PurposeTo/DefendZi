using System;
using Desdiene.MonoBehaviourExtention;

public class UserInputMono : MonoBehaviourExt, IUserInput
{
    private IUserInput userInput;

    bool IUserInput.IsActive => userInput.IsActive;

    event Action<IUserInput> IUserInput.OnInputChange
    {
        add => userInput.OnInputChange += value;
        remove => userInput.OnInputChange -= value;
    }

    protected override void Constructor()
    {
        userInput = new UserInputCreator(this).GetOrDefault();
    }
}
