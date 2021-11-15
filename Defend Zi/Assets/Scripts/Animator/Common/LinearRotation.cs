using Desdiene.MonoBehaviourExtension;
using Desdiene.TimeControls;
using UnityEngine;
using Zenject;

[RequireComponent((typeof(IRotation)))]
public class LinearRotation : MonoBehaviourExt
{
    [SerializeField] private UpdateActionType.Mode _updateActionType;
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
        _rotation = new Transform2DRotation(transform);
    }

    private void Update()
    {
        bool isScaledTime = _updateActionType == UpdateActionType.Mode.Update;

        if (_time.IsPause && isScaledTime) return;

        float deltaTime = GetDeltaTime(_updateActionType);
        float speed = _speed * deltaTime;
        _rotation.RotateBy(Quaternion.AngleAxis(speed, Vector3.forward));
    }

    // todo заменить на правильное обращение к объектам Desdiene Update/FixedUpdate
    private float GetDeltaTime(UpdateActionType.Mode type)
    {
        switch (type)
        {
            case UpdateActionType.Mode.Update:
                return Time.deltaTime;
            case UpdateActionType.Mode.UpdateRealTime:
                return Time.unscaledDeltaTime;
            default:
                return Time.deltaTime;
        }
    }
}
