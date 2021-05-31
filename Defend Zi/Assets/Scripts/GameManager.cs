using System;
using Desdiene.Singleton;
using Desdiene.TimeControl.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене.
/// </summary>
public class GameManager : SingletonMonoBehaviourExt<GameManager>
{
    public event Action OnGameOver;
    private PausableGlobalTime IsGameOver;

    protected override void AwakeSingleton()
    {
        IsGameOver = new PausableGlobalTime(this, "Игра окончена");
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameObjectsHolder.OnInited += (gameObjectsHolder) =>
        {
            gameObjectsHolder.Player.OnDied += EndGame;
        };
    }

    private void UnsubscribeEvents()
    {
        GameObjectsHolder.Instance.Player.OnDied -= EndGame;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
