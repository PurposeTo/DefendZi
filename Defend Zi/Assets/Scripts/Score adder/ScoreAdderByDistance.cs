using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

//todo: перенести данный класс в SceneContext?
[RequireComponent(typeof(IScoreCollector))]
public class ScoreAdderByDistance : MonoBehaviourExt
{
    [SerializeField, NotNull] private float distanceToGetScore = 17.5f;
    [SerializeField, NotNull] private int scorePerDistance = 1;
    [SerializeField, NotNull] private float delay = 1f;

    private IScoreCollector collector;
    private IPositionGetter position;

    [Inject]
    private void Constructor(ComponentsProxy componentsProxy)
    {
        collector = GetComponent<IScoreCollector>();
        position = componentsProxy.PlayerPosition;
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(Adder());
    }

    private IEnumerator Adder()
    {
        yield return new WaitForSeconds(delay);

        float positionOxToGetPoints = default;
        var wait = new WaitUntil(() => position.Value.x >= positionOxToGetPoints);

        while (true)
        {
            positionOxToGetPoints = position.Value.x + distanceToGetScore;
            yield return wait;
            collector.Add(scorePerDistance);
        }
    }
}
