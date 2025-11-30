using UnityEngine;

/// <summary>
/// Moment Heir - A Tier 2 descendant of the Tempus lineage.
/// Very forgiving and builds bond with the player over time.
/// Tethering with Moment also builds affinity with Tempus Mater and Chrono Scion.
/// </summary>
public class MomentHeir : Heir
{
    [Header("Moment Settings")]
    public float temporalRadius = 3f;
    public Color timeGlow = new Color(0.7f, 0.6f, 0.9f); // Soft lavender
    public float pulseSpeed = 0.8f;
    
    private float lastCheckTime;
    private Light pointLight;
    
    private void Start()
    {
        entityName = "Moment, The Fleeting";
        entityId = "MomentHeir";
        ancestralLine = "Tempus";
        tetherCostPerSecond = 1.8f; // Very low base cost
        
        // Heirs don't go rampant - they fade away
        rampantBehavior = RampantBehavior.Chaotic;
        rampantDuration = 2f;
        rampantDamage = 1f;
        
        // Set heir-specific properties
        isForgiving = true;
        bondGrowthRate = 1.8f; // Bonds well
        
        ConfigureRampantState();
    }
    
    protected override void Update()
    {
        base.Update();
        
        // Moment pulses with temporal energy
        UpdatePulse();
    }
    
    void UpdatePulse()
    {
        if (pointLight != null)
        {
            // Slow, meditative pulse
            float pulse = 0.8f + Mathf.Sin(Time.time * pulseSpeed) * 0.2f;
            pointLight.intensity = pulse;
        }
    }
    
    protected override void ApplyMinimalEnvironmentalShift()
    {
        // Create a subtle time effect
        Debug.Log("Time seems to slow imperceptibly around Moment. Each second feels precious.");
        
        CreateOrUpdateLight();
    }
    
    void CreateOrUpdateLight()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
            if (pointLight == null)
            {
                pointLight = gameObject.AddComponent<Light>();
            }
        }
        
        pointLight.type = LightType.Point;
        pointLight.color = timeGlow;
        pointLight.range = temporalRadius;
        pointLight.intensity = 0.8f;
        
        Debug.Log("Moment creates a serene temporal aura.");
    }
    
    protected override void PerformTemperamentCheck()
    {
        // Moment has no strict temperament requirements
        // It simply appreciates being present
        
        if (boundPlayer == null) return;
        
        if (Time.time - lastCheckTime < temperamentGracePeriod)
        {
            return;
        }
        
        lastCheckTime = Time.time;
        
        // Moment is always content
        Debug.Log("Moment savors the passing seconds.");
    }
    
    protected override void PerformGentleTemperamentCheck()
    {
        // Moment is very forgiving - no actual checks
    }
    
    public override void OnTetherBroken()
    {
        Debug.Log("Moment fades like a half-remembered dream, leaving only echoes...");
        
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }
        
        base.OnTetherBroken();
    }
    
    /// <summary>
    /// Provides a calming, time-appreciating effect when bond is strong.
    /// </summary>
    public void ProvideTranquility()
    {
        if (boundPlayer != null && bondStrength > 40f)
        {
            Debug.Log($"Moment's presence helps you appreciate the flow of time.");
        }
    }
    
    /// <summary>
    /// Gets the ancestral parent ID for this heir.
    /// </summary>
    protected override string GetAncestralParentId()
    {
        if (ancestralLine == "Tempus" || ancestralLine == "Time")
        {
            return "TempusMater";
        }
        return base.GetAncestralParentId();
    }
}
