using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menus_UI
{
    public class HowToInGame : MonoBehaviour
    {
        public Canvas pauseMenu;
        public Canvas howToMenu;

        private void Start()
        {
            pauseMenu.enabled = true;
            howToMenu.enabled = false;
        }

        public void OnClick()
        {
            pauseMenu.enabled = !pauseMenu.enabled;
            howToMenu.enabled = !howToMenu.enabled;
        }
    }
}
