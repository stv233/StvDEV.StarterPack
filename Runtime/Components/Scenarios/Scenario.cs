using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.Scenarios
{
    /// <summary>
    /// Events Scenario Component.
    /// </summary>
    [AddComponentMenu("StvDEV/Scenarios/Scenario")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/Scenarios/Scenario/index.html")]
    public class Scenario : MonoBehaviour
    {
        [Serializable]
        private struct Scene
        {
            [Header("Settings")]
            [Tooltip("Delay before starting the scene.")]
            [SerializeField] private float _startDelay;

            [Header("Events")]
            [Tooltip("Scene event.")]
            [SerializeField] private UnityEvent _scene;

            /// <summary>
            /// Gets the value of the start delay.
            /// </summary>
            public float StartDelay => _startDelay; 

            /// <summary>
            /// Plays this instance.
            /// </summary>
            public void Play()
            {
                _scene?.Invoke();
            }
        }

        [Header("Settings")]
        [Tooltip("Run the scenario at startup.")]
        [SerializeField] private bool _onStart;

        [Header("Scenary")]
        [Tooltip("Scenes list.")]
        [SerializeField] private List<Scene> _scenes;

        [Header("Events")]
        [Tooltip("Occurs when the scenario is started.")]
        [SerializeField] private UnityEvent _scenarioStarted;

        [Tooltip("Occurs when the scenario is ended.")]
        [SerializeField] private UnityEvent _scenarioEnded;

        /// <summary>
        /// Occurs when the scenario is started.
        /// </summary>
        public UnityEvent ScenarioStarted => _scenarioStarted;

        /// <summary>
        /// Occurs when the scenario is ended.
        /// </summary>
        public UnityEvent ScenarioEnded => _scenarioEnded;

        /// <summary>
        /// Starts this instance
        /// </summary>
        private void Start()
        {
            if (_onStart)
            {
                Execute();
            }
        }

        /// <summary>
        /// Execute scenario.
        /// </summary>
        public void Execute()
        {
            PlayScene(0);
            _scenarioStarted?.Invoke();
        }

        /// <summary>
        /// Play scenario scene.
        /// </summary>
        /// <param name="scene">Scene id</param>
        public void PlayScene(int scene)
        {
            if (scene < _scenes.Count)
            {
                Scene currentScene = _scenes[scene];
                StartCoroutine(StartNext(currentScene.StartDelay));
                IEnumerator StartNext(float delay)
                {
                    yield return new WaitForSeconds(delay);
                    currentScene.Play();
                    PlayScene(scene + 1);
                }
            }
            else
            {
                Stop();
            }
        }

        /// <summary>
        /// Stop scenario.
        /// </summary>
        public void Stop()
        {
            StopAllCoroutines();
            _scenarioEnded?.Invoke();
        }
    }
}
