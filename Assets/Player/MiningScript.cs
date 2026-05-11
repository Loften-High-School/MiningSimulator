using UnityEngine;

public class MiningScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    
	
    }

    // Detects when the mouse clicks on this object's collider
    private void OnMouseDown()                
    {
        Debug.Log(gameObject.name + " was clicked!");
    }
}
