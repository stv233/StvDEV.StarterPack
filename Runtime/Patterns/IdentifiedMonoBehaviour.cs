using StvDEV.Inspector;
using System;
using UnityEngine;

namespace StvDEV.Patterns
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> with ID.
    /// </summary>
    public abstract class IdentifiedMonoBehaviour : MonoBehaviour
    {
        [ReadOnly, Tooltip("Instance id.")]
        [SerializeField] private string _id;

        /// <summary>
        /// Instance id.
        /// </summary>
        public string ID => _id;

#if UNITY_EDITOR

        protected void OnValidate()
        {
            if (string.IsNullOrEmpty(_id))
            {
                ResetId();
            }
        }

        [ContextMenu("Reset ID")]
        private void ResetId()
        {
            _id = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ContextMenu("Copy ID")]
        private void CopyID()
        {
            UnityEditor.EditorGUIUtility.systemCopyBuffer = _id;
        }

#endif
    }
}
