using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IScoreCollector))]
public class ScoreAdderByDistance : MonoBehaviourExt
{
    [SerializeField] private float _distancePerScore = 17.5f;
    [SerializeField] private float _delay = 1.5f;

    private IScoreCollector _collector;
    private IPositionGetter _position;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        _collector = GetComponent<IScoreCollector>();
        _position = componentsProxy.PlayerPosition;
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(Adder());
    }

    private IEnumerator Adder()
    {
        yield return new WaitForSeconds(_delay);

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
