How to use the tower system!
The tower system is a very easy and straightforward process once you get to know it
For this demonstration I will go over the turret because it meets all requirements.

To start, you will need a Node2D holder with a script that inherits from Tower.
Because we inherit from tower, we will need to call TowerGenerics(); in the process.
Calling this just does stuff that all the towers need to do.

Moving down the node tree, we will need sprites. This is very obvious though.

Next, we will need territory claiming Area2D's.
They will need to be named 'Player1Territory' on layer 4, and 'Player2Territory' which is on layer 5
These should have the same collision shape underneath. Just make one and then duplicate it so they are the same.

Next, if you want to attack you will need two more Area2D's.
One named 'Player1' masking layer 2. There will be another named 'Player2' that masks layer 3.

If you want an attack, you will also have to make a cooldown timer. Time to start coding!

This is the code for finding the enemies
	public void Player1Entered(Node2D body){
		player1Colliding.Add(body.GetParent() as CharacterBody2D);
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
Basically, this just adds them to lists so we can attack them later.

When we get into the actual damaging, it goes like this. 
	if(Player1 && player2Colliding.Count>0){
		canShoot=false;
		cooldown.Start();
		turret.LookAt(player2Colliding[0].GlobalPosition);
		turret.GlobalRotation-=(float)Math.PI/2;
		Troop troop = player2Colliding[0] as Troop;
		troop.health-=damage;
		GetNode<AnimationPlayer>("Animator").Play("pew");
	}
Obiously, this is the script for Player1, but the code for player2 is very similar.
so canShoot=false and cooldown.Start makes it so we can only shoot so fast.
Anything with turret is purely for appearance.
Troop troop is set as the first person in the attacking list
We then deal damage to the troop, and play an animation.
Thats pretty much it.
