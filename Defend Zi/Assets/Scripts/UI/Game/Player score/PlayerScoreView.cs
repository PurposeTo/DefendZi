using UnityEngine;
using TMPro;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;

    public void ShowScore(int value)
    {
        score.text = $"score: {value}";
    }
}
