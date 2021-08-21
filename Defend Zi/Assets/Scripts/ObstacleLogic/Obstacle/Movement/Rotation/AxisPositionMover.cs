using Desdiene.MonoBehaviourExtension;
using Desdiene.Random;
using UnityEngine;

public class AxisPositionMover : MonoBehaviourExt
{
    [SerializeField] private Transform _obstacleMono;
    [SerializeField] private Transform[] _offsets;

    protected override void AwakeExt()
    {
        InitPosition();
    }

    private void InitPosition()
    {
        Vector3 offset = _offsets.Length == 0
            ? transform.localPosition
            : Randomizer.GetRandomItem(_offsets).localPosition;

        //todo как правильно сместить ось?
    }
}
