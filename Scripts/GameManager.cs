using Godot;
using System;

public partial class GameManager : Node
{
	
	public enum Towers{
		Turret,
		Plasma_Turret,
		Wall,
		Watch_Tower,
		Spikes,
		Melee,
		Ranged,
		Brute,
		Healer
	}
	
	public enum Upgrades{
		Plasma_Turret
	}
	
	public static Base player1Base;
	public static Base player2Base;
	public static TerritoryChecker territory;
	public static bool player1=true;
	public static bool keyboard=false;
	
	public static bool player1HUDOpen=false;
	public static bool player2HUDOpen=false;
	
	public static Placement player1Placement;
	public static Placement player2Placement;
	
	public static BakeHandler baker;

	public static TargetBase player1Target;
	public static TargetBase player2Target; 
	public static TargetBase player1DefaultTarget;
	public static TargetBase player2DefaultTarget;
}
