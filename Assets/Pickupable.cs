using System;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
        public GameObject invmanager;
        public GameObject Itemininv;
        public InteractSystem MyIntSys;

        float slightpickupdelay = 0.7f;
        float time;

    void Start()
    {
        invmanager = GameObject.Find("GameController");
    }

    void Update()
    {

        time = time + Time.deltaTime;

        if(MyIntSys.CanInteract == true && MyIntSys.playerInRange == true)
        {
            PickupDrops();
        }
        

    }
    public void PickupDrops()
    {
        if (invmanager.GetComponent<InventoryController>().EmptySlots.Count > 0)
        {
            if (time >= slightpickupdelay)
            {
                invmanager.GetComponent<InventoryController>().AddItemtoInv(Itemininv);
                Destroy(gameObject);
                Debug.Log("Pick up +1");
            }

        }
        else   
        {
           Debug.Log("inv full");
        }

    }

    
}

