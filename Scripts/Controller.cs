using Godot;
using System;

public partial class Controller : Button
{
	[Export] Controller up;
	[Export] Controller down;
	[Export] public Controller left;
	[Export] public Controller right;
	[Export] bool selected;
	[Export] Hud hud;
	[Export] ColorRect holder;
	[Export] bool exception=false;
	[Export] bool sideException=false;
	public Vector2 baseSize;
	public Vector2 increasedSize;
	[Export] int upgrade = 0;
	[Export] Controller upgradeButton;
	// Called when the node enters the scene tree for the first time.
	/*

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
			if(hud.input=="Left" && upgrade==-1 && upgradeButton!=null && upgradeButton.GetParent<ColorRect>().Scale.X==1){
				hud.input="";
				selected=false;
				upgradeButton.selected=true;
				upgradeButton.Scale=upgradeButton.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				upgradeButton.Modulate = Colors.White;
				return;
			}
			if(hud.input=="Left" && left!=null && (holder.Visible || sideException)){
				hud.input="";
				selected=false;
				left.selected=true;
				left.Scale=left.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				left.Modulate = Colors.White;
			}
			if(hud.input=="Right" && upgrade==1 && upgradeButton!=null && upgradeButton.GetParent<ColorRect>().Scale.X==1){
				hud.input="";
				selected=false;
				upgradeButton.selected=true;
				upgradeButton.Scale=upgradeButton.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				upgradeButton.Modulate = Colors.White;
				return;
			}
			if(hud.input=="Right" && right!=null && (holder.Visible || sideException)){
				hud.input="";
				selected=false;
				right.selected=true;
				right.Scale=right.increasedSize;
				Scale=baseSize;
				Modulate = Colors.Gray;
				right.Modulate = Colors.White;
			}
			if(hud.input=="Select" && (holder.Visible || exception)){
				hud.input="";
				EmitSignal(Button.SignalName.Pressed);
			}
		}
	}
	*/
	
	public override void _Ready()
	{
		baseSize=Scale;
		increasedSize=Scale*=new Vector2(1.1f, 1.1f);
		if(selected){
			Scale=increasedSize;
			Modulate = Colors.White;
		} else {
			Scale=baseSize;
			Modulate = Colors.Gray;
		}
	}
	
	public void deselect(){
		Scale=baseSize;
		Modulate = Colors.Gray;
	}
	
	public void select(){
		Scale=increasedSize;
		Modulate = Colors.White;
	}
}
