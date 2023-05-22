using StvDEV.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components
{
    /// <summary>
    /// Basic trigger component.
    /// </summary>
    [AddComponentMenu("StvDEV/Basic/Trigger")]
    public class Trigger : MonoBehaviour
    {
        [Header("Filter")]
        [CaptionedBool("On", "Off"), Tooltip("Filter objects by tag.")]
        [SerializeField] private bool _filterByTag;
        [ShowIf("_filterByTag"), Tooltip("Object tag.")]
        [SerializeField] private string _tag;

        [Header("Evenetns")]
        [Tooltip("Occurs when collider enter in trigger.")]
        [SerializeField] private UnityEvent<Collider> _onTriggerEnter;
        [Tooltip("Occurs when collider stay in trigger.")]
        [SerializeField] private UnityEvent<Collider> _onTriggerStay;
        [Tooltip("Occurs when collider exit from trigger.")]
        [SerializeField] private UnityEvent<Collider> _onTriggerExit;

        /// <summary>
        /// Occurs when collider enter in trigger.
        /// </summary>
        public UnityEvent<Collider> TriggerEnter => _onTriggerEnter;

        /// <summary>
        /// Occurs when collider stay in trigger.
        /// </summary>
        public UnityEvent<Collider> TriggerStay => _onTriggerStay;

        /// <summary>
        /// Occurs when collider exit from trigger.
        /// </summary>
        public UnityEvent<Collider> TriggerExit => _onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!_filterByTag || other.CompareTag(_tag))
            {
                _onTriggerEnter?.Invoke(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_filterByTag || other.CompareTag(_tag))
            {
                _onTriggerStay?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_filterByTag || other.CompareTag(_tag))
            {
                _onTriggerExit?.Invoke(other);
            }
        }
    }
}