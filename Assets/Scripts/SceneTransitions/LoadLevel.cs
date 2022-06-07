using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string levelName;

    public void Load()
    {
        SceneManager.LoadScene(levelName);
    }

    public void AfterLeaderboard()
    {
        GameState.Instance.ResetStats();
        SceneManager.LoadScene(levelName);
    }
}
