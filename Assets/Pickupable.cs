using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public bool playerInRange;
    public bool CanPickup;
    public GameObject Intsystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //find the "Player" gameobject so we can call the script (had to do it this way because if we didnt it would get the prefabs script and not the actual player)
        Intsystem = GameObject.Find("playerV2(Clone)");
        //Defines playerInRange, makes the inRange in player int system the same as in this script
        //Makes it where we dont need to define each interactable object in the player script
        playerInRange = Intsystem.GetComponent<PlayerPickupSystem>().inRange;


        //if player is close enough to see the interact button
        if (playerInRange == true && CanPickup == true)
		{
            Debug.Log("item in rnage");
        }	
        else
        {
            
        }
    }
    //check to see if the player is in range of this gameobject (this is so we can have multiple interact buttons)
    void OnTriggerStay2D()
    {
        CanPickup = true;
    }
    void OnTriggerExit2D()
    {
        CanPickup = false;
    }
}
