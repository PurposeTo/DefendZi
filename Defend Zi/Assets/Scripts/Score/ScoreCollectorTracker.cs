using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreCollectorTracker : MonoBehaviourExt
{
    public event Action<int> OnTracked;
    private IScoreCollector scoreCollector;

    protected override void Constructor()
    {
        //todo: верное ли использование?
        scoreCollector = GetComponentInParent<IScoreCollector>();
    }

    // Ќачисление очков за близкое огибание преп€тствий происходит через триггер выхода из коллайдера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScoreGetter score))
        {
            int value = score.Value;

            scoreCollector.Add(value);
            OnTracked?.Invoke(value);
            Debug.Log($"{GetType()}. ƒобавлено очков: {value}");
        }
    }
}
