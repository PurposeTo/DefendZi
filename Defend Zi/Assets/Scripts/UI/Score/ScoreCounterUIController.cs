using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextView))]
public class ScoreCounterUIController : MonoBehaviourExt
{
    private IScoreGetter score;
    private IScoreNotification scoreNotification;
    private TextView scoreCounterView;

    [Inject]
    private void Constructor(ComponentsProxy components)
    {
        score = components.PlayerScore;
        scoreNotification = components.PlayerScoreNotification;
        scoreCounterView = GetComponent<TextView>();
        scoreCounterView.SetText($"{score.Value}");
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void UpdateScore()
    {
        scoreCounterView.SetText($"{score.Value}");
    }

    private void SubcribeEvents()
    {
        scoreNotification.OnScoreChanged += UpdateScore;
    }

    private void UnsubcribeEvents()
    {
        scoreNotification.OnScoreChanged -= UpdateScore;
    }
}
