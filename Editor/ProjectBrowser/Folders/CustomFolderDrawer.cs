using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StvDEV.ProjectBrowser.Folders
{
    /// <summary>
    /// Draw a custom folder icon.
    /// </summary>
    [InitializeOnLoad]
    internal static class CustomFolderDrawer
    {
        static CustomFolderDrawer()
        {
            IconsStorage.Rebuild();
            EditorApplication.projectWindowItemOnGUI += DrawFolderIcon;
        }

        private static void DrawFolderIcon(string guid, Rect rect)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            IReadOnlyDictionary<string, Texture> icons = IconsStorage.Folders;

            if (string.IsNullOrEmpty(path) 
                || Event.current.type != EventType.Repaint 
                || !File.GetAttributes(path).HasFlag(FileAttributes.Directory) 
                || !icons.ContainsKey(guid))
            {
                return;
            }

            Rect imageRect;

            if (rect.height > 20)
            {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.width + 2, rect.width + 2);
            }
            else if (rect.x > 20)
            {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.height + 2, rect.height + 2);
            }
            else
            {
                imageRect = new Rect(rect.x + 2, rect.y - 1, rect.height + 2, rect.height + 2);
            }

            Texture texture = IconsStorage.Folders[guid];
            if (!texture)
            {
                return;
            }

            GUI.DrawTexture(imageRect, texture);
        }
    }
}
