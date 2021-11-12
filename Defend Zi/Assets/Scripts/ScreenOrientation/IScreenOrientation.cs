using System;
using UnityEngine;

public interface IScreenOrientation
{
    event Action<ScreenOrientation> OnChange;
    ScreenOrientation Get();
}
