using System.Collections;
using Desdiene.CoroutineWrapper;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IScoreCollector))]
public class ScoreAdderByDistance : MonoBehaviourExt
{
    [Tooltip("Задержка перед началом исполнения")]
    [SerializeField] private float _delayBeforeStart = 1.5f;
    [Tooltip("Дистанция, за прохождение которой начисляется одно очко")]
    [SerializeField] private float _distancePerScore = 17.5f;

    private IScoreCollector _collector;
    private IPositionGetter _position;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _collector = GetInitedComponent<IScoreCollector>();
        _position = componentsProxy.PlayerPosition;
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(Adder());
    }

    private IEnumerator Adder()
    {
        yield return new WaitForSeconds(_delayBeforeStart);

        float nextOxPosition = 0f;
        var wait = new WaitUntil(() => _position.Value.x >= nextOxPosition);

        while (true)
        {
            nextOxPosition = _position.Value.x + _distancePerScore;
            yield return wait;
            _collector.Add(1);
        }
    }
}
