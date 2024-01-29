using StvDEV.Extensions;
using StvDEV.Inspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components
{
    /// <summary>
    /// Component to handle command line arguments.
    /// </summary>
    [AddComponentMenu("StvDEV/Basic/Arguments Handler")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/ArgumentsHandler/index.html")]
    public class ArgumentsHandler : MonoBehaviour
    {
        /// <summary>
        /// Command line argument handler.
        /// </summary>
        [Serializable]
        public struct ArgumentHandler
        {
            [Header("Argument")]
            [Tooltip("Command line argument.")]
            [SerializeField] private string _argument;

            [Header("Handler")]
            [Tooltip("Argument handler.")]
            [SerializeField] private UnityEvent<string> _handler;

            /// <summary>
            /// Command line argument.
            /// </summary>
            public string Argument => _argument;

            /// <summary>
            /// Argument handler.
            /// </summary>
            public UnityEvent<string> Handler => _handler;

            /// <summary>
            /// Invoke handler.
            /// </summary>
            /// <param name="parameter">Argument parameter</param>
            public void Handle(string parameter)
            {
                _handler?.Invoke(parameter);
            }

            /// <summary>
            /// Command line argument handler.
            /// </summary>
            /// <param name="argument">Command line argument</param>
            /// <param name="handler">Argument handler</param>
            public ArgumentHandler(string argument, UnityAction<string> handler)
            {
                _argument = argument;
                _handler = new UnityEvent<string>();
                _handler.AddListener(handler);
            }

        }

        [Header("Settings")]
        [Tooltip("When handle command line arguments.")]
        [Enum("Manual Only", "Awake", "Start", "Awake And Start")]
        [SerializeField] private int _handleOn;

        [Header("Handles")]
        [Tooltip("Command line arguments handlers.")]
        [SerializeField] private List<ArgumentHandler> _handlers;

#if UNITY_EDITOR
        [Header("Debug (Editor Only)")]
        [CaptionedBool("Yes", "No"), Tooltip("Simulate command line arguments input.")]
        [SerializeField] private bool _simulateArguments;
        [ShowIf("_simulateArguments"), Tooltip("Simulated command line arguments.")]
        [SerializeField] private string _simulatedArguments;
#endif

        private void Awake()
        {
            if (_handleOn == 1 || _handleOn == 3)
            {
                Handle();
            }
        }

        private void Start()
        {
            if (_handleOn >= 2)
            {
                Handle();
            }
        }

        /// <summary>
        /// Handle command line arguments.
        /// </summary>
        public void Handle()
        {
            string[] arguments = Environment.GetCommandLineArgs();

#if UNITY_EDITOR
            if (_simulateArguments)
            {
                arguments = _simulatedArguments.Split(" ");
            }
#endif

            for (var i = 0; i < arguments.Length; i++)
            {
                string argument = arguments[i];
                if (argument.StartsWith("-"))
                {
                    string parameter = string.Empty;
                    if (i + 1 < arguments.Length && !arguments[i + 1].StartsWith("-"))
                    {
                        parameter = arguments[i + 1];
                    }
                    _handlers.Where(x => x.Argument == argument).ForEach(x => x.Handle(parameter));
                }
            }
        }

    }
}
