using System;
using Desdiene.Types.AtomicReferences.Init.States.Base;

namespace Desdiene.Types.AtomicReferences.Init.States
{
    internal class Inited<T> : State<T>
    {
        public Inited(in IRef<State<T>> state, in Func<T> initFunc, in IRef<T> valueRef)
            : base(state, initFunc, valueRef) { }

        public override T GetOrInit() => valueRef.Value;

        public override void Set(T value) => valueRef.Set(value);

        public override T SetAndGet(T value) => valueRef.SetAndGet(value);
    }
}
