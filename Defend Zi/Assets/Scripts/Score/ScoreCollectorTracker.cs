using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreCollectorTracker : MonoBehaviourExt
{
    private IScoreCollector scoreCollector;

    protected override void Constructor()
    {
        //todo: ������ �� �������������?
        scoreCollector = GetComponentInParent<IScoreCollector>();
    }

    // ���������� ����� �� ������� �������� ����������� ���������� ����� ������� ������ �� ����������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScore score))
        {
            scoreCollector.Add(score.Value);
            Debug.Log($"��������� �����: {score.Value}");
        }
    }
}
