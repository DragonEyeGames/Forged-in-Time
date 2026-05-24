# Forged in Time

**Forged in Time** is a multiplayer RTS Tower Defense game where you take over land and outplay your opponent!

Place down watch towers to quickly take over large swaths of land, turrets to shoot enemies, and much more!

The key feature is **time advancements**, where you can send all of your troops and towers up to the next level. This allows you to quickly gain an advantage over your opponent, but it is expensive, so you have to plan accordingly!

---

# Contributions of Each Team Member

## Ben
Ben set up the core of the game and time upgrades. He got the initial tower placement and blocking, territory, HUD, and many other things running.

He also made it so you could upgrade troops and advance to the next time period. This got us an early start on the project.

---

## Carson
Carson made the majority of the assets in the game. This was a team effort between Nick and Carson, but Carson did the majority of them. He made several different troop types and time advancement levels.

Carson also made the health for the main tower early into the game.

---

## Nick
Nick has made assets, the base for tower selling, start and end menus, pause menu, and level select. Nick made assets in partnership with Carson, most notably the Turret. I think it turned out quite well.

Nick also made various HUD menus such as tower selling, the pause menu, end menu, start menu, and the level select screen. He made some important menus for the game.

---

## Garrett
Even though Ben originally made the enemy system, Garrett completely redid it and made it a lot better. It now is more modular and based on an abstract class.

Garrett also made the miner node, which acts as a way to get the player more money. It can be captured by either player and takes over territory. He also reworked some things to make them more reusable.

---

# How to Test the Gameplay Features

## Towers

We have six different towers in our game:

- Base
- Miner
- Wall
- Spikes
- Watch Tower
- Turret

The miner and base can be seen when starting the game.

The other towers need to be purchased in the HUD to find them. All of their scenes can be found in the `towers/scenes` folder. Their scripts can be found in the `towers/scripts` folder.

### Tower Upgrades

The following towers can be modified:

- Wall
- Turret
- Watch Tower

They are vastly upgraded by clicking the **time advance** button in the HUD.

### Turret Projectiles

We have two different projectiles that the turret can fire:

- Base version fires rocks
- Upgraded version fires arrows

These can be found in the `towers/scenes` and `towers/scripts` folders. Their implementation can be found in the turret scene.

### Placement Rules

Towers can only be placed:

- Inside your territory
- If there is still a valid path to the enemy base

You can test this by:

- Trying to place towers outside of the appropriate colored ground
- Trying to completely encircle your base with walls

---

## Enemies

We have five different enemy types:

- Melee
- Ranged
- Brute
- Healer
- Vehicle

All enemy scenes can be found in the `troops/scenes` folder. Their scripts are in `troops/scripts`.

### Enemy Upgrades

Enemies can receive several modifications:

- Health upgrades
- Speed upgrades
- Attack upgrades
- Full time advancements

These can all be found in the HUD.

### Enemy Stats

Enemies have three core stats:

- Speed
- Health
- Defense

These can be upgraded in the HUD or viewed in the `BaseTroop.cs` file.

### Enemy Pathfinding

Enemies automatically head toward a target when they spawn. This behavior can be seen in the `Recalculate()` function inside the `BaseTroop` class.

### Waves

Players can only hold up to **15 troops** at a time. They must release them afterward, creating a wave-based gameplay loop.

---

# Other Features

## Score System & Leaderboard

We have a scoring mechanism and leaderboard that tracks high scores.

Players earn points for:

- Killing enemy troops
- Destroying the enemy base

Scores can be viewed by pressing the button in the main menu.

Scores automatically save and persist between sessions.

---

## Levels

We have four different playable levels:

- Forest
- Plains
- Tundra
- Desert

These can be selected from the level select scene accessible from the main menu.

All level scenes can also be viewed in the `scenes` folder.

---

## Economy System

The game includes an in-game economy.

Players:

- Can only buy items if they have enough money
- Gain coins over time
- Can capture miner nodes to increase income generation

---

## Pause Menu

Players can pause the game by pressing the `Escape` key.

The pause menu allows players to:

- Resume the game
- Restart the level
- Return to the start screen
- Close the game

---

# How to Test the Programming Concepts

## C# / OOP Concepts

### Enums

We use an enum to store tower and troop types. This can be found in the `GameManager`.

### Generics

We use generics when spawning troops in the `Base.cs` script because all troops share a common class: `BaseTroop`.

### `as` Keyword

We use the `as` keyword in `Turret.cs` so we can safely access troop-specific variables while attacking enemies.

### Access Modifiers

We use private methods and variables in `Turret.cs` because not everything needs to be accessible externally.

### Abstract Classes

`Tower.cs` is an abstract class used for all placeable towers.

The following towers inherit from it:

- Watch Tower
- Spikes
- Turret
- Wall

---

## UI Features

### HUD

We have a HUD that displays important player information, including:

- Health
- Money

It can be seen at the top of the screen during gameplay.

### Button Feedback

Buttons visually indicate whether an action can be performed.

If an action is unavailable, the button becomes darker to communicate this to the player.

### Menus

The game includes:

- Start menu
- Level select screen
- End game screen
- Pause menu

The pause menu can be accessed by pressing `Escape` during gameplay.

### High Scores Scene

Players can view the top 10 highest scores through the main menu.

---

## Godot Features

### Custom Signals

We use a custom signal to declare when a player has died.

`Base.cs` emits the signal, which other scripts use to trigger the end of the game.

### Scene Instantiation

We instantiate objects dynamically when:

- Releasing troops
- Placing towers
- Turrets throw rocks at enemies

### Particle Effects

We use particle effects with the volcano base, which emits smoke periodically.

### UI Resources

We use shared UI resources to style components consistently across scenes.

### Shaders

We use shaders in two places:

- Outline shader when hovering over towers
- Flash shader when troops are hit

---

# Why We Designed Our Enemies and Towers This Way

## Towers

### Wall Tower
A cheap way to reroute enemies. It increases turret effectiveness by keeping enemies nearby longer.

### Turret
The standard single-target defensive tower.

### Spikes
Effective against hordes of enemies. Their small range prevents them from becoming overpowered.

### Watch Tower
Allows rapid territorial expansion and remote land control.

---

## Troops

### Melee
The standard basic troop.

### Ranged
Can attack from a distance, increasing hit reliability. It is weaker to balance this advantage.

### Brute
A slow but powerful troop with high durability.

### Healer
Keeps allied troops alive longer but is fragile and slow.

### Vehicle
Moves quickly across the map and drops two melee troops upon destruction. Expensive, but highly effective for troop transport.
