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
        // При присвоении нового значения Screen.orientation не факт, что оно сразу же изменится - поэтому проверка об изменении производится в Update
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
