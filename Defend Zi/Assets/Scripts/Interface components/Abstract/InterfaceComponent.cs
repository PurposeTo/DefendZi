using Desdiene.MonoBehaviourExtension;

// В дочерних классах добавить GetComponent()
public abstract class InterfaceComponent<T> : MonoBehaviourExt where T : class
{
    public T Implementation
    {
        get
        {
            if (_implementation == null) _implementation = GetComponent<T>();
            return _implementation;
        }
    }

    private T _implementation;
}
