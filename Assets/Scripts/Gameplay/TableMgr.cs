using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Menus_UI;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Random = UnityEngine.Random;

public class TableMgr : MonoBehaviour
{
    //---- this will be replaced with a gameobject containing the food assets
    public GameObject readyToOrderIndicator;
    public GameObject deliverFoodIndicator;
    public List<Transform> spawnLocation;
    public List<Transform> spawnLocationCopy;
    private Queue<GameObject> customers;
    public Transform lookAtLocation;
    public GameObject[] customerPrefabs;
    public GameObject[] enemyPrefabs;
    public bool isReadyToOrder = false;
    public bool isReadyToDeliver = false;
    public GameObject curFood;
    public Transform enemySpawnPoint;
    public GameObject enemyPrefab;
    public int tableNumber;

    //public GameObject player;

    private double doubleTimer = 0;
    private float timer = 0;
    private int rng = 0;
    private bool bother = false;
    private bool waiting = false;
    private bool reset = true;
    private bool breakCheck = false;
    public string tableName = "";
    
    private string takeOrderMessage = "Press 'E' to Take Order";
    private string deliverFoodMessage = "Press 'E' to Deliver Food";
    
    void Start()
    {
        rng = Random.Range(1, 11) * 5;
        customers = new Queue<GameObject>();
        //rng = 5;
        StartCoroutine(BotherTimer());
    }
    
    void Update()
    {
        SetReadyToOrderIndicator();
        SetDeliverFoodIndicator();
        
        if(bother)
        {
            
        }
        else if(waiting)
        {

        }
        else if(reset)
        {
  
        }
        
    }

    IEnumerator BotherTimer()
    {
        yield return new WaitForSeconds(rng);
        UIMgr.Instance.ActivateHappinessImage(tableNumber);
        spawnLocationCopy = new List<Transform>(spawnLocation);
        
        while (spawnLocationCopy.Count != 0)
        {
            Transform tempSpawn = spawnLocationCopy[Random.Range(0, spawnLocationCopy.Count)];
            spawnLocationCopy.Remove(tempSpawn);
            GameObject temp = Instantiate(customerPrefabs[Random.Range(0, 18)], tempSpawn);
            customers.Enqueue(temp);
            temp.transform.LookAt(lookAtLocation);
        }
        
        bother = true;
        reset = false;
        isReadyToOrder = true;
    }

    IEnumerator TimerUpdate()
    {
        while (true)
        {
            doubleTimer = Mathf.Floor(timer);
            timer = timer + 1 * Time.deltaTime;
            yield return new WaitForSeconds(1*Time.deltaTime);
            //Debug.Log("inside timer update");
            if (breakCheck){
                yield return null;
            }
        }
    }

    public void StartEatTimer()
    {
        StartCoroutine(EatTimer());
    }

    private IEnumerator EatTimer()
    {
        yield return new WaitForSeconds(Random.Range(2, 7));
        while (customers.Count > 0)
        {
            var temp = customers.Dequeue();
            Destroy(temp);
        }
        
        customers.Clear();
        Destroy(curFood);

        var tipAmt = Random.Range(3, 12);
        var happinessModifier = UIMgr.Instance.GetHappinessModifier(tableNumber);
        UIMgr.Instance.desiredTips += tipAmt * happinessModifier;
        Debug.Log("Base Tip : " + tipAmt);
        Debug.Log("Happiness Modifier: " + happinessModifier);
        Debug.Log("Final Tip: " + tipAmt * happinessModifier);
        GetComponent<AudioSource>().Play();

        StartCoroutine(BotherTimer());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(bother && other.gameObject.CompareTag("Player"))
        {
            bother = false;
            waiting = true;
            timer = 0;
            StartCoroutine(TimerUpdate());
            StopCoroutine(BotherTimer());
        }
        if(waiting && doubleTimer > 1 && other.gameObject.CompareTag("Player"))
        {
            waiting = false;
            reset = true;
            breakCheck = true;
            StopCoroutine(TimerUpdate());
            StartCoroutine(BotherTimer());
            timer = 0;
        }
    }

    public string ReturnName()
    {
        return tableName;
    }

    public void SetReadyToOrderIndicator()
    {
        readyToOrderIndicator.SetActive(isReadyToOrder);
    }

    public void SetDeliverFoodIndicator()
    {
        deliverFoodIndicator.SetActive(isReadyToDeliver);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReadyToOrder && other.gameObject.CompareTag("Player"))
        {
            UIMgr.Instance.SetInteractText(takeOrderMessage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isReadyToOrder && other.gameObject.CompareTag("Player"))
        {
            UIMgr.Instance.SetInteractText(takeOrderMessage);
        }
        else if (isReadyToDeliver && other.gameObject.CompareTag("Player"))
        {
            UIMgr.Instance.SetInteractText(deliverFoodMessage);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            UIMgr.Instance.SetInteractText("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIMgr.Instance.SetInteractText("");
        }
    }

    public void SpawnKaren()
    {
        //Angry customers
        
        GameObject customerDel = customers.Peek();
        Debug.Log("Spawn enemy");
        Debug.Log(customerDel.name);
        customerDel.transform.position = enemySpawnPoint.position;
        customerDel.AddComponent<BoxCollider>();
        customerDel.AddComponent<Rigidbody>();
        customerDel.GetComponent<Rigidbody>().mass = 5000;
        customerDel.GetComponent<Rigidbody>().angularDrag = 0;
        customerDel.GetComponent<Rigidbody>().isKinematic = true;
        customerDel.AddComponent<AI>();
        customerDel.GetComponent<AI>().active = true;
        customerDel.AddComponent<NavMeshAgent>();
        customerDel.GetComponent<NavMeshAgent>().baseOffset = 1.5f;
        customerDel.GetComponent<NavMeshAgent>().speed = 0.7f;
        customerDel.GetComponent<NavMeshAgent>().radius = .6f;


        customers.Dequeue();

        while (customers.Count > 0)
        {
            var temp = customers.Dequeue();
            Destroy(temp);
        }

        isReadyToDeliver = false;
        isReadyToOrder = false;

        StartCoroutine(BotherTimer());

        
    }
}
