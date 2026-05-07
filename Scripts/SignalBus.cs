using Godot;
using System;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void PlayerKilledEventHandler(bool player1);
	[Signal]
	public delegate void TimeAdvanceEventHandler(bool player1, int level);
}
