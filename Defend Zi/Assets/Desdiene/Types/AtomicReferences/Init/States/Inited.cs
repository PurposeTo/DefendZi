using System;
using Desdiene.Types.AtomicReferences.Init.States.Base;

namespace Desdiene.Types.AtomicReferences.Init.States
{
    internal class Inited<T> : State<T>
    {
        public Inited(in Ref<State<T>> state, in Func<T> initFunc, in Ref<T> valueRef)
            : base(state, initFunc, valueRef) { }

        public override T GetOrInit() => valueRef.Get();

        public override void Set(T value) => valueRef.Set(value);

        public override T SetAndGet(T value) => valueRef.SetAndGet(value);
    }
}
