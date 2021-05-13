using UnityEngine;
using TMPro;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;

    private IStat<int> playerScore;

    public void Constructor(IStat<int> playerScore)
    {
        this.playerScore = playerScore;
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void EnableScoreView() => score.gameObject.SetActive(true);

    public void DisableScoreView() => score.gameObject.SetActive(false);

    public void ShowScore()
    {
        int value = playerScore.Value;
        score.text = $"score: {value}";
    }

    private void SubscribeEvents()
    {
        playerScore.OnValueChanged += ShowScore;
    }

    private void UnsubscribeEvents()
    {
        playerScore.OnValueChanged -= ShowScore;
    }
}
