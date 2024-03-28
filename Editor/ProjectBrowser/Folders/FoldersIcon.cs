using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.ProjectBrowser.Folders
{
    /// <summary>
    /// Folders icon information.
    /// </summary>
    internal class FoldersIcon : ScriptableObject 
    {
        [SerializeField] private Texture2D _icon;
        [SerializeField] private List<string> _folders = new List<string>();

        /// <summary>
        /// Icon.
        /// </summary>
        public Texture2D Icon
        {
            get => _icon;
            set => _icon = value;
        }

        /// <summary>
        /// List of folders with this icon.
        /// </summary>
        public List<string> Folders => _folders;

        public void OnValidate() 
        {
            IconsStorage.Rebuild();
        }
    }
}

