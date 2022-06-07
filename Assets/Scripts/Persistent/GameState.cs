using System;
using System.Collections;
using System.Collections.Generic;
using Menus_UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public List<GameObject> playerCharacters;
    public GameObject[] tableList;
    public GameObject[] foodSpawners;
    public int selectedCharacter = -1;

    public int shiftStart;
    public int shiftEnd;

    public int currentTime;

    public float baseTips;
    public int multiplier;
    public float tipsStolen;
    public int ordersDelivered;
    public int karenAttacks;
    public float takeHomeTips;

    public String playerName = "NATHAN";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Start()
    {
        currentTime = shiftStart;
    }

    public void StartLevel()
    {
        tableList = GameObject.FindGameObjectsWithTag("Table");
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodPickup");
    }

    public void ResetStats()
    {
        baseTips = 0;
        tipsStolen = 0;
        ordersDelivered = 0;
        karenAttacks = 0;
        takeHomeTips = 0;
    }

    public void UpdateTime(int hour)
    {
        currentTime = hour;

        if (currentTime == shiftEnd)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        Debug.Log("END LEVEL");
        baseTips = UIMgr.Instance.desiredTips;
        multiplier = 1;
        SceneManager.LoadScene("BetweenLevel");
    }

    public void SpawnKaren(int tableNumber)
    {
        Debug.Log("GAME STATE CALLED");
        foreach (var table in tableList)
        {
            if (table.GetComponent<TableMgr>().tableNumber == tableNumber)
            {
                table.GetComponent<TableMgr>().SpawnKaren();
                foreach (var spawner in foodSpawners)
                {
                    spawner.GetComponent<FoodSpawn>().KarenSpawn(table.GetComponent<TableMgr>().tableNumber);
                    OrderMgr.Instance.SpawnKaren(table.GetComponent<TableMgr>().tableNumber);
                }
            }
        }
    }
}
