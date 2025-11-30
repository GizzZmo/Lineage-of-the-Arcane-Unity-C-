using UnityEngine;

/// <summary>
/// Base class for Tier 1 entities - The Scions.
/// Scions are descendants of the Parents with moderate power.
/// They have simpler temperament requirements and local environmental effects.
/// </summary>
public abstract class Scion : MagicParent
{
    [Header("Scion Configuration")]
    public string parentLineage;              // Which Parent they descend from
    public float localEffectRadius = 15f;     // Range of environmental effects
    public float affinityBonus = 0.8f;        // Reduced tether cost multiplier
    
    [Header("Scion Temperament")]
    public float temperamentTolerance = 5f;   // Seconds before temperament check
    public float punishmentMultiplier = 0.5f; // Reduced punishment compared to Parents
    
    protected override void ApplyEnvironmentalShift()
    {
        // Scions have local environmental effects rather than global
        ApplyLocalEnvironmentalShift();
    }
    
    /// <summary>
    /// Applies environmental effects within a limited radius.
    /// Override this to implement specific scion environmental effects.
    /// </summary>
    protected abstract void ApplyLocalEnvironmentalShift();
    
    /// <summary>
    /// Called when the scion is first summoned.
    /// </summary>
    public override void OnSummon(PlayerController player)
    {
        // Apply affinity bonus to reduce tether cost
        tetherCostPerSecond *= affinityBonus;
        
        base.OnSummon(player);
        Debug.Log($"Scion {entityName} of the {parentLineage} lineage has answered the call.");
    }
    
    /// <summary>
    /// Scions have simpler temperament requirements than Parents.
    /// </summary>
    public override void CheckTemperament()
    {
        if (boundPlayer == null) return;
        
        // Scions are more forgiving with temperament checks
        PerformTemperamentCheck();
    }
    
    /// <summary>
    /// Override this to implement specific temperament checking logic.
    /// </summary>
    protected abstract void PerformTemperamentCheck();
    
    /// <summary>
    /// Applies reduced punishment compared to Parents.
    /// </summary>
    protected void ApplyPunishment(float baseDamage)
    {
        if (boundPlayer != null)
        {
            float actualDamage = baseDamage * punishmentMultiplier;
            boundPlayer.TakeDamage(actualDamage);
            Debug.Log($"Scion {entityName} shows displeasure. ({actualDamage} damage)");
        }
    }
    
    public override void OnTetherBroken()
    {
        base.OnTetherBroken();
        // Scions have less severe rampant behavior
        Debug.Log($"Scion {entityName} fades with disappointment rather than rage.");
    }
}
