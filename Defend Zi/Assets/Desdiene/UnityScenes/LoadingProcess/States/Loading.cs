using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
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

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            if (progressInfo.LessThan90Percents) return true;
            else if (progressInfo.Equals90Percents && progressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return true;
            }
            else return false;
        }
    }
}
