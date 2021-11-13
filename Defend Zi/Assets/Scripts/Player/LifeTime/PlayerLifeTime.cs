using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Отвечает за сбор данных за игровую попытку.
/// Существует только на игровой сцене.
/// НЕ использовать вычисление времени по DateTime.now, 
/// тк пользователь может сменить время на часах 
/// И время должно считаться когда нет паузы.
/// </summary>
[RequireComponent(typeof(IHealthNotification))]
public class PlayerLifeTime : MonoBehaviourExt
{
    private IHealthNotification _playerDeath;
    private ICoroutine _lifeTimeCounting;

    protected override void AwakeExt()
    {
        _playerDeath = GetComponent<IHealthNotification>();
        _lifeTimeCounting = new CoroutineWrap(this);
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    public TimeSpan Value { get; set; }

    private void SubscribeEvents()
    {
        _playerDeath.WhenAlive += StartLifeTimeCounter;
        _playerDeath.WhenDead += _lifeTimeCounting.Terminate;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.WhenAlive -= StartLifeTimeCounter;
        _playerDeath.WhenDead -= _lifeTimeCounting.Terminate;
    }

    private void StartLifeTimeCounter()
    {
        _lifeTimeCounting.StartContinuously(LifeTimeCounting());
    }

    /// <summary>
    /// Cчитает время только тогда, когда игрок жив.
    /// Так же, необходимо вновь запускать, если игрок вернулся к жизни после смерти.
    /// </summary>
    private IEnumerator LifeTimeCounting()
    {
        while (true)
        {
            float deltaTime = Time.deltaTime;
            Value += TimeSpan.FromSeconds(deltaTime);
            yield return null;
        }
    }
}
