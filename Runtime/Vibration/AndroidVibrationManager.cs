using UnityEngine;

namespace StvDEV.Vibration
{
    /// <summary>
    /// Performs Android device vibration.
    /// </summary>
    public static class AndroidVibrationManager
    {
        private const int DEFAULT_AMPLITUDE = -1;

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaClass s_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        private static AndroidJavaObject s_currentActivity = s_unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        private static AndroidJavaObject s_vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    	private static AndroidJavaClass s_vibrationEffectClass;
		private static AndroidJavaObject s_vibrationEffect;
#else

        private static AndroidJavaClass s_unityPlayer;
        private static AndroidJavaObject s_currentActivity;
        private static AndroidJavaObject s_vibrator = null;
        private static AndroidJavaClass s_vibrationEffectClass;
        private static AndroidJavaObject s_vibrationEffect;
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
        /// Vibrates an Android device for the specified length of time.
        /// </summary>
        /// <param name="length">Vibration length</param>
        public static void Vibrate(VibrationLength length)
        {
            Vibrate((long)length);
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
        /// <param name="milliseconds">Vibration length</param>
        /// <param name="amplitude">Amplitude (0-1)</param>
        public static void Vibrate(long milliseconds, float amplitude)
        {
            Vibrate(milliseconds, (int)(255 * amplitude));
        }

        /// <summary>
        /// Vibrates an Android device for the specified length of time, with a given strength.
        /// </summary>
        /// <param name="length">Vibration length</param>
        /// <param name="strength">Vibration strength</param>
        public static void Vibrate(VibrationLength length, VibrationStrength strength)
        {
            Vibrate((long)length, (int)strength);
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
                    if (s_vibrationEffectClass == null)
                    {
                        s_vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                    }

                    s_vibrationEffect = s_vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", new object[] { milliseconds, amplitude });
                    s_vibrator.Call("vibrate", s_vibrationEffect);
                }
                else
                {
                    s_vibrator.Call("vibrate", milliseconds);
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
                s_vibrator.Call("vibrate", pattern, repeat);
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
                s_vibrator.Call("cancel");
            }
        }

        /// <summary>
        /// Returns a boolean value of whether the device is Android or not.
        /// </summary>
        /// <returns>Is android</returns>
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
        /// <returns>SDK level</returns>
        public static int GetSDKLevel()
        {
            var @class = AndroidJNI.FindClass("android.os.Build$VERSION");
            var fieldID = AndroidJNI.GetStaticFieldID(@class, "SDK_INT", "I");
            var sdkLevel = AndroidJNI.GetStaticIntField(@class, fieldID);
            return sdkLevel;
        }
    }
}
