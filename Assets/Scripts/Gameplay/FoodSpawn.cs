using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Menus_UI;
using UnityEngine;
using UnityEngine.UI;

public class FoodSpawn : MonoBehaviour
{
    public bool isOpen;
    public Ticket ticket;
    public Sprite transImg;
    public Image icon;
    public GameObject spawnedObjectRef;
    public int foodIndex;

    public void Start()
    {
        isOpen = true;
        icon.sprite = transImg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if((other.gameObject.GetComponent<HeldFood>().HandsFree())==true && !isOpen)
            {
                other.gameObject.GetComponent<OrderMgr>().AddOrder(ticket);
                ticket.tableRef.GetComponent<TableMgr>().isReadyToDeliver = true;
                Ticket passedTicket = new Ticket(ticket.tableRef);
                other.gameObject.GetComponent<HeldFood>().AddFood(spawnedObjectRef, foodIndex, passedTicket);
                isOpen = true;
                ticket = null;
                icon.sprite = transImg;
            }
        }
    }
    
    public void PassTicket(Ticket temp)
    {
        ticket = temp;
    }

    public void KarenSpawn(int tableNumber)
    {
        if (ticket == null)
        {
            return;
        }

        if (ticket.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
        {
            Destroy(spawnedObjectRef);
            ticket = null;
            icon.sprite = transImg;
            isOpen = true;
        }
    }
}
