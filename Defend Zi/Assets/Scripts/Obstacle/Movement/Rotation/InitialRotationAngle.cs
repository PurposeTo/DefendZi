using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class InitialRotationAngle : MonoBehaviourExt
{
    [SerializeField] private IRotationComponent _rotation;

    private IRotation Rotation => _rotation.Implementation;
    private readonly float eulerStep = 7.5f;

    protected override void AwakeExt()
    {
        InitRotations();
    }

    private void InitRotations()
    {
        float angle = new BestRotationEulers(eulerStep).GetRandom();
        var quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        Rotation.RotateTo(quaternion);
    }
}
