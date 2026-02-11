using Godot;
using System;

public partial class GameManager : Node
{
	public enum Towers{
		Turret,
		Plasma_Turret,
		Wall,
		Tower
	}
	public static Towers toPlace;
	public static bool placing=false;
	public static bool validPlacement=true;
}
