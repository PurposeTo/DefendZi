using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using Zenject;

/// <summary>
/// Занимается поворотом экрана и, в отличие от Unity.AutoRotation, учитывает настройки автопорота экрана на Android-устройстве пользователя.
/// При использовании этого класса нельзя использовать значение AutoRotation для параметра Default Orientation в Unity Player settings
/// </summary>
public class AndroidScreenAutoRotation : MonoBehaviourExt
{
    private readonly DeviceOrientation _defaultDeviceOrientation = DeviceOrientation.LandscapeLeft;
    private DeviceOrientation _pastDeviceOrientation;
    private ScreenOrientationWrap _screenOrientation;

    [Inject]
    private void Constructor(ScreenOrientationWrap screenOrientationWrap)
    {
        _screenOrientation = screenOrientationWrap ?? throw new ArgumentNullException(nameof(screenOrientationWrap));
    }

    protected override void AwakeExt()
    {
        _pastDeviceOrientation = _defaultDeviceOrientation;
    }

    private void Update()
    {
        SetOrientation();
    }

    private void SetOrientation()
    {
        DeviceOrientation nextDeviceOrientation = Input.deviceOrientation;

        if (Application.platform == RuntimePlatform.Android)
        {
            if (!AndroidScreenAutoRotationSetting.IsRotationAllowed()) return;
        }

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
