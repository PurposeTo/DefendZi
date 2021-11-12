using System.Collections;
using System.Collections.Generic;
using Desdiene;
using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class UiRotation : MonoBehaviourExt
{
    [SerializeField] private TimeSpeed.ScalingType _scalingType;
    [SerializeField] private float _speed;
    private ITimeAccessorNotificator _time;
    private IRotation _rotation;

    [Inject]
    private void Constructor(ITime time)
    {
        _time = time;
    }

    protected override void AwakeExt()
    {
        var rectTransform = GetComponent<RectTransform>();
        _rotation = new RectTransform2DRotation(rectTransform);
    }

    private void Update()
    {
        bool isScaledTime = _scalingType == TimeSpeed.ScalingType.Scaled;

        if (_time.IsPause && isScaledTime) return;

        float deltaTime = GetDeltaTime(_scalingType);
        float speed = _speed * deltaTime;
        _rotation.RotateBy(Quaternion.AngleAxis(speed, Vector3.forward));
    }

    private float GetDeltaTime(TimeSpeed.ScalingType type)
    {
        switch (type)
        {
            case TimeSpeed.ScalingType.Scaled:
                return Time.deltaTime;
            case TimeSpeed.ScalingType.RealTime:
                return Time.unscaledDeltaTime;
            default:
                return 0;
        }
    }
}
