using Godot;
using System;

public partial class Controller : Button
{
	
	public Vector2 baseSize;
	public Vector2 increasedSize;
	public bool selected=false;
	public int pressor = 0 ; //0=not pressed, 1=player 1 pressed, 2=player 2 pressed
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
