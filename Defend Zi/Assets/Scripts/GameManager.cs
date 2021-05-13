using System;
using Desdiene.Singleton;
using Desdiene.TimeControl.Pause;
using UnityEngine;


/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене.
/// </summary>
public class GameManager : SingletonSuperMonoBehaviour<GameManager>
{
    public event Action OnGameOver;
    private PausableGlobalTime IsGameOver;

    protected override void AwakeSingleton()
    {
        IsGameOver = new PausableGlobalTime(this, "Игра окончена");
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
        IsGameOver.SetPause(true);
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    public void ResumeEndedGame()
    {
        IsGameOver.SetPause(false);
    }

    public void ReloadLvl()
    {
        //обратиться к контроллеру сцен, перезапустить уровень.
    }
}
