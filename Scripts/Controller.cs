using Godot;
using System;

public partial class Controller : Button
{
	
	public Vector2 baseSize;
	public Vector2 increasedSize;
	public bool selected=false;
	[Export] private bool transformLock=false;
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
		if(transformLock==false){
			Scale=baseSize;
		}
		Modulate = Colors.Gray;
	}
	
	public void select(){
		if(Disabled){
			return;
		}
		if(transformLock==false){
			Scale=increasedSize;
		}
		Modulate = Colors.White;
	}
}
