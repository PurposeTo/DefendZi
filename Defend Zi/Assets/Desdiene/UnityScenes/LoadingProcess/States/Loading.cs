using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitcher;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class Loading : State
    {
        public Loading(MonoBehaviourExt mono,
                       IStateSwitcher<State> stateSwitcher,
                       AsyncOperation loadingOperation,
                       string sceneName)
            : base(mono,
                   stateSwitcher,
                   loadingOperation,
                   sceneName)
        { }

        public override void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode enablingMode)
        {
            bool isAllow = SceneEnablingAfterLoading.Check(enablingMode);
            LoadingOperation.allowSceneActivation = isAllow;
        }

        protected override void OnCheckingState()
        {
            if (ProgressInfo.LessThan90Percents) return;
            if (ProgressInfo.Equals90Percents && ProgressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return;
            }

            SwitchState<WaitingForAllowingToEnabling>();
        }
    }
}
