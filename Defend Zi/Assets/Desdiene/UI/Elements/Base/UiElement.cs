using System;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.StateMachines.StateSwitchers;
using Desdiene.Types.AtomicReferences;
using Desdiene.Types.Processes;
using UnityEngine;

namespace Desdiene.UI.Elements
{
    /// <summary>
    /// Класс описывает UI элемент, находящийся на Canvas
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public abstract partial class UiElement : MonoBehaviourExt, IUiElement
    {
        private readonly IRef<State> _refCurrentState = new Ref<State>();
        private string _stateName; //for unity log
        private string _typeName;
        private string _gameObjectName;

        protected sealed override void AwakeExt()
        {
            _typeName = GetType().Name;
            _gameObjectName = gameObject.name;
            _refCurrentState.OnChanged += () => _stateName = CurrentState.GetType().Name;
            RectTransform = GetComponent<RectTransform>();
            Canvas = GetComponent<Canvas>();

            var stateSwitcher = new StateSwitcher<State>(_refCurrentState);
            List<State> allStates = new List<State>()
            {
                new Displayed(this, stateSwitcher),
                new FromDisplayedToHidden(this, stateSwitcher),
                new Hidden(this, stateSwitcher),
                new FromHiddenToDisplayed(this, stateSwitcher),
            };
            stateSwitcher.Add(allStates);

            if (Canvas.enabled) stateSwitcher.Switch<Displayed>();
            else stateSwitcher.Switch<Hidden>();

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

        protected Canvas Canvas { get; private set; }
        protected RectTransform RectTransform { get; private set; }
        private State CurrentState => _refCurrentState.Value ?? throw new NullReferenceException(nameof(CurrentState));

        public IProcessAccessorNotifier Show() => CurrentState.Show();

        public IProcessAccessorNotifier Hide() => CurrentState.Hide();

        protected abstract void AwakeElement();
        protected abstract void OnDestroyElement();
        protected abstract IProcessAccessorNotifier ShowElement();
        protected abstract IProcessAccessorNotifier HideElement();

        private void DisableCanvas() => Canvas.enabled = false;
        private void EnableCanvas() => Canvas.enabled = true;
    }
}
