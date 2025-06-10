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

        [MenuItem(PARENT_MENU + "/Animations")]
        private static void MarkAsAnimations()
        {
            AddIconToFolder("Animations", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Audio")]
        private static void MarkAsAudio()
        {
            AddIconToFolder("Audio", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Editor")]
        private static void MarkAsEditor()
        {
            AddIconToFolder("Editor", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Fonts")]
        private static void MarkAsFonts()
        {
            AddIconToFolder("Fonts", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Home")]
        private static void MarkAsHome()
        {
            AddIconToFolder("Home", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Materials")]
        private static void MarkAsMaterials()
        {
            AddIconToFolder("Materials", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Models")]
        private static void MarkAsModels()
        {
            AddIconToFolder("Models", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Plugins")]
        private static void MarkAsPlugins()
        {
            AddIconToFolder("Plugins", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Prefabs")]
        private static void MarkAsPrefabs()
        {
            AddIconToFolder("Prefabs", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Presets")]
        private static void MarkAsPresets()
        {
            AddIconToFolder("Presets", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Resources")]
        private static void MarkAsResources()
        {
            AddIconToFolder("Resources", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Runtime")]
        private static void MarkAsRuntime()
        {
            AddIconToFolder("Runtime", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Scenes")]
        private static void MarkAsScenes()
        {
            AddIconToFolder("Scenes", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Scripts")]
        private static void MarkAsScripts()
        {
            AddIconToFolder("Scripts", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Settings")]
        private static void MarkAsSettings()
        {
            AddIconToFolder("Settings", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Shaders")]
        private static void MarkAsShaders()
        {
            AddIconToFolder("Shaders", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Sprites")]
        private static void MarkAsSprites()
        {
            AddIconToFolder("Sprites", Selection.assetGUIDs[0]);
        }

        [MenuItem(PARENT_MENU + "/Textures")]
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
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
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

            FoldersIcon iconSO = EditorGUIUtility.Load(Path.Combine(IconsStorage.STORAGE_PATH, $"{icon}.asset")) as FoldersIcon;
            if (iconSO == null)
            {
                iconSO = ScriptableObject.CreateInstance<FoldersIcon>();
                Texture2D iconImage = Resources.Load<Texture2D>(Path.Combine(IconsStorage.STORAGE_PATH, "2D", icon)); 
                iconSO.Icon = iconImage;

                string storagePath = Path.Combine("Assets", "Editor Default Resources", IconsStorage.STORAGE_PATH);
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

            FoldersIcon iconSO = EditorGUIUtility.Load(Path.Combine(IconsStorage.STORAGE_PATH, $"{icon}.asset")) as FoldersIcon;

            iconSO.Folders.Remove(folder);
            EditorUtility.SetDirty(iconSO);
            AssetDatabase.SaveAssetIfDirty(iconSO);
            IconsStorage.Rebuild();
            AssetDatabase.Refresh();
        }
    }
}
