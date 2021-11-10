﻿using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Класс описывает UI элемент, находящийся на Canvas
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public abstract partial class UiElement : MonoBehaviourExt, IUiElement
    {
        private IStateSwitcher<State> _stateSwitcher;
        private string _typeName;
        private string _gameObjectName;

        protected sealed override void AwakeExt()
        {
            _typeName = GetType().Name;
            _gameObjectName = gameObject.name;
            Canvas = GetComponent<Canvas>();
            CanvasGroup = GetComponent<CanvasGroup>();
            RectTransform = GetComponent<RectTransform>();

            State displayed = new Displayed(this);
            State hidden = new Hidden(this);

            State initState = Canvas.enabled
                ? displayed
                : hidden;

            List<State> allStates = new List<State>()
            {
                displayed,
                new FromDisplayedToHidden(this),
                hidden,
                new FromHiddenToDisplayed(this),
            };
            _stateSwitcher = new StateSwitcher<State>(initState, allStates);

            AwakeElement();
        }

        protected sealed override void OnDestroyExt()
        {
            OnDestroyElement();
        }

        public event Action WhenDisplayed
        {
            add => whenDisplayed = CurrentState.SubscribeToWhenDisplayed(whenDisplayed, value);
            remove => whenDisplayed -= value;
        }

        public event Action WhenHidden
        {
            add => whenHidden = CurrentState.SubscribeToWhenHidden(whenHidden, value);
            remove => whenHidden -= value;
        }

        private Action whenDisplayed;
        private Action whenHidden;

        protected virtual IUiElementAnimation Animation => new UiElementAnimationEmpty();
        protected Canvas Canvas { get; private set; }
        protected CanvasGroup CanvasGroup { get; private set; }
        protected RectTransform RectTransform { get; private set; }

        private State CurrentState => _stateSwitcher.CurrentState;

        public void Show() => CurrentState.Show();

        public void Hide() => CurrentState.Hide();

        protected abstract void AwakeElement();
        protected abstract void OnDestroyElement();
        protected abstract void ShowElement();
        protected abstract void HideElement();

        private void DisableCanvas() => Canvas.enabled = false;
        private void EnableCanvas() => Canvas.enabled = true;
        private void DisableInteractible() => CanvasGroup.interactable = false;
        private void EnableInteractible() => CanvasGroup.interactable = true;
    }
}
