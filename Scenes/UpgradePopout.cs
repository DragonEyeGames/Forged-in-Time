using Godot;
using System;

public partial class UpgradePopout : ColorRect
{
	
	private GameManager.Towers tower;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tower = GetParent<ShopSlot>().tower;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (TroopUpgrades.TimePeriod.Contains((int)GameManager.fetchUpgrades(1, tower).X))
		{
			GetNode<UpgradeSlot>("UpgradeSlot").disabled=true;
			GetNode<UpgradeSlot>("UpgradeSlot2").disabled=true;
			GetNode<UpgradeSlot>("UpgradeSlot3").disabled=true;
		}
	}
}
