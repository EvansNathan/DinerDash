using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menus_UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;
        public Canvas pMenu;
        public Canvas howToMenu;
        public Canvas HUD;
        public bool isPaused;
    
        private void Start()
        {   
            pauseMenu.SetActive(false);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                if(isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            pauseMenu.SetActive(true);
            HUD.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void ResumeGame()
        {
            pMenu.enabled = true;
            pauseMenu.SetActive(false);
            HUD.GetComponent<Canvas>().enabled = true;
            howToMenu.enabled = false;
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            isPaused = false;
            MusicMgr.Instance.musicSource.Stop();
            MusicMgr.Instance.musicSource.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
