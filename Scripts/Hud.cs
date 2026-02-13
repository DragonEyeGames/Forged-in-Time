using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Export] public bool player1=true;
	private bool open = false;
	private AnimationPlayer animator;
	public int turretUpgrade=0;
	[Export] CompressedTexture2D turretUpgradeSprite;
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
		if(GameManager.placing==false){
			if(turretUpgrade==0){
				GameManager.toPlace=GameManager.Towers.Turret;
			} else if (turretUpgrade==1){
				GameManager.toPlace=GameManager.Towers.Plasma_Turret;
			}
			GameManager.placing=true;
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Turret/AnimationPlayer").Play("wobble");
			toggle();
		}
		
	}
	
	public void upgradeTurret(){
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
		if(GameManager.placing==false){
			GameManager.toPlace=GameManager.Towers.Tower;
			GameManager.placing=true;
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Watch Tower/AnimationPlayer").Play("wobble");
			toggle();
		}
		
	}
	
	public void wall(){
		if(GameManager.placing==false){
			GameManager.toPlace=GameManager.Towers.Wall;
			GameManager.placing=true;
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Wall/AnimationPlayer").Play("wobble");
			toggle();
		}
		
	}
	
	public void basicTroop(){
		if(player1 && GameManager.player1Base.reserveTroops.Count<GameManager.player1Base.maxTroops){
			GameManager.player1Base.reserveTroops.Add(Base.Troops.Melee);
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Basic/AnimationPlayer").Play("wobble");
		}
		
	}
}
