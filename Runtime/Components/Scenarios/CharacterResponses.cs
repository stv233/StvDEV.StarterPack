using StvDEV.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
            /// <summary>
            /// Response localization.
            /// </summary>
            [Serializable]
            public struct Localization
            {
                [Header("Language")]
                [Tooltip("Language identifier.")]
                [SerializeField] private string _language;

                [Header("Content")]
                [Multiline(3), Tooltip("Localized response text.")]
                [SerializeField] private string _text;

                [Tooltip("Localized response audioclip")]
                [SerializeField] private AudioClip _clip;

                /// <summary>
                /// Language identifier.
                /// </summary>
                public string Language => _language;
            
                /// <summary>
                /// Localized response text.
                /// </summary>
                public string Text => _text;

                /// <summary>
                /// Localized response audilclip.
                /// </summary>
                public AudioClip Clip => _clip;
            }

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

            [Header("Localization")]
            [Tooltip("Localized response variants.")]
            [SerializeField] private List<Localization> _localizations;

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

            /// <summary>
            /// Localized response variants.
            /// </summary>
            public Dictionary<string, Localization> Localizations => _localizations.ToDictionary(x => x.Language, x => x);
        }

        private static PrefsValue<string> _selectedLanguage = new PrefsValue<string>("StvDEV/Localization/Language", "RU-ru");

        /// <summary>
        /// Current language.
        /// </summary>
        public static string Language
        {
            get => _selectedLanguage.Value;
            set => _selectedLanguage.Value = value;
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
                string text = response.Text;
                AudioClip clip = response.Clip;

                if (response.Localizations.ContainsKey(Language))
                {
                    Response.Localization localization = response.Localizations[Language];
                    if (!string.IsNullOrEmpty(localization.Text))
                    {
                        text = localization.Text;
                    }
                    if (localization.Clip != null)
                    {
                        clip = localization.Clip;
                    }
                }

                if (_subtitlesText)
                {
                    _subtitlesText?.SetText(text);
                    _subtitlesText?.gameObject.SetActive(true);
                }

                if (clip)
                {
                    if (_audioSource)
                    {
                        _audioSource?.PlayOneShot(clip);
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
