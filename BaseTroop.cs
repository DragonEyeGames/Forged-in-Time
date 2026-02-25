using Godot;
using System;

public abstract partial class BaseTroop : CharacterBody2D
{
    public bool player1 = true;
    public abstract float Speed {get; set;}
    
    public NavigationAgent2D navAgent;
    
}
