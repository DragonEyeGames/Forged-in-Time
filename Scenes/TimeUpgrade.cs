using Godot;
using System;

public partial class TimeUpgrade : ColorRect
{
	private bool player1=false;
	private int upgrade=1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player1=GetParent().GetParent<ShopSlot>().player1;
		if(player1){
			GetNode<Area2D>("Button/Player2").QueueFree();
		} else {
			GetNode<Area2D>("Button/Player1").QueueFree();
			GetNode<Sprite2D>("Sprite").QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<Controller>("Button").Disabled=!Visible;
		if(player1){
			GetNode<Area2D>("Button/Player1").Monitorable=Visible;
		} else {
			GetNode<Area2D>("Button/Player2").Monitorable=Visible;
		}
	}
	
	public void Upgrade(){
		int id = 1;
		if(!player1){
			id=2;
		}
		//GD.Print(GameManager.timeAdvance(GetParent().GetParent<ShopSlot>().tower, id));
		GetParent<UpgradePopout>().GetParent<ShopSlot>().upgraded();
		upgrade+=1;
		//GD.Print(GetParent().GetParent<ShopSlot>().Names[upgrade]);
		GetNode<RichTextLabel>("Description").Text=GetParent().GetParent<ShopSlot>().Names[upgrade];
	}
}
