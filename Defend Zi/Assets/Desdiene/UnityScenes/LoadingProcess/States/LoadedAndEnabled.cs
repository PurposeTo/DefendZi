using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachine.StateSwitcher;
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

        protected override void OnCheckingState()
        {
            if (ProgressInfo.IsDone
                && ProgressInfo.Equals100Percents
                && ProgressInfo.SceneEnablindAfterLoading == SceneEnablingAfterLoading.Mode.Allow)
            {
                return;
            }

            throw new InvalidOperationException($"Unknown loading status! "
                                                + $"Progress: {Progress * 100}%, "
                                                + $"allowSceneActivation = {ProgressInfo.SceneEnablindAfterLoading}, "
                                                + $"_loadingOperation.isDone = {ProgressInfo.IsDone}");
        }
    }
}
