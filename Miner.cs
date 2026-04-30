using Godot;
using System;

public partial class Miner : TargetBase
{
	public override int health { get; set; } = 200;
	public override int maxHealth { get; set; } = 200;
	public override bool isBase { get; set; } = false;
	public bool playerKilled;
	private int playerOwned = 0;
	[Export] public int moneyGenerated = 100;


	public void money()
	{
	}
	public async override void Die()
	{
		if (playerKilled)
		{
			GameManager.player1Target=GameManager.player1DefaultTarget;
			GD.Print("aplles");
			GetNode<Area2D>("Player-2").SetCollisionLayerValue(6, true);
			GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, true);
			await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
			GetNode<TerritoryChecker>("../Territory").recalculate();
			GetNode<Area2D>("Player-2").SetCollisionLayerValue(6, false);
			GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, false);
			GetNode<Area2D>("Player-1").SetCollisionLayerValue(4, true);
			await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
			GetNode<TerritoryChecker>("../Territory").recalculate();


		}
		else if (playerKilled == false)
		{
			GameManager.player2Target=GameManager.player2DefaultTarget;
			GetNode<Area2D>("Player-1").SetCollisionLayerValue(6, true);
			GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, true);
			GetNode<TerritoryChecker>("../Territory").recalculate();
			GetNode<Area2D>("Player-1").SetCollisionLayerValue(6, false);
			GetNode<Area2D>("Player-None").SetCollisionLayerValue(6, false);
			GetNode<Area2D>("Player-2").SetCollisionLayerValue(5, true);
			GetNode<TerritoryChecker>("../Territory").recalculate();



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
	
	
}
