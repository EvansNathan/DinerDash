using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public Canvas howTo;

    public void Start()
    {
        mainMenu.GetComponent<Canvas>().enabled = true;
        howTo.enabled = false;
    }

    public void StartGame()
    {
        Debug.Log("STARTGAME");
        SceneManager.LoadScene("Character Select");
    }

    public void Credits()
    {
        Debug.Log("CREDITS");
        SceneManager.LoadScene("Credits");
    }

    public void SpecialThanks()
    {
        Debug.Log("SpecialThanks");
        SceneManager.LoadScene("Credits 1");
    }

    public void ToMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("Main Menu");
    }

    public void HowTo()
    {
        howTo.enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
    }

    public void CloseHowTo()
    {
        howTo.enabled = false;
        mainMenu.GetComponent<Canvas>().enabled = true;
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
