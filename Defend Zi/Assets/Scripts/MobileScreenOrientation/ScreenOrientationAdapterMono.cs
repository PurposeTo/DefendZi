using System;
using Desdiene.MonoBehaviourExtension;
using UnityEditor;
using UnityEngine;
using Zenject;

public class ScreenOrientationAdapterMono : MonoBehaviourExt
{
    private ScreenOrientationWrap _screenOrientation;
    private ScreenOrientationAdapter _screenOrientationAdapter;

    [Inject]
    private void Constructor(ScreenOrientationWrap screenOrientationWrap)
    {
        _screenOrientation = screenOrientationWrap != null
            ? screenOrientationWrap
            : throw new ArgumentNullException(nameof(screenOrientationWrap));
    }

    protected override void AwakeExt()
    {
        if (PlayerSettings.defaultInterfaceOrientation == UIOrientation.AutoRotation)
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            SetAutoRotation();
        }
    }

    private void SetAutoRotation()
    {
        if (Application.isEditor)
        {
            _screenOrientationAdapter = new EditorScreenAutoRotation(this, _screenOrientation);
        }
        else
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _screenOrientationAdapter = new AndroidScreenAutoRotation(this, _screenOrientation);
                    break;
                default:
                    break;
            }
        }
    }
}
