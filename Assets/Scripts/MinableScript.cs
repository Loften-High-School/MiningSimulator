using System;
using UnityEngine;

public class MinableScript : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs
    public int minAmount = 2;
    public int maxAmount = 7;
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
            SpawnDrops(UnityEngine.Random.Range(minAmount, maxAmount + 1));
        }
	
    }

    // Detects when the mouse clicks on this object's collider
    private void OnMouseDown()                
    {
        Debug.Log(gameObject.name + " was clicked!");
        Health -= 10;
    }

    

    void SpawnDrops(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 MoveAmount = new Vector3 (UnityEngine.Random.Range(-1.5f, 1.5f), 0f,0f);
            Vector3 SlightMove = MoveAmount + gameObject.transform.position;
            
            GameObject prefab = objectsToSpawn[UnityEngine.Random.Range(0, objectsToSpawn.Length)];
            Instantiate(prefab, SlightMove, gameObject.transform.rotation);
            Debug.Log(amount);
        }
    }
}
