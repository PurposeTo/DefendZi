using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using Desdiene.UnityScenes.LoadingProcess.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class Enabling : State
    {
        public Enabling(MonoBehaviourExt mono,
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
            Debug.LogWarning($"You can't change allowing for scene enabling after loading to {enablingMode} while {nameof(Enabling)}");
        }

        protected override void FindAndSwitchState(ProgressInfo progressInfo)
        {
            bool isEnablingAllow = SceneEnablingAfterLoading.IsAllow(progressInfo.SceneEnablindAfterLoading);
            if (progressInfo.Equals100Percents && isEnablingAllow && progressInfo.IsDone)
            {
                SwitchState<LoadedAndEnabled>();
            }
            else throw new InvalidOperationException();
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            bool isEnablingAllow = progressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow;
            if (progressInfo.Equals90Percents && isEnablingAllow) return true;
            else if (progressInfo.Between90And100PercentsExcluding && isEnablingAllow)
            {
                return true;
            }
            else return false;
        }
    }
}
