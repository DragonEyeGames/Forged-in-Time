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
			GetNode<RichTextLabel>("Type").Text="Speed";
		}
		else if(upgradeType==GameManager.UpgradeTypes.Damage){
			GetNode<Sprite2D>("Icons/Attack").Visible=true;
			GetNode<RichTextLabel>("Type").Text="Attack";
		}
		else if(upgradeType==GameManager.UpgradeTypes.Health){
			GetNode<Sprite2D>("Icons/Defense").Visible=true;
			GetNode<RichTextLabel>("Type").Text="Defense";
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
	}
	
	public void upgrade(){
		GD.Print(GameManager.upgradeTroop(GetParent().GetParent<ShopSlot>().tower, upgradeType, 1));
	}
}
