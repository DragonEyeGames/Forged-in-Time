using Godot;
using System;

public partial class Miner : Tower
{
    public override void _PhysicsProcess(double delta)
    {
        TowerGenerics();
    }
}
