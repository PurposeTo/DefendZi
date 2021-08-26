using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using Desdiene.UnityScenes.LoadingProcess.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class WaitingForAllowingToEnabling : State
    {
        public WaitingForAllowingToEnabling(MonoBehaviourExt mono,
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
            switch (enablingMode)
            {
                case SceneEnablingAfterLoading.Mode.Allow:
                    AllowSceneEnabling();
                    break;
                case SceneEnablingAfterLoading.Mode.Forbid:
                    Debug.LogWarning($"Scene enabling after loading mode is already {enablingMode}");
                    break;
                default:
                    throw new ArgumentException($"{enablingMode} is unknown Scene enabling after loading mode");
            }
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            onWaitingForAllowingToEnabling?.Invoke();
        }

        protected override void FindAndSwitchState(ProgressInfo progressInfo)
        {
            bool isEnablingAllow = SceneEnablingAfterLoading.IsAllow(progressInfo.SceneEnablindAfterLoading);
            if (progressInfo.MoreOrEqualsThan90Percents && isEnablingAllow)
            {
                SwitchState<Enabling>();
            }
            else throw new InvalidOperationException();
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            bool isEnablingForbid = SceneEnablingAfterLoading.IsForbid(progressInfo.SceneEnablindAfterLoading);
            if (progressInfo.Equals90Percents && isEnablingForbid)
            {
                return true;
            }
            else return false;
        }

        private void AllowSceneEnabling()
        {
            LoadingOperation.allowSceneActivation = true;
        }
    }
}
