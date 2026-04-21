using Godot;
using System;

public partial class TimeUpgrade : ColorRect
{
	private bool player1=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player1=GetParent().GetParent<ShopSlot>().player1;
		if(player1){
			GetNode<Area2D>("Button/Player2");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<Controller>("Button").Disabled=!Visible;
		GetNode<Area2D>("Button/Player1").Monitorable=Visible;
		if(Visible){
			GD.Print(GetNode<Controller>("Button").Disabled);
		}
	}
	
	public void Upgrade(){
		GD.Print(GameManager.timeAdvance(GetParent().GetParent<ShopSlot>().tower, 1));
		GetParent<UpgradePopout>().GetParent<ShopSlot>().upgraded();
	}
}
