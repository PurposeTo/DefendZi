using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneTypes;
using Desdiene.StateMachines.States;
using Desdiene.StateMachines.StateSwitchers;

namespace Desdiene.SceneLoaders.Single.States.Base
{
    public abstract class State : MonoBehaviourExtContainer, IStateEntryExitPoint<StateContext>
    {
        private readonly IStateSwitcher<State, StateContext> _stateSwitcher;
        private SceneAsset _sceneToLoad;

        public State(MonoBehaviourExt mono, IStateSwitcher<State, StateContext> stateSwitcher) : base(mono)
        {
            _stateSwitcher = stateSwitcher;
        }

        void IStateEntryExitPoint<StateContext>.OnEnter(StateContext stateContext)
        {
            if (stateContext != null)
            {
                SceneToLoad = stateContext.Scene;
            }

            OnEnter();
        }

        StateContext IStateEntryExitPoint<StateContext>.OnExit()
        {
            OnExit();
            return new StateContext(SceneToLoad);
        }

        protected SceneAsset SceneToLoad
        {
            get
            {
                return _sceneToLoad ?? throw new System.NullReferenceException(nameof(_sceneToLoad));
            }
            set => _sceneToLoad = value;
        }

        public abstract void Load(SceneAsset scene);
        public abstract void Reload();

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected State SwitchState<stateT>() where stateT : State => _stateSwitcher.Switch<stateT>();
    }
}
