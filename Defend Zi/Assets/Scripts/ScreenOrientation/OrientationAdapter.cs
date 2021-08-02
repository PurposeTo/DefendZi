using System.Collections;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Coroutine;
using UnityEngine;

/// <summary>
/// Изменяет ориентацию экрана, вращает камеру, отдаляет и смещает её.
/// </summary>
public abstract class OrientationAdapter : MonoBehaviourExt
{
    [SerializeField, NotNull] protected Camera _camera;

    protected abstract float PortraitCameraSize { get; }
    protected abstract float LandscapeCameraSize { get; }

    private readonly float _portraitCameraOffset = 12f;
    private readonly float _landscapeCameraOffset = 8f;

    protected override void AwakeExt()
    {
        new CoroutineWrap(this).StartContinuously(SetOrientation());
    }

    protected abstract void ResizeCamera(float newSize);

    private void AdjustCameraToLandscape()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        ResizeCamera(LandscapeCameraSize);
        _camera.transform.position += Vector3.right * _landscapeCameraOffset;
    }

    private void AdjustCameraToPortrait()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
        ResizeCamera(PortraitCameraSize);
        _camera.transform.position += Vector3.right * _portraitCameraOffset;
    }

    private void SetPortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        AdjustCameraToPortrait();
    }

    private void SetPortraitUpsideDown()
    {
        Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        AdjustCameraToPortrait();
    }

    private void SetLandscapeLeft()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        AdjustCameraToLandscape();
    }

    private void SetLandscapeRight()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        AdjustCameraToLandscape();
    }

    private IEnumerator SetOrientation()
    {
        DeviceOrientation lastFrameOrientation = DeviceOrientation.LandscapeLeft;
        DeviceOrientation currentOrientation;

        while (true)
        {
            currentOrientation = Input.deviceOrientation;

            if (currentOrientation != lastFrameOrientation)
            {
                lastFrameOrientation = currentOrientation;
                Debug.Log($"Device orientation: {currentOrientation}.");
                Debug.Log($"Screen orientation: {Screen.orientation}.");

                switch (currentOrientation)
                {
                    case DeviceOrientation.Portrait:
                        SetPortrait();
                        break;
                    case DeviceOrientation.PortraitUpsideDown:
                        SetPortraitUpsideDown();
                        break;
                    case DeviceOrientation.LandscapeLeft:
                        SetLandscapeLeft();
                        break;
                    case DeviceOrientation.LandscapeRight:
                        SetLandscapeRight();
                        break;
                    default:
                        SetLandscapeLeft();
                        break;
                }
            }

            yield return null;
        }
    }
}
