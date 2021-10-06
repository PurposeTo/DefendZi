using System;
using Desdiene.StateMachines.StateSwitchers;

public partial class HealthReincarnation
{
    private class Dead : State
    {
        public Dead(HealthReincarnation _it, IStateSwitcher<State, HealthReincarnation> stateSwitcher) : base(_it, stateSwitcher) { }

        public override Action SubscribeToWhenAlive(Action action, Action value) => action += value;

        public override Action SubscribeToWhenDead(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        protected override void OnEnter(HealthReincarnation it)
        {
            it.OnDeath?.Invoke();
            it.WhenDead?.Invoke();
        }

        protected override void TakeDamage(HealthReincarnation it, IDamage damage) { }

        protected override void Revive(HealthReincarnation it)
        {
            it.OnReviving?.Invoke();
            it._healthPercent.SetMax();
            SwitchState<Alive>();
        }
    }
}
