using System;
using UnityEngine;

public static class AndroidScreenAutoRotationSetting
{
    public static bool IsRotationAllowed()
    {
        try
        {
            using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            using var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            using var systemSettings = new AndroidJavaClass("android.provider.Settings$System");
            int rotation = systemSettings.CallStatic<int>("getInt", activity.Call<AndroidJavaObject>("getContentResolver"), "accelerometer_rotation");
            return rotation == 1;
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.ToString());
            return false;
        }
    }
}
