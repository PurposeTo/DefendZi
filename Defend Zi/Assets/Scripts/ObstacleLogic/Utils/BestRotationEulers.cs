using System.Linq;
using Desdiene.Randoms;

public class BestRotationEulers
{
    private readonly float[] _rotations;

    public BestRotationEulers() : this(7.5f) { }

    public BestRotationEulers(float eulerStep)
    {
        _rotations = Enumerable.Range(0, (int)(360 / eulerStep))
            .Select(euler => euler * eulerStep)
            .ToArray();
    }

    public float[] GetAll() => _rotations;

    public float GetRandom() => Randomizer.GetRandomItem(_rotations);
}
