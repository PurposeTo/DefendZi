using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class TwoSquares : Chunk
{
    [SerializeField, NotNull] private GameObject _squadFirst;
    [SerializeField, NotNull] private GameObject _squadSecond;

    private readonly float[] hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private readonly BestRotationEulers bestRotationEulers = new BestRotationEulers();

    private void Start()
    {
        OnSpawn();
    }

    private void OnSpawn()
    {
        //todo: Высота. Выбрать два случайных НЕ рядом стоящих числа. 

        _squadFirst.transform
            .SetPositionOy(GetRandomHight())
            .SetRotation(Quaternion.Euler(0f, 0f, bestRotationEulers.GetRandom()));

        _squadSecond.transform
            .SetPositionOy(GetRandomHight())
            .SetRotation(Quaternion.Euler(0f, 0f, bestRotationEulers.GetRandom()));
    }

    private float GetRandomHight() => hights[Random.Range(0, hights.Length)];
}
