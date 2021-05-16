using UnityEngine;
using TMPro;

public class PlayerScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;

    public void Enable() => score.gameObject.SetActive(true);

    public void Disable() => score.gameObject.SetActive(false);

    public void ShowScore(int value)
    {
        score.text = $"score: {value}";
    }
}
