using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Export] public bool player1=true;
	private bool open = false;
	private AnimationPlayer animator;
	public int turretUpgrade=0;
	[Export] CompressedTexture2D turretUpgradeSprite;
	[Export] Timer timer;
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
		if(GameManager.player1!=player1){
			return;
		}
		open=!open;
		if(open){
			animator.Play("open");
			timer.Start();
		} else {
			animator.Play("close");
		}
	}
	
	public void turret(){
		timer.Start();
		if(player1){
			if(Player1Manager.placing==false){
				if(turretUpgrade==0){
					Player1Manager.toPlace=GameManager.Towers.Turret;
				} else if (turretUpgrade==1){
					Player1Manager.toPlace=GameManager.Towers.Plasma_Turret;
				}
				Player1Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Turret/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if(!player1){
			if(Player2Manager.placing==false){
				if(turretUpgrade==0){
					Player2Manager.toPlace=GameManager.Towers.Turret;
				} else if (turretUpgrade==1){
					Player2Manager.toPlace=GameManager.Towers.Plasma_Turret;
				}
				Player2Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Turret/AnimationPlayer").Play("wobble");
				toggle();
			}
		}
		
		
	}
	
	public void upgradeTurret(){
		timer.Start();
		turretUpgrade+=1;
		if(turretUpgrade==1){
			GetNode<Sprite2D>("ColorRect/VBoxContainer/Turret/Base").Texture=turretUpgradeSprite;
			GetNode<Sprite2D>("ColorRect/VBoxContainer/Turret/Turret").Texture=turretUpgradeSprite;
			GetNode<ColorRect>("ColorRect/VBoxContainer/Turret/ColorRect2").Visible=false;
			GetNode<HudTower>("ColorRect/VBoxContainer/Turret").toggle();
			GetNode<RichTextLabel>("ColorRect/VBoxContainer/Turret/Label").Text="Plasma Turret";
		}
	}
	
	public void tower(){
		timer.Start();
		if(player1){
			if(Player1Manager.placing==false){
				Player1Manager.toPlace=GameManager.Towers.Tower;
				Player1Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Watch Tower/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if(!player1){
			if(Player2Manager.placing==false){
				Player2Manager.toPlace=GameManager.Towers.Tower;
				Player2Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Watch Tower/AnimationPlayer").Play("wobble");
				toggle();
			}
		}
	}
	
	public void wall(){
		timer.Start();
		if(player1){
			if(Player1Manager.placing==false){
				Player1Manager.toPlace=GameManager.Towers.Wall;
				Player1Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Wall/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if (!player1){
			if(Player2Manager.placing==false){
				Player2Manager.toPlace=GameManager.Towers.Wall;
				Player2Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Wall/AnimationPlayer").Play("wobble");
				toggle();
			}
		}
		
		
	}
	
	public void basicTroop(){
		timer.Start();
		if(player1 && GameManager.player1Base.reserveTroops.Count<GameManager.player1Base.maxTroops){
			GameManager.player1Base.reserveTroops.Add(Base.Troops.Melee);
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Basic/AnimationPlayer").Play("wobble");
		} else if(!player1 && GameManager.player2Base.reserveTroops.Count<GameManager.player2Base.maxTroops){
			GameManager.player2Base.reserveTroops.Add(Base.Troops.Melee);
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Basic/AnimationPlayer").Play("wobble");
		}
		
	}
	
	public void timerTime(){
		if(open){
			toggle();
		}
	}
}
