using Desdiene.Types.RectangleAsset;
using UnityEngine;

public interface ITransform2DGetter
{
    Vector2 Position { get; }
    Quaternion Rotation { get; }
    Rectangle Size { get; }
    Vector2 Pivot { get; }
}
