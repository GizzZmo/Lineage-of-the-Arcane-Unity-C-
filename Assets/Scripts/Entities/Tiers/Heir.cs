using UnityEngine;

/// <summary>
/// Base class for Tier 2 entities - The Heirs.
/// Heirs are the weakest magical entities with forgiving temperaments.
/// They have minimal environmental effects and low tether costs.
/// </summary>
public abstract class Heir : MagicParent
{
    [Header("Heir Configuration")]
    public string ancestralLine;              // The lineage they belong to
    public float effectRange = 5f;            // Very limited effect range
    public float affinityBonus = 0.5f;        // Significantly reduced tether cost
    
    [Header("Heir Temperament")]
    public float temperamentGracePeriod = 10f;  // Long grace period before any check
    public float punishmentMultiplier = 0.2f;   // Very mild punishment
    public bool isForgiving = true;             // Heirs rarely punish
    
    [Header("Growth System")]
    public float bondStrength = 0f;             // Increases with successful tethering
    public float maxBondStrength = 100f;        // Maximum bond level
    public float bondGrowthRate = 1f;           // How fast bond grows per second
    
    protected override void ApplyEnvironmentalShift()
    {
        // Heirs have minimal environmental effects
        ApplyMinimalEnvironmentalShift();
    }
    
    /// <summary>
    /// Applies very subtle environmental effects.
    /// Override this to implement specific heir environmental effects.
    /// </summary>
    protected abstract void ApplyMinimalEnvironmentalShift();
    
    /// <summary>
    /// Called when the heir is summoned.
    /// </summary>
    public override void OnSummon(PlayerController player)
    {
        // Apply significant affinity bonus
        tetherCostPerSecond *= affinityBonus;
        
        base.OnSummon(player);
        Debug.Log($"Heir {entityName} of the {ancestralLine} bloodline emerges timidly.");
    }
    
    protected virtual void Update()
    {
        // Grow bond strength while tethered
        if (boundPlayer != null && bondStrength < maxBondStrength)
        {
            GrowBond();
        }
    }
    
    /// <summary>
    /// Increases the bond strength with the player.
    /// </summary>
    protected void GrowBond()
    {
        bondStrength += bondGrowthRate * Time.deltaTime;
        bondStrength = Mathf.Min(bondStrength, maxBondStrength);
        
        // Reduce tether cost as bond grows
        float bondBonus = 1f - (bondStrength / maxBondStrength * 0.5f);
        // Tether cost reduction applied through affinity system
    }
    
    /// <summary>
    /// Heirs have very forgiving temperament requirements.
    /// </summary>
    public override void CheckTemperament()
    {
        if (boundPlayer == null) return;
        
        if (isForgiving)
        {
            // Heirs only check temperament after a long grace period
            PerformGentleTemperamentCheck();
        }
        else
        {
            PerformTemperamentCheck();
        }
    }
    
    /// <summary>
    /// A gentle temperament check that rarely results in punishment.
    /// </summary>
    protected virtual void PerformGentleTemperamentCheck()
    {
        // By default, heirs are very lenient
        // Override this for specific heir behavior
    }
    
    /// <summary>
    /// Override this to implement specific temperament checking logic.
    /// </summary>
    protected abstract void PerformTemperamentCheck();
    
    /// <summary>
    /// Applies very mild punishment.
    /// </summary>
    protected void ApplyMildPunishment(float baseDamage)
    {
        if (boundPlayer != null && !isForgiving)
        {
            float actualDamage = baseDamage * punishmentMultiplier;
            boundPlayer.TakeDamage(actualDamage);
            Debug.Log($"Heir {entityName} is upset. ({actualDamage} damage)");
        }
        else
        {
            Debug.Log($"Heir {entityName} looks sad but forgives you.");
        }
    }
    
    /// <summary>
    /// Called when the tether breaks.
    /// NOTE: Heirs intentionally do NOT call base.OnTetherBroken() because:
    /// 1. Heirs are too weak to enter a rampant state
    /// 2. They simply fade away instead of becoming hostile
    /// 3. This prevents the RampantState from being triggered
    /// The boundPlayer reference is intentionally not cleared here to allow
    /// for potential rebinding mechanics in the future.
    /// </summary>
    public override void OnTetherBroken()
    {
        // Heirs don't go rampant - they simply fade away
        Debug.Log($"Heir {entityName} fades away with a sad expression...");
        // Intentionally NOT calling base.OnTetherBroken() - see method summary
    }
    
    /// <summary>
    /// Returns the current bond level as a percentage.
    /// </summary>
    public float GetBondPercentage()
    {
        return (bondStrength / maxBondStrength) * 100f;
    }
    
    /// <summary>
    /// Checks if the bond is strong enough for evolution.
    /// </summary>
    public bool CanEvolve()
    {
        return bondStrength >= maxBondStrength;
    }
}
