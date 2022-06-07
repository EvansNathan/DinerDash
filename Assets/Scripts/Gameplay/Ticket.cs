using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket
{
    public GameObject tableRef;
    public int foodOrder;

    public Ticket(GameObject table)
    {
        tableRef = table;
        foodOrder = Random.Range(0, 10);

        return;
    }
}
