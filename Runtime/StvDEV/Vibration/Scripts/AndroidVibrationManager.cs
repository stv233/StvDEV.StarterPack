using UnityEngine;

namespace StvDEV.Vibration
{
    /// <summary>
    /// Performs Android device vibration
    /// </summary>
    public static class AndroidVibrationManager
    {
        private const int DEFAULT_AMPLITUDE = -1;

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        private static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    	private static AndroidJavaClass vibrationEffectClass;
		private static AndroidJavaObject vibrationEffect;
#else
        private static AndroidJavaClass unityPlayer;
        private static AndroidJavaObject currentActivity;
        private static AndroidJavaObject vibrator = null;
        private static AndroidJavaClass vibrationEffectClass;
        private static AndroidJavaObject vibrationEffect;
#endif

        /// <summary>
        /// Vibrates an Android device for the specified length of time in seconds.
        /// </summary>
        /// <param name="seconds">Vibration length</param>
        public static void Vibrate(float seconds)
        {
            Vibrate((long)(seconds * 1000));
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time in millisecond.
        /// </summary>
        /// <param name="milliseconds">Vibration length</param>
        public static void Vibrate(long milliseconds)
        {
            Vibrate(milliseconds, DEFAULT_AMPLITUDE);
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time in seconds, with a given amplitude.
        /// </summary>
        /// <param name="seconds">Vibration length</param>
        /// <param name="amplitude">Amplitude (1-255)</param>
        public static void Vibrate(float seconds, int amplitude)
        {
            Vibrate((long)seconds * 1000, amplitude);
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time in seconds, with a given amplitude.
        /// </summary>
        /// <param name="seconds">Vibration length</param>
        /// <param name="amplitude">Amplitude (0-1)</param>
        public static void Vibrate(float seconds, float amplitude)
        {
            Vibrate((long)seconds * 1000, amplitude);
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time in milliseconds, with a given amplitude.
        /// </summary>
        /// <param name="seconds">Vibration length</param>
        /// <param name="amplitude">Amplitude (0-1)</param>
        public static void Vibrate(long milliseconds, float amplitude)
        {
            Vibrate(milliseconds, (int)(255 * amplitude));
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time in millisecond, with a given amplitude.
        /// </summary>
        /// <param name="milliseconds">Vibration length</param>
        /// <param name="amplitude">Amplitude (1-255)</param>
        public static void Vibrate(long milliseconds, int amplitude)
        {
            if (IsAndroid())
            {
                if (GetSDKLevel() >= 26)
                {
                    if (vibrationEffectClass == null)
                    {
                        vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                    }

                    vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", new object[] { milliseconds, amplitude });
                    vibrator.Call("vibrate", vibrator);
                }
                else
                {
                    vibrator.Call("vibrate", milliseconds);
                }
            }
            else
            {
                Handheld.Vibrate();
            }
        }

        /// <summary>
        /// Vibrates an Android device for the specified pattern and number of repetitions.
        /// </summary>
        /// <param name="pattern">Pattern</param>
        /// <param name="repeat">Number of repetitions</param>
        public static void Vibrate(long[] pattern, int repeat)
        {
            if (IsAndroid())
            {
                vibrator.Call("vibrate", pattern, repeat);
            }
            else
            {
                Handheld.Vibrate();
            }
        }

        /// <summary>
        /// Cancels any active vibration.
        /// </summary>
        public static void Cancel()
        {
            if (IsAndroid())
            {
                vibrator.Call("cancel");
            }
        }

        /// <summary>
        /// Returns a boolean value of whether the device is Android or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
	        return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// Returns an int value that is the Android SDK level.
        /// </summary>
        /// <returns></returns>
        public static int GetSDKLevel()
        {
            var @class = AndroidJNI.FindClass("android.os.Build$VERSION");
            var fieldID = AndroidJNI.GetStaticFieldID(@class, "SDK_INT", "I");
            var sdkLevel = AndroidJNI.GetStaticIntField(@class, fieldID);
            return sdkLevel;
        }
    }
}
