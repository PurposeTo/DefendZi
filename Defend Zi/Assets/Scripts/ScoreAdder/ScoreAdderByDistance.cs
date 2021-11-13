using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IScoreCollector))]
public class ScoreAdderByDistance : MonoBehaviourExt
{
    [Tooltip("Задержка перед началом исполнения")]
    [SerializeField] private float _delayBeforeStart = 0.5f;
    [Tooltip("Дистанция, за прохождение которой начисляется одно очко")]
    [SerializeField] private float _distancePerScore = 17.5f;

    private IScoreCollector _collector;
    private IPositionAccessor _position;

    private ICoroutine _scoreAdding;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _collector = GetComponent<IScoreCollector>();
        _position = componentsProxy.PlayerPosition;
        _scoreAdding = new CoroutineWrap(this);
        _scoreAdding.StartContinuously(Adding());
    }

    private IEnumerator Adding()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

        float nextOxPosition = 0f;
        IEnumerator wait = new WaitUntil(() => _position.Value.x >= nextOxPosition);

        while (true)
        {
            nextOxPosition = _position.Value.x + _distancePerScore;
            yield return _scoreAdding.StartNested(wait);
            _collector.Add(1);
        }
    }
}
