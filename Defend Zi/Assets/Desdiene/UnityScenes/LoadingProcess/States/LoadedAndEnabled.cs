using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitching;
using Desdiene.UnityScenes.LoadingOperationAsset;
using Desdiene.UnityScenes.LoadingOperationAsset.States.Base;
using Desdiene.UnityScenes.LoadingProcess.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess.States
{
    public class LoadedAndEnabled : State
    {
        public LoadedAndEnabled(MonoBehaviourExt mono,
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
            Debug.LogWarning($"You can't change scene enabling after loading mode to {enablingMode}, because it is already loaded and enabled");
        }

        protected override void OnEnter()
        {
            DisableStateMachine();
        }

        protected override void FindAndSwitchState(ProgressInfo progressInfo)
        {
            // Нельзя выйти из данного состояния.
            throw new InvalidOperationException();
        }

        protected override bool IsThisState(ProgressInfo progressInfo)
        {
            bool isEnablingAllow = SceneEnablingAfterLoading.IsAllow(progressInfo.SceneEnablindAfterLoading);
            if (progressInfo.IsDone
                && progressInfo.Equals100Percents
                && isEnablingAllow)
            {
                return true;
            }
            else return false;
        }
    }
}
