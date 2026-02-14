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
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(selected){
			Scale*=new Vector2(1.1f, 1.1f);
		} else {
			Modulate = Colors.Gray;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(selected){
			if(hud.input=="Up" && up!=null){
				hud.input="";
				selected=false;
				up.selected=true;
				up.Scale*=new Vector2(1.1f, 1.1f);
				Scale/=new Vector2(1.1f, 1.1f);
				Modulate = Colors.Gray;
				up.Modulate = Colors.White;
			}
			if(hud.input=="Down" && down!=null){
				hud.input="";
				selected=false;
				down.selected=true;
				down.Scale*=new Vector2(1.1f, 1.1f);
				Scale/=new Vector2(1.1f, 1.1f);
				Modulate = Colors.Gray;
				down.Modulate = Colors.White;
			}
			if(hud.input=="Select"){
				hud.input="";
				EmitSignal(Button.SignalName.Pressed);
			}
		}
	}
}
