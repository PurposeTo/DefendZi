using System;
using Desdiene.GameDataAsset.Storage;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControl.Pausable;
using Desdiene.TimeControl.Pauser;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Класс отвечает за события, происходящие на игровой сцене. (не относится к сценам main menu и тп.)
/// Need to be a singleton!
/// </summary>
public class GameManager : MonoBehaviourExt
{
    private GlobalTimePauser _isGameOver;

    private IDeath _playerDeath;
    private IScoreGetter _playerScore;

    private IStorage<IGameData> _dataStorage;

    [Inject]
    private void Constructor(GlobalTimePausable globalTimePausable, ComponentsProxy componentsProxy, IStorage<IGameData> dataStorage)
    {
        _playerDeath = componentsProxy.PlayerDeath;
        _playerScore = componentsProxy.PlayerScore;
        _dataStorage = dataStorage;

        _isGameOver = new GlobalTimePauser(this, globalTimePausable, "Окончание игры");
        SubscribeEvents();
        OnGameStarted?.Invoke();
    }

    public event Action OnGameStarted;
    public event Action OnGameOver;

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    public void ReloadLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SubscribeEvents()
    {
        _playerDeath.OnDied += SavePlayerScore;
        _playerDeath.OnDied += EndGame;
        _playerDeath.OnReborn += ResumeEndedGame;
        OnGameOver += SaveGameData;
    }

    private void UnsubscribeEvents()
    {
        _playerDeath.OnDied -= SavePlayerScore;
        _playerDeath.OnDied -= EndGame;
        _playerDeath.OnReborn -= ResumeEndedGame;
        OnGameOver += SaveGameData;
    }

    /// <summary>
    /// Закончить игру
    /// </summary>
    private void EndGame()
    {
        _isGameOver.SetPause(true);
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Продолжить законченную игру (Например, после возрождения игрока).
    /// </summary>
    private void ResumeEndedGame()
    {
        _isGameOver.SetPause(false);
    }

    private void SavePlayerScore()
    {
        _dataStorage
            .GetData()
            .SetBestScore((uint)_playerScore.Value);
    }

    private void SaveGameData() => _dataStorage.InvokeSavingData();
}
