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
		Melee
	}
	
	public enum Upgrades{
		Plasma_Turret
	}
	
	public static Base player1Base;
	public static Base player2Base;
<<<<<<< HEAD
	public static Territory territory;
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
	public static bool player1=true;
	public static bool keyboard=false;

<<<<<<< HEAD
	
	public static bool player1HUDOpen=false;
	public static bool player2HUDOpen=false;
	
	public static Placement player1Placement;
	public static Placement player2Placement;
	
	public static BakeHandler baker;
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
}
