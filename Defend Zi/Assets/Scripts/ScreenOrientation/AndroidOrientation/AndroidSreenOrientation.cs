using System;
using Desdiene.Containers;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class AndroidSreenOrientation : MonoBehaviourExtContainer, IScreenOrientation
{
    private readonly bool _isAutoRotationSupported = true;
    private readonly Update _update;
    private ScreenOrientation _pastScreenOrientation;
    private DeviceOrientation _pastDeviceOrientation;

    public AndroidSreenOrientation(MonoBehaviourExt mono) : base(mono)
    {
        _update = new Update(MonoBehaviourExt, () =>
        {
            if (_isAutoRotationSupported)
            {
                AutoRotateOrientation();
            }
            Check(Get());
        });
    }

    event Action<ScreenOrientation> IScreenOrientation.OnChange
    {
        add => OnChange += value;
        remove => OnChange -= value;
    }

    ScreenOrientation IScreenOrientation.Get() => Get();

    private event Action<ScreenOrientation> OnChange;

    private ScreenOrientation Get()
    {
        return Screen.orientation;
    }

    private void Set(ScreenOrientation orientation)
    {
        // ��� ���������� ������ �������� Screen.orientation �� ����, ��� ��� ����� �� ��������� - ������� �������� �� ��������� ������������ � Update
        Screen.orientation = orientation;
    }

    private void Check(ScreenOrientation current)
    {
        if (_pastScreenOrientation != current)
        {
            _pastScreenOrientation = current;
            OnChange?.Invoke(current);
        }
    }

    private void AutoRotateOrientation()
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
                    Set(orientation);
                    break;
            }

            _pastDeviceOrientation = nextDeviceOrientation;
        }
    }
}
