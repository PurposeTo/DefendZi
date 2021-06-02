using UnityEngine;
using System.Collections;

public class ScoreAdderByTime : MonoBehaviour
{
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private int speed = 1;

    private IScoreCollector collector;

    private void Awake()
    {
        /*Ожидание инициализации collector
         * 
         */
        StartCoroutine(AdderEnumerator());
    }

    public ScoreAdderByTime Constructor(IScoreCollector collector)
    {
        this.collector = collector;
        return this;
    }

    private IEnumerator AdderEnumerator()
    {
        yield return new WaitForSecondsRealtime(delay);

        while (Time.timeScale != 0f)
        {
            var wait = new WaitForSecondsRealtime(1f);
            yield return wait;
            collector.Add(speed / (int)wait.waitTime);
        }
    }
}
