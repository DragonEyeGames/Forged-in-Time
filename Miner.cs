using Godot;
using System;

public partial class Miner : TargetBase
{
    public override int health { get; set; } = 200;
    public override int maxHealth { get; set; } = 200;
    public override bool isBase { get; set; } = false;
    public bool playerKilled;

    public override void Die()
    {
        if (playerKilled = true)
        {
            
        }
        
    }

    public void onSelect()
    {
        GD.Print("onSelect");
        if (GameManager.player1Target == this)
        {
            
        }
    }

    public override void _Ready()
    {
        
    }
}
