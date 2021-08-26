using System;

namespace Desdiene.UnityScenes.Loadings.States.Base
{
    public class MutableData
    {
        public MutableData(Action onWaitingForAllowingToEnabling)
        {
            OnWaitingForAllowingToEnabling = onWaitingForAllowingToEnabling;
        }

        public Action OnWaitingForAllowingToEnabling { get; }
    }
}
