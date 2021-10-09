using UnityEngine;

/// <summary>
/// Регулирует видимость перспективной камеры
/// </summary>
public class PerspectiveCameraOrientationAdapter : CameraOrientationAdapter
{
    protected sealed override void ChangeVisionToLandscape()
    {
        var position = Camera.transform.position;
        Camera.transform.position = new Vector3(position.x, position.y, GetDistanceToPlain());
    }

    protected sealed override void ChangeVisionToPortrait()
    {
        var position = Camera.transform.position;
        Camera.transform.position = new Vector3(position.x, position.y, GetDistanceToPlain() * AspectRatio);
    }

    private float GetDistanceToPlain()
    {
        float oppositeSide = GameSpaceInSight.Height / 2f;
        float angle = Camera.fieldOfView / 2f;
        float adjacentSide = oppositeSide / Mathf.Tan(Mathf.Deg2Rad * angle);

        return -1f * adjacentSide;
    }
}
