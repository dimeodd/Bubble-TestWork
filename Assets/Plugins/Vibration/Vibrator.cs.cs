using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public static class Vibrator
{
#if UNITY_ANDROID && !UNITY_EDITOR
    static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    static AndroidJavaClass unityPlayer;
    static AndroidJavaObject currentActivity;
    static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long ms = 250)
    {
#if UNITY_ANDROID
        vibrator?.Call("vibrate", ms);
#endif
    }

#if UNITY_ANDROID
    public const bool isAndroid = true;
#else
    public const bool isAndroid = false;
#endif

}
