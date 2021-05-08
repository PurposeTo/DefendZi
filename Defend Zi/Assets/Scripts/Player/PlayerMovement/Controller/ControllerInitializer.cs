using UnityEngine;

public static class ControllerInitializer
{
    public static Controller Initialize()
    {
        return Initialize(Application.platform);
    }

    public static Controller Initialize(RuntimePlatform currentRuntimePlatform)
    {
        Debug.Log($"InitializeController with {currentRuntimePlatform}!");

        switch (currentRuntimePlatform)
        {
            case RuntimePlatform.Android:
                return new MobileController();
            case RuntimePlatform.WindowsEditor:
                return new EditorController();
            default:
                Debug.LogError($"{currentRuntimePlatform} is unknown platform!");
                return new EditorController();
        }
    }
}
