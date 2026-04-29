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


    public async void money()
    {
    }
    public async override void Die()
    {
        if (playerKilled)
        {
            GD.Print("aplles");
            GetNode<CollisionShape2D>("Player-1/Player1Territory").Disabled = false;
            GetNode<CollisionShape2D>("Player-2/Player2Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-None/PlayerNoneTerritory").Disabled = true;
            GetNode<TerritoryChecker>("../Territory").recalculate();
        }
        else if (playerKilled == false)
        {
            GetNode<CollisionShape2D>("Player-1/Player1Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-2/Player2Territory").Disabled = false;
            GetNode<CollisionShape2D>("Player-None/PlayerNoneTerritory").Disabled = true;
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

