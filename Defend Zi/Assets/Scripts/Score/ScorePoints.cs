using UnityEngine;

public class ScorePoints : IScore
{
    [SerializeField] private int points = 1;

    int IScore.Value => points;
}
