using UnityEngine;

public interface IRotationAccessor
{
    float Angle { get; }
    Quaternion Quaternion { get; }
}
