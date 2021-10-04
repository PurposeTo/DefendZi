using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.Rectangles;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class BoxColliderAsRectangle2D : MonoBehaviourExt, IRectangleIn2DAccessor
{
    private BoxCollider2D _boxCollider2D;

    float IRectangleAccessor.Height => _boxCollider2D.size.y;

    float IRectangleAccessor.Width => _boxCollider2D.size.x;

    Vector2 IPositionAccessor.Value => transform.position;

    Vector2 IPivotOffset2DAccessor.Value => _boxCollider2D.offset;

    float IRotationAccessor.Angle => transform.rotation.eulerAngles.z;

    Quaternion IRotationAccessor.Quaternion => transform.rotation;

    protected override void AwakeExt()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
