using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

/// <summary>
/// Ориентация экрана в эдиторе.
/// Вычисляется один раз, при создании экземпляра класса.
/// </summary>
public class EditorScreenOrientation : MonoBehaviourExtContainer, IScreenOrientation
{
    private ScreenOrientation _current;

    public EditorScreenOrientation(MonoBehaviourExt mono) : base(mono)
    {
        // Нужен доступ к ширине и высоте экрана в окне Game. Сейчас он осуществляется через доступ к первой попавшейся камере.
        Camera camera = Camera.main;
        if (camera == null)
        {
            Debug.LogWarning("Невозможно определить ориентацию экрана в UnityEditor при отсутствии камеры на сцене!");
            return;
        }

        _current = CalculateScreenOrientation(camera);
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
        return _current;
    }

    private void Set(ScreenOrientation orientation)
    {
        _current = orientation;
        OnChange?.Invoke(orientation);
    }

    private ScreenOrientation CalculateScreenOrientation(Camera camera)
    {
        return camera.pixelWidth > camera.pixelHeight
            ? ScreenOrientation.LandscapeLeft
            : ScreenOrientation.Portrait;
    }
}
