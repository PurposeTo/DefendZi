using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent((typeof(IRotation)))]
public class RandomInitRotation2D : MonoBehaviourExt
{
    [SerializeField] private int _from;
    [SerializeField] private int _to;
    private IRotation _rotation;

    protected override void AwakeExt()
    {
        _rotation = GetComponent<IRotation>();
        int randomEuler = Random.Range(_from, _to);
        _rotation.RotateTo(Quaternion.AngleAxis(randomEuler, Vector3.forward));
    }
}
