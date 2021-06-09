using TMPro;
using UnityEngine;
using Desdiene.MonoBehaviourExtention;

public class ScoreView : MonoBehaviourExt
{
    [SerializeField] private TMP_Text scoreText;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
