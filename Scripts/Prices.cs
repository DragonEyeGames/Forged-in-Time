using Godot;
using System;
using System.Collections.Generic;

public partial class Prices : Node
{
		
	public static Dictionary<GameManager.Towers, int> towerPrices = new Dictionary<GameManager.Towers, int>
	{
		{ GameManager.Towers.Turret, 100 },
		{ GameManager.Towers.Wall, 50 },
		{ GameManager.Towers.Watch_Tower, 75 },
		{ GameManager.Towers.Spikes, 50 },
		{ GameManager.Towers.Melee, 25 },
		{ GameManager.Towers.Brute, 50 },
		{ GameManager.Towers.Ranged, 25 },
		{ GameManager.Towers.Healer, 75 }
	};
}
