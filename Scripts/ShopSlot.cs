using Godot;
using System;
using Godot.Collections;

public partial class ShopSlot : Control
{
	private bool open=false;
	int upgrade=0;
	[Export] public GameManager.Towers tower;
	[Export] public bool player1 = true;
	[Export] public bool troop = false;
	[Export] public Array<Texture2D> Sprites = new Array<Texture2D>();
	[Export] public Array<String> Names = new Array<String>();
	public bool upgradeOpen=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(player1){
			GetNode<Button>("Player2Upgrade").QueueFree();
			GetNode<Area2D>("Button/Player2").QueueFree();
			if(!troop){
				GetNode<Button>("Player1Upgrade").QueueFree();
				GetNode<ColorRect>("UpgradePopout").QueueFree();
			}
		} else {
			GetNode<Sprite2D>("Base").FlipH=true;
			GetNode<Button>("Player1Upgrade").QueueFree();
			GetNode<Area2D>("Button/Player1").QueueFree();
			if(!troop){
				GetNode<Button>("Player2Upgrade").QueueFree();
				GetNode<ColorRect>("UpgradePopout").QueueFree();
			}
		};
		GetNode<Sprite2D>("Base").Texture = (Texture2D)GD.Load(Cosmetics.towerDisplays[tower]);
		GetNode<RichTextLabel>("Name").Text=Cosmetics.towerNames[tower].ToString();
		GetNode<RichTextLabel>("Description").Text=Cosmetics.towerDescriptions[tower].ToString();
		GetNode<RichTextLabel>("Price").Text="$" + Prices.towerPrices[tower].ToString();
		if(Sprites.Count>=2){
			GetNode<Sprite2D>("UpgradePopout/TimeUpgrade/Sprite").Texture=Sprites[1];//(Texture2D)GD.Load(Sprites[1]);
			GD.Print("Changed: ", Name);
		} else {
			//GD.Print(Sprites.Count, Name);
		}
		if(Names.Count>=2){
			GetNode<RichTextLabel>("UpgradePopout/TimeUpgrade/Description").Text=Names[1];
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(troop){
			if(GameManager.fetchUpgrades(1, tower).X>=TroopUpgrades.Prices.Count && GetNodeOrNull<ColorRect>("UpgradePopout") != null){
				GetNode<ColorRect>("UpgradePopout").QueueFree();
				if(player1){
					GetNode<Button>("Player1Upgrade").QueueFree();
				} else {
					GetNode<Button>("Player2Upgrade").QueueFree();
				}
			}
		}
		
		if(Input.IsActionJustPressed("Click-1") && player1 && upgradeOpen){
			player1Upgrade();
		} else if (Input.IsActionJustPressed("Click-2") && !player1 && upgradeOpen){
			player2Upgrade();
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
		AnimationPlayer animator = GetNodeOrNull<AnimationPlayer>("Player1Upgrade/Animator");
		if(animator==null){
			return;
		}
		if(upgradeOpen==false && !animator.IsPlaying()){
			animator.Play("open");
			upgradeOpen=true;
		} else if (!animator.IsPlaying()){
			animator.Play("close");
			upgradeOpen=false;
		}
		
	}
	
	public void player2Upgrade(){
		AnimationPlayer animator = GetNodeOrNull<AnimationPlayer>("Player2Upgrade/Animator");
		if(animator==null){
			return;
		}
		if(upgradeOpen==false && !animator.IsPlaying()){
			animator.Play("open");
			upgradeOpen=true;
		} else if (!animator.IsPlaying()){
			animator.Play("close");
			upgradeOpen=false;
		}
		
	}
	
	public void upgraded(){
		if(player1){
			AnimationPlayer animator = GetNode<AnimationPlayer>("Player1Upgrade/Animator");
			animator.Play("freeze");
		} else if(!player1){
			AnimationPlayer animator = GetNode<AnimationPlayer>("Player2Upgrade/Animator");
			animator.Play("freeze");
		}
		upgradeOpen=true;
		int id = 1;
		if(!player1){
			id=2;
		}
		return;
		if(troop && GameManager.fetchUpgrades(id, tower).X>3){
			if(Sprites.Count>=2){
				GetNode<Sprite2D>("Base").Texture = Sprites[1];
			}
			
		}
		if(troop && GameManager.fetchUpgrades(id, tower).X>6){
			if(Sprites.Count>=3){
				GetNode<Sprite2D>("Base").Texture = Sprites[2];
			}
		}
	}
	
}
