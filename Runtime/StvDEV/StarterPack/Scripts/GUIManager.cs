using System.Collections.Generic;
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
        /// <param name="menu">Menu name</param>
        public void OpenMenu(string menu)
        {
            if (MenuExist(menu))
            {
                menus[menu].Open();
            }
        }

        /// <summary>
        /// Close the menu.
        /// </summary>
        /// <param name="menu">Menu name</param>
        public void CloseMenu(string menu)
        {
            if (MenuExist(menu))
            {
                menus[menu].Close();
            }
        }

        /// <summary>
        /// Checks the menu for existence.
        /// </summary>
        /// <param name="menu">Menu name</param>
        /// <returns>Existence</returns>
        public bool MenuExist(string menu)
        {
            return menus.ContainsKey(menu);
        }

        /// <summary>
        /// Searches for all menus on the level.
        /// </summary>
        private void FindAllMenu()
        {
            foreach(GUIMenu menu in GameObject.FindObjectsOfType<GUIMenu>())
            {
                menus[menu.gameObject.name] = menu;
            }
        }
    }
}
