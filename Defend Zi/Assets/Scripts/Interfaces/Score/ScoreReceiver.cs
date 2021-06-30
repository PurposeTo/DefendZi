using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreReceiver : MonoBehaviourExt
{
    private readonly Desdiene.Logger logger = new Desdiene.Logger(typeof(ScoreReceiver));

    public event Action<int> OnReceived;
    private IScoreCollector scoreCollector;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        scoreCollector = GetComponentInParent<IScoreCollector>();
    }

    // Начисление очков за близкое огибание препятствий происходит через триггер выхода из коллайдера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScoreGetter score))
        {
            int value = score.Value;

            scoreCollector.Add(value);
            OnReceived?.Invoke(value);
            logger.Log($"Добавлено очков: {value}");
        }
    }
}
