using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
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

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            if (progressInfo.Between90And100PercentsIncluding
                && progressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return true;
            }
            else return false;
        }
    }
}
