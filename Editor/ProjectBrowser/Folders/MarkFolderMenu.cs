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
            RemoveIconFromFolder(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU  + "/Default", true)]
        private static bool MarkAsDefaultValidator()
        {
            return ObjectIsFolder(Selection.activeObject) && FolderHasIcon(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Animations")]
        private static void MarkAsAnimations()
        {
            AddIconToFolder("Animations", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Audio")]
        private static void MarkAsAudio()
        {
            AddIconToFolder("Audio", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Editor")]
        private static void MarkAsEditor()
        {
            AddIconToFolder("Editor", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Fonts")]
        private static void MarkAsFonts()
        {
            AddIconToFolder("Fonts", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Home")]
        private static void MarkAsHome()
        {
            AddIconToFolder("Home", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Materials")]
        private static void MarkAsMaterials()
        {
            AddIconToFolder("Materials", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Models")]
        private static void MarkAsModels()
        {
            AddIconToFolder("Models", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Plugins")]
        private static void MarkAsPlugins()
        {
            AddIconToFolder("Plugins", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Prefabs")]
        private static void MarkAsPrefabs()
        {
            AddIconToFolder("Prefabs", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Presets")]
        private static void MarkAsPresets()
        {
            AddIconToFolder("Presets", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Resources")]
        private static void MarkAsResources()
        {
            AddIconToFolder("Resources", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Runtime")]
        private static void MarkAsRuntime()
        {
            AddIconToFolder("Runtime", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Scenes")]
        private static void MarkAsScenes()
        {
            AddIconToFolder("Scenes", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Scripts")]
        private static void MarkAsScripts()
        {
            AddIconToFolder("Scripts", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Settings")]
        private static void MarkAsSettings()
        {
            AddIconToFolder("Settings", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Shaders")]
        private static void MarkAsShaders()
        {
            AddIconToFolder("Shaders", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Sprites")]
        private static void MarkAsSprites()
        {
            AddIconToFolder("Sprites", AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(PARENT_MENU + "/Textures")]
        private static void MarkAsTextures()
        {
            AddIconToFolder("Textures", AssetDatabase.GetAssetPath(Selection.activeObject));
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
            return ObjectIsFolder(Selection.activeObject);
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

            FoldersIcon iconSO = Resources.Load<FoldersIcon>(Path.Combine(IconsStorage.STORAGE_PATH, icon));
            if (iconSO == null)
            {
                iconSO = new FoldersIcon();
                Texture2D iconImage = Resources.Load<Texture2D>(Path.Combine(IconsStorage.STORAGE_PATH, "2D", icon)); 
                iconSO.Icon = iconImage;

                string storagePath = Path.Combine("Assets", "Resources", IconsStorage.STORAGE_PATH);
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

            FoldersIcon iconSO = Resources.Load<FoldersIcon>(Path.Combine(IconsStorage.STORAGE_PATH, icon));

            iconSO.Folders.Remove(folder);
            EditorUtility.SetDirty(iconSO);
            AssetDatabase.SaveAssetIfDirty(iconSO);
            IconsStorage.Rebuild();
            AssetDatabase.Refresh();
        }
    }
}
