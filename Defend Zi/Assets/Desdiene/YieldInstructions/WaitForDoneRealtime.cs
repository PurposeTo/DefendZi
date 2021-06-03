using System;
using Desdiene.Coroutine.YieldInstructions.Base;
using UnityEngine;

namespace Desdiene.Coroutine.YieldInstructions
{
    public sealed class WaitForDoneRealtime : WaitForDoneBase
    {
        public WaitForDoneRealtime(float timeout, Func<bool> predicate) : base(timeout, predicate) { }

        protected override float DeltaTime => Time.unscaledDeltaTime;
    }
}
