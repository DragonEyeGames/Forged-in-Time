using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Wall : Tower
{
	public override void OnTimeAdvance(bool upgradePlayer, int level){
		if(Player1==upgradePlayer){
			upgrade(level);
		}
	}
	
	public override void upgrade(int level){
		if(level==1){
			GetNode<Sprite2D>("Base").Texture=(Texture2D)GD.Load("res://Assets/TowerArt/MedievalWall.png");
			if(Player1){
				GetNode<Area2D>("Player1Territory").Scale=new Vector2(1.4f, 1.4f);
			}
		}
		if(level==2){
			GetNode<Sprite2D>("Base").Texture=(Texture2D)GD.Load("res://Assets/TowerArt/SandbagWallwithBarbwire.png");
			if(Player1){
				GetNode<Area2D>("Player1Territory").Scale=new Vector2(1.7f, 1.7f);
			}
		}
		if(level==3){
			GetNode<Sprite2D>("Base").Texture=(Texture2D)GD.Load("res://Assets/TowerArt/LazzzzzzzerrrrWall.png");
			if(Player1){
				GetNode<Area2D>("Player1Territory").Scale=new Vector2(2.0f, 2.0f);
			}
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TowerGenerics();
	}
}
