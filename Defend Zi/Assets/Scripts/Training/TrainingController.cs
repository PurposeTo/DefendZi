using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class TrainingController : MonoBehaviourExt
{
    [SerializeField, NotNull] private TrainingAnimator _trainingAnimator;
    private readonly int _gamesNumberToEnableTraining = 10;
    private readonly int _trainingTime = 10;
    private IGameStatisticsAccessor _gameStatistics;
    private ICoroutine _training;

    [Inject]
    private void Constructor(IGameStatistics gameStatistics)
    {
        _gameStatistics = gameStatistics ?? throw new ArgumentNullException(nameof(gameStatistics));
        _training = new CoroutineWrap(this);

    }

    protected override void AwakeExt()
    {
        SetDefaultState();
        TryToEnable();
    }

    protected override void OnDisableExt()
    {
        SetDefaultState();
    }

    private IEnumerator Training()
    {
        Enable();
        yield return new WaitForSeconds(_trainingTime);
        Disable();
    }

    private void Enable()
    {
        _trainingAnimator.Enable();
    }

    private void Disable()
    {
        _trainingAnimator.Disable();
    }

    private void SetDefaultState() => Disable();

    private void TryToEnable()
    {
        if (_gameStatistics.GamesNumber <= _gamesNumberToEnableTraining)
        {
            _training.StartContinuously(Training());
        }
    }
}
