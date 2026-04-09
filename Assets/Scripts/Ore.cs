using UnityEngine;

public class Ore : MonoBehaviour {
    public int health = 3;
    public int minReward = 5;
    public int maxReward = 15;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            BreakOre();
        }
    }

    void BreakOre() {
        // Generate a random reward value between min (inclusive) and max (exclusive for int)
        int rewardValue = Random.Range(minReward, maxReward);
        Debug.Log("Ore broken! Gained: " + rewardValue);
        
        // Logic to add rewardValue to player's inventory goes here
        
        Destroy(gameObject); // Remove ore from the scene
    }
}
