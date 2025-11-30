using UnityEngine;

/// <summary>
/// Pebble Heir - A Tier 2 descendant of the Terra lineage.
/// Very forgiving and builds bond with the player over time.
/// Tethering with Pebble also builds affinity with Terra Mater and Stone Scion.
/// </summary>
public class PebbleHeir : Heir
{
    [Header("Pebble Settings")]
    public float trembleRadius = 3f;
    public Color earthGlow = new Color(0.7f, 0.6f, 0.4f); // Warm earth tone
    public float bounceSpeed = 2f;
    
    private float lastCheckTime;
    private Vector3 originalPosition;
    private float bounceOffset;
    
    private void Start()
    {
        entityName = "Pebble, The Foundation";
        entityId = "PebbleHeir";
        ancestralLine = "Terra";
        tetherCostPerSecond = 1.5f; // Very low base cost
        
        // Heirs don't go rampant - they fade away
        rampantBehavior = RampantBehavior.Destructive;
        rampantDuration = 3f;
        rampantDamage = 1f;
        
        // Set heir-specific properties
        isForgiving = true;
        bondGrowthRate = 2f; // Bonds quickly
        
        ConfigureRampantState();
        
        originalPosition = transform.position;
    }
    
    protected override void Update()
    {
        base.Update();
        
        // Pebble bounces gently
        UpdateBounce();
    }
    
    void UpdateBounce()
    {
        // Gentle hovering effect
        bounceOffset = Mathf.Sin(Time.time * bounceSpeed) * 0.1f;
        transform.position = originalPosition + new Vector3(0, bounceOffset, 0);
    }
    
    protected override void ApplyMinimalEnvironmentalShift()
    {
        // Create a subtle earth effect
        Debug.Log("Tiny particles of earth orbit around Pebble. You feel grounded.");
        
        // Subtle ambient change
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, earthGlow, 0.1f);
    }
    
    protected override void PerformTemperamentCheck()
    {
        // Pebble has no strict temperament requirements
        // It simply appreciates steady presence
        
        if (boundPlayer == null) return;
        
        if (Time.time - lastCheckTime < temperamentGracePeriod)
        {
            return;
        }
        
        lastCheckTime = Time.time;
        
        // Pebble is always content
        Debug.Log("Pebble rolls happily nearby.");
    }
    
    protected override void PerformGentleTemperamentCheck()
    {
        // Pebble is very forgiving - no actual checks
    }
    
    public override void OnTetherBroken()
    {
        Debug.Log("Pebble crumbles into sand, leaving a small pile behind...");
        
        // Reset position
        transform.position = originalPosition;
        
        base.OnTetherBroken();
    }
    
    /// <summary>
    /// Provides a stabilizing effect when bond is strong.
    /// </summary>
    public void ProvideStability()
    {
        if (boundPlayer != null && bondStrength > 40f)
        {
            // Could provide stability buff (reduced knockback, etc.)
            Debug.Log($"Pebble's presence makes you feel more grounded.");
        }
    }
    
    /// <summary>
    /// Gets the ancestral parent ID for this heir.
    /// </summary>
    protected override string GetAncestralParentId()
    {
        if (ancestralLine == "Terra" || ancestralLine == "Earth")
        {
            return "TerraMater";
        }
        return base.GetAncestralParentId();
    }
}
