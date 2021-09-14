using System.Collections;
using System.Collections.Generic;
using Desdiene.Coroutines;
using Desdiene.DataStorageFactories.Storages;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class TrainingController : MonoBehaviourExt
{
    [SerializeField, NotNull] private TrainingAnimator _trainingAnimator;
    private readonly int _gamesNumberToEnableTraining = 10;
    private readonly int _trainingTime = 10;
    private IStorage<IGameData> _storage;
    private ICoroutine _training;

    [Inject]
    private void Constructor(IStorage<IGameData> storage)
    {
        TryAwake();
        _storage = storage ?? throw new System.ArgumentNullException(nameof(storage));
        _training = new CoroutineWrap(this);
        SetDefaultState();
        TryToEnable();
    }

    protected override void OnDisableExt()
    {
        SetDefaultState();
    }

    private IGameData Data => _storage.GetData();

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
        if (Data.GamesNumber <= _gamesNumberToEnableTraining)
        {
            _training.StartContinuously(Training());
        }
    }
}
