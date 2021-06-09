using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTracker : MonoBehaviourExt, IScoreGetter
{
    private IScoreGetter score;

    protected override void Constructor()
    {
        //todo: ������ �� �������������?
        score = GetComponentOnlyInParent<IScoreGetter>();
    }

    int IScoreGetter.Value => score.Value;
}
