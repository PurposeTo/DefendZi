using System.Linq;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InitialRotationAngle : MonoBehaviourExt
{
    private readonly float eulerStep = 7.5f;

    protected override void Constructor()
    {
        IRotation rotation = new Rotation(GetComponent<Rigidbody2D>());
        InitRotations(rotation);
    }

    private void InitRotations(IRotation rotation)
    {
        float[] rotations = Enumerable.Range(0, (int)(360 / eulerStep))
            .Select(euler => euler * eulerStep)
            .ToArray();

        float angle = Random.Range(rotations[0], rotations[rotations.Length-1]);
        rotation.RotateTo(angle);
    }
}
