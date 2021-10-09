﻿using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextView))]
public class ScoreCounterUIController : MonoBehaviourExt
{
    private IScoreAccessor score;
    private IScoreNotification scoreNotification;
    private TextView scoreCounterView;

    [Inject]
    private void Constructor(ComponentsProxy components)
    {
        score = components.PlayerScore;
        scoreNotification = components.PlayerScoreNotification;
        scoreCounterView = GetInitedComponent<TextView>();
        scoreCounterView.SetText($"{score.Value}");
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void UpdateScore(uint scoreReceived)
    {
        scoreCounterView.SetText($"{score.Value}");
    }

    private void SubcribeEvents()
    {
        scoreNotification.OnReceived += UpdateScore;
    }

    private void UnsubcribeEvents()
    {
        scoreNotification.OnReceived -= UpdateScore;
    }
}
