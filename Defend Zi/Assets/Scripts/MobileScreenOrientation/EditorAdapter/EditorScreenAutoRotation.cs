using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class EditorScreenAutoRotation : ScreenOrientationAdapter
{
    private readonly ScreenOrientationWrap _screenOrientation;

    public EditorScreenAutoRotation(MonoBehaviourExt mono, ScreenOrientationWrap screenOrientationWrap) : base(mono)
    {
        _screenOrientation = screenOrientationWrap != null
            ? screenOrientationWrap
            : throw new ArgumentNullException(nameof(screenOrientationWrap));
    }
}
