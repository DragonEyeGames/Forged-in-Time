using Godot;
using System;
using System.Collections.Generic;

public partial class Cosmetics : Node
{
	public static Dictionary<GameManager.Towers, String> towerDisplays = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "res://Assets/turretdisplay.png" },
		{ GameManager.Towers.Wall, "res://Assets/TowerArt/CavemanWoodWall.png" },
		{ GameManager.Towers.Watch_Tower, "res://Assets/TowerArt/stick-watchtower.png" },
		{ GameManager.Towers.Spikes, "res://Assets/spikeDisplay.png" },
		{ GameManager.Towers.Melee, "res://Assets/CharacterArt/MeleeCaveman.png" },
		{ GameManager.Towers.Brute, "res://Assets/CharacterArt/BruteCavemanCharacter.png" },
		{ GameManager.Towers.Ranged, "res://Assets/CharacterArt/RangedCavemanCharacter.png" },
		{ GameManager.Towers.Healer, "res://Assets/CharacterArt/HealerCaveman.png" }
	};
	
	public static Dictionary<GameManager.Towers, String> towerNames = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "Turret" },
		{ GameManager.Towers.Wall, "Wall" },
		{ GameManager.Towers.Watch_Tower, "Watch Tower" },
		{ GameManager.Towers.Spikes, "Spikes" },
		{ GameManager.Towers.Melee, "Melee Troop" },
		{ GameManager.Towers.Ranged, "Ranged Troop" },
		{ GameManager.Towers.Brute, "Brute" },
		{ GameManager.Towers.Healer, "Healer" }
	};
	
	public static Dictionary<GameManager.Towers, String> towerDescriptions = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "Shoots at the closest enemy." },
		{ GameManager.Towers.Wall, "It blocks the path." },
		{ GameManager.Towers.Watch_Tower, "Quickly takes over lots of land." },
		{ GameManager.Towers.Spikes, "A 2x2 grid that spawn in spikes to hit enemies." },
		{ GameManager.Towers.Melee, "A troop that attacks with a sword. Stab" },
		{ GameManager.Towers.Ranged, "A troop that can attack from a distance." },
		{ GameManager.Towers.Brute, "A buff guy. Strong" },
		{ GameManager.Towers.Healer, "A healer. Heals things." }
	};
}
