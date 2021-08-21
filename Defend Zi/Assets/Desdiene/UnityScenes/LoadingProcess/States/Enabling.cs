using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitcher;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class Enabling : State
    {
        public Enabling(MonoBehaviourExt mono,
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
            Debug.LogWarning($"You can't change allowing for scene enabling after loading to {enablingMode} while {nameof(Enabling)}");
        }

        protected override void OnCheckingState()
        {
            if (ProgressInfo.Between90And100PercentsIncluding
                && ProgressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return;
            }
            SwitchState<LoadedAndEnabled>();
        }
    }
}
