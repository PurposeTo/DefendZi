using System;
using Desdiene.MonoBehaviourExtension;

public partial class PlayerHealth
{
    private class Dead : State
    {
        public Dead(MonoBehaviourExt mono, PlayerHealth _it) : base(mono, _it) { }

        public override Action SubscribeToWhenAlive(Action action, Action value) => action += value;

        public override Action SubscribeToWhenDead(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenInvulnerable(Action action, Action value) => action += value;
        public override Action SubscribeToWhenMortal(Action action, Action value) => action += value;

        public override void TakeDamage(IDamage damage) { }

        public override void Revive()
        {
            It.OnReviving?.Invoke();
            It._healthPercent.SetMax();
            SwitchState<Alive>();
        }

        protected override void OnEnter()
        {
            It.OnDeath?.Invoke();
            It.WhenDead?.Invoke();
        }
    }
}
