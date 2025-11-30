using UnityEngine;

/// <summary>
/// Wave Scion - A Tier 1 descendant of Aqua Pater.
/// Less demanding than Aqua Pater but still requires passivity.
/// Tethering with Wave also builds affinity with Aqua Pater.
/// </summary>
public class WaveScion : Scion
{
    [Header("Wave Settings")]
    public float aggressionCooldown = 3.0f; // More lenient than Aqua Pater
    public float basePunishmentDamage = 2.0f;
    public Color localAmbientColor = new Color(0.4f, 0.6f, 0.9f); // Soft blue
    
    private float lastAggressionTime;
    private float lastTemperamentCheckTime;
    
    private void Start()
    {
        entityName = "Wave, The Ripple";
        entityId = "WaveScion";
        parentLineage = "Aqua Pater";
        tetherCostPerSecond = 4.0f; // Lower base cost than Aqua
        
        // Configure less severe rampant behavior
        rampantBehavior = RampantBehavior.Chaotic;
        rampantDuration = 12f;
        rampantDamage = 8f;
        
        ConfigureRampantState();
    }
    
    protected override void ApplyLocalEnvironmentalShift()
    {
        // Apply a localized water effect
        Debug.Log("A gentle mist surrounds Wave. The air becomes humid.");
        
        // Subtle ambient light change (less dramatic than Aqua)
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, localAmbientColor, 0.3f);
    }
    
    protected override void PerformTemperamentCheck()
    {
        if (boundPlayer == null) return;
        
        // Only check periodically (more tolerant)
        if (Time.time - lastTemperamentCheckTime < temperamentTolerance)
        {
            return;
        }
        
        lastTemperamentCheckTime = Time.time;
        
        // Check if player has attacked recently - Wave prefers calm
        if (Time.time - boundPlayer.lastAttackTime < aggressionCooldown)
        {
            if (Time.time - lastAggressionTime > 1.0f)
            {
                ApplyPunishment(basePunishmentDamage);
                lastAggressionTime = Time.time;
                Debug.Log("Wave retreats from your aggression.");
            }
        }
        else
        {
            ApproveTemperament();
        }
    }
    
    public override void OnTetherBroken()
    {
        base.OnTetherBroken();
        Debug.Log("Wave dissipates into mist!");
        
        // Reset ambient light effect
        RenderSettings.ambientLight = Color.white * 0.5f;
    }
}
