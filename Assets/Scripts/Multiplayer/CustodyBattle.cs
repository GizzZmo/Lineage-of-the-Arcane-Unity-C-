using UnityEngine;

/// <summary>
/// Manages the "Custody Battle" multiplayer mode where two players
/// attempt to tether the same Parent entity simultaneously.
/// </summary>
public class CustodyBattle : MonoBehaviour
{
    [Header("Battle Configuration")]
    public MagicParent contestedParent;
    public float battleDuration = 30f;            // Maximum battle duration
    public float drainMultiplier = 1.5f;          // Health drain is increased during battle
    public float backlashDamage = 25f;            // Damage dealt to the loser
    
    [Header("Contestants")]
    public PlayerController player1;
    public PlayerController player2;
    
    [Header("Battle State")]
    public bool isBattleActive = false;
    public float player1Favor = 50f;              // 0-100 scale
    public float player2Favor = 50f;              // 0-100 scale
    public float battleStartTime;
    
    [Header("Temperament Tracking")]
    public float player1TemperamentScore = 0f;
    public float player2TemperamentScore = 0f;
    
    private TetherSystem player1Tether;
    private TetherSystem player2Tether;
    
    void Update()
    {
        if (isBattleActive)
        {
            UpdateBattle();
            CheckBattleEnd();
        }
    }
    
    /// <summary>
    /// Initiates a Custody Battle between two players for a Parent.
    /// </summary>
    /// <param name="p1">First player entering the battle.</param>
    /// <param name="p2">Second player entering the battle.</param>
    /// <param name="parent">The Parent entity being contested.</param>
    public void StartCustodyBattle(PlayerController p1, PlayerController p2, MagicParent parent)
    {
        if (p1 == null || p2 == null || parent == null)
        {
            Debug.LogError("Cannot start Custody Battle: Missing participants!");
            return;
        }
        
        player1 = p1;
        player2 = p2;
        contestedParent = parent;
        
        // Reset favor to neutral
        player1Favor = 50f;
        player2Favor = 50f;
        
        // Reset temperament scores
        player1TemperamentScore = 0f;
        player2TemperamentScore = 0f;
        
        battleStartTime = Time.time;
        isBattleActive = true;
        
        Debug.Log($"[CUSTODY BATTLE] Battle for {parent.entityName} has begun!");
        Debug.Log($"[CUSTODY BATTLE] {player1.name} vs {player2.name}");
        
        OnBattleStart();
    }
    
    /// <summary>
    /// Called when the battle starts. Override for custom effects.
    /// </summary>
    protected virtual void OnBattleStart()
    {
        // Visual/audio cues for battle start
        Debug.Log("[CUSTODY BATTLE] The Parent's attention is divided...");
    }
    
    void UpdateBattle()
    {
        // Apply increased health drain to both players
        ApplyBattleDrain();
        
        // Update favor based on temperament adherence
        UpdateFavorBasedOnTemperament();
        
        // Update favor based on current actions
        UpdateFavorBasedOnActions();
        
        // Clamp favor values to ensure they stay non-negative
        player1Favor = Mathf.Max(0f, player1Favor);
        player2Favor = Mathf.Max(0f, player2Favor);
        
        // Normalize favor (should always add up to 100)
        float total = player1Favor + player2Favor;
        if (total > 0.001f) // Use small epsilon to avoid floating point issues
        {
            player1Favor = (player1Favor / total) * 100f;
            player2Favor = (player2Favor / total) * 100f;
        }
        else
        {
            // If both are zero, reset to neutral
            player1Favor = 50f;
            player2Favor = 50f;
        }
    }
    
    void ApplyBattleDrain()
    {
        float baseDrain = contestedParent.tetherCostPerSecond * drainMultiplier * Time.deltaTime;
        
        // Drain health from both players
        if (player1.currentHealth > 0)
        {
            player1.currentHealth -= baseDrain;
        }
        
        if (player2.currentHealth > 0)
        {
            player2.currentHealth -= baseDrain;
        }
    }
    
