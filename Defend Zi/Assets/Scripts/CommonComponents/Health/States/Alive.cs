using System;
using Desdiene.StateMachines.StateSwitchers;

public partial class PlayerHealth
{
    private class Alive : State
    {
        public Alive(PlayerHealth _it, IStateSwitcher<State, PlayerHealth> stateSwitcher) : base(_it, stateSwitcher) { }

        public override Action SubscribeToWhenAlive(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenDead(Action action, Action value) => action += value;

        protected override void OnEnter(PlayerHealth it)
        {
            it.WhenAlive?.Invoke();
        }

        protected override void TakeDamage(PlayerHealth it, IDamage damage)
        {
            UnityEngine.Debug.Log("КРЯ " + it);
            UnityEngine.Debug.Log("КРЯ " + it._health);
            int pastHp = it._health.Value;
            int damagePoints = (int)damage.Value;
            int nextHp = it._health.SetAndGet(pastHp - damagePoints);
            if (pastHp != nextHp) it.OnDamaged?.Invoke();
            if (it._healthPercent.IsMin) Die();
        }

        protected override void Revive(PlayerHealth it) { }

        private void Die()
        {
            SwitchState<Dead>();
        }
    }
}