using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timers : MonoBehaviour
{
    public float timer;
    public double doubleTimer;
    public GameObject pickupCounter;
    public GameObject dropOffTable1;
    public GameObject dropOffTable2;
    public GameObject dropOffTable3;
    public GameObject dropOffTable4;
    public GameObject dropOffTable5;
    public GameObject dropOffTable6;
    public GameObject dropOffTable7;
    public TextMeshPro timeDisplay;
    private bool timerWait;
    private string textMessage;
    private bool pickUp = false;
    private bool timerCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        dropOffTable1 = PlayerReferences.Instance.tables[0];
        dropOffTable2 = PlayerReferences.Instance.tables[1];
        dropOffTable3 = PlayerReferences.Instance.tables[2];
        dropOffTable4 = PlayerReferences.Instance.tables[3];
        dropOffTable5 = PlayerReferences.Instance.tables[4];
        dropOffTable6 = PlayerReferences.Instance.tables[5];
        dropOffTable7 = PlayerReferences.Instance.tables[6];
        timeDisplay = PlayerReferences.Instance.timer.GetComponent<TextMeshPro>();

        StartCoroutine(TimerUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        

        if(timerCheck)
        {
            timeDisplay.text = "Time Taken to Deliver: " + textMessage;
        }
        else
        {
            timeDisplay.text = "";
        }
        if (pickUp)
        {
            timer = timer + 1*Time.deltaTime;
        }
        
    }

    IEnumerator TimerUpdate()
    {
        while (true)
        {
            doubleTimer = Mathf.Floor(timer);
            textMessage = doubleTimer.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == pickupCounter)
        {
            pickUp = true;
            timerCheck = true;
        }
        if(other.gameObject == dropOffTable1 || other.gameObject == dropOffTable2 || other.gameObject == dropOffTable3 || other.gameObject == dropOffTable4 || other.gameObject == dropOffTable5 || other.gameObject == dropOffTable6 || other.gameObject == dropOffTable7)
        {
            timer = 0;
            pickUp = false;
            timerCheck = false;
        }
    }
}
