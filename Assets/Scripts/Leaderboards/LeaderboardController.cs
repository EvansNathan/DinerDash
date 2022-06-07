using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public static LeaderboardController Instance;

    public int leaderboardID;

    public GameObject[] leaderboardRows;

    public InputField playerName;

    public GameObject loadingScreen;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(StartSession());
        DisplayScore();
    }

    IEnumerator StartSession()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
                done = true;
            }
            else
            {
                Debug.Log("Failed");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

    }

    public IEnumerator ScoreSubmit()
    {
        int score = (int) GameState.Instance.takeHomeTips;
        bool done = false;

        LootLockerSDKManager.SubmitScore(playerName.text, score, 2933, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Submission Success");
                done = true;
            }
            else
            {
                Debug.Log("Submission Failed");
            }
        });
        yield return new WaitUntil(() => done == false);
        
        DisplayScore();
    }


    public void SubmitScore()
    {
        if (playerName.text == "")
        {
            Debug.Log("ENTER PLAYER NAME");
        }
        else
        {
            StartCoroutine(ScoreSubmit());
        }
        
    }

    public void DisplayScore()
    {
        StartCoroutine(FetchScores());
    }

    private IEnumerator FetchScores()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        bool isDone = false;
        LootLockerLeaderboardMember[] members = new LootLockerLeaderboardMember[] { };

        LootLockerSDKManager.GetScoreListMain(leaderboardID, 6, 0, (response) =>
        {
            if (response.success)
            {
                members = response.items;
                Debug.Log("Leaderboard Fetch Success");
                isDone = true;
            }
            else
            {
                Debug.Log("Leaderboard Fetch Failed");
                isDone = true;
            }

        });
        yield return new WaitForSeconds(2);

        int index = 0;
        
        foreach (var member in members)
        {
            Debug.Log("FORLOOP");
            
            var tempRank = member.rank.ToString();
            var tempName = member.member_id;
            var tempScore = member.score.ToString("c2");

            leaderboardRows[index].GetComponent<LeaderboardRowController>().rankText.text = tempRank;
            leaderboardRows[index].GetComponent<LeaderboardRowController>().nameText.text = tempName;
            leaderboardRows[index].GetComponent<LeaderboardRowController>().scoreText.text = tempScore;
            index += 1;
        }

        loadingScreen.SetActive(false);
    }
}
