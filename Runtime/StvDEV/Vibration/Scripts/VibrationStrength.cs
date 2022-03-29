
namespace StvDEV.Vibration
{
    /// <summary>
    /// Vibration strength.
    /// </summary>
    public enum VibrationStrength : int
    {
        /// <summary>
        /// Low vibration (Amplitude = 63).
        /// </summary>
        Low = 63,

        /// <summary>
        /// Medium vibration (Amplitude = 127).
        /// </summary>
        Medium = 127,

        /// <summary>
        /// Hight vibration (Amplitude = 255).
        /// </summary>
        High = 255,

        /// <summary>
        /// Default vibration strength.
        /// </summary>
        Default = -1,
    }
}