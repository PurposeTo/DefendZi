using System;
using Desdiene.YieldInstructions.Base;
using UnityEngine;

namespace Desdiene.YieldInstructions
{
    public sealed class WaitForDoneRealtime : WaitForDoneBase
    {
        public WaitForDoneRealtime(float timeout, Func<bool> predicate) : base(timeout, predicate) { }

        protected override float DeltaTime => Time.unscaledDeltaTime;
    }
}
