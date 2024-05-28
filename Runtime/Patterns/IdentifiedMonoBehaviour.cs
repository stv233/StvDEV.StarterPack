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
        [ReadOnly]
        [SerializeField] private string _id;

        /// <summary>
        /// <see cref="MonoBehaviour"/> instance id.
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
        }

        [ContextMenu("Copy ID")]
        private void CopyID()
        {
            UnityEditor.EditorGUIUtility.systemCopyBuffer = _id;
        }

#endif
    }
}
