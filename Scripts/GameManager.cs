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
	public static Base player1Base;
	public static Base player2Base;
	public static bool player1=true;
}
