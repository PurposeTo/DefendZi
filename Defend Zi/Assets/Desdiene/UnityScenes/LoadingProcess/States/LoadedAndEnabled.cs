using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class LoadedAndEnabled : State
    {
        public LoadedAndEnabled(MonoBehaviourExt mono,
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
            Debug.LogWarning($"You can't change scene enabling after loading mode to {enablingMode}, because it is already loaded and enabled");
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            if (progressInfo.IsDone
                && progressInfo.Equals100Percents
                && progressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return true;
            }
            else return false;
        }
    }
}
