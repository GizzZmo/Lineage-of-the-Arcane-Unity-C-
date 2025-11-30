# Lineage of the Arcane - Game Design Document

## Overview

**Lineage of the Arcane** is a gameplay prototype exploring a unique magic system where spells are sentient entities. Players do not cast magic; they negotiate with ancient "Progenitors" (Parents) to lend their aid.

> "Magic is not a tool. It is a family tree. And you are the youngest child."

---

## Core Concept

In this game, magic is not a resource to be spent—it's a relationship to be maintained. Each magical entity (called a "Parent") has its own personality, demands, and risks. Summoning one creates a "Tether" that drains the player's life force.

---

## Core Mechanics

### 1. The Tether System

**Concept:** Mana does not exist. Summoning drains the user's max health (Physical Tether) and sanity.

**How It Works:**
- When a player summons a Parent, a Tether is formed
- The Tether continuously drains the player's current health
- Different Parents have different drain rates
- If health drops to zero while tethered, the Tether snaps

**Risk/Reward:**
- High-power Parents have higher drain rates
- Players must balance power gained vs. health lost
- Strategic timing of tethering is crucial

### 2. Ancestral Temperament

**Concept:** Summons have specific personalities (Aggressive, Passive, Rhythm-based). Failing to adhere to their playstyle results in the summon damaging the player.

**Personality Types:**
| Type | Requirement | Example Entity |
|------|-------------|----------------|
| Aggressive | Must attack frequently | Ignis Mater |
| Passive | Must avoid combat | Aqua Pater |
| Rhythmic | Must follow patterns | Terra Mater |
| Still | Must wait patiently | Tempus Mater |
| Sacrificial | Must take damage | Dolor Mater |

**Punishment System:**
- Each Parent monitors player behavior
- Violating temperament triggers punishment
- Punishments include damage, debuffs, or instant tether break

### 3. Environmental Shifts

**Concept:** Summoning a Parent changes the physics and lighting of the game map globally.

**Examples:**
- **Ignis Mater:** Ambient light turns red, floor becomes hazardous
- **Aqua Pater:** World becomes flooded, movement slowed
- **Terra Mater:** Gravity increases, structures emerge
- **Tempus Mater:** Time slows, purple temporal effects
- **Dolor Mater:** Crimson pain aura, damage amplification

---

## Entity Hierarchy

### Tier 0: The Parents (Progenitors)
- Most powerful entities
- Highest tether cost
- Global environmental effects
- Complex temperament requirements

### Tier 1: The Scions
- Descendants of Parents
- Moderate tether cost
- Local environmental effects
- Simpler temperament requirements
- Grant bonus affinity to their parent lineage when tethered

### Tier 2: The Heirs
- Weakest magical entities
- Low tether cost
- Minimal environmental effects
- Forgiving temperament
- Build affinity very quickly due to their gentle nature
- Do not go rampant - they simply fade away when tether breaks

---

## The Rampant State

When a Tether breaks unexpectedly (not manually severed), the Parent enters a **Rampant** state:

- The Parent is no longer bound to the player
- It may attack the player or act chaotically
- Environmental effects remain active
- Player must flee or find a way to rebind
- **Note:** Heirs do NOT enter a rampant state due to their gentle nature

---

## Evolution/Affinity System

The Affinity System tracks the relationship between players and magical entities over time. Building strong bonds provides significant gameplay benefits.

### Affinity Levels

| Level | Threshold | Tether Cost Modifier | Description |
|-------|-----------|---------------------|-------------|
| Hostile | <20% with 3+ betrayals | 1.5x (50% increase) | Entity remembers betrayal |
| Stranger | 0-19% | 1.0x (no change) | No relationship established |
| Acquainted | 20-39% | 0.9x (10% reduction) | Beginning to know each other |
| Bonded | 40-69% | 0.8x (20% reduction) | Established trust |
| Devoted | 70-99% | 0.65x (35% reduction) | Deep connection |
| Ascended | 100% | 0.5x (50% reduction) | Maximum affinity, special ability unlocked |

### How Affinity is Gained

- **Continuous Tethering:** +0.5 affinity per second while tethered
- **Temperament Compliance:** +0.2 bonus per second when meeting temperament requirements
- **Clean Sever:** +5 affinity when manually severing the tether safely
- **Heir Bonus:** Heirs grant 1.5x normal affinity gain rate

### How Affinity is Lost

- **Betrayal (Forced Tether Break):** -15 affinity
- **Multiple Betrayals:** After 3 betrayals with low affinity, entity becomes Hostile
- **Heir Betrayal:** Only -5 affinity (they are more forgiving)

### Cross-Lineage Affinity

Tethering with lower-tier entities (Scions and Heirs) grants bonus affinity to their parent lineage:
- **Scions:** Grant 50% of gained affinity to their Parent
- **Heirs:** Grant 30% of gained affinity to their ancestral Parent

This encourages players to build relationships from the bottom up, training with gentle Heirs before attempting to bond with powerful Parents.

### Special Abilities (Ascended Level)

Each Parent unlocks a unique special ability when the player reaches Ascended affinity:

| Entity | Ability Name | Effect | Cooldown |
|--------|-------------|--------|----------|
| Ignis Mater | Inferno Embrace | Temporary invulnerability with AoE damage (5s duration) | 30s |
| Aqua Pater | Tidal Sanctuary | Creates a healing zone restoring 5 HP/sec (8s duration) | 45s |
| Terra Mater | Earthen Bulwark | Protective barrier absorbing 50 damage (10s duration) | 60s |
| Tempus Mater | Temporal Stasis | Freezes time for all except the player (6s duration) | 50s |
| Dolor Mater | Martyrdom | Collects damage taken and releases as AoE explosion (5s collect, 3x multiplier) | 60s |

### Affinity-Based Visual Changes

Entities visually change based on their affinity level with the player:

| Level | Visual Effect |
|-------|---------------|
| Hostile | Dim, shrunk (0.9x scale), dark red aura |
| Stranger | Normal appearance, white aura |
| Acquainted | Slight glow, yellow-white aura |
| Bonded | Moderate glow, green aura |
| Devoted | Strong glow + pulse effect, blue aura |
| Ascended | Maximum glow + strong pulse, golden aura, 1.3x scale |

### Affinity Memory

Parents "remember" their relationship with players:
- Affinity persists across play sessions
- Hostile entities require more effort to rebuild trust
- High affinity from previous sessions provides immediate benefits on re-summoning

---

## Custody Battle (Multiplayer)

A competitive mode where two players attempt to tether the same Parent:
- Both players' health drains faster
- The Parent favors whoever better matches its temperament
- One player eventually "wins" the Parent
- Loser suffers backlash damage

---

## Affinity Competition (Multiplayer)

A new competitive mode where multiple players (2-4) compete to build affinity:

### Scoring System
| Action | Points |
|--------|--------|
| Reach Acquainted | +10 |
| Reach Bonded | +25 |
| Reach Devoted | +50 |
| Reach Ascended | +100 |
| Successful tether | +5 |
| Betrayal | -10 |

### Competition Rules
- Default duration: 5 minutes
- Can target specific entity or compete for overall affinity
- Winner determined by highest score at end
- Can be combined with Custody Battle for more intense gameplay

---

## Achievement System

The achievement system tracks player progress and rewards milestones.

### Achievement Categories

| Category | Example | Reward |
|----------|---------|--------|
| Affinity Milestones | First Ascension | XP + cosmetic unlocks |
| Entity Mastery | Master of Fire | Aura effects |
| Tether Achievements | Master Tetherer (100 tethers) | XP |
| Duration Achievements | Eternal Connection (300s) | XP |
| Special Ability | Inferno Unleashed | XP |
| Challenge | Perfect Partner (10 no-betrayal) | XP |
| Multi-Entity | The Arcane Master | Title unlock |

### Notable Achievements

| Achievement | Requirement | Reward |
|-------------|-------------|--------|
| The Arcane Master | Ascended with all 5 Parents | 500 XP + Title |
| Perfect Partner | 10 tethers without betrayal | 100 XP |
| From the Ashes | Ascended after being Hostile | 200 XP (hidden) |

---

## Technical Architecture

### Script Structure
```
Assets/Scripts/
├── Core/
│   ├── MagicParent.cs        - Abstract base class for all entities
│   ├── TetherSystem.cs       - Manages tether logic and health drain
│   ├── RampantState.cs       - Handles rampant AI behavior
│   ├── AffinitySystem.cs     - Manages player-entity relationships
│   └── AchievementSystem.cs  - Tracks achievements and milestones
├── Entities/
│   ├── IgnisMater.cs         - Fire Mother implementation
│   ├── AquaPater.cs          - Water Father implementation
│   ├── TerraMater.cs         - Earth Mother implementation
│   ├── TempusMater.cs        - Time Mother implementation
│   ├── DolorMater.cs         - Pain Mother implementation
│   └── Tiers/
│       ├── Scion.cs          - Tier 1 base class
│       ├── Heir.cs           - Tier 2 base class
│       ├── EmberScion.cs     - Fire Scion implementation
│       ├── WaveScion.cs      - Water Scion implementation
│       ├── StoneScion.cs     - Earth Scion implementation
│       ├── ChronoScion.cs    - Time Scion implementation
│       ├── WoundScion.cs     - Pain Scion implementation
│       ├── CandlelightHeir.cs - Fire Heir implementation
│       ├── DewdropHeir.cs    - Water Heir implementation
│       ├── PebbleHeir.cs     - Earth Heir implementation
│       ├── MomentHeir.cs     - Time Heir implementation
│       └── ScratchHeir.cs    - Pain Heir implementation
├── Effects/
│   ├── TetherVisualEffect.cs   - Line renderer for tether visualization
│   └── AffinityVisualEffect.cs - Affinity-based entity visuals
├── Audio/
│   ├── AudioManager.cs       - Singleton audio management system
│   └── ParentAudioProfile.cs - Audio configuration for Parents
├── UI/
│   ├── HealthBarUI.cs        - Health bar with burned health overlay
│   ├── SanityIndicatorUI.cs  - Sanity display with peripheral effects
│   ├── TetherDisplayUI.cs    - Tether status and temperament indicator
│   ├── AffinityDisplayUI.cs  - Affinity level and progress display
│   └── AchievementUI.cs      - Achievement display and notifications
├── Multiplayer/
│   ├── CustodyBattle.cs      - Multiplayer tug-of-war system
│   └── AffinityCompetition.cs - Multiplayer affinity competition
└── Player/
    └── PlayerController.cs   - Player state and combat tracking
```

### Class Relationships
```
MagicParent (Abstract)
    ↳ IgnisMater (Tier 0 - Parent, Aggressive)
    ↳ AquaPater (Tier 0 - Parent, Passive)
    ↳ TerraMater (Tier 0 - Parent, Rhythmic)
    ↳ TempusMater (Tier 0 - Parent, Still)
    ↳ DolorMater (Tier 0 - Parent, Sacrificial)
    ↳ Scion (Abstract - Tier 1)
        ↳ EmberScion (Ignis lineage)
        ↳ WaveScion (Aqua lineage)
        ↳ StoneScion (Terra lineage)
        ↳ ChronoScion (Tempus lineage)
        ↳ WoundScion (Dolor lineage)
    ↳ Heir (Abstract - Tier 2)
        ↳ CandlelightHeir (Ignis lineage)
        ↳ DewdropHeir (Aqua lineage)
        ↳ PebbleHeir (Terra lineage)
        ↳ MomentHeir (Tempus lineage)
        ↳ ScratchHeir (Dolor lineage)

AffinitySystem (Singleton)
    → Tracks all player-entity relationships
    → Provides tether cost modifiers
    → Manages affinity events and notifications

AchievementSystem (Singleton)
    → Tracks player achievements
    → Listens to AffinitySystem events
    → Manages achievement unlocks and rewards

AffinityVisualEffect
    → Attached to MagicParent entities
    → Updates visual appearance based on affinity level
    → Manages glow, scale, and particle effects

RampantState
    → Attached to MagicParent
    → Manages rampant behavior

TetherSystem
    → References MagicParent
    → References PlayerController
    → Integrates with AffinitySystem

TetherVisualEffect
    → References TetherSystem
    → Renders visual connection

AudioManager (Singleton)
    → Manages all game audio
    → References ParentAudioProfile

CustodyBattle
    → References MagicParent
    → References multiple PlayerControllers

AffinityCompetition (Singleton)
    → Manages multiplayer affinity competition
    → Tracks competitor scores and standings
    → Integrates with AffinitySystem events

PlayerController
    → Tracked by MagicParent instances

UI Components
    → HealthBarUI → References PlayerController
    → SanityIndicatorUI → References PlayerController
    → TetherDisplayUI → References TetherSystem
    → AffinityDisplayUI → References AffinitySystem, TetherSystem
    → AchievementUI → References AchievementSystem
```

---

## UI Elements

### Health Bar
- Standard red bar for current health
- Grey overlay showing "burned" max health from tethering

### Sanity Indicator
- Peripheral screen effects at low sanity
- Sound distortion effects

### Tether Indicator
- Visual line connecting player to summon
- Color indicates Parent's current temperament status

### Affinity Display
- Shows current affinity level with color coding
- Progress bar to next level
- Special ability icon when unlocked
- Session statistics (tethers, time spent)

### Achievement Display
- Achievement list with unlock status
- Progress bars for incomplete achievements
- Unlock notifications with animations
- Overall completion percentage

---

## Audio Design

- Each Parent has a unique ambient sound
- Temperament violations trigger warning sounds
- Rampant state has intense audio cues
- Environmental shifts affect ambient soundscape
- Affinity level changes trigger notification sounds
- Special ability activation has unique audio cues

---

## Art Direction

- Dark fantasy aesthetic
- Each Parent has a distinct visual identity
- Tether effects glow with Parent's color
- Environmental shifts are dramatic but readable
- Affinity UI uses color progression from gray to gold

---

## Development Roadmap

### Phase 1: Core Mechanics ✅
- [x] Base MagicParent class
- [x] TetherSystem implementation
- [x] PlayerController
- [x] Ignis Mater (first Parent)
- [x] Security scanning (CodeQL)

### Phase 2: Content Expansion ✅
- [x] Additional Parents (Aqua Pater, Terra Mater)
- [x] Tier 1 Scions (Base class + Ember Scion)
- [x] Tier 2 Heirs (Base class + Candlelight Heir)

### Phase 3: Polish ✅
- [x] Visual effects for tethering
- [x] Audio implementation
- [x] UI systems

### Phase 4: Advanced Features ✅
- [x] Custody Battle multiplayer mode
- [x] Evolution/Affinity system
- [x] Rampant AI behaviors
- [x] Special abilities for Ascended affinity
- [x] Cross-lineage affinity bonuses

### Phase 5: Expanded Content ✅
- [x] Additional Parents (Tempus Mater, Dolor Mater)
- [x] More Scions for each lineage (Wave, Stone, Chrono, Wound)
- [x] More Heirs for each lineage (Dewdrop, Pebble, Moment, Scratch)
- [x] Affinity-based visual changes for entities
- [x] Multiplayer affinity competition
- [x] Achievement system tied to affinity milestones

### Phase 6: Future Features (Planned)
- [ ] Quest system
- [ ] Procedural dungeon generation
- [ ] Story campaign mode
- [ ] Entity evolution/upgrade paths
- [ ] New element lineages

---

## License

MIT License - See LICENSE file for details.
