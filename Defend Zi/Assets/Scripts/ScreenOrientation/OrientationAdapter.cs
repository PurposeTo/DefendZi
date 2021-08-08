using System.Collections;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Coroutine;
using Desdiene;
using UnityEngine;

/// <summary>
/// Изменяет ориентацию экрана, вращает камеру и отдаляет её.
/// </summary>
public abstract class OrientationAdapter : MonoBehaviourExt
{
    [SerializeField, NotNull] private Camera _camera;
    private float _aspectRatio;

    protected Camera Camera => _camera;
    protected float AspectRatio => _aspectRatio;

    private ScreenOrientation DefaultScreenOrientation => ScreenOrientation.LandscapeLeft;

    protected override void AwakeExt()
    {
        _aspectRatio = GetAspectRatio();
        new CoroutineWrap(this).StartContinuously(SetOrientation());
    }

    protected abstract void ChangeVisionToLandscape();
    protected abstract void ChangeVisionToPortrait();

    private float GetAspectRatio()
    {
        float width = Camera.pixelWidth;
        float hight = Camera.pixelHeight;

        Math.Compare(ref hight, ref width);
        return width / hight;
    }

    private void AdjustCameraToLandscape()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        ChangeVisionToLandscape();
    }

    private void AdjustCameraToPortrait()
    {
        _camera.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
        ChangeVisionToPortrait();
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

    private void SetPrevious(DeviceOrientation previousOrientation)
    {
        switch (previousOrientation)
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

        //    // Переход в FaceUp и FaceDown возможен только из вышеперечисленных состояний.
        //    // Возможно ли попасть из FaceUp в FaceDown или наоборот ?
        //    default:
        //        SetLandscapeLeft();
        //        break;
        }
    }

    private IEnumerator SetOrientation()
    {
        // Брать фактическое значение во избежании поворота при перезагрузке сцены.
        DeviceOrientation previousOrientation = DeviceOrientation.LandscapeLeft;
        DeviceOrientation currentOrientation;

        while (true)
        {
            currentOrientation = Input.deviceOrientation;

            // Unity-приложение не учитывает настройки поворота экрана, установленные пользователем, на Android-устройстве.
            if (Application.platform == RuntimePlatform.Android)
            {
                // if (!IsRotationAllowed() currentOrientation = DefaultScreenOrientation;
            }

            if (currentOrientation != previousOrientation)
            {
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
                    case DeviceOrientation.FaceUp:
                        SetPrevious(previousOrientation);
                        break;
                    case DeviceOrientation.FaceDown:
                        SetPrevious(previousOrientation);
                        break;
                    default:
                        SetLandscapeLeft();
                        break;
                }

                previousOrientation = currentOrientation;
            }

            yield return null;
        }
    }
}
