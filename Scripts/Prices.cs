using Godot;
using System;
using System.Collections.Generic;

public partial class Prices : Node
{
		
	public static Dictionary<GameManager.Towers, int> towerPrices = new Dictionary<GameManager.Towers, int>
	{
		{ GameManager.Towers.Turret, 100 },
		{ GameManager.Towers.Plasma_Turret, 100 },
		{ GameManager.Towers.Wall, 50 },
		{ GameManager.Towers.Watch_Tower, 75 },
		{ GameManager.Towers.Spikes, 50 },
		{ GameManager.Towers.Melee, 25 }
	};
	
	public static Dictionary<GameManager.Upgrades, int> upgradePrices = new Dictionary<GameManager.Upgrades, int>
	{
		{ GameManager.Upgrades.Plasma_Turret, 150 },
	};
}
