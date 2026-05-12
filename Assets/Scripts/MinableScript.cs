using System;
using UnityEngine;

public class MinableScript : MonoBehaviour
{
    public GameObject itemPrefab;
    public int Health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (Health <= 0)
        {
            Destroy(gameObject); 
            Instantiate(itemPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
	
    }

    // Detects when the mouse clicks on this object's collider
    private void OnMouseDown()                
    {
        Debug.Log(gameObject.name + " was clicked!");
        Health -= 10;
    }
}
