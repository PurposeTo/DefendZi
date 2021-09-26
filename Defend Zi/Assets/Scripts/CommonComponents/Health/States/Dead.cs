using System;
using Desdiene.StateMachines.StateSwitchers;

public partial class PlayerHealth
{
    private class Dead : State
    {
        public Dead(PlayerHealth _it, IStateSwitcher<State, PlayerHealth> stateSwitcher) : base(_it, stateSwitcher) { }

        public override Action SubscribeToWhenAlive(Action action, Action value) => action += value;

        public override Action SubscribeToWhenDead(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        protected override void OnEnter(PlayerHealth it)
        {
            it.OnDeath?.Invoke();
            it.WhenDead?.Invoke();
        }

        protected override void TakeDamage(PlayerHealth it, IDamage damage) { }

        protected override void Revive(PlayerHealth it)
        {
            it.OnReviving?.Invoke();
            it._healthPercent.SetMax();
            SwitchState<Alive>();
        }
    }
}
