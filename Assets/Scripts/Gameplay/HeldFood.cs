using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class HeldFood : MonoBehaviour
    {
        public GameObject player;
        public List<GameObject> curHeldFood;
        public Transform left;
        public Transform right;
        private int numHeldFood=0;
        private bool leftHand = false;
        private bool rightHand = false;
        private GameObject foodLeft;
        private GameObject foodRight;
        private bool nothing;

        void Start()
        {
            player = this.gameObject;
            curHeldFood = new List<GameObject>();
        }

        public bool HandsFree()
        {
            if(leftHand == false)
            {
                Debug.Log("HANDs FREE");
                return true;
            }
            else if(rightHand==false)
            {
                Debug.Log("HANDs FREE");
                return true;
            }

            Debug.Log("HANDs NOT FREE");
            return false;
        }

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

        public void AddFood(GameObject food,int index, Ticket passedTicket)
        {
            if(leftHand == false)
            {
                foodLeft = food;
                food.AddComponent<FoodScript>();
                food.GetComponent<FoodScript>().foodObj = food;
                food.GetComponent<FoodScript>().foodData = new HeldFoodObj(food, index,"l", passedTicket);
                curHeldFood.Add(food);
                
                food.transform.parent = left;
                food.transform.position = left.position;
                numHeldFood++;
                leftHand = true;
            }
            else if(rightHand == false)
            {
                foodRight = food;
                food.AddComponent<FoodScript>();
                food.GetComponent<FoodScript>().foodObj = food;
                food.GetComponent<FoodScript>().foodData = new HeldFoodObj(food, index,"r", passedTicket);
                curHeldFood.Add(food);
                
                food.transform.parent = right;
                food.transform.position = right.position;
                numHeldFood++;
                rightHand = true;
            }
        

        }
        public void RemoveFood(GameObject table, int foodItem)
        {
            for(int i = 0; i < curHeldFood.Count; i++)
            {
                if (curHeldFood[i] != null)
                {
                    if (curHeldFood[i].GetComponent<FoodScript>().foodData.ticket.tableRef.GetComponent<TableMgr>().tableNumber == table.GetComponent<TableMgr>().tableNumber)
                    {
                        var tempFood = curHeldFood[i].GetComponent<FoodScript>().foodObj;
                        tempFood.transform.parent = table.gameObject.transform.GetChild(0);
                        tempFood.transform.position = table.gameObject.transform.GetChild(0).position;
                        tempFood.transform.position += new Vector3(0, 1, 0);
                    
                        table.GetComponent<TableMgr>().curFood = tempFood;
                
                        if(curHeldFood[i].GetComponent<FoodScript>().foodData.hand =="r")
                        {
                            rightHand = false;
                        }
                        if (curHeldFood[i].GetComponent<FoodScript>().foodData.hand == "l")
                        {
                            leftHand = false;
                        }
                        curHeldFood.RemoveAt(i);
                        numHeldFood -= 1;
                    }
                }
            }
        }

        public void DestroyFood(int tableNumber)
        {
            GameObject temp = null;

            foreach (var food in curHeldFood)
            {
                if (food != null)
                {
                    if (food.GetComponent<FoodScript>().foodData.ticket.tableRef.GetComponent<TableMgr>().tableNumber == tableNumber)
                    {
                        temp = food;

                        Debug.Log("Left Hand: " + leftHand);
                        Debug.Log("Right Hand: " + rightHand);
                    }
                }
            }

            if (temp != null)
            {
                if (temp.GetComponent<FoodScript>().foodData.hand == "l")
                {
                    Destroy(foodLeft);
                    Destroy(temp);
                    leftHand = false;
                }
                else
                {
                    Destroy(foodRight);
                    rightHand = false;
                }
            }

           
        }
    }
}
