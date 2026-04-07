using Godot;
using System;

public abstract partial class TargetBase : Node2D
{
    public int health;
    
    public abstract void Die();
}
