using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Loadings.States.Base;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Loadings.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.Loadings.States
{
    public class Loading : State
    {
        public Loading(MonoBehaviourExt mono,
                       IStateSwitcher<State, StateContext> stateSwitcher,
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
