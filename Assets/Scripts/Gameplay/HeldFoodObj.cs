using UnityEngine;

namespace Gameplay
{
    public class HeldFoodObj
    {
        public GameObject foodObject;
        public int foodIndex;
        public string hand;
        public Ticket ticket;

        public HeldFoodObj(GameObject food,int index,string newHand, Ticket passedTicket)
        {
            foodObject = food;
            foodIndex = index;
            hand = newHand;
            ticket = passedTicket;
        }


    }
}
