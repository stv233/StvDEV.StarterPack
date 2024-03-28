using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.ProjectBrowser.Folders
{
    /// <summary>
    /// Icons storage.
    /// </summary>
    internal static class IconsStorage
    {
        /// <summary>
        /// Path to icons information storage in Resources.
        /// </summary>
        internal const string STORAGE_PATH = "StvDEV/Editor/Icons";

        private static Dictionary<string, Texture> _iconsCache = new Dictionary<string, Texture>();

        /// <summary>
        /// Collection of folders for which icons are installed.
        /// </summary>
        internal static IReadOnlyDictionary<string, Texture> Folders => _iconsCache;

        /// <summary>
        /// Rebuild collection.
        /// </summary>
        internal static void Rebuild()
        {
            _iconsCache.Clear();

            FoldersIcon[] icons = Resources.LoadAll<FoldersIcon>(STORAGE_PATH);

            foreach(FoldersIcon icon in icons)
            {
                foreach(string folder in icon.Folders)
                {
                    _iconsCache.Add(folder, icon.Icon);
                }
            }

        }
    }
}
