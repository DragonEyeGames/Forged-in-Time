using Godot;
using System;
using System.Collections.Generic;

public partial class AoeTower :  Tower
{
    private List<CharacterBody2D> player1Colliding = new List<CharacterBody2D> {};
    private List<CharacterBody2D> player2Colliding = new List<CharacterBody2D> {};
    [Export] public int damage=3;
    [Export] public Timer cooldown;
    private bool canShoot = true;
    
    public override void _Process(double delta)
    {
        TowerGenerics();
        if (!hovering && canShoot)
        {
            if (Player1 && player2Colliding.Count > 0)
            {
                GD.Print(player2Colliding.Count);
                canShoot = false;
                cooldown.Start();
                for (int i = 0; i <= player2Colliding.Count - 1; i++)
                {
                    Troop troop = player2Colliding[i] as Troop;
                    troop.health -= damage;
                    GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
                }
            }

            if (!Player1 && player1Colliding.Count > 0)
            {
                GD.Print(player1Colliding.Count);
                canShoot = false;
                cooldown.Start();
                for (int i = 0; i <= player1Colliding.Count - 1; i++)
                {
                    Troop troop = player1Colliding[i] as Troop;
                    GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
                    troop.health -= damage;
                    GD.Print("Damagin");
                }
            }
        }
    }
    
    public void Player1Entered(Node2D body){
        player1Colliding.Add(body.GetParent() as CharacterBody2D);
        GD.Print(player1Colliding.Count);

    }
	
    public void Player1Exited(Node2D body){
        player1Colliding.Remove(body.GetParent() as CharacterBody2D);
    }
	
    public void Player2Entered(Node2D body){
        player2Colliding.Add(body.GetParent() as CharacterBody2D);
    }
	
    public void Player2Exited(Node2D body){
        player2Colliding.Remove(body.GetParent() as CharacterBody2D);
    }

    public void on_cooldown_timeout()
    {
        canShoot = true;
    }
}
