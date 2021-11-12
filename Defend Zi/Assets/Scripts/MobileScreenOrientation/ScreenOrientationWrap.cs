using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class ScreenOrientationWrap : MonoBehaviourExt
{
    private ScreenOrientation _previous;
    private string _orientationDebug;

    protected override void AwakeExt()
    {
        _orientationDebug = Screen.orientation.ToString();
        OnChange += (orientation) => _orientationDebug = orientation.ToString();
    }

    private void Update()
    {
        // todo переработать
        if (Application.isEditor) return;

        Check(Get());
    }

    public event Action<ScreenOrientation> OnChange;

    public ScreenOrientation Get()
    {
        // todo переработать
        if (Application.isEditor) return ScreenOrientation.Landscape;

        return Screen.orientation;
    }

    public void Set(ScreenOrientation orientation)
    {
        // При присвоении нового значения Screen.orientation не факт, что оно сразу же изменится - поэтому проверка об изменении производится в Update
        Screen.orientation = orientation;
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
