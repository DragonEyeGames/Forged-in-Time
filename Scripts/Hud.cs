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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		openButton=GetNode<Controller>("Button");
		if(player1) {
			baseButton.right=openButton;
			openButton.left=baseButton;
		} else {
			baseButton.left=openButton;
			openButton.right=baseButton;
		}
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
	}
	
	public void toggle(){
		open=!open;
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
	
	public void turret(){
		timer.Start();
		if(player1 && Player1Manager.money>=Prices.prices[GameManager.Towers.Turret]){
			if(Player1Manager.placing==false){
				if(turretUpgrade==0){
					Player1Manager.toPlace=GameManager.Towers.Turret;
				} else if (turretUpgrade==1){
					Player1Manager.toPlace=GameManager.Towers.Plasma_Turret;
				}
				Player1Manager.money-=Prices.prices[GameManager.Towers.Turret];
				Player1Manager.placing=true;
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Turret/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if(!player1 && Player2Manager.money>=Prices.prices[GameManager.Towers.Turret]){
			if(Player2Manager.placing==false){
				if(turretUpgrade==0){
					Player2Manager.toPlace=GameManager.Towers.Turret;
				} else if (turretUpgrade==1){
					Player2Manager.toPlace=GameManager.Towers.Plasma_Turret;
				}
				Player2Manager.money-=Prices.prices[GameManager.Towers.Turret];
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
			GetNode<Button>("ColorRect/VBoxContainer/Turret/Button2").Visible=false;
			GetNode<ShopSlot>("ColorRect/VBoxContainer/Turret").toggle();
			GetNode<RichTextLabel>("ColorRect/VBoxContainer/Turret/Label").Text="Plasma Turret";
			GetNode<Controller>("ColorRect/VBoxContainer/Turret/Popout/Button").deselect();
			GetNode<Controller>("ColorRect/VBoxContainer/Turret/Button").select();
		}
	}
	
	public void tower(){
		timer.Start();
		if(player1 && Player1Manager.money>=Prices.prices[GameManager.Towers.Watch_Tower]){
			if(Player1Manager.placing==false){
				Player1Manager.toPlace=GameManager.Towers.Watch_Tower;
				Player1Manager.placing=true;
				Player1Manager.money-=Prices.prices[GameManager.Towers.Watch_Tower];
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Watch Tower/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if(!player1 && Player2Manager.money>=Prices.prices[GameManager.Towers.Watch_Tower]){
			if(Player2Manager.placing==false){
				Player2Manager.toPlace=GameManager.Towers.Watch_Tower;
				Player2Manager.placing=true;
				Player2Manager.money-=Prices.prices[GameManager.Towers.Watch_Tower];
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Watch Tower/AnimationPlayer").Play("wobble");
				toggle();
			}
		}
	}
	
	public void wall(){
		timer.Start();
		if(player1 && Player1Manager.money>=Prices.prices[GameManager.Towers.Wall]){
			if(Player1Manager.placing==false){
				Player1Manager.toPlace=GameManager.Towers.Wall;
				Player1Manager.placing=true;
				Player1Manager.money-=Prices.prices[GameManager.Towers.Wall];
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Wall/AnimationPlayer").Play("wobble");
				toggle();
			}
		} else if (!player1 && Player2Manager.money>=Prices.prices[GameManager.Towers.Wall]){
			if(Player2Manager.placing==false){
				Player2Manager.toPlace=GameManager.Towers.Wall;
				Player2Manager.placing=true;
				Player2Manager.money-=Prices.prices[GameManager.Towers.Wall];
				GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Wall/AnimationPlayer").Play("wobble");
				toggle();
			}
		}
		
		
	}
	
	public void basicTroop(){
		timer.Start();
		if(player1 && GameManager.player1Base.reserveTroops.Count<GameManager.player1Base.maxTroops && Player1Manager.money>=Prices.prices[GameManager.Towers.Melee]){
			GameManager.player1Base.reserveTroops.Add(Base.Troops.Melee);
			Player1Manager.money-=Prices.prices[GameManager.Towers.Melee];
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Basic/AnimationPlayer").Play("wobble");
		} else if(!player1 && GameManager.player2Base.reserveTroops.Count<GameManager.player2Base.maxTroops && Player2Manager.money>=Prices.prices[GameManager.Towers.Melee]){
			GameManager.player2Base.reserveTroops.Add(Base.Troops.Melee);
			Player2Manager.money-=Prices.prices[GameManager.Towers.Melee];
			GetNode<AnimationPlayer>("ColorRect/VBoxContainer/Basic/AnimationPlayer").Play("wobble");
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
