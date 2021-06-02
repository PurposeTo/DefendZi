using UnityEngine;

public class ScorePoints : MonoBehaviour, IScore
{
    [SerializeField] private int points = 1;

    int IScore.Value => points;
}
