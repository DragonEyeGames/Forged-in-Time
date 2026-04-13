using Godot;
using System;
using System.Threading.Tasks;

public partial class Miner: TargetBase
{
    private string player = "null";
    private int maxHealth = 200;
    public int health= 4;
    private int money = 10;
    private int playerPressed;

    public async override void Die()
    {
        GD.Print("start");
        await Task.Delay(200);
        GameManager.territory.CallDeferred("recalculate");
        health = maxHealth;
    }

    public async void onSelect()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        playerPressed = GetNode<Controller>("Button").pressor;
        if (playerPressed == 1)
        {
            GetNode<Base>("Player1Base").target = this;
        }
    }
}