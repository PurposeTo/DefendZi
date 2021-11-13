using Desdiene.Types.AtomicReferences.RuntimeInit;

namespace Desdiene.MonoBehaviourExtension
{
    // В дочерних классах добавить RequireComponent
    public abstract class InterfaceComponent<T> : MonoBehaviourExt where T : class
    {
        public T Implementation => GetOrInitImplementation() ?? throw new System.NullReferenceException(nameof(Implementation));

        private readonly RefRuntimeInit<T> _implementation = new RefRuntimeInit<T>();

        protected override void AwakeExt()
        {
            GetOrInitImplementation();
        }

        private T GetOrInitImplementation() => _implementation.GetOrInit(GetComponent<T>);
    }
}
