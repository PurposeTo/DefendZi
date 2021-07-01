using Desdiene.MonoBehaviourExtension;

// В дочерних классах добавить GetComponent()
public abstract class InterfaceComponent<T> : MonoBehaviourExt where T : class
{
    public T Implementation => _implementation;

    private T _implementation;

    protected override void AwakeExt()
    {
        _implementation = GetComponent<T>();
    }
}
