using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScreenOrientationMono : MonoBehaviourExt, IScreenOrientation
{
    private ScreenOrientation _orientationDebug;
    private IScreenOrientation _screenOrientation;

    protected override void AwakeExt()
    {
        if (Application.isEditor)
        {
            _screenOrientation = new EditorScreenOrientation(this);
        }
        else
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _screenOrientation = new AndroidSreenOrientation(this);
                    break;
                default:
                    _screenOrientation = new EditorScreenOrientation(this);
                    break;
            }
        }

        _orientationDebug = _screenOrientation.Get();
        _screenOrientation.OnChange += (orientation) => _orientationDebug = orientation;
    }

    event Action<ScreenOrientation> IScreenOrientation.OnChange
    {
        add => _screenOrientation.OnChange += value;
        remove => _screenOrientation.OnChange -= value;
    }

    ScreenOrientation IScreenOrientation.Get() => _screenOrientation.Get();
}
