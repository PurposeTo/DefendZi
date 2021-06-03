using System;

namespace Desdiene.Types.AtomicReference.RefRuntimeInit.States
{
    internal class InitedValue<T> : InitStateValue<T>
    {
        public InitedValue(in Ref<InitStateValue<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
            : base(state, initFunc, valueRef) { }

        public override T GetOrInit() => valueRef.Get();

        public override void Set(T value) => valueRef.Set(value);

        public override T SetAndGet(T value) => valueRef.SetAndGet(value);
    }
}
