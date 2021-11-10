﻿using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public partial class PlayerHealth
{
    private class Alive : State
    {
        private const float _immortalityTime = 3f;
        private ICoroutine _immortality;

        public Alive(MonoBehaviourExt mono, PlayerHealth _it) : base(mono, _it)
        {
            _immortality = new CoroutineWrap(mono);
            It.OnReviving += EnableImmortality;
            _immortality.OnStarted += () => It.WhenImmortal?.Invoke();
            _immortality.OnStopped += () => It.WhenMortal?.Invoke();
        }

        public override Action SubscribeToWhenAlive(Action action, Action value)
        {
            value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenDead(Action action, Action value) => action += value;

        public override Action SubscribeToWhenImmortal(Action action, Action value)
        {
            if (_immortality.IsExecuting) value?.Invoke();
            return action += value;
        }

        public override Action SubscribeToWhenMortal(Action action, Action value)
        {
            if (!_immortality.IsExecuting) value?.Invoke();
            return action += value;
        }

        public override void TakeDamage(IDamage damage)
        {
            if (_immortality.IsExecuting) return;

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

        private void EnableImmortality()
        {
            _immortality.StartContinuously(Immortality());
        }

        private IEnumerator Immortality()
        {
            yield return new WaitForSeconds(_immortalityTime);
        }
    }
}