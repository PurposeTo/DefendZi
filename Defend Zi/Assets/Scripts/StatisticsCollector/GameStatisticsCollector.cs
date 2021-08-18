using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class GameStatisticsCollector : MonoBehaviourExt
{
    private IDeath _playerDeath;
    private GameStatistics _statistics = new GameStatistics();

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _playerDeath = componentsProxy.PlayerDeath;
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        if (!_playerDeath.IsDeath) _statistics.LifeTimeSec += deltaTime;
    }

    public GameStatistics GetStatistics() => _statistics;

    private void SubscribeEvents()
    {
        //todo: понадобиться позже
    }

    private void UnsubscribeEvents()
    {
        //todo: понадобиться позже
    }
}
