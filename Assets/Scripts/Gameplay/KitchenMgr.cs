using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMgr : MonoBehaviour
{
    public static KitchenMgr Instance;
    public Queue<Ticket> KitchenQueue;
    public GameObject[] foodPrefabs;
    public Transform[] foodSpawns;
    public Sprite[] readyIcons;

    public bool isBusy;

    private Ticket currentTicket;

    public enum Food
    {
        Donuts,
        FrenchFries,
        HotDog,
        Meat1,
        Meat2,
        Pizza,
        Soda,
        Soup2,
        Soup3,
        Soup4
    };
    
    public int foodItem;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void Start()
    {
        KitchenQueue = new Queue<Ticket>();
    }

    private void Update()
    {
        StartCooking();
    }

    public void AddTicket(Ticket ticket)
    {
        Debug.Log("TICKET ADDED");
        KitchenQueue.Enqueue(ticket);
    }

    private void StartCooking()
    {
        if (!isBusy && KitchenQueue.Count != 0)
        {
            Debug.Log("Starting");
            currentTicket  = KitchenQueue.Dequeue();
            foodItem = currentTicket.foodOrder;
            StartCoroutine(CookTime());
        }
    }

    private IEnumerator CookTime()
    {
        var hasSpawned = false;
        isBusy = true;
        yield return new WaitForSeconds(1);

        while(!hasSpawned)
        {
            if(foodSpawns[0].GetComponent<FoodSpawn>().isOpen)
            {
                GameObject temp = Instantiate(foodPrefabs[foodItem]);
                GetComponent<AudioSource>().Play();
                temp.transform.position = foodSpawns[0].position;
                foodSpawns[0].GetComponent<FoodSpawn>().icon.sprite = readyIcons[currentTicket.tableRef.GetComponent<TableMgr>().tableNumber];
                foodSpawns[0].GetComponent<FoodSpawn>().isOpen = false;
                foodSpawns[0].GetComponent<FoodSpawn>().PassTicket(currentTicket);
                foodSpawns[0].GetComponent<FoodSpawn>().spawnedObjectRef = temp;
                foodSpawns[0].GetComponent<FoodSpawn>().foodIndex = foodItem;
                hasSpawned = true;

            }
            else if(foodSpawns[1].GetComponent<FoodSpawn>().isOpen)
            {
                GameObject temp = Instantiate(foodPrefabs[foodItem]);
                GetComponent<AudioSource>().Play();
                temp.transform.position = foodSpawns[1].position;
                foodSpawns[1].GetComponent<FoodSpawn>().icon.sprite = readyIcons[currentTicket.tableRef.GetComponent<TableMgr>().tableNumber];
                foodSpawns[1].GetComponent<FoodSpawn>().isOpen = false;
                foodSpawns[1].GetComponent<FoodSpawn>().PassTicket(currentTicket);
                foodSpawns[1].GetComponent<FoodSpawn>().spawnedObjectRef = temp;
                foodSpawns[1].GetComponent<FoodSpawn>().foodIndex = foodItem;
                hasSpawned = true;
            }
            else if(foodSpawns[2].GetComponent<FoodSpawn>().isOpen)
            {
                GameObject temp = Instantiate(foodPrefabs[foodItem]);
                GetComponent<AudioSource>().Play();
                temp.transform.position = foodSpawns[2].position;
                foodSpawns[2].GetComponent<FoodSpawn>().icon.sprite = readyIcons[currentTicket.tableRef.GetComponent<TableMgr>().tableNumber];
                foodSpawns[2].GetComponent<FoodSpawn>().isOpen = false;
                foodSpawns[2].GetComponent<FoodSpawn>().PassTicket(currentTicket);
                foodSpawns[2].GetComponent<FoodSpawn>().spawnedObjectRef = temp;
                foodSpawns[2].GetComponent<FoodSpawn>().foodIndex = foodItem;
                hasSpawned = true;
            }

            if (!hasSpawned)
            {
                Debug.Log("WAITING");
                yield return new WaitForSeconds(1);
            }
        }

        isBusy = false;

    }
}
