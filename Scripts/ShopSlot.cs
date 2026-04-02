using Godot;
using System;

public partial class ShopSlot : Control
{
	private bool open=false;
	int upgrade=0;
	[Export] GameManager.Towers tower;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>("Base").Texture=GD.Load(Cosmetics.towerDisplays[tower]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void toggle(){
		open=!open;
		if(open){
			GetNode<AnimationPlayer>("UpgradeSlider").Play("open");
		} else {
			GetNode<AnimationPlayer>("UpgradeSlider").Play("close");
		}
	}
	
	public void purchase(){
		GetParent().GetParent().GetParent<Hud>().purchaseTower(tower, GetNode<AnimationPlayer>("AnimationPlayer"));
	}
	
}
