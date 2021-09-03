using System;

namespace Desdiene.UnityScenes.Loadings.States.Base
{
    public class StateContext
    {
        public StateContext(Action onWaitingForAllowingToEnabling)
        {
            OnWaitingForAllowingToEnabling = onWaitingForAllowingToEnabling;
        }

        public Action OnWaitingForAllowingToEnabling { get; }
    }
}
