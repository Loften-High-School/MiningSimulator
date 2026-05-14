using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    [Header("Mining Settings")]
    public float holdTime = 0.5f;        // Base time required to mine
    public float maxMiningRange = 3.0f;  // Maximum distance the player can mine a block
    public LayerMask blockLayer;         // Assign your block layer here
    public float blockSize = 1f;         // Must match the WorldGen.blockSize
    public Color highlightColor = Color.yellow; // NEW: Color to highlight the selected block

    [Header("Combat Settings")]
    public int attackDamage = 20;
    public float attackRange = 1.5f;

    // --- Internal State ---
    private GameObject targetedBlock;
    private Color originalBlockColor; // NEW: Store the original color
    private float blockHealth;
    private float maxBlockHealth = 100f;
    private Vector2Int targetedGridPos;

    void Update()
    {
        bool isMiningInputHeld = Input.GetMouseButton(0);
        bool isAttackInputPressed = Input.GetMouseButtonDown(1);

        HandleMining(isMiningInputHeld);

        if (isAttackInputPressed)
        {
            HandleAttack();
        }
    }

    void HandleMining(bool inputHeld)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = WorldToGrid(mouseWorldPos);

        if (targetedBlock != null && targetedBlock.CompareTag("Player"))
        {
            ResetMining();
            return;
        }

        if (inputHeld)
        {
            if (targetedBlock == null || gridPos != targetedGridPos)
            {
                AcquireNewTarget(gridPos);
            }

            if (targetedBlock != null)
            {
                MineBlock();
            }
        }
        else
        {
            ResetMining();
        }
    }

    void AcquireNewTarget(Vector2Int gridPos)
    {
        // 1. Check if the block is within mining range
        Vector3 blockWorldPos = GridToWorld(gridPos);
        float distance = Vector3.Distance(transform.position, blockWorldPos);

        if (distance > maxMiningRange + (blockSize / 2f))
        {
            ResetMining();
            return;
        }

        // 2. Raycast/OverlapCircle to confirm a block exists at the grid position
        Collider2D hit = Physics2D.OverlapCircle(blockWorldPos, blockSize / 2f, blockLayer);

        if (hit != null)
        {
            GameObject block = hit.gameObject;

            // 3. Protection checks
            if (block.CompareTag("Player") || block.CompareTag("Bedrock"))
            {
                ResetMining();
                return;
            }

            // 4. Successful target acquisition
            targetedBlock = block;
            targetedGridPos = gridPos;

            maxBlockHealth = 100f;
            blockHealth = maxBlockHealth;

            // 5. ⭐ HIGHLIGHT LOGIC (NEW) ⭐
            SpriteRenderer sr = targetedBlock.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                originalBlockColor = sr.color; // Save the original color
                sr.color = highlightColor;     // Set the new highlight color
            }
        }
        else
        {
            ResetMining();
        }
    }

    void MineBlock()
    {
        if (targetedBlock == null)
        {
            ResetMining();
            return;
        }

        float miningPower = (maxBlockHealth / holdTime) * Time.deltaTime;
        blockHealth -= miningPower;

        if (blockHealth <= 0)
        {
            // Restore color before destroying to avoid errors if the script resets later
            // (Though technically Destroy() makes this less critical, it's good practice)
            ResetBlockColor();
            Destroy(targetedBlock);
            ResetMining();
        }
    }

    void ResetMining()
    {
        // ⭐ RESTORE COLOR LOGIC (NEW) ⭐
        if (targetedBlock != null)
        {
            ResetBlockColor();
        }

        targetedBlock = null;
        blockHealth = 0f;
    }

    // NEW: Helper function to safely restore the block's original color
    void ResetBlockColor()
    {
        if (targetedBlock != null)
        {
            SpriteRenderer sr = targetedBlock.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = originalBlockColor;
            }
        }
    }

    void HandleAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Assuming "Health" component exists
                hit.GetComponent<Health>()?.TakeDamage(attackDamage);
            }
        }
    }

    // --- Grid Utility Functions ---

    Vector2Int WorldToGrid(Vector2 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / blockSize);
        int y = Mathf.FloorToInt(worldPos.y / blockSize);
        return new Vector2Int(x, y);
    }

    Vector3 GridToWorld(Vector2Int gridPos)
    {
        float x = gridPos.x * blockSize + blockSize / 2f;
        float y = gridPos.y * blockSize + blockSize / 2f;
        return new Vector3(x, y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxMiningRange);
    }
}