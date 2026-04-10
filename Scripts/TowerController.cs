using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class TowerController : Controller
{
	[Export] public Sprite2D sprite;
	
	public override void _Process(double delta){
		if(sprite!=null){
			sprite.UseParentMaterial=(Modulate==Colors.White && !GetParent<Tower>().hovering);
		}
		
	}
}
