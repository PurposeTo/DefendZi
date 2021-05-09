using System;
using Desdiene.Singleton;
using UnityEngine;


/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене.
/// </summary>
public class GameManager : SingletonSuperMonoBehaviour<GameManager>
{
    public event Action OnGameOver;

    protected override void AwakeSingleton()
    {
        SubscribeEvents();
    }

    protected override void OnDestroyWrapped()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameObjectsHolder.InitializedInstance += (instance) =>
        {
            instance.ZiPresenter.Health.OnZiDie += EndGame;
        };
    }

    private void UnsubscribeEvents()
    {
        GameObjectsHolder.Instance.ZiPresenter.Health.OnZiDie -= EndGame;
    }

    /// <summary>
    /// Закончить игру
    /// </summary>
    public void EndGame()
    {
        OnGameOver?.Invoke();
        //todo: включить паузу
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    public void ResumeEndedGame()
    {
        //todo: выключить паузу.
    }

    public void ReloadLvl()
    {
        //обратиться к контроллеру сцен, перезапустить уровень.
    }
}
