using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent((typeof(Transform2DRotationMono)))]
public class RandomRotation : MonoBehaviourExt
{
    [SerializeField] private int _from;
    [SerializeField] private int _to;
    private IRotation _rotation;

    protected override void AwakeExt()
    {
        _rotation = GetComponent<Transform2DRotationMono>();
        int randomEuler = Random.Range(_from, _to);
        _rotation.RotateTo(Quaternion.AngleAxis(randomEuler, Vector3.forward));
    }
}
