using Desdiene.Extensions.UnityEngine;
using UnityEngine;

public class TwoSquads : Chunk
{
    [SerializeField, NotNull] private GameObject _squadFirst;
    [SerializeField, NotNull] private GameObject _squadSecond;

    private readonly float[] _hights = { -7, -4.5f, -3.75f, -3, -2, -1, 0, 1, 2, 3, 3.75f, 4.5f, 7 };
    private readonly BestRotationEulers _bestRotationEulers = new BestRotationEulers();

    private void Start()
    {
        OnSpawn();
    }

    private void OnSpawn()
    {
        float[] hights = new TwoNonAdjacentNumbers().Get(_hights);

        _squadFirst.transform
            .SetPositionOy(GetRandomHight())
            .SetRotation(Quaternion.Euler(0f, 0f, _bestRotationEulers.GetRandom()));

        _squadSecond.transform
            .SetPositionOy(GetRandomHight())
            .SetRotation(Quaternion.Euler(0f, 0f, _bestRotationEulers.GetRandom()));
    }

    private float GetRandomHight() => _hights[Random.Range(0, _hights.Length)];
}
