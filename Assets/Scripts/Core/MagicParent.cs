using UnityEngine;

/// <summary>
/// The Blueprint for all "Parents of Magic".
/// This abstract class defines what a "Parent" is and handles personality traits.
/// </summary>
public abstract class MagicParent : MonoBehaviour
{
    [Header("Entity Stats")]
    public string entityName;
    public float tetherCostPerSecond = 5.0f; // How much Health/Sanity it costs
    [Range(0, 100)] public float defianceLevel = 0f; // Chance to ignore orders

    [Header("Rampant Configuration")]
    public RampantBehavior rampantBehavior = RampantBehavior.Aggressive;
    public float rampantDuration = 30f;
    public float rampantDamage = 15f;

    protected PlayerController boundPlayer;
    protected RampantState rampantState;

    protected virtual void Awake()
    {
        // Ensure RampantState component exists
        rampantState = GetComponent<RampantState>();
        if (rampantState == null)
        {
            rampantState = gameObject.AddComponent<RampantState>();
        }
        
        // Configure rampant state based on this parent's settings
        ConfigureRampantState();
    }

    /// <summary>
    /// Configures the RampantState component with this Parent's settings.
    /// </summary>
    protected virtual void ConfigureRampantState()
    {
        if (rampantState != null)
        {
            rampantState.behavior = rampantBehavior;
            rampantState.rampantDuration = rampantDuration;
            rampantState.damagePerAttack = rampantDamage;
        }
    }

    /// <summary>
    /// Called when the player initiates the Tether.
    /// </summary>
    /// <param name="player">The player forming the tether.</param>
    public virtual void OnSummon(PlayerController player)
    {
        boundPlayer = player;
        Debug.Log($"{entityName} has entered the reality. The Tether is formed.");
        ApplyEnvironmentalShift();
    }

    /// <summary>
    /// Every Parent changes the game world physics/lighting.
    /// </summary>
    protected abstract void ApplyEnvironmentalShift();

    /// <summary>
    /// The unique passive rule (e.g., "Must keep attacking").
    /// Each Parent has different temperament requirements.
    /// </summary>
    public abstract void CheckTemperament();
    
    /// <summary>
    /// Called when the tether breaks.
    /// Triggers the Rampant state behavior.
    /// </summary>
    public virtual void OnTetherBroken()
    {
        Debug.LogWarning($"{entityName} is now RAMPANT! The bond has been severed.");
        
        // Trigger rampant state
        if (rampantState != null && boundPlayer != null)
        {
            rampantState.EnterRampantState(boundPlayer.transform);
        }
        
        // Clear the bound player reference
        boundPlayer = null;
    }

    /// <summary>
    /// Checks if this Parent is currently in a rampant state.
    /// </summary>
    public bool IsRampant()
    {
        return rampantState != null && rampantState.isRampant;
    }

    /// <summary>
    /// Gets the currently bound player.
    /// </summary>
    public PlayerController GetBoundPlayer()
    {
        return boundPlayer;
    }
}
