using Godot;
using System;

public partial class Miner : TargetBase
{
    public override int health { get; set; } = 200;
    public override int maxHealth { get; set; } = 200;
    public override bool isBase { get; set; } = false;
    public bool playerKilled;
    private int playerClicked;

    public override void Die()
    {
        if (playerKilled == true)
        {
            GetNode<CollisionShape2D>("Player-1/Player1Territory").Disabled = false;
            GetNode<CollisionShape2D>("Player-1/Player2Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-1/PlayerNoneTerritory").Disabled = true;
        }
        else if (playerKilled == false)
        {
            GetNode<CollisionShape2D>("Player-1/Player1Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-1/Player2Territory").Disabled = false;
            GetNode<CollisionShape2D>("Player-1/PlayerNoneTerritory").Disabled = true;
        }
        else
        {
            GetNode<CollisionShape2D>("Player-1/Player1Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-1/Player2Territory").Disabled = true;
            GetNode<CollisionShape2D>("Player-1/PlayerNoneTerritory").Disabled = false;
        }
        
    }

    public void onSelect()
    {
        
        GD.Print("onSelect");
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
    }

    public override void _Ready()
    {
        
    }
}
