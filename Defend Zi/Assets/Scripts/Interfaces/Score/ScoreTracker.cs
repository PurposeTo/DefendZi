using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTracker : MonoBehaviourExt, IScore
{
    private IScore score;

    protected override void Constructor()
    {
        //todo: верное ли использование?
        score = GetComponentOnlyInParent<IScore>();
    }

    int IScore.Value => score.Value;
}
