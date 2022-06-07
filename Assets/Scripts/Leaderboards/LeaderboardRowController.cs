using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRowController : MonoBehaviour
{
    public Text rankText;
    public Text nameText;
    public Text scoreText;

    private void Start()
    {
        rankText.text = "";
        nameText.text = "";
        scoreText.text = "";
    }
}
