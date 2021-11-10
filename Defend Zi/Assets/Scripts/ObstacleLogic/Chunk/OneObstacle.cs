using Desdiene.Extensions.UnityEngine;
using Desdiene.Randoms;
using UnityEngine;

public class OneObstacle : Chunk
{
    [SerializeField, NotNull] private GameObject _obstacle;

    private readonly float[] hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private readonly BestRotationEulers bestRotationEulers = new BestRotationEulers();

    protected override void OnSpawn()
    {
        _obstacle.transform
            .SetPositionOy(Randomizer.GetRandomItem(hights))
            .SetRotation(Quaternion.Euler(0f, 0f, bestRotationEulers.GetRandom()));
    }
}
