using StvDEV.Patterns;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace StvDEV.Components.UI
{
    /// <summary>
    /// GUI Menu Management Manager.
    /// </summary>
    [MovedFrom(true, "StvDEV.StarterPack", "StvDEV.StarterPack", "GUIManager")]
    [AddComponentMenu("StvDEV/UI/UI Menu Manager")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/UI/UIMenuManager/index.html")]
    public class UIMenuManager : MonoBehaviourSingleton<UIMenuManager>
    {
        private readonly Dictionary<string, UIMenu> _menus = new();

        protected override void AwakeSingleton()
        {
            FindAllMenu();
        }

        /// <summary>
        /// Open the menu.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        public static void OpenMenu(string menuName)
        {
            if (TryGetMenu(menuName, out UIMenu menu))
            {
                menu.Open();    
            }
        }

        /// <summary>
        /// Opens the menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        public static void OpenMenu<T>() where T : UIMenu
        {
            if (TryGetMenu(out T menu))
            {
                menu.Open();
            }
        }

        /// <summary>
        /// Close the menu.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        public static void CloseMenu(string menuName)
        {
            if (TryGetMenu(menuName, out UIMenu menu))
            {
                menu.Close();
            }
        }

        /// <summary>
        /// Closes the menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        public static void CloseMenu<T>() where T: UIMenu
        {
            if (TryGetMenu(out T menu))
            {
                menu.Close();
            }
        }

        /// <summary>
        /// Returns the menu by its name
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <returns>Menu</returns>
        public static UIMenu GetMenu(string menuName)
        {
            if (MenuExist(menuName))
            {
                return Instance._menus[menuName];
            }

            return null;
        }

        /// <summary>
        /// Tries to get a menu by its name.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <param name="menu">Result</param>
        /// <returns>Success</returns>
        public static bool TryGetMenu(string menuName, out UIMenu menu)
        {
            menu = GetMenu(menuName);
            if (menu != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <returns>Menu</returns>
        public static T GetMenu<T>() where T: UIMenu
        {
            return Instance._menus.Values.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
        }

        /// <summary>
        /// Tries to get a menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <param name="menu">Result</param>
        /// <returns>Success</returns>
        public static bool TryGetMenu<T>(out T menu) where T: UIMenu
        {
            menu = GetMenu<T>();
            if (menu != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks the menu for existence.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <returns>Existence</returns>
        public static bool MenuExist(string menuName)
        {
            return Instance._menus.ContainsKey(menuName);
        }

        /// <summary>
        /// Checks the menu for existence.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <returns>Existence</returns>
        public static bool MenuExist<T>() where T : UIMenu
        {
            return TryGetMenu(out T menu);
        }

        /// <summary>
        /// Gets all existing menus.
        /// </summary>
        /// <returns>Gets list of all existing menus</returns>
        public static List<UIMenu> GetExistingMenus()
        {
            return Instance._menus.Values.ToList();
        }

        /// <summary>
        /// Registers the menu in the manager.
        /// </summary>
        /// <param name="menu">Menu</param>
        public static void RegisterMenu(UIMenu menu)
        {
            Instance._menus[menu.gameObject.name] = menu;
        }

        /// <summary>
        /// Searches for all menus on the level.
        /// </summary>
        private static void FindAllMenu()
        {
            foreach(UIMenu menu in GameObject.FindObjectsOfType<UIMenu>(true))
            {
                RegisterMenu(menu);
            }
        }
    }
}
