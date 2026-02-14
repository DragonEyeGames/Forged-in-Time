using Godot;
using System;

public partial class Controller : Button
{
	[Export] Controller up;
	[Export] Controller down;
	[Export] Controller left;
	[Export] Controller right;
	[Export] bool selected;
	[Export] Hud hud;
	[Export] ColorRect holder;
	[Export] bool exception=false;
	public Vector2 baseSize;
	public Vector2 increasedSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		baseSize=Scale;
		increasedSize=Scale*=new Vector2(1.1f, 1.1f);
		if(selected){
			Scale=increasedSize;
		} else {
			Modulate = Colors.Gray;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(selected){
			if(hud.input=="Up" && up!=null && holder.Visible){
				hud.input="";
				selected=false;
				up.selected=true;
				up.Scale=up.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				up.Modulate = Colors.White;
			}
			if(hud.input=="Down" && down!=null && holder.Visible){
				hud.input="";
				selected=false;
				down.selected=true;
				down.Scale=down.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				down.Modulate = Colors.White;
			}
			if(hud.input=="Select" && (holder.Visible || exception)){
				hud.input="";
				EmitSignal(Button.SignalName.Pressed);
			}
		}
	}
}
