using Godot;
using System;
using System.Collections.Generic;

public partial class Prices : Node
{
		
	public static Dictionary<GameManager.Towers, int> prices = new Dictionary<GameManager.Towers, int>
	{
		{ GameManager.Towers.Turret, 100 },
		{ GameManager.Towers.Plasma_Turret, 100 },
		{ GameManager.Towers.Wall, 50 },
		{GameManager.Towers.Watch_Tower, 75}
	};
}
