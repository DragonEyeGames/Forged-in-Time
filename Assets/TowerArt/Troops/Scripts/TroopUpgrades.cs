using Godot;
using System;
using System.Collections.Generic;


public partial class TroopUpgrades : Node
{
	public static List<int> Health = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
	public static List<int> Damage = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
	public static List<int> Speed = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160};
	
	public static List<int> Prices = new List<int> {10, 15, 25, 40, 60, 100, 150, 210, 280, 350};//, 500, 750};
	
	public static List<int> TimePeriod = new List<int> {30, 60, 100};
}
