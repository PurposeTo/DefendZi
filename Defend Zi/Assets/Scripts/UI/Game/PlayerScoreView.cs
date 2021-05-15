using UnityEngine;
using TMPro;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;

    public void EnableScoreView() => score.gameObject.SetActive(true);

    public void DisableScoreView() => score.gameObject.SetActive(false);

    public void ShowScore(int value)
    {
        score.text = $"score: {value}";
    }
}
