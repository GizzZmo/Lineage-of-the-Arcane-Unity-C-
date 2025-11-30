using UnityEngine;

/// <summary>
/// Stone Scion - A Tier 1 descendant of Terra Mater.
/// Less demanding than Terra Mater but still requires rhythmic actions.
/// Tethering with Stone also builds affinity with Terra Mater.
/// </summary>
public class StoneScion : Scion
{
    [Header("Stone Settings")]
    public float rhythmWindow = 4.0f; // More lenient than Terra Mater
    public float rhythmTolerance = 1.0f; // More forgiving tolerance
    public float basePunishmentDamage = 3.0f;
    public Color localAmbientColor = new Color(0.5f, 0.4f, 0.3f); // Brown-gray
    
    private float lastActionTime;
    private bool hasStartedRhythm = false;
    private int rhythmStreak = 0;
    private float lastTemperamentCheckTime;
    
    private void Start()
    {
        entityName = "Stone, The Rumble";
        entityId = "StoneScion";
        parentLineage = "Terra Mater";
        tetherCostPerSecond = 4.0f; // Lower base cost than Terra
        
        // Configure less severe rampant behavior
        rampantBehavior = RampantBehavior.Destructive;
        rampantDuration = 15f;
        rampantDamage = 12f;
        
        ConfigureRampantState();
    }
    
    protected override void ApplyLocalEnvironmentalShift()
    {
        // Apply a localized earth effect
        Debug.Log("Small pebbles begin to hover around Stone. The ground trembles slightly.");
        
        // Subtle ambient light change
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, localAmbientColor, 0.3f);
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
        
        // Check for rhythmic actions with more lenient timing
        if (boundPlayer.lastAttackTime > lastActionTime)
        {
            float interval = boundPlayer.lastAttackTime - lastActionTime;
            
            if (hasStartedRhythm)
            {
                float deviation = Mathf.Abs(interval - rhythmWindow);
                
                if (deviation <= rhythmTolerance)
                {
                    // Good rhythm
                    rhythmStreak++;
                    ApproveTemperament();
                    Debug.Log($"Stone resonates with your rhythm. Streak: {rhythmStreak}");
                }
                else
                {
                    // Rhythm broken but mild punishment
                    ApplyPunishment(basePunishmentDamage);
                    rhythmStreak = 0;
                    Debug.Log("Stone loses the rhythm but forgives you.");
                }
            }
            else
            {
                hasStartedRhythm = true;
                ApproveTemperament();
                Debug.Log("Stone begins to feel your rhythm...");
            }
            
            lastActionTime = boundPlayer.lastAttackTime;
        }
        
        // Lenient timeout - only punish after long silence
        if (hasStartedRhythm && Time.time - lastActionTime > rhythmWindow * 2f)
        {
            ApplyPunishment(basePunishmentDamage * 0.5f);
            hasStartedRhythm = false;
            rhythmStreak = 0;
            Debug.Log("Stone grows silent waiting for your rhythm.");
        }
    }
    
    public override void OnTetherBroken()
    {
        base.OnTetherBroken();
        Debug.Log("Stone crumbles into gravel!");
        
        // Reset ambient light effect
        RenderSettings.ambientLight = Color.white * 0.5f;
    }
    
    /// <summary>
    /// Gets the current rhythm streak count.
    /// </summary>
    public int GetRhythmStreak()
    {
        return rhythmStreak;
    }
}
