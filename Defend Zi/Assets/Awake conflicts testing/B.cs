using Desdiene.MonoBehaviourExtension;
using Logger = Desdiene.Logger;

public class B : MonoBehaviourExt
{
    private static readonly Logger LOGGER = new Logger(typeof(B));
    public NameHandler Value { get; private set; }

    protected override void AwakeExt()
    {
        LOGGER.Log("AwakeExt calling");
        A a = GetInitedComponent<A>();
        string name = a.Value.Name;
        Value = new NameHandler("into B. using " + name);
    }
}
