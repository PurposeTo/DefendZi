using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScreenOrientationWrap : MonoBehaviourExt
{
    private ScreenOrientation _previous;

    public event Action<ScreenOrientation> OnChange;

    public ScreenOrientation Get() => Screen.orientation;

    public void Set(ScreenOrientation orientation)
    {
        Screen.orientation = orientation;
    }

    private void Update()
    {
        Check(Get());
    }

    private void Check(ScreenOrientation current)
    {
        if (_previous != current)
        {
            _previous = current;
            OnChange?.Invoke(current);
        }
    }
}
