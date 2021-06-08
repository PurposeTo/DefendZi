using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreCollectorTracker : MonoBehaviourExt
{
    private IScoreCollector scoreCollector;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        scoreCollector = GetComponentInParent<IScoreCollector>();
    }

    // Ќачисление очков за близкое огибание преп€тствий происходит через триггер выхода из коллайдера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScore score))
        {
            scoreCollector.Add(score.Value);
            Debug.Log($"ƒобавлено очков: {score.Value}");
        }
    }
}
