using System;

namespace Desdiene.UnityScenes.LoadingProcess.States.Base
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
