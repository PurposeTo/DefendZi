using UnityEngine;

public interface IRotationGetter
{
    float Angle { get; }
    Quaternion Quaternion { get; }
}
