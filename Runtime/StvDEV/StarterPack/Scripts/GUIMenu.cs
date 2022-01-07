using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// GUI Menu.
    /// </summary>
    public class GUIMenu : MonoBehaviour 
    {
        /// <summary>
        /// Open menu.
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Close menu.
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
