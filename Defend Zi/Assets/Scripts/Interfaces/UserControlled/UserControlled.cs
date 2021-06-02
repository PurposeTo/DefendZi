using Desdiene.MonoBehaviourExtention;

public abstract class UserControlled : MonoBehaviourExt, IUserControlled
{
    protected override void AwakeExt()
    {
        OnIsAwaked += Add;
        OnDestroyed += Remove;
    }

    public abstract void Control(IUserInput input);

    private void Add() => UserInteractive.AddControlled(this);

    private void Remove() => UserInteractive.RemoveControlled(this);
}
