using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class BoxColliderAsRectangle2D : MonoBehaviourExt, IRectangleIn2DGetter
{
    private BoxCollider2D _boxCollider2D;

    float IRectangleGetter.Height => _boxCollider2D.size.y;

    float IRectangleGetter.Width => _boxCollider2D.size.x;

    Vector2 IPositionGetter.Value => transform.position;

    Vector2 IPivotOffset2DGetter.Value => _boxCollider2D.offset;

    float IRotationGetter.Angle => transform.rotation.eulerAngles.z;

    Quaternion IRotationGetter.Quaternion => transform.rotation;

    protected override void AwakeExt()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
