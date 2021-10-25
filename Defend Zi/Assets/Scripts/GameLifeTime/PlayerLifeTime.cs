using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

/// <summary>
/// Отвечает за сбор данных за игровую попытку.
/// Существует только на игровой сцене.
/// НЕ использовать вычисление времени по DateTime.now, 
/// тк пользователь может сменить время на часах 
/// И время должно считаться когда нет паузы.
/// </summary>
public class PlayerLifeTime : MonoBehaviourExt
{
    private IHealthNotification _playerDeath;
    private ICoroutine _lifeTimeCounting;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _playerDeath = componentsProxy.PlayerDeath;
        _lifeTimeCounting = new CoroutineWrap(this);
    }

    public TimeSpan Value { get; set; }

    protected override void AwakeExt()
    {
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

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
