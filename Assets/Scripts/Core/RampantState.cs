using UnityEngine;

/// <summary>
/// Defines the behavior states for Rampant entities.
/// When a Tether breaks unexpectedly, the Parent enters a Rampant state.
/// </summary>
public enum RampantBehavior
{
    Aggressive,   // Attacks the nearest target including the player
    Chaotic,      // Moves and attacks randomly
    Vengeful,     // Specifically targets the player who broke the tether
    Destructive   // Destroys environment objects
}

/// <summary>
/// Handles the Rampant state behavior when a Parent's tether is broken.
/// The Rampant state makes the Parent act independently and often hostilely.
/// </summary>
public class RampantState : MonoBehaviour
{
    [Header("Rampant Configuration")]
    public RampantBehavior behavior = RampantBehavior.Aggressive;
    public float rampantDuration = 30f;        // How long the rampant state lasts
    public float attackInterval = 2f;           // Time between attacks
    public float damagePerAttack = 15f;         // Damage dealt per attack
    public float detectionRadius = 10f;         // Range to detect targets
    public float moveSpeed = 4f;                // Movement speed while rampant
    
    [Header("State Tracking")]
    public bool isRampant = false;
    public float rampantStartTime;
    public Transform lastKnownPlayerPosition;
    
    [Header("Performance Settings")]
    public float targetRefreshInterval = 0.5f; // Time between target searches
    
    private MagicParent parentEntity;
    private float lastAttackTime;
    private float lastTargetRefreshTime;
    private Transform currentTarget;
    
    void Start()
    {
        parentEntity = GetComponent<MagicParent>();
    }
    
    void Update()
    {
        if (isRampant)
        {
            UpdateRampantBehavior();
            CheckRampantExpiry();
        }
    }
    
    /// <summary>
    /// Triggers the Rampant state for this entity.
    /// </summary>
    /// <param name="playerPosition">The last known position of the player.</param>
    public void EnterRampantState(Transform playerPosition)
    {
        isRampant = true;
        rampantStartTime = Time.time;
        lastKnownPlayerPosition = playerPosition;
        
        Debug.LogWarning($"[RAMPANT] {parentEntity?.entityName ?? "Entity"} has entered Rampant state!");
        Debug.LogWarning($"[RAMPANT] Behavior: {behavior}, Duration: {rampantDuration}s");
        
        OnRampantEnter();
    }
    
    /// <summary>
    /// Called when entering rampant state. Override for custom effects.
    /// </summary>
    protected virtual void OnRampantEnter()
    {
        // Environmental intensification when going rampant
        Debug.Log("[RAMPANT] Environmental effects intensifying...");
    }
    
    /// <summary>
    /// Updates the entity's behavior while in rampant state.
    /// </summary>
    void UpdateRampantBehavior()
    {
        switch (behavior)
        {
            case RampantBehavior.Aggressive:
                PerformAggressiveBehavior();
                break;
            case RampantBehavior.Chaotic:
                PerformChaoticBehavior();
                break;
            case RampantBehavior.Vengeful:
                PerformVengefulBehavior();
                break;
            case RampantBehavior.Destructive:
                PerformDestructiveBehavior();
                break;
        }
    }
    
    void PerformAggressiveBehavior()
    {
        // Find and attack nearest target (with caching to reduce performance impact)
        if (Time.time - lastTargetRefreshTime > targetRefreshInterval)
        {
            FindNearestTarget();
            lastTargetRefreshTime = Time.time;
        }
        MoveTowardsTarget();
        TryAttackTarget();
    }
    
    void PerformChaoticBehavior()
    {
        // Random movement and occasional attacks
        if (Random.value > 0.7f)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
            ).normalized;
            
            transform.position += randomDirection * moveSpeed * Time.deltaTime;
        }
        
        if (Time.time - lastAttackTime > attackInterval && Random.value > 0.5f)
        {
            PerformAreaAttack();
        }
    }
    
    void PerformVengefulBehavior()
    {
        // Specifically target the player
        if (lastKnownPlayerPosition != null)
        {
            currentTarget = lastKnownPlayerPosition;
            MoveTowardsTarget();
            TryAttackTarget();
        }
    }
    
    void PerformDestructiveBehavior()
    {
        // Destroy environment objects (placeholder for actual implementation)
        // Destructive behavior doesn't require a target - it damages the area
        if (currentTarget != null)
        {
            MoveTowardsTarget();
        }
        if (Time.time - lastAttackTime > attackInterval)
        {
            PerformAreaAttack();
        }
    }
    
    void FindNearestTarget()
    {
        // Find all potential targets within detection radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        
        float nearestDistance = float.MaxValue;
        Transform nearestTarget = null;
        
        foreach (Collider col in colliders)
        {
            // Look for players or other valid targets
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = col.transform;
                }
            }
        }
        
        currentTarget = nearestTarget ?? lastKnownPlayerPosition;
    }
    
    void MoveTowardsTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            direction.y = 0; // Keep on ground level
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    
    void TryAttackTarget()
    {
        if (currentTarget == null) return;
        
        float distance = Vector3.Distance(transform.position, currentTarget.position);
        
        if (distance < 2f && Time.time - lastAttackTime > attackInterval)
        {
            PerformAttack(currentTarget);
        }
    }
    
    void PerformAttack(Transform target)
    {
        lastAttackTime = Time.time;
        
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damagePerAttack);
            Debug.Log($"[RAMPANT] {parentEntity?.entityName ?? "Entity"} attacks player for {damagePerAttack} damage!");
        }
    }
    
    void PerformAreaAttack()
    {
        lastAttackTime = Time.time;
        
        // Damage all players in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius / 2f);
        
        foreach (Collider col in colliders)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damagePerAttack * 0.5f);
                Debug.Log($"[RAMPANT] Area attack hits player for {damagePerAttack * 0.5f} damage!");
            }
        }
    }
    
    void CheckRampantExpiry()
    {
        if (Time.time - rampantStartTime > rampantDuration)
        {
            ExitRampantState();
        }
    }
    
    /// <summary>
    /// Exits the Rampant state and returns to normal behavior or despawns.
    /// </summary>
    public void ExitRampantState()
    {
        isRampant = false;
        Debug.Log($"[RAMPANT] {parentEntity?.entityName ?? "Entity"} rampant state has ended.");
        OnRampantExit();
    }
    
    /// <summary>
    /// Called when exiting rampant state. Override for custom effects.
    /// </summary>
    protected virtual void OnRampantExit()
    {
        // Entity fades away or becomes dormant
        Debug.Log("[RAMPANT] Entity becomes dormant...");
        // In a full implementation, this would trigger a fade-out or despawn
    }
    
    /// <summary>
    /// Allows a player to attempt to re-bind the rampant entity.
    /// </summary>
    /// <param name="player">The player attempting to rebind.</param>
    /// <returns>True if rebinding was successful.</returns>
    public bool AttemptRebind(PlayerController player)
    {
        if (!isRampant) return false;
        
        // Rebind chance based on player's current state
        float rebindChance = player.currentHealth / player.maxHealth;
        
        if (Random.value < rebindChance)
        {
            ExitRampantState();
            Debug.Log($"[RAMPANT] Player successfully rebinds {parentEntity?.entityName ?? "Entity"}!");
            return true;
        }
        else
        {
            // Failed rebind causes damage
            player.TakeDamage(damagePerAttack * 0.5f);
            Debug.Log("[RAMPANT] Rebind attempt failed! The entity lashes out!");
            return false;
        }
    }
}
