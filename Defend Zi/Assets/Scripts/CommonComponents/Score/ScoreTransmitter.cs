using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTransmitter : MonoBehaviourExt, IScoreAccessor
{
    private IScoreAccessor _score;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        _score = GetInitedComponentOnlyInParent<IScoreAccessor>();
    }

    int IScoreAccessor.Value => _score.Value;
}
