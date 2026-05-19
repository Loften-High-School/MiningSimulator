using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public InteractSystem MyIntSys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MyIntSys.CanInteract == true && MyIntSys.playerInRange == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Open shop");
        }
    }
}
