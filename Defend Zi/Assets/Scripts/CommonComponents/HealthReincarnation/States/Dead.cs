using System;

public partial class HealthReincarnation
{
    private class Dead : State
    {
        public Dead(HealthReincarnation _it) : base(_it) { }

        public override Action SubscribeToWhenAlive(Action action, Action value) => action += value;

        public override Action SubscribeToWhenDead(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

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
