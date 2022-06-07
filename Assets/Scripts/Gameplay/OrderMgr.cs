using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Menus_UI;
using UnityEngine;
using TMPro;

public class OrderMgr : MonoBehaviour

{
    public GameObject pickupCounter;
    public GameObject player;
    private GameObject[] collection = new GameObject[7]; 
    public GameObject dropOffTable1;
    public GameObject dropOffTable2;
    public GameObject dropOffTable3;
    public GameObject dropOffTable4;
    public GameObject dropOffTable5;
    public GameObject dropOffTable6;
    public GameObject dropOffTable7;
    public GameObject kitchenDropOffIndicator;
    public Queue<Ticket> ticketQueue;
    public List<Ticket> heldOrder = new List<Ticket>();
    public int numOrdersHeld = 0;

    public static OrderMgr Instance;

    private void Awake()
    {
        if(Instance != null)
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
        player = this.gameObject;
        
        dropOffTable1 = PlayerReferences.Instance.tables[0];
        dropOffTable2 = PlayerReferences.Instance.tables[1];
        dropOffTable3 = PlayerReferences.Instance.tables[2];
        dropOffTable4 = PlayerReferences.Instance.tables[3];
        dropOffTable5 = PlayerReferences.Instance.tables[4];
        dropOffTable6 = PlayerReferences.Instance.tables[5];
        dropOffTable7 = PlayerReferences.Instance.tables[6];

        pickupCounter = PlayerReferences.Instance.pickupCounter;

        kitchenDropOffIndicator = PlayerReferences.Instance.kitchenDropOffIndicator;
        
        
        collection[0] = dropOffTable1;
        collection[1] = dropOffTable2;
        collection[2] = dropOffTable3;
        collection[3] = dropOffTable4;
        collection[4] = dropOffTable5;
        collection[5] = dropOffTable6;
        collection[6] = dropOffTable7;

        ticketQueue = new Queue<Ticket>();
        

    }

    public void AddOrder(Ticket temp)
    {
        heldOrder.Add(temp);
        numOrdersHeld++;
    }
    
    private void OnTriggerStay(Collider other)
    {
        //Check if trigger volume belongs to a Table
        if (other.gameObject.CompareTag(tag: "Table"))
        {
            //Check if table is either ready to order or ready to deliver if not exit event
            if (!other.gameObject.GetComponent<TableMgr>().isReadyToOrder &&
                !other.gameObject.GetComponent<TableMgr>().isReadyToDeliver)
            {
                return;
            }

            //While inside trigger volume listen for 'E' key
            if (Input.GetKey(KeyCode.E))
            {
                if(other.gameObject.GetComponent<TableMgr>().isReadyToOrder)
                {
                    //Check if there is room to hold more ticket (MAX 3)
                    if (UIMgr.Instance.SendTable(other.gameObject.GetComponent<TableMgr>().tableName))
                    {
                        kitchenDropOffIndicator.SetActive(true);

                        //Create new ticket that holds table number and food order the add to held tickets and
                        //remove ready to order from table
                        var tempTicket = new Ticket(other.gameObject);
                        ticketQueue.Enqueue(tempTicket);
                        other.gameObject.GetComponent<TableMgr>().isReadyToOrder = false;
                        UIMgr.Instance.BoostHappiness(tempTicket.tableRef.GetComponent<TableMgr>().tableNumber, .25f);
                    }
                }
                else if (other.gameObject.GetComponent<TableMgr>().isReadyToDeliver)
                {
                    for(int i=0;i<numOrdersHeld;i++)
                    {
                        if (heldOrder[i].tableRef == other.gameObject)
                        {
                            int temp = heldOrder[i].foodOrder;
                            player.gameObject.GetComponent<HeldFood>().RemoveFood(other.gameObject, temp);

                            heldOrder.RemoveAt(i);
                            numOrdersHeld -= 1;

                            other.gameObject.GetComponent<TableMgr>().StartEatTimer();
                            other.gameObject.GetComponent<TableMgr>().isReadyToDeliver = false;
                            GameState.Instance.ordersDelivered += 1;
                            UIMgr.Instance.DeactivateHappinessImage(other.gameObject.GetComponent<TableMgr>().tableNumber);

                        }
                    }
                }
            }
        }
        //Check if trigger volume belongs to the KitchenDropOff
        else if(other.gameObject.CompareTag("KitchenDropOff"))
        {
            //Check if holding any tickets
            if (ticketQueue.Count != 0)
            {
                //Remove from held tickets and add to kitchen queue
                KitchenMgr.Instance.AddTicket(ticketQueue.Dequeue());
                UIMgr.Instance.ResetHeldTickets();
                kitchenDropOffIndicator.SetActive(false);
            }
        }
    }

    public void SpawnKaren(int tableNumber)
    {
        CheckHeldTickets(tableNumber);

        Ticket temp = null;

        foreach (var ticket in heldOrder)
        {
            if(ticket.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
            {
                temp = ticket;
            }
        }

        if (temp != null)
        {
            heldOrder.Remove(temp);
            numOrdersHeld -= 1;
            gameObject.GetComponent<HeldFood>().DestroyFood(tableNumber);
        }
    }

    private void CheckHeldTickets(int tableNumber)
    {
        if (ticketQueue.Count != 0)
        {
            kitchenDropOffIndicator.SetActive(false);
            UIMgr.Instance.ResetHeldTickets();

            Ticket ticket1 = null;
            Ticket ticket2 = null;
            Ticket ticket3 = null;

            if (ticketQueue.Count != 0)
            {
                ticket1 = ticketQueue.Dequeue();
            }
        
            if (ticketQueue.Count != 0)
            {
                ticket2 = ticketQueue.Dequeue();
            }
        
            if (ticketQueue.Count != 0)
            {
                ticket3 = ticketQueue.Dequeue();
            }

            if(ticket1 != null && ticket1.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
            {
                if (ticket2 != null)
                {
                    ticketQueue.Enqueue(ticket2);
                    UIMgr.Instance.SendTable(ticket2.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                if (ticket3 != null)
                {
                    ticketQueue.Enqueue(ticket3);
                    UIMgr.Instance.SendTable(ticket3.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
            }
            else if(ticket2 != null && ticket2.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
            {
                if (ticket1 != null)
                {
                    ticketQueue.Enqueue(ticket1);
                    UIMgr.Instance.SendTable(ticket1.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                if (ticket3 != null)
                {
                    ticketQueue.Enqueue(ticket3);
                    UIMgr.Instance.SendTable(ticket3.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                
                
                
            }
            else if(ticket3 != null && ticket3.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
            {
                if (ticket1 != null)
                {
                    ticketQueue.Enqueue(ticket1);
                    UIMgr.Instance.SendTable(ticket1.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                if (ticket2 != null)
                {
                    ticketQueue.Enqueue(ticket2);
                    UIMgr.Instance.SendTable(ticket2.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
            }
            else
            {
                if (ticket1 != null)
                {
                    ticketQueue.Enqueue(ticket1);
                    UIMgr.Instance.SendTable(ticket1.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                if (ticket2 != null)
                {
                    ticketQueue.Enqueue(ticket2);
                    UIMgr.Instance.SendTable(ticket2.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
                if (ticket3 != null)
                {
                    ticketQueue.Enqueue(ticket3);
                    UIMgr.Instance.SendTable(ticket3.tableRef.GetComponent<TableMgr>().tableName);
                    kitchenDropOffIndicator.SetActive(true);
                }
            }
        }
    }

}
