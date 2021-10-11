using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IPosition))]
public class Position2DTracker : MonoBehaviourExt
{
    [SerializeField] private InterfaceComponent<IPositionAccessorNotifier> _targetComponent;
    [SerializeField] private bool _trackX = true;
    [SerializeField] private bool _trackY = true;
    private IPosition _position;
    private Vector2 _offset;

    protected override void AwakeExt()
    {
        _position = GetInitedComponent<IPosition>();
        _offset = _position.Value - Target.Value;
        SubscribeEvents();
    }

    protected override void OnDestroyExt()
    {
        UnsubscribeEvents();
    }

    private IPositionAccessorNotifier Target => _targetComponent.Implementation;

    private void SubscribeEvents()
    {
        Target.OnChanged += Move;
    }

    private void UnsubscribeEvents()
    {
        Target.OnChanged -= Move;
    }

    private void Move()
    {
        Vector2 target = Target.Value;

        float x = _trackX
            ? target.x
            : 0;
        float y = _trackY
            ? target.y
            : 0;

        _position.MoveTo(new Vector2(x, y) + _offset);
    }
}
