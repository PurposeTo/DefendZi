using System.Linq;
using UnityEngine;

public class BestRotationEulers
{
    private readonly float[] rotations;

    public BestRotationEulers() : this(7.5f) { }

    public BestRotationEulers(float eulerStep)
    {
        rotations = Enumerable.Range(0, (int)(360 / eulerStep))
            .Select(euler => euler * eulerStep)
            .ToArray();
    }

    public float[] GetAll() => rotations;

    public float GetRandom() => rotations[Random.Range(0, rotations.Length)];
}
