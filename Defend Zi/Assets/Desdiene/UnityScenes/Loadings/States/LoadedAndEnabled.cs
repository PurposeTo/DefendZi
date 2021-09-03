﻿using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.Loadings.States.Base;
using UnityEngine;

namespace Desdiene.UnityScenes.Loadings.States
{
    public class LoadedAndEnabled : State
    {
        public LoadedAndEnabled(MonoBehaviourExt mono,
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
