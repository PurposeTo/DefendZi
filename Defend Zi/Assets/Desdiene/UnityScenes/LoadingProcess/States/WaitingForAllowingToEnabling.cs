using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class WaitingForAllowingToEnabling : State
    {
        public WaitingForAllowingToEnabling(MonoBehaviourExt mono,
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

        public override void OnEnter()
        {
            base.OnEnter();
            onWaitingForAllowingToEnabling?.Invoke();
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            if (progressInfo.Equals90Percents && progressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Forbid)
            {
                return true;
            }
            else return false;
        }

        private void AllowSceneEnabling() => LoadingOperation.allowSceneActivation = true;
    }
}
