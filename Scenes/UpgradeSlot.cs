using Godot;
using System;

public partial class UpgradeSlot : ColorRect
{
	
	[Export] private GameManager.UpgradeTypes upgradeType;
		
	private bool player1=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print(GetParent().GetParent<ShopSlot>().player1);
		foreach (Node2D child in GetNode<Node2D>("Icons").GetChildren())
		{
			child.Visible=false;
		}
		if(upgradeType==GameManager.UpgradeTypes.Speed){
			GetNode<Sprite2D>("Icons/Speed").Visible=true;
		}
		else if(upgradeType==GameManager.UpgradeTypes.Damage){
			GetNode<Sprite2D>("Icons/Attack").Visible=true;
		}
		else if(upgradeType==GameManager.UpgradeTypes.Health){
			GetNode<Sprite2D>("Icons/Defense").Visible=true;
		}
		player1=GetParent().GetParent<ShopSlot>().player1;
		if(player1==true){
			GetNode<Area2D>("Button/Player2").QueueFree();
		} else {
			GetNode<Area2D>("Button/Player1").QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).X>=TroopUpgrades.Prices.Count){
			return;
		}
		if(upgradeType==GameManager.UpgradeTypes.Speed){
			GetNode<RichTextLabel>("Type").Text="Speed Lv. " + GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).Y.ToString();
		}
		if(upgradeType==GameManager.UpgradeTypes.Health){
			GetNode<RichTextLabel>("Type").Text="Health Lv. " + GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).Z.ToString();
		}
		if(upgradeType==GameManager.UpgradeTypes.Damage){
			GetNode<RichTextLabel>("Type").Text="Attack Lv. " + GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).W.ToString();
		}
		int price = TroopUpgrades.Prices[(int)GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).X];
		GetNode<RichTextLabel>("Price").Text="$" + price.ToString();
		if(player1){
			GetNode<Button>("Button").Disabled=(price>Player1Manager.money);
		}
		
	}
	
	public void upgrade(){
		int price = TroopUpgrades.Prices[(int)GameManager.fetchUpgrades(1, GetParent().GetParent<ShopSlot>().tower).X];
		if(player1){
			if(price>Player1Manager.money){
				return;
			} else {
				Player1Manager.money-=price;
			}
		}
		GameManager.upgradeTroop(GetParent().GetParent<ShopSlot>().tower, upgradeType, 1);
		GetParent().GetParent<ShopSlot>().upgraded();
	}
}
