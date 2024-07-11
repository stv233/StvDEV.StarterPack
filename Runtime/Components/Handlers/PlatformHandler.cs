using StvDEV.Inspector;
using StvDEV.Patterns;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.Handlers
{
    /// <summary>
    /// Component for handle current platform.
    /// </summary>
    [AddComponentMenu("StvDEV/Basic/Handlers/Platform Handler")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/Handlers/PlatformHandler/index.html")]
    public class PlatformHandler : MonoBehaviourSingleton<PlatformHandler>
    {
        /// <summary>
        /// Runtime plaftorm type.
        /// </summary>
        public enum PlatformType
        {
            /// <summary>
            /// None.
            /// </summary>
            None = 0,

            /// <summary>
            /// Windows or Linux desktop.
            /// </summary>
            Desktop,

            /// <summary>
            /// Windows or Linux server.
            /// </summary>
            Server,

            /// <summary>
            /// Android.
            /// </summary>
            Android,

            /// <summary>
            /// IOS.
            /// </summary>
            IOS,
            
            /// <summary>
            /// WebGL.
            /// </summary>
            Web,
        }

        [Header("Debug Settings")]
        [CaptionedBool("Yes", "No"), Tooltip("Force some platform for debug")]
        [SerializeField] private bool _forcePlatform;
        [ShowIf(nameof(_forcePlatform)), CaptionedBool("Editor And Standalone", "Editor Only"), Tooltip("Force platform runtime")]
        [SerializeField] private bool _forcePlatformRuntime;
        [ShowIf(nameof(_forcePlatform)), Tooltip("Forced platform")]
        [SerializeField] private PlatformType _forcedPlatform;

        [Header("Runtime Settings")]
        [Enum("Manual Only", "Awake")]
        [SerializeField] private int _trigger;

        [Header("Events")]
        [Header("Universal")]
        [Tooltip("On platform detected (PlatformType)")]
        [SerializeField] private UnityEvent<PlatformType> _platformDetected;

        [Header("Platforms")]
        [Tooltip("On is desktop platform")]
        [SerializeField] private UnityEvent _isDesktop;
        [Tooltip("On is server platform")]
        [SerializeField] private UnityEvent _isServer;
        [Tooltip("On is android platform")]
        [SerializeField] private UnityEvent _isAndroid;
        [Tooltip("On is IOS platform")]
        [SerializeField] private UnityEvent _isIOS;
        [Tooltip("On is WebGL platform")]
        [SerializeField] private UnityEvent _isWeb;

        /// <summary>
        /// Current runtime platform.
        /// </summary>
        public static PlatformType RuntimePlatform => Instance.DetectPlatform();

        /// <summary>
        /// On platform detected (PlatformType)
        /// </summary>
        public static UnityEvent<PlatformType> PlatformDetected => Instance._platformDetected;

        protected override void AwakeSingletone()
        {
            if (_trigger == 1)
            {
                HandlePlatform();
            }
            base.AwakeSingletone();
        }

        /// <summary>
        /// Handle platform type.
        /// </summary>
        public static void HandlePlatform()
        {
            Instance.HandlePlatformType(Instance.DetectPlatform());
        }

        private PlatformType DetectPlatform()
        {
            if (_forcePlatform && (_forcePlatformRuntime || Application.isEditor))
            {
                return _forcedPlatform;
            }

            switch (Application.platform)
            {
                case UnityEngine.RuntimePlatform.WindowsPlayer:
                case UnityEngine.RuntimePlatform.WindowsEditor:
                case UnityEngine.RuntimePlatform.LinuxPlayer:
                case UnityEngine.RuntimePlatform.LinuxEditor:
                case UnityEngine.RuntimePlatform.OSXPlayer:
                case UnityEngine.RuntimePlatform.OSXEditor:
                    return PlatformType.Desktop;
                case UnityEngine.RuntimePlatform.WindowsServer:
                case UnityEngine.RuntimePlatform.LinuxServer:
                case UnityEngine.RuntimePlatform.OSXServer:
                    return PlatformType.Server;
                case UnityEngine.RuntimePlatform.Android:
                    return PlatformType.Android;
                case UnityEngine.RuntimePlatform.IPhonePlayer:
                    return PlatformType.Android;
                case UnityEngine.RuntimePlatform.WebGLPlayer:
                    return PlatformType.Web;
                default:
                    return PlatformType.None;
            }
        }

        private void HandlePlatformType(PlatformType type)
        {
            _platformDetected?.Invoke(type);
            switch(type)
            {
                case PlatformType.Desktop:
                    _isDesktop?.Invoke();
                    break;
                case PlatformType.Server:
                    _isServer?.Invoke();
                    break;
                case PlatformType.Android:
                    _isAndroid?.Invoke();
                    break;
                case PlatformType.IOS:
                    _isIOS?.Invoke();
                    break;
                case PlatformType.Web:
                    _isWeb?.Invoke();
                    break;
            }
        }
    }
}
