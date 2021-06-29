using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class TwoSquares : Chunk
{
    [SerializeField, NotNull] private GameObject _squadFirst;
    [SerializeField, NotNull] private GameObject _squadSecond;

    private readonly float[] _hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private readonly BestRotationEulers _bestRotationEulers = new BestRotationEulers();

    protected override void OnSpawn()
    {
        new TwoNonAdjacentNumbers().Get(_hights, out float firstHeight, out float secondHeight);

        _squadFirst.transform
            .SetPositionOy(firstHeight)
            .SetRotation(Quaternion.Euler(0f, 0f, _bestRotationEulers.GetRandom()));

        _squadSecond.transform
            .SetPositionOy(secondHeight)
            .SetRotation(Quaternion.Euler(0f, 0f, _bestRotationEulers.GetRandom()));
    }
}