    void UpdateFavorBasedOnTemperament()
    {
        // The Parent favors whoever better matches its temperament
        // This is tracked externally and fed into the temperament scores
        
        float temperamentDifference = player1TemperamentScore - player2TemperamentScore;
        float favorShift = temperamentDifference * Time.deltaTime * 5f;
        
        player1Favor += favorShift;
        player2Favor -= favorShift;
    }
    
    void UpdateFavorBasedOnActions()
    {
        // Additional favor adjustments based on player actions
        // This would hook into combat/action systems
    }
    
    /// <summary>
    /// Reports a temperament action from a player.
    /// Call this when a player performs an action that matches the Parent's temperament.
    /// </summary>
    /// <param name="player">The player performing the action.</param>
    /// <param name="score">The temperament score for this action.</param>
    public void ReportTemperamentAction(PlayerController player, float score)
    {
        if (!isBattleActive) return;
        
        if (player == player1)
        {
            player1TemperamentScore += score;
            Debug.Log($"[CUSTODY BATTLE] Player 1 gains {score} temperament points.");
        }
        else if (player == player2)
        {
            player2TemperamentScore += score;
            Debug.Log($"[CUSTODY BATTLE] Player 2 gains {score} temperament points.");
        }
    }
    
    void CheckBattleEnd()
    {
        bool player1Dead = player1.currentHealth <= 0;
        bool player2Dead = player2.currentHealth <= 0;
        bool timeExpired = Time.time - battleStartTime > battleDuration;
        bool decisiveVictory = player1Favor >= 100f || player2Favor >= 100f;
        
        if (player1Dead || player2Dead || timeExpired || decisiveVictory)
        {
            EndBattle();
        }
    }
    
    void EndBattle()
    {
        isBattleActive = false;
        
        PlayerController winner;
        PlayerController loser;
        
        // Determine winner based on favor (or survival)
        if (player1.currentHealth <= 0)
        {
            winner = player2;
            loser = player1;
        }
        else if (player2.currentHealth <= 0)
        {
            winner = player1;
            loser = player2;
        }
        else if (player1Favor > player2Favor)
        {
            winner = player1;
            loser = player2;
        }
        else
        {
            winner = player2;
            loser = player1;
        }
        
        Debug.Log($"[CUSTODY BATTLE] {winner.name} wins custody of {contestedParent.entityName}!");
        
        // Apply backlash damage to loser
        ApplyBacklash(loser);
        
        // Award the Parent to the winner
        AwardParentToWinner(winner);
        
        OnBattleEnd(winner, loser);
    }
    
    void ApplyBacklash(PlayerController loser)
    {
        loser.TakeDamage(backlashDamage);
        Debug.Log($"[CUSTODY BATTLE] {loser.name} suffers backlash damage!");
    }
    
    void AwardParentToWinner(PlayerController winner)
    {
        // Create a proper tether between the winner and the Parent
        TetherSystem winnerTether = winner.GetComponent<TetherSystem>();
        if (winnerTether != null)
        {
            winnerTether.InitiateTether(contestedParent);
        }
        
        Debug.Log($"[CUSTODY BATTLE] {contestedParent.entityName} is now bound to {winner.name}.");
    }
    
    /// <summary>
    /// Called when the battle ends. Override for custom effects.
    /// </summary>
    protected virtual void OnBattleEnd(PlayerController winner, PlayerController loser)
    {
        Debug.Log("[CUSTODY BATTLE] The battle has concluded.");
    }
    
    /// <summary>
    /// Gets the current favor percentage for a specific player.
    /// </summary>
    public float GetPlayerFavor(PlayerController player)
    {
        if (player == player1) return player1Favor;
        if (player == player2) return player2Favor;
        return 0f;
    }
    
    /// <summary>
    /// Allows a player to voluntarily withdraw from the battle.
    /// </summary>
    public void WithdrawFromBattle(PlayerController player)
    {
        if (!isBattleActive) return;
        
        Debug.Log($"[CUSTODY BATTLE] {player.name} withdraws from the battle!");
        
        // The withdrawing player loses by default
        if (player == player1)
        {
            player1Favor = 0f;
            player2Favor = 100f;
        }
        else
        {
            player2Favor = 0f;
            player1Favor = 100f;
        }
        
        EndBattle();
    }
}
