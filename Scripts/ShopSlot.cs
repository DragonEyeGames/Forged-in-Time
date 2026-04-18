using Godot;
using System;

public partial class ShopSlot : Control
{
	private bool open=false;
	int upgrade=0;
	[Export] public GameManager.Towers tower;
	[Export] public bool player1 = true;
	[Export] public bool troop = false;
	public bool upgradeOpen=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			GetNode<Button>("Player2Upgrade").QueueFree();
			GetNode<Area2D>("Button/Player2").QueueFree();
		} else {
			GetNode<Button>("Player1Upgrade").QueueFree();
			GetNode<Area2D>("Button/Player1").QueueFree();
		};
		GetNode<Sprite2D>("Base").Texture = (Texture2D)GD.Load(Cosmetics.towerDisplays[tower]);
		GetNode<RichTextLabel>("Name").Text=Cosmetics.towerNames[tower].ToString();
		GetNode<RichTextLabel>("Description").Text=Cosmetics.towerDescriptions[tower].ToString();
		GetNode<RichTextLabel>("Price").Text="$" + Prices.towerPrices[tower].ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Click-1") && player1 && upgradeOpen){
			player1Upgrade();
		} else if (Input.IsActionJustPressed("Click-2") && !player1 && upgradeOpen){
			
		}
		if(player1){
			GetNode<Button>("Button").Disabled = (Player1Manager.money<Prices.towerPrices[tower] || (troop && GameManager.player1Base.reserveTroops.Count>=GameManager.player1Base.maxTroops));
		} else {
			GetNode<Button>("Button").Disabled = (Player2Manager.money<Prices.towerPrices[tower] || (troop && GameManager.player2Base.reserveTroops.Count>=GameManager.player2Base.maxTroops));
		}
		
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
		if(troop==false){
			GetParent().GetParent().GetParent<Hud>().purchaseTower(tower, GetNode<AnimationPlayer>("AnimationPlayer"));
		}
		else if(troop==true){
			GetParent().GetParent().GetParent<Hud>().purchaseTroop(tower, GetNode<AnimationPlayer>("AnimationPlayer"));
		};
	}
	
	public void player1Upgrade(){
		AnimationPlayer animator = GetNode<AnimationPlayer>("Player1Upgrade/Animator");
		if(upgradeOpen==false && !animator.IsPlaying()){
			animator.Play("open");
			upgradeOpen=true;
		} else if (!animator.IsPlaying()){
			animator.Play("close");
			upgradeOpen=false;
		}
		
	}
	
	public void upgraded(){
		AnimationPlayer animator = GetNode<AnimationPlayer>("Player1Upgrade/Animator");
		animator.Play("freeze");
		upgradeOpen=true;
	}
	
}
