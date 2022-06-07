using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadRestaurant()
    {
        if(GameState.Instance.selectedCharacter != -1)
        {
            SceneManager.LoadScene("Restaurant");
        }
    }
}
