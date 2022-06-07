using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
       public static PlayerReferences Instance;

       public List<GameObject> tables;
       public GameObject kitchenDropOffIndicator;
       public GameObject pickupCounter;
       public GameObject timer;
       


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
}
