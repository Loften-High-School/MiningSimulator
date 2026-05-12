using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(Player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
