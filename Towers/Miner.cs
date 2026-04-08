using Godot;
using System;

public partial class Miner: TargetBase
{
    private string player = "null";
    private int maxHealth = 200;
    public int health= 4;
    private int money = 10;

    public override void Die()
    {
        health = maxHealth;
    }
    

}
