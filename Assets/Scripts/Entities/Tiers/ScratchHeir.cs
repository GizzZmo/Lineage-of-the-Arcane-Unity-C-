using UnityEngine;

/// <summary>
/// Scratch Heir - A Tier 2 descendant of the Dolor lineage.
/// Very forgiving and builds bond with the player over time.
/// Tethering with Scratch also builds affinity with Dolor Mater and Wound Scion.
/// </summary>
public class ScratchHeir : Heir
{
    [Header("Scratch Settings")]
    public float painRadius = 3f;
    public Color painGlow = new Color(0.8f, 0.4f, 0.4f); // Soft red-pink
    public float throb = 1.2f;
    
    private float lastCheckTime;
    private Light pointLight;
    
    private void Start()
    {
        entityName = "Scratch, The Tender";
        entityId = "ScratchHeir";
        ancestralLine = "Dolor";
        tetherCostPerSecond = 1.8f; // Very low base cost
        
        // Heirs don't go rampant - they fade away
        rampantBehavior = RampantBehavior.Vengeful;
        rampantDuration = 3f;
        rampantDamage = 2f;
        
        // Set heir-specific properties
        isForgiving = true;
        bondGrowthRate = 2f; // Bonds well
        
        ConfigureRampantState();
    }
    
    protected override void Update()
    {
        base.Update();
        
        // Scratch throbs gently
        UpdateThrob();
    }
    
    void UpdateThrob()
    {
        if (pointLight != null)
        {
            // Gentle throbbing like a healing wound
            float pulse = 0.7f + Mathf.Sin(Time.time * throb) * 0.3f;
            pointLight.intensity = pulse;
        }
    }
    
    protected override void ApplyMinimalEnvironmentalShift()
    {
        // Create a subtle pain awareness effect
        Debug.Log("A slight ache resonates around Scratch. Pain becomes a companion, not an enemy.");
        
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
        pointLight.color = painGlow;
        pointLight.range = painRadius;
        pointLight.intensity = 0.7f;
        
        Debug.Log("Scratch creates a tender, understanding aura.");
    }
    
    protected override void PerformTemperamentCheck()
    {
        // Scratch has no strict temperament requirements
        // It simply appreciates any small sacrifice
        
        if (boundPlayer == null) return;
        
        if (Time.time - lastCheckTime < temperamentGracePeriod)
        {
            return;
        }
        
        lastCheckTime = Time.time;
        
        // Scratch is always understanding
        Debug.Log("Scratch throbs sympathetically, understanding your hesitation to suffer.");
    }
    
    protected override void PerformGentleTemperamentCheck()
    {
        // Scratch is very forgiving - no actual checks
    }
    
    public override void OnTetherBroken()
    {
        Debug.Log("Scratch fades like a healing wound, leaving only a memory of discomfort...");
        
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }
        
        base.OnTetherBroken();
    }
    
    /// <summary>
    /// Provides an empathetic connection when bond is strong.
    /// </summary>
    public void ProvideEmpathy()
    {
        if (boundPlayer != null && bondStrength > 40f)
        {
            Debug.Log($"Scratch's presence helps you understand that pain is part of growth.");
        }
    }
    
    /// <summary>
    /// Gets the ancestral parent ID for this heir.
    /// </summary>
    protected override string GetAncestralParentId()
    {
        if (ancestralLine == "Dolor" || ancestralLine == "Pain")
        {
            return "DolorMater";
        }
        return base.GetAncestralParentId();
    }
}
