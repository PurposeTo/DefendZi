using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Loadings.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.Loadings.States
{
    public class Enabling : State
    {
        public Enabling(MonoBehaviourExt mono,
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
