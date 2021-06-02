﻿using UnityEngine;
using System.Collections;
using Desdiene.MonoBehaviourExtention;
using Desdiene.Coroutine.CoroutineExecutor;

public class ScoreAdderByTime : MonoBehaviourExt
{
    [SerializeField] private float delay = 1.5f;
    [SerializeField] private int scorePerSec = 1;

    private IScoreCollector collector;

    public ScoreAdderByTime Constructor(IScoreCollector collector)
    {
        ICoroutine routine = CreateCoroutine();

        GameObjectsHolder.OnInited += (gameObjectsHolder) =>
        {
            gameObjectsHolder.Player.OnIsAwaked += () =>
            {
                ExecuteCoroutineContinuously(routine, Adder());
            };
        };

        this.collector = collector;
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
