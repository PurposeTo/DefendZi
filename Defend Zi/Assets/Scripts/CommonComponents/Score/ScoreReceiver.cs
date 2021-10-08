using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreReceiver : MonoBehaviourExt, IScoreNotification
{
    [SerializeField, NotNull] private InterfaceComponent<IScoreCollector> _scoreCollector;

    private event Action<int> OnReceived;

    event Action<int> IScoreNotification.OnReceived
    {
        add => OnReceived += value;
        remove => OnReceived -= value;
    }

    private IScoreCollector ScoreCollector => _scoreCollector.Implementation;

    // Начисление очков за близкое огибание препятствий происходит через триггер выхода из коллайдера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScoreAccessor score))
        {
            int value = score.Value;

            ScoreCollector.Add(value);
            OnReceived?.Invoke(value);
            Debug.Log($"Добавлено очков: {value}");
        }
    }
}
