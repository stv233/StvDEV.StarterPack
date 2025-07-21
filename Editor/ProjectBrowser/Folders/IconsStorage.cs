using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
        internal const string STORAGE_PATH = "StvDEV/Icons";

        private static readonly Dictionary<string, Texture> s_iconsCache = new();

        /// <summary>
        /// Collection of folders for which icons are installed.
        /// </summary>
        internal static IReadOnlyDictionary<string, Texture> Folders => s_iconsCache;

        /// <summary>
        /// Rebuild collection.
        /// </summary>
        internal static void Rebuild()
        {
            s_iconsCache.Clear();
            FoldersIcon[] icons 
                = AssetDatabase.FindAssets("*", new string[] { $"Assets/Editor Default Resources/{STORAGE_PATH}" })
                    .Select(x => AssetDatabase.LoadAssetAtPath<FoldersIcon>(AssetDatabase.GUIDToAssetPath(x)))
                    .Where(x => x != null).ToArray();

            foreach (FoldersIcon icon in icons)
            {
                foreach(string folder in icon.Folders)
                {
                    s_iconsCache.TryAdd(folder, icon.Icon);
                }
            }

        }
    }
}
