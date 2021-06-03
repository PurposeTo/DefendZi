using System.Collections;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

public class ScoreAdderByTime : MonoBehaviourExt
{
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private int scorePerSec = 1;

    private IScoreCollector collector;

    public ScoreAdderByTime Constructor(IScoreCollector collector)
    {
        this.collector = collector;
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(Adder());
        return this;
    }

    private IEnumerator Adder()
    {
        yield return new WaitForSeconds(delay);
        var wait = new WaitForSeconds(1f);

        while (true)
        {
            yield return wait;
            collector.Add(scorePerSec);
        }
    }
}
