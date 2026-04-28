using Godot;
using System;

public abstract partial class TargetBase : Node2D
{
    public abstract int health { get; set; }
    public abstract int maxHealth { get; set; }
    public abstract bool isBase {get; set;}
    public abstract void Die(); 
    public int playerClicked;

}
