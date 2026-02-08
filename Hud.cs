using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private bool open = true;
	private AnimationPlayer animator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator=GetNode<AnimationPlayer>("Animator");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public void toggle(){
		open=!open;
		if(open){
			animator.Play("open");
		} else {
			animator.Play("close");
		}
	}
	
	public void turret(){
		GD.Print("Placae");
		if(GameManager.placing==false){
			GameManager.toPlace=GameManager.Towers.Turret;
			GameManager.placing=true;
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Turret/AnimationPlayer").Play("wobble");
			toggle();
		}
		
	}
}
