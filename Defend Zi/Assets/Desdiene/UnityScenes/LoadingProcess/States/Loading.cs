using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingProcess.Components;
using Desdiene.UnityScenes.LoadingProcess.States.Base;
using Desdiene.UnityScenes.LoadingProcess.Components;
using Desdiene.UnityScenes.LoadingProcess.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class Loading : State
    {
        public Loading(MonoBehaviourExt mono,
                       IStateSwitcher<State, MutableData> stateSwitcher,
                       AsyncOperation loadingOperation,
                       string sceneName)
            : base(mono,
                   stateSwitcher,
                   loadingOperation,
                   sceneName)
        { }

        public override void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode enablingMode)
        {
            bool isAllow = SceneEnablingAfterLoading.IsAllow(enablingMode);
            LoadingOperation.allowSceneActivation = isAllow;
        }

        protected override void FindAndSwitchState(ProgressInfo progressInfo)
        {
            bool isEnablingForbid = SceneEnablingAfterLoading.IsForbid(progressInfo.SceneEnablindAfterLoading);
            bool isEnablingAllow = !isEnablingForbid;
            if (progressInfo.Equals90Percents && isEnablingForbid)
            {
                SwitchState<WaitingForAllowingToEnabling>();
            }
            else if (progressInfo.MoreOrEqualsThan90Percents && isEnablingAllow)
            {
                SwitchState<Enabling>();
            }
            else throw new InvalidOperationException();
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            bool isEnablingAllow = SceneEnablingAfterLoading.IsAllow(progressInfo.SceneEnablindAfterLoading);
            if (progressInfo.LessThan90Percents) return true;
            else if (progressInfo.Equals90Percents && isEnablingAllow)
            {
                return true;
            }
            else return false;
        }
    }
}
