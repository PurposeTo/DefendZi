using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTransmitter : MonoBehaviourExt, IScoreAccessor
{
    private IScoreAccessor score;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        score = GetInitedComponentOnlyInParent<IScoreAccessor>();
    }

    int IScoreAccessor.Value => score.Value;
}
