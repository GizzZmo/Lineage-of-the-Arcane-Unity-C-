using UnityEngine;

/// <summary>
/// Chrono Scion - A Tier 1 descendant of Tempus Mater.
/// Less demanding than Tempus Mater but still prefers patience.
/// Tethering with Chrono also builds affinity with Tempus Mater.
/// </summary>
public class ChronoScion : Scion
{
    [Header("Chrono Settings")]
    public float actionCooldown = 3.0f; // More lenient than Tempus Mater
    public float basePunishmentDamage = 2.5f;
    public Color localAmbientColor = new Color(0.5f, 0.4f, 0.7f); // Soft violet
    public float localTimeSlowMultiplier = 0.85f; // Less time slow than Tempus
    
    private float lastActionTime;
    private float lastTemperamentCheckTime;
    private float originalTimeScale;
    
    private void Start()
    {
        entityName = "Chrono, The Moment";
        entityId = "ChronoScion";
        parentLineage = "Tempus Mater";
        tetherCostPerSecond = 5.0f; // Moderate cost
        
        // Configure less severe rampant behavior
        rampantBehavior = RampantBehavior.Chaotic;
        rampantDuration = 15f;
        rampantDamage = 9f;
        
        ConfigureRampantState();
    }
    
    protected override void ApplyLocalEnvironmentalShift()
    {
        // Apply a localized time effect
        Debug.Log("Time shimmers around Chrono. Moments seem to stretch.");
        
        // Subtle ambient light change
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, localAmbientColor, 0.3f);
        
        // Slight local time slow
        originalTimeScale = Time.timeScale;
        Time.timeScale = localTimeSlowMultiplier;
    }
    
    protected override void PerformTemperamentCheck()
    {
        if (boundPlayer == null) return;
        
        // Only check periodically
        if (Time.time - lastTemperamentCheckTime < temperamentTolerance)
        {
            return;
        }
        
        lastTemperamentCheckTime = Time.time;
        
        // Chrono prefers measured, patient actions
        bool actedTooQuickly = (Time.time - boundPlayer.lastAttackTime) < actionCooldown &&
                              (boundPlayer.lastAttackTime - lastActionTime) < actionCooldown;
        
        if (actedTooQuickly)
        {
            ApplyPunishment(basePunishmentDamage);
            Debug.Log("Chrono flickers in disapproval of your haste.");
        }
        else
        {
            ApproveTemperament();
        }
        
        if (boundPlayer.lastAttackTime > lastActionTime)
        {
            lastActionTime = boundPlayer.lastAttackTime;
        }
    }
    
    public override void OnTetherBroken()
    {
        // Restore time scale
        if (originalTimeScale > 0)
        {
            Time.timeScale = originalTimeScale;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
        base.OnTetherBroken();
        Debug.Log("Chrono splinters into temporal fragments!");
        
        // Reset ambient light effect
        RenderSettings.ambientLight = Color.white * 0.5f;
    }
    
    /// <summary>
    /// Gets the parent entity ID for this scion.
    /// </summary>
    protected override string GetParentEntityId()
    {
        // Override to map to new parent
        if (parentLineage == "Tempus Mater" || parentLineage == "Tempus")
        {
            return "TempusMater";
        }
        return base.GetParentEntityId();
    }
}
