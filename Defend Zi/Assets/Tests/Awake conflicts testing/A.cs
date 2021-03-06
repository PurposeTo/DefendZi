using Desdiene.MonoBehaviourExtension;
using Logger = Desdiene.Logger;

public class A : MonoBehaviourExt
{
    private static readonly Logger LOGGER = new Logger(typeof(A));

    public NameHandler Value { get; private set; }

    protected override void AwakeExt()
    {
        LOGGER.Log("AwakeExt calling");
        B b = GetComponent<B>();
        string name = b.Value.Name;
        Value = new NameHandler("into A. using " + name);
    }
}
