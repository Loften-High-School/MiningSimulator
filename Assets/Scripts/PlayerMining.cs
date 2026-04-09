using UnityEngine;

public class PlayerMining : MonoBehaviour {
    public float mineRange = 2f;
    public LayerMask oreLayer;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) { // Press Space to mine
            Mine();
        }
    }

    void Mine() {
        // Check for ores in a small circle around the player
        Collider2D oreHit = Physics2D.OverlapCircle(transform.position, mineRange, oreLayer);
        
        if (oreHit != null) {
            Ore ore = oreHit.GetComponent<Ore>();
            if (ore != null) {
                ore.TakeDamage(1);
            }
        }
    }
}
