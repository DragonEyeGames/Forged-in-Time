using Godot;
using System;
using System.Threading.Tasks;

public partial class Miner: TargetBase
{
    private string player = "null";
    private int maxHealth = 200;
    public int health= 4;
    private int money = 10;

    public async override void Die()
    {
        GD.Print("start");
        await Task.Delay(200);
        GameManager.territory.CallDeferred("recalculate");
        health = maxHealth;
    }

    public void onSelect()
    {
        
    }
}