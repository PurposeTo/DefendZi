using UnityEngine;

public class ScorePoints : IScoreGetter
{
    [SerializeField] private int points = 1;

    int IScoreGetter.Value => points;
}
