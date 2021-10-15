using System;

public partial class HealthReincarnation
{
    private class Alive : State
    {
        public Alive(HealthReincarnation _it)
            : base(_it) { }

        public override Action SubscribeToWhenAlive(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenDead(Action action, Action value) => action += value;

        public override void TakeDamage(IDamage damage)
        {
            int pastHp = It._health.Value;
            int damagePoints = (int)damage.Value;
            int nextHp = It._health.SetAndGet(pastHp - damagePoints);
            if (pastHp != nextHp) It.OnDamaged?.Invoke();
            if (It._healthPercent.IsMin) Die();
        }

        public override void Revive() { }

        protected override void OnEnter()
        {
            It.WhenAlive?.Invoke();
        }

        private void Die()
        {
            SwitchState<Dead>();
        }
    }
}