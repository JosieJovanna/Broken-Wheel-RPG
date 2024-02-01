# Lorendis-RPG-Core
The net standard library to be used by Unity only at the outermost layers: UI, game objects, etc. -- only the physical. All calculations are handled by the core. This is so that a) programming is easier, and b) it can be used in another engine if so desired.


## Stats

### Statistic Code Table

| Stat Name    | Code |
|--------------|------|
| Level        | LVL  |
| Experience   | EXP  |
| Luck         | LUCK |
| HP           | HP   |
| SP           | SP   |
| WP           | WP   |
| Hydration    | DRNK |
| Satiation    | FOOD |
| Rest         | REST |
| Sprint       | SPRN |
| Leap         | JUMP |
| Climb        | CLMB |
| Swim         | SWIM |
| Fortitude    | FORT |
| Evasion      | EVAD |
| Block        | BLCK |
| Parry        | PRRY |
| OneHanded    | 1H   |
| TwoHanded    | 2H   |
| Ranged       | RNGD |
| Unarmed      | UNRM |
| Evocation    | EVOC |
| WildMagic    | WILD |
| Conjuration  | CONJ |
| Faith        | FAI  |
| Transmutation| TRNS |
| Illusion     | ILSN |
| Necromancy   | NCRO |
| Psionics     | PSI  |
| Cooking      | COOK |
| Alchemy      | ALCH |
| Enchantment  | ENCH |
| Inscription  | INSC |
| Perception   | PER  |
| Intuition    | INT  |
| Charisma     | CHA  |
| Deftness     | DEFT |
| Sword        | SWRD |
| Axe          | AXE  |
| Blunt        | BASH |
| Flail        | FLAI |
| SmallArm     | SML  |
| PoleArm      | POLE |
| Throwing     | THRO |
| ShortBow     | SBOW |
| LongBow      | LBOW |
| Trigger      | TRIG |
| Punch        | PNCH |
| Kick         | KICK |
| Grab         | GRAB |
| Qi           | QI   |
| Self         | SELF |
| Touch        | TCH  |
| Projectile   | PROJ |
| AreaOfEffect | AOE  |

## Weapon Types
*Swords*
- (1H) Straight Sword (broadswords, shortswords, etc.)
  - Primary: swing
  - Alt: thrust
  - Adaptive Secondary: block/parry
  - Default Special: bash
  
- (1H) Thrusting Straight Sword
  - Primary: thrust
  - Alt: swing
  - Adaptive Secondary: block/parry
  - Default Special: bash
  
- (1H) Thrusting Sword (rapiers, etc.)
  - Primary: thrust
  - Alt: parry
  
- (1H) Curved Sword (falchion, dao, katana, saber)
  - Primary: swing
  - Adaptive Secondary: block/parry
  - Default Special: bash
  
- (1H) Hooked Sword
  - Primary: swing
  - Alt: pull
  - Adaptive Secondary: parry
  
- (VH) Longsword
  - Extra damage when 2H
  - Primary: swing
  - Alt Primary: swing
  - Special: swap handedness
  - Vers Secondary: block/parry
  
- (VH) Large Curved Sword (dadao, katana, etc.)
  - Extra damage when 2H
  - Primary: swing
  - Special: swap handedness
  - Vers/Adaptive Secondary: block/parry
  
- (2H) Greatsword
  - Primary: swing
  - Alt Primary: thrust
  - Secondary: block/parry
  - Default Special: bash

*Axes*
- (1H) Ax
  - Primary: chop
  - Default Special: bash
  - Adaptive Secondary: block

- (1H) Two-Sided Ax
  - Primary: chop
  - Alt: swing (blunt or spike)
  - Default Special: bash
  - Adaptive Secondary: block
  
- (1H) Spiked Ax
  - Primary: chop
  - Alt: thrust
  - Default Special: bash
  - Adaptive Secondary: block
  
- (1H) Throwable Ax
  - Primary: press to chop, hold to throw (r to cancel)
  
- (VH) Versatile Ax
  - Primary: chop
  - Vers/Adaptive Secondary: block/parry
  - Special: swap handedness
  
- (2H) Greatax
  - Primary: chop
  - Secondary: block/parry
  - Default Special: bash
  
- (2H) Two-Sided Greatax
  - Primary: chop
  - Alt Primary: swing (blunt or spike)
  - Secondary: block/parry
  - Default Special: bash

*Bludgeoning*
- (1H) One-Hand Blunt Weapon (club, small hammer, etc.)
  - Primary: swing
  - Adaptive Secondary: block/parry
  
- (1H) Two-Sided Hammer
  - Primary: swing (blunt)
  - Alt: other swing (usually pierce)
  - Adaptive Secondary: block/parry
  
- ()

*Flails*

*Polearms (2H)*

*Small Arms (1H)*




### Weapon Aspects
Actions:
- Base: Has a primary
- Alt: Has an alternate attack, usually for a two-sided 
- Special: Has a special; usually a bash, but can be overridden
- Versatile: Has a secondary thats used if nothing else is equipped, and when two-handed; damage is increased when 2H
- Two-Handed: Has a secondary attack, or blocks/parries
- Alt Two-Handed: Has a secondary attack, thus block/parry is the other action


