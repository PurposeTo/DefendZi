using System;
using Desdiene.StateMachines.StateSwitchers;

public partial class HealthReincarnation
{
    private class Alive : State
    {
        public Alive(HealthReincarnation _it, IStateSwitcher<State, HealthReincarnation> stateSwitcher)
            : base(_it, stateSwitcher) { }

        public override Action SubscribeToWhenAlive(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenDead(Action action, Action value) => action += value;

        protected override void OnEnter(HealthReincarnation it)
        {
            it.WhenAlive?.Invoke();
        }

        protected override void TakeDamage(HealthReincarnation it, IDamage damage)
        {
            int pastHp = it._health.Value;
            int damagePoints = (int)damage.Value;
            int nextHp = it._health.SetAndGet(pastHp - damagePoints);
            if (pastHp != nextHp) it.OnDamaged?.Invoke();
            if (it._healthPercent.IsMin) Die();
        }

        protected override void Revive(HealthReincarnation it) { }

        private void Die()
        {
            SwitchState<Dead>();
        }
    }
}