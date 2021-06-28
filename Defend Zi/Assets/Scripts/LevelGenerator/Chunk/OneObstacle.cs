using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class OneObstacle : Chunk
{
    [SerializeField, NotNull] private GameObject _obstacle;

    private readonly float[] hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private readonly BestRotationEulers bestRotationEulers = new BestRotationEulers();

    private void Start()
    {
        OnSpawn();
    }

    private void OnSpawn()
    {
        _obstacle.transform
            .SetPositionOy(GetRandomHight())
            .SetRotation(Quaternion.Euler(0f, 0f, bestRotationEulers.GetRandom()));
    }

    private float GetRandomHight() => hights[Random.Range(0, hights.Length)];
}
