using Desdiene.MonoBehaviourExtension;
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
        scoreCounterView = GetComponent<TextView>();
        UpdateScoreText(score.Value);
        SubcribeEvents();
    }

    private void OnDestroy()
    {
        UnsubcribeEvents();
    }

    private void UpdateScoreText(uint scoreReceived)
    {
        scoreCounterView.SetText($"{score.Value}");
    }

    private void SubcribeEvents()
    {
        scoreNotification.OnReceived += UpdateScoreText;
    }

    private void UnsubcribeEvents()
    {
        scoreNotification.OnReceived -= UpdateScoreText;
    }
}
