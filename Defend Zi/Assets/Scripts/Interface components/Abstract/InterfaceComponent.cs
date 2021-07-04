using Desdiene.MonoBehaviourExtension;

// В дочерних классах добавить RequireComponent
public abstract class InterfaceComponent<T> : MonoBehaviourExt where T : class
{
    public T Implementation => _implementation ?? throw new System.NullReferenceException(nameof(Implementation));

    private T _implementation;

    protected override void AwakeExt()
    {
        _implementation = GetInitedComponent<T>();
    }
}
