using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTransmitter : MonoBehaviourExt, IScoreGetter
{
    private IScoreGetter score;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        score = GetInitedComponentOnlyInParent<IScoreGetter>();
    }

    int IScoreGetter.Value => score.Value;
}
