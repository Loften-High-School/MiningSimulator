using UnityEngine;

public class PlayerPickupSystem : MonoBehaviour
{
    [SerializeField] Transform IntertactCheckPoint;
    [SerializeField] Vector2 IntertactRange = new Vector2(1.68f, 1.61f);
    [SerializeField] LayerMask DroppedItemsLayer;
    [SerializeField] LayerMask ShopLayer;

    public bool inRange;
 
    void Start()
    {
        
    }

    void Update()
    {
        //Interact Check
		if (Physics2D.OverlapBox(IntertactCheckPoint.position, IntertactRange, 0, DroppedItemsLayer) || Physics2D.OverlapBox(IntertactCheckPoint.position, IntertactRange, 0, ShopLayer)) //checks if their is an Interactible Object in range
		{
			inRange = true;
        }	
        else
        {
            inRange = false;
        }
    }

    //Creates the shape of the range when the player object is selected (Easier to see and to move)
    private void OnDrawGizmosSelected()
    {
        //makes the lines blue
        Gizmos.color = Color.blue;
        //Draws the lines
		Gizmos.DrawWireCube(IntertactCheckPoint.position, IntertactRange);
    }
}
