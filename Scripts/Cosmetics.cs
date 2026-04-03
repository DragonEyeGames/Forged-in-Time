using Godot;
using System;
using System.Collections.Generic;

public partial class Cosmetics : Node
{
	public static Dictionary<GameManager.Towers, String> towerDisplays = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Plasma_Turret, "res://Towers/Assets/PlasmaTurret.png" },
		{ GameManager.Towers.Wall, "res://Towers/Assets/WallTower.png" },
		{ GameManager.Towers.Watch_Tower, "res://Towers/Assets/Watch Tower.png" },
		{ GameManager.Towers.Spikes, "res://Towers/Spikes.png" },
		{ GameManager.Towers.Melee, "res://Towers/Assets/WallTower.png" }
	};
	
	public static Dictionary<GameManager.Towers, String> towerNames = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "Turret" },
		{ GameManager.Towers.Plasma_Turret, "Plasma Turret" },
		{ GameManager.Towers.Wall, "Wall" },
		{ GameManager.Towers.Watch_Tower, "Watch Tower" },
		{ GameManager.Towers.Spikes, "Spikes" },
		{ GameManager.Towers.Melee, "Melee Troop" }
	};
	
	public static Dictionary<GameManager.Towers, String> towerDescriptions = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "Shoots at the closest enemy." },
		{ GameManager.Towers.Plasma_Turret, "Shoots at the closest enemy, but better." },
		{ GameManager.Towers.Wall, "It blocks the path." },
		{ GameManager.Towers.Watch_Tower, "Quickly takes over lots of land." },
		{ GameManager.Towers.Spikes, "A 2x2 grid that spawn in spikes to hit enemies." },
		{ GameManager.Towers.Melee, "A troop that attacks with a sword. Stab" }
	};
}
