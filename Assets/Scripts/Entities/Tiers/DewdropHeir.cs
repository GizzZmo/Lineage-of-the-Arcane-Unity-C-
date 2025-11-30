using UnityEngine;

/// <summary>
/// Dewdrop Heir - A Tier 2 descendant of the Aqua lineage.
/// Very forgiving and builds bond with the player over time.
/// Tethering with Dewdrop also builds affinity with Aqua Pater and Wave Scion.
/// </summary>
public class DewdropHeir : Heir
{
    [Header("Dewdrop Settings")]
    public float mistRadius = 4f;
    public Color dewGlow = new Color(0.6f, 0.8f, 1f); // Soft blue-white
    public float sparkleSpeed = 1.5f;
    
    private float lastCheckTime;
    private Light pointLight;
    
    private void Start()
    {
        entityName = "Dewdrop, The Morning Mist";
        entityId = "DewdropHeir";
        ancestralLine = "Aqua";
        tetherCostPerSecond = 1.5f; // Very low base cost
        
        // Heirs don't go rampant - they fade away
        rampantBehavior = RampantBehavior.Chaotic;
        rampantDuration = 3f;
        rampantDamage = 1f;
        
        // Set heir-specific properties
        isForgiving = true;
        bondGrowthRate = 2.5f; // Bonds very quickly
        
        ConfigureRampantState();
    }
    
    protected override void Update()
    {
        base.Update();
        
        // Dewdrop sparkles gently
        UpdateSparkle();
    }
    
    void UpdateSparkle()
    {
        if (pointLight != null)
        {
            // Gentle sparkling effect
            float sparkle = 1f + Mathf.Sin(Time.time * sparkleSpeed) * 0.15f;
            pointLight.intensity = sparkle;
        }
    }
    
    protected override void ApplyMinimalEnvironmentalShift()
    {
        // Create a soft mist light effect
        Debug.Log("A cool mist settles around Dewdrop. You feel refreshed.");
        
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
        pointLight.color = dewGlow;
        pointLight.range = mistRadius;
        pointLight.intensity = 0.8f;
        
        Debug.Log("Dewdrop creates a gentle, refreshing aura.");
    }
    
    protected override void PerformTemperamentCheck()
    {
        // Dewdrop has no strict temperament requirements
        // It simply appreciates peaceful moments
        
        if (boundPlayer == null) return;
        
        if (Time.time - lastCheckTime < temperamentGracePeriod)
        {
            return;
        }
        
        lastCheckTime = Time.time;
        
        // Dewdrop is always content
        Debug.Log("Dewdrop glistens peacefully.");
    }
    
    protected override void PerformGentleTemperamentCheck()
    {
        // Dewdrop is very forgiving - no actual checks
    }
    
    public override void OnTetherBroken()
    {
        Debug.Log("Dewdrop evaporates with a quiet sigh, leaving only moisture behind...");
        
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }
        
        base.OnTetherBroken();
    }
    
    /// <summary>
    /// Provides a cooling, refreshing effect when bond is strong.
    /// </summary>
    public void ProvideRefreshment()
    {
        if (boundPlayer != null && bondStrength > 40f)
        {
            // Could provide minor healing or buff
            float refreshAmount = bondStrength / maxBondStrength * 1f;
            Debug.Log($"Dewdrop's cool mist refreshes you.");
        }
    }
    
    /// <summary>
    /// Gets the ancestral parent ID for this heir.
    /// </summary>
    protected override string GetAncestralParentId()
    {
        if (ancestralLine == "Aqua" || ancestralLine == "Water")
        {
            return "AquaPater";
        }
        return base.GetAncestralParentId();
    }
}
