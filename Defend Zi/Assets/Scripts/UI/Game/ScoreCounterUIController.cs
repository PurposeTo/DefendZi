using Desdiene.MonoBehaviourExtention;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(ScoreView))]
public class ScoreCounterUIController : MonoBehaviourExt
{
    private IScoreGetter score;
    private IScoreNotification scoreNotification;
    private ScoreView scoreCounterView;

    [Inject]
    private void Constructor(ComponentsProxy components)
    {
        score = components.PlayerScore;
        scoreNotification = components.PlayerScoreNotification;
        scoreCounterView = GetComponent<ScoreView>();
        scoreCounterView.SetScore(score.Value);
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void UpdateScore()
    {
        scoreCounterView.SetScore(score.Value);
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
