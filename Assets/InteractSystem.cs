using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public bool playerInRange;
    public bool CanInteract;
    public GameObject Intsystem;

    // Update is called once per frame
    void Update()
    {   
        //find the "Player" gameobject so we can call the script (had to do it this way because if we didnt it would get the prefabs script and not the actual player)
        Intsystem = GameObject.Find("playerV2(Clone)");
        //Defines playerInRange, makes the inRange in player int system the same as in this script
        //Makes it where we dont need to define each interactable object in the player script
        playerInRange = Intsystem.GetComponent<PlayerPickupSystem>().inRange;

            //if player is close enough to see the interact button
        if (playerInRange == true && CanInteract == true)
        {
            
        }	
        else
        {
                
        }
    }
    //check to see if the player is in range of this gameobject (this is so we can have multiple interact buttons)
    void OnTriggerStay2D(Collider2D Other)
    {
        GameObject triggeringObject = Other.gameObject;

        if (triggeringObject.CompareTag("Player"))
        {
            CanInteract = true;
        } 

    }
    void OnTriggerExit2D()
    {
        CanInteract = false;
    }
}

