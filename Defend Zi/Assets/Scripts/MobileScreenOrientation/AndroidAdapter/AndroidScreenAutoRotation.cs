using System;
using System.Collections;
using Desdiene.Coroutines;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Занимается поворотом экрана и, в отличие от Unity.AutoRotation, учитывает настройки автопорота экрана на Android-устройстве пользователя.
/// При использовании этого класса нельзя использовать значение AutoRotation для параметра Default Orientation в Unity Player settings
/// </summary>
public class AndroidScreenAutoRotation : ScreenOrientationAdapter
{
    private readonly ScreenOrientationWrap _screenOrientation;
    private readonly ICoroutine _unityUpdate;
    private DeviceOrientation _pastDeviceOrientation;

    public AndroidScreenAutoRotation(MonoBehaviourExt mono, ScreenOrientationWrap screenOrientationWrap) : base(mono)
    {
        _screenOrientation = screenOrientationWrap != null
            ? screenOrientationWrap
            : throw new ArgumentNullException(nameof(screenOrientationWrap));

        _unityUpdate = new CoroutineWrap(MonoBehaviourExt);
        _unityUpdate.StartContinuously(Update());
    }

    private IEnumerator Update()
    {
        while (true)
        {
            SetOrientation();
            yield return null;
        }
    }

    private void SetOrientation()
    {
        DeviceOrientation nextDeviceOrientation = Input.deviceOrientation;

        if (!AndroidScreenAutoRotationSetting.IsRotationAllowed()) return;

        if (nextDeviceOrientation != _pastDeviceOrientation)
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

            _pastDeviceOrientation = nextDeviceOrientation;
        }
    }
}
