using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Export] public bool player1=true;
	private int ID=0;
	private bool open = false;
	private AnimationPlayer animator;
	public int turretUpgrade=0;
	[Export] CompressedTexture2D turretUpgradeSprite;
	[Export] Timer timer;
	public String input="";
	private bool canInput=true;
	[Export] Timer inputCooldown;
	[Export] Controller baseButton;
	private Controller openButton;
	[Export] Base playerBase;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator=GetNode<AnimationPlayer>("Animator");
		if(player1){
			ID=0;
		} else {
			ID=1;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(player1){
			GetNode<RichTextLabel>("ColorRect2/Money/Money").Text=Player1Manager.money.ToString();
		}
		if(!player1){
			GetNode<RichTextLabel>("ColorRect2/Money/Money").Text=Player2Manager.money.ToString();
		}
		GetNode<RichTextLabel>("ColorRect2/Health/RichTextLabel").Text=playerBase.health.ToString();
		
		if(Input.IsActionJustPressed("1Money")){
			Player1Manager.money+=500;
		}
		if(Input.IsActionJustPressed("2Money")){
			Player2Manager.money+=500;
		}
	}
	
	public void closedPressed(){
		toggle();
		if(player1){
			GameManager.player1HUDOpen=open;
		};
		if(!player1){
			GameManager.player2HUDOpen=open;
		};
		GD.Print(open);
	}
	
	public void toggle(){
		open=!open;
		GetNode<CollisionShape2D>("BigClose/Area2D/CollisionShape2D").SetDeferred("disabled", !open);
		if(open){
			animator.Play("open");
			timer.Start();
		} else {
			animator.Play("close");
		}
		if(player1){
			Player1Manager.hudOpen=open;
		}
		else if(!player1){
			Player2Manager.hudOpen=open;
		}
	}
	
	public void purchaseTower(GameManager.Towers tower, AnimationPlayer buttonAnimator){
		timer.Start();
		if(player1 && Player1Manager.money>=Prices.towerPrices[tower]){
			if(Player1Manager.placing==false){
				Player1Manager.toPlace=tower;
				Player1Manager.money-=Prices.towerPrices[tower];
				Player1Manager.placing=true;
				buttonAnimator.Play("wobble");
				toggle();
			}
		} else if(!player1 && Player2Manager.money>=Prices.towerPrices[tower]){
			if(Player2Manager.placing==false){
				Player2Manager.toPlace=tower;
				Player2Manager.money-=Prices.towerPrices[tower];
				Player2Manager.placing=true;
				buttonAnimator.Play("wobble");
				toggle();
			}
		}
	}
	
	public void turret(){
		if(turretUpgrade==0){
			//purchaseTower(GameManager.Towers.Turret, "ColorRect/VBoxContainer/Turret/AnimationPlayer");
		}
		if(turretUpgrade==1){
			//purchaseTower(GameManager.Towers.Plasma_Turret, "ColorRect/VBoxContainer/Turret/AnimationPlayer");
		}
	}
	
	public void tower(){
		//purchaseTower(GameManager.Towers.Watch_Tower, "ColorRect/VBoxContainer/Watch Tower/AnimationPlayer");
	}
	
	public void wall(){
		//purchaseTower(GameManager.Towers.Wall, "ColorRect/VBoxContainer/Wall/AnimationPlayer");
	}
	
	public void spikes(){
		//purchaseTower(GameManager.Towers.Spikes, "ColorRect/VBoxContainer/AOE/AnimationPlayer");
	}
	
	//Troop Section
	
	public void purchaseTroop(GameManager.Towers troop, AnimationPlayer buttonAnimator){
		timer.Start();
		if(player1 && GameManager.player1Base.reserveTroops.Count<GameManager.player1Base.maxTroops && Player1Manager.money>=Prices.towerPrices[troop]){
			if(troop==GameManager.Towers.Melee){
				GameManager.player1Base.reserveTroops.Add(Base.Troops.Melee);
			}
			if(troop==GameManager.Towers.Brute){
				GameManager.player1Base.reserveTroops.Add(Base.Troops.Brute);
			}
			if(troop==GameManager.Towers.Ranged){
				GameManager.player1Base.reserveTroops.Add(Base.Troops.Ranged);
			}
			if(troop==GameManager.Towers.Healer){
				GameManager.player1Base.reserveTroops.Add(Base.Troops.Healer);
			}
			Player1Manager.money-=Prices.towerPrices[troop];
			buttonAnimator.Play("wobble");
		} else if(!player1 && GameManager.player2Base.reserveTroops.Count<GameManager.player2Base.maxTroops && Player2Manager.money>=Prices.towerPrices[troop]){
			if(troop==GameManager.Towers.Melee){
				GameManager.player2Base.reserveTroops.Add(Base.Troops.Melee);
			}
			if(troop==GameManager.Towers.Brute){
				GameManager.player2Base.reserveTroops.Add(Base.Troops.Brute);
			}
			if(troop==GameManager.Towers.Ranged){
				GameManager.player2Base.reserveTroops.Add(Base.Troops.Ranged);
			}
			if(troop==GameManager.Towers.Healer){
				GameManager.player2Base.reserveTroops.Add(Base.Troops.Healer);
			}
			Player2Manager.money-=Prices.towerPrices[troop];
			buttonAnimator.Play("wobble");
		}
	}
	
	//Upgrade Section
	
	//public void upgradeTower(ref int upgradeLevel, NodePath nodeHolder, ref CompressedTexture2D upgradeSprite, string newName){
		/*if(player1 && Player1Manager.money>=Prices.upgradePrices[upgrade]){
		//	Player1Manager.money-=Prices.upgradePrices[upgrade];
		//} else if(!player1 && Player2Manager.money>=Prices.upgradePrices[upgrade]){
		//	Player2Manager.money-=Prices.upgradePrices[upgrade];
		//} else {
		//	return;
		//}
		timer.Start();
		upgradeLevel+=1;
		if(upgradeLevel==1){
			foreach(Node child in GetNode<Control>(nodeHolder).GetChildren()){
				if(child is Sprite2D){
					Sprite2D childSprite = child as Sprite2D;
					childSprite.Texture=upgradeSprite;
				}
			}
			GetNode<ShopSlot>(nodeHolder).toggle();
			GetNode<RichTextLabel>(nodeHolder+"/Label").Text=newName;
			GetNode<Controller>(nodeHolder+"/Popout/Button").deselect();
			GetNode<Controller>(nodeHolder+"/Button").select();
		}
	}
	*/
	public void upgradeTurret(){
		if(turretUpgrade==0){
			//upgradeTower(ref turretUpgrade, "ColorRect/VBoxContainer/Turret", ref turretUpgradeSprite, "Plasma Turret", GameManager.Upgrades.Plasma_Turret);
		}
		
	}
	
	public void timerTime(){
		if(open){
			toggle();
		}
	}
	
	public void inputCool(){
		canInput=true;
	}
	
	public override void _Input(InputEvent @event)
	{
	if(ID==@event.Device && canInput){
		if (@event.IsActionPressed("Up"))
		{
			input="Up";
		}
		else if (@event.IsActionPressed("Down"))
		{
			input="Down";
		}
		else if (@event.IsActionPressed("Left"))
		{
			input="Left";
		}
		else if (@event.IsActionPressed("Right"))
		{
			input="Right";
		} else if (@event.IsActionPressed("Select"))
		{
			input="Select";
			GD.Print("Selecto");
		} else{
			return;
		}
		canInput=false;
		inputCooldown.Start();
	}
	
}
}
