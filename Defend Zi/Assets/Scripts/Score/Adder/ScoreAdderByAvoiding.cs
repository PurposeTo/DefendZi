using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Начисление очков за близкое огибание препятствий происходит через триггер коллайдеров
[RequireComponent(typeof(ScorePoints))]
public class ScoreAdderByAvoiding : MonoBehaviour
{
    private IScoreGetter score;

    private void Awake()
    {
        score = GetComponent<ScorePoints>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScoreCollector collector))
        {
            collector.Add(score.Value);
            Debug.Log($"Добавлено очков: {score.Value}");
        }
    }
}
