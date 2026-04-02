using Godot;
using System;
using System.Collections.Generic;

public partial class Cosmetics : Node
{
	public static Dictionary<GameManager.Towers, String> towerDisplays = new Dictionary<GameManager.Towers, String>
	{
		{ GameManager.Towers.Turret, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Plasma_Turret, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Wall, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Watch_Tower, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Spikes, "res://Towers/Assets/Turret.png" },
		{ GameManager.Towers.Melee, "res://Towers/Assets/Turret.png" }
	};
}
