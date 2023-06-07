using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.Scenarios
{
    /// <summary>
    /// Character Responses Component.
    /// </summary>
    [AddComponentMenu("StvDEV/Scenarios/Character Responses")]
    public class CharacterResponses : MonoBehaviour
    {
        /// <summary>
        /// The response
        /// </summary>
        [Serializable]
        private struct Response
        {

            [Header("Settings")]
            [Tooltip("Response ID.")]
            [SerializeField] private string _id;

            [Tooltip("Response duration.")]
            [SerializeField] private float _duration;

            [Header("Content")]
            [Multiline(3), Tooltip("Response text.")]
            [SerializeField] private string _text;

            [Tooltip("Response audioclip.")]
            [SerializeField] private AudioClip _clip;

            /// <summary>
            /// Response ID.
            /// </summary>
            public string Id => _id;

            /// <summary>
            /// Response duration.
            /// </summary>
            public float Duration => _duration;

            /// <summary>
            /// Response text.
            /// </summary>
            public string Text => _text;

            /// <summary>
            /// Response audioclip.
            /// </summary>
            public AudioClip Clip => _clip;
        }

        [Header("Responses")]
        [Tooltip("Responses list.")]
        [SerializeField] private List<Response> _responses;

        [Header("Links")]
        [Tooltip("Subtitles text component.")]
        [SerializeField] private TMP_Text _subtitlesText;
        [Tooltip("Responses audio source.")]
        [SerializeField] private AudioSource _audioSource;

        /// <summary>
        /// Show character response.
        /// </summary>
        /// <param name="responseId">Relica id</param>
        public void Show(string responseId)
        {
            Hide();
            Response response = _responses.Where(x => x.Id == responseId).FirstOrDefault();
            if (!string.IsNullOrEmpty(response.Id))
            {
                if (_subtitlesText)
                {
                    _subtitlesText?.SetText(response.Text);
                    _subtitlesText?.gameObject.SetActive(true);
                }
                if (response.Clip)
                {
                    if (_audioSource)
                    {
                        _audioSource?.PlayOneShot(response.Clip);
                    }
                }
                StartCoroutine(HideDelay(response.Duration));
                IEnumerator HideDelay(float delay)
                {
                    yield return new WaitForSeconds(delay);
                    Hide();
                }
            }
        }

        /// <summary>
        /// Hide character response.
        /// </summary>
        public void Hide()
        {
            StopAllCoroutines();
            if (_subtitlesText)
            {
                _subtitlesText?.gameObject.SetActive(false);
            }
            if (_audioSource)
            {
                _audioSource?.Stop();
            }
        }
    }
}
