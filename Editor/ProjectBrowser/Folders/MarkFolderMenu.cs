using System.IO;
using UnityEditor;
using UnityEngine;

namespace StvDEV.ProjectBrowser.Folders
{
    /// <summary>
    /// Folders menu items.
    /// </summary>
    internal static class MarkFolderMenu
    {
        private const string PARENT_MENU = "Assets/Mark Folder As";

        [MenuItem(PARENT_MENU + "/Default")]
        private static void MarkAsDefault()
        {
            RemoveIconFromFolder(Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU  + "/Default", true)]
        private static bool MarkAsDefaultValidator()
        {
            return Selection.assetGUIDs.Length == 1 && ObjectIsFolder(Selection.activeObject) && FolderHasIcon(Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Animations", priority = 1015)]
        private static void MarkAsAnimations()
        {
            AddIconToFolder("Animations", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Audio", priority = 1015)]
        private static void MarkAsAudio()
        {
            AddIconToFolder("Audio", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Editor", priority = 1015)]
        private static void MarkAsEditor()
        {
            AddIconToFolder("Editor", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Fonts", priority = 1015)]
        private static void MarkAsFonts()
        {
            AddIconToFolder("Fonts", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Home", priority = 1015)]
        private static void MarkAsHome()
        {
            AddIconToFolder("Home", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Materials", priority = 1015)]
        private static void MarkAsMaterials()
        {
            AddIconToFolder("Materials", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Models", priority = 1015)]
        private static void MarkAsModels()
        {
            AddIconToFolder("Models", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Plugins", priority = 1015)]
        private static void MarkAsPlugins()
        {
            AddIconToFolder("Plugins", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Prefabs", priority = 1015)]
        private static void MarkAsPrefabs()
        {
            AddIconToFolder("Prefabs", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Presets", priority = 1015)]
        private static void MarkAsPresets()
        {
            AddIconToFolder("Presets", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Resources", priority = 1015)]
        private static void MarkAsResources()
        {
            AddIconToFolder("Resources", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Runtime", priority = 1015)]
        private static void MarkAsRuntime()
        {
            AddIconToFolder("Runtime", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Scenes", priority = 1015)]
        private static void MarkAsScenes()
        {
            AddIconToFolder("Scenes", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Scripts", priority = 1015)]
        private static void MarkAsScripts()
        {
            AddIconToFolder("Scripts", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Settings", priority = 1015)]
        private static void MarkAsSettings()
        {
            AddIconToFolder("Settings", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Shaders", priority = 1015)]
        private static void MarkAsShaders()
        {
            AddIconToFolder("Shaders", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Sprites", priority = 1015)]
        private static void MarkAsSprites()
        {
            AddIconToFolder("Sprites", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Textures", priority = 1015)]
        private static void MarkAsTextures()
        {
            AddIconToFolder("Textures", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Animations", true)]
        [MenuItem(PARENT_MENU + "/Audio", true)]
        [MenuItem(PARENT_MENU + "/Editor", true)]
        [MenuItem(PARENT_MENU + "/Fonts", true)]
        [MenuItem(PARENT_MENU + "/Home", true)]
        [MenuItem(PARENT_MENU + "/Materials", true)]
        [MenuItem(PARENT_MENU + "/Models", true)]
        [MenuItem(PARENT_MENU + "/Plugins", true)]
        [MenuItem(PARENT_MENU + "/Prefabs", true)]
        [MenuItem(PARENT_MENU + "/Presets", true)]
        [MenuItem(PARENT_MENU + "/Resources", true)]
        [MenuItem(PARENT_MENU + "/Runtime", true)]
        [MenuItem(PARENT_MENU + "/Scenes", true)]
        [MenuItem(PARENT_MENU + "/Scripts", true)]
        [MenuItem(PARENT_MENU + "/Settings", true)]
        [MenuItem(PARENT_MENU + "/Shaders", true)]
        [MenuItem(PARENT_MENU + "/Sprites", true)]
        [MenuItem(PARENT_MENU + "/Textures", true)]
        private static bool MarkAsValidator()
        {
            return ObjectIsFolder(Selection.activeObject) && Selection.assetGUIDs.Length == 1;
        }

        private static bool ObjectIsFolder(Object @object)
        {
            return PathIsFolder(AssetDatabase.GetAssetPath(@object));
        }

        private static bool PathIsFolder(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            }
            return false;
        }

        private static bool FolderHasIcon(string folder)
        {
            return IconsStorage.Folders.ContainsKey(folder);
        }

        private static void AddIconToFolder(string icon, string folder)
        {
            if (FolderHasIcon(folder))
            {
                RemoveIconFromFolder(folder);
            }

            FoldersIcon iconSO = AssetDatabase.LoadAssetAtPath<FoldersIcon>(Path.Combine("Assets", "Settings", IconsStorage.STORAGE_PATH, $"{icon}.asset"));
            if (iconSO == null)
            {
                iconSO = ScriptableObject.CreateInstance<FoldersIcon>();
                Texture2D iconImage = Resources.Load<Texture2D>(Path.Combine(IconsStorage.STORAGE_PATH, "2D", icon)); 
                iconSO.Icon = iconImage;

                string storagePath = Path.Combine("Assets", "Settings", IconsStorage.STORAGE_PATH);
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }

                AssetDatabase.CreateAsset(iconSO, Path.Combine(storagePath ,$"{icon}.asset"));
            }
            iconSO.Folders.Add(folder);
            EditorUtility.SetDirty(iconSO);
            AssetDatabase.SaveAssetIfDirty(iconSO);
            IconsStorage.Rebuild();
            AssetDatabase.Refresh();
        }

        private static void RemoveIconFromFolder(string folder)
        {
            if (!FolderHasIcon(folder))
            {
                return;
            }

            string icon = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(IconsStorage.Folders[folder]));

            FoldersIcon iconSO = AssetDatabase.LoadAssetAtPath<FoldersIcon>(Path.Combine("Assets", "Settings", IconsStorage.STORAGE_PATH, $"{icon}.asset"));

            iconSO.Folders.Remove(folder);
            EditorUtility.SetDirty(iconSO);
            AssetDatabase.SaveAssetIfDirty(iconSO);
            IconsStorage.Rebuild();
            AssetDatabase.Refresh();
        }
    }
}
