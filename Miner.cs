using Godot;
using System;

public partial class Miner : TargetBase
{
	public override int health { get; set; } = 100;
	public override int maxHealth { get; set; } = 100;
	public override bool isBase { get; set; } = false;
	public bool playerKilled;
	public int playerOwned = 0;
	[Export] public int moneyGenerated = 100;
	public bool needHealth = false;


	public void money(int moneyAmount)
	{
		GD.Print("ben is not cool");
		if (playerOwned == 1);
		{
			Player1Manager.money += moneyAmount;
			GetNode<Timer>("Timer").Start();
		}
		if (playerOwned == 2);
		{
			Player2Manager.money += moneyAmount;
			GetNode<Timer>("Timer").Start();
		}
	}
	public async override void Die()
	{
		if (playerKilled)
		{
			if (playerOwned != 1)
			{
				playerOwned = 1;
				GameManager.player1Target = GameManager.player1DefaultTarget;
				GetNode<Area2D>("Player-2").SetCollisionLayerValue(5, false);
				GetNode<Area2D>("Player-2").SetCollisionLayerValue(6, true);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(12, false);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, true);
				await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
				GetNode<TerritoryChecker>("../Territory").recalculate();
				GetNode<Area2D>("Player-2").SetCollisionLayerValue(6, false);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, false);
				GetNode<Area2D>("Player-1").SetCollisionLayerValue(4, true);
				await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
				GetNode<TerritoryChecker>("../Territory").recalculate();
				money(moneyGenerated);
				needHealth = true;
			}

		}
		else if (playerKilled == false)
		{
			if (playerOwned != 2)
			{
				GameManager.player2Target = GameManager.player2DefaultTarget;
				GetNode<Area2D>("Player-1").SetCollisionLayerValue(4, false);
				GetNode<Area2D>("Player-1").SetCollisionLayerValue(6, true);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(12, false);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, true);
				GetNode<TerritoryChecker>("../Territory").recalculate();
				GetNode<Area2D>("Player-1").SetCollisionLayerValue(6, false);
				GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, false);
				GetNode<Area2D>("Player-2").SetCollisionLayerValue(5, true);
				GetNode<TerritoryChecker>("../Territory").recalculate();
				playerOwned = 2;
				money(moneyGenerated);
				needHealth = true;	
			}

		}
	}

	public void onSelect()
	{
		GD.Print(playerClicked);
		playerClicked = GetNode<Controller>("Button").clickedBy;
		if (playerClicked == 1)
		{
			GameManager.player1Target = this;
			playerClicked = 0;
		}
		else if (playerClicked == 2)
		{
			GameManager.player2Target = this;
			playerClicked = 0;
		}

		GD.Print("onSelect Target Changed " + GameManager.player1Target.GetType());
	}

	public void moneyTimer()
	{
		GD.Print("I like cheese");
		money(moneyGenerated);
	}
	
}
