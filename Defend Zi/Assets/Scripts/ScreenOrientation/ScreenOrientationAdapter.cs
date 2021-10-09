using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

public class ScreenOrientationAdapter : MonoBehaviourExt
{
    private readonly DeviceOrientation _defaultDeviceOrientation = DeviceOrientation.LandscapeLeft;
    private ScreenOrientationWrap _screenOrientation;

    [Inject]
    private void Constructor(ScreenOrientationWrap screenOrientationWrap)
    {
        _screenOrientation = screenOrientationWrap ?? throw new ArgumentNullException(nameof(screenOrientationWrap));
    }

    protected override void AwakeExt()
    {
        ICoroutine routine = new CoroutineWrap(this);
        routine.StartContinuously(SetOrientation());
    }

    private IEnumerator SetOrientation()
    {
        DeviceOrientation pastDeviceOrientation = _defaultDeviceOrientation;
        DeviceOrientation nextDeviceOrientation;

        while (true)
        {
            nextDeviceOrientation = Input.deviceOrientation;

            // Unity-приложение не учитывает настройки поворота экрана, установленные пользователем, на Android-устройстве.
            if (Application.platform == RuntimePlatform.Android)
            {
                if (!AndroidScreenRotation.IsRotationAllowed())
                {
                    nextDeviceOrientation = pastDeviceOrientation;
                }
            }

            if (nextDeviceOrientation != pastDeviceOrientation)
            {
                switch (nextDeviceOrientation)
                {
                    case DeviceOrientation.FaceUp:
                        break;
                    case DeviceOrientation.FaceDown:
                        break;
                    case DeviceOrientation.Portrait:
                    case DeviceOrientation.PortraitUpsideDown:
                    case DeviceOrientation.LandscapeLeft:
                    case DeviceOrientation.LandscapeRight:
                        var orientation = (ScreenOrientation)Enum.Parse(typeof(ScreenOrientation), nextDeviceOrientation.ToString());
                        _screenOrientation.Set(orientation);
                        break;
                }

                pastDeviceOrientation = nextDeviceOrientation;
            }

            yield return null;
        }
    }
}
