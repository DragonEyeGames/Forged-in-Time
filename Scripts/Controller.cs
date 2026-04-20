using Godot;
using System;

public partial class Controller : Button
{
	
	public Vector2 baseSize;
	public Vector2 increasedSize;
	public bool selected=false;
	public int clickedBy = 0;
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
	
	public async void select(){
		Scale=increasedSize;
		Modulate = Colors.White;
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		GD.Print(clickedBy);
	}
}
