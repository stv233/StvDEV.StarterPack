using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// GUI Menu Management Manager.
    /// </summary>
    public class GUIManager : MonoBehaviourSingleton<GUIManager>
    {
        private readonly Dictionary<string, GUIMenu> menus = new Dictionary<string, GUIMenu>();

        protected override void AwakeSingletone()
        {
            FindAllMenu();
        }

        /// <summary>
        /// Open the menu.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        public void OpenMenu(string menuName)
        {
            if (TryGetMenu(menuName, out GUIMenu menu))
            {
                menu.Open();    
            }
        }

        /// <summary>
        /// Opens the menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        public void OpenMenu<T>() where T : GUIMenu
        {
            if (TryGetMenu<T>(out GUIMenu menu))
            {
                menu.Open();
            }
        }

        /// <summary>
        /// Close the menu.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        public void CloseMenu(string menuName)
        {
            if (TryGetMenu(menuName, out GUIMenu menu))
            {
                menu.Close();
            }
        }

        /// <summary>
        /// Closes the menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        public void CloseMenu<T>() where T: GUIMenu
        {
            if (TryGetMenu<T>(out GUIMenu menu))
            {
                menu.Close();
            }
        }

        /// <summary>
        /// Returns the menu by its name
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <returns>Menu</returns>
        public GUIMenu GetMenu(string menuName)
        {
            if (MenuExist(menuName))
            {
                return menus[menuName];
            }

            return null;
        }

        /// <summary>
        /// Tries to get a menu by its name.
        /// </summary>
        /// <param name="menuName">Menu name</param>
        /// <param name="menu">Result</param>
        /// <returns>Success</returns>
        public bool TryGetMenu(string menuName, out GUIMenu menu)
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
        public GUIMenu GetMenu<T>() where T: GUIMenu
        {
            return menus.Values.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
        }

        /// <summary>
        /// Tries to get a menu by its type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <param name="menu">Result</param>
        /// <returns>Success</returns>
        public bool TryGetMenu<T>(out GUIMenu menu) where T: GUIMenu
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
        public bool MenuExist(string menuName)
        {
            return menus.ContainsKey(menuName);
        }

        /// <summary>
        /// Checks the menu for existence.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <returns>Existence</returns>
        public bool MenuExist<T>() where T : GUIMenu
        {
            return TryGetMenu<T>(out GUIMenu menu);
        }

        /// <summary>
        /// Registers the menu in the manager.
        /// </summary>
        /// <param name="menu">Menu</param>
        public void RegisterMenu(GUIMenu menu)
        {
            menus[menu.gameObject.name] = menu;
        }

        /// <summary>
        /// Searches for all menus on the level.
        /// </summary>
        private void FindAllMenu()
        {
            foreach(GUIMenu menu in GameObject.FindObjectsOfType<GUIMenu>(true))
            {
                RegisterMenu(menu);
            }
        }
    }
}
