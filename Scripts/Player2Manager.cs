using Godot;
using System;

public partial class Player2Manager : Node
{
	public static GameManager.Towers toPlace;
	public static bool placing=false;
	public static bool validPlacement=true;
	public static Cursor cursor;
	public static bool hudOpen=false;
}
