using Godot;
using System;

public partial class BakeHandler : Node2D
{
	private int baking=0;
	
<<<<<<< HEAD
	[Export] public PackedScene polygon;
	[Export] public PackedScene bigPolygon;
	[Export] public PackedScene turret;
	[Export] public PackedScene plasmaTurret;
	[Export] public PackedScene tower;
	[Export] public PackedScene wall;
	[Export] public PackedScene spikes;
	
	public override void _Ready(){
		GameManager.baker=this;
	}
	
=======
>>>>>>> parent of 215896e (Merge branch 'main' into 23-add-material-farming-tower)
	public void BakePoly(){
		if(baking==0){
			baking=2;
			GetNode<NavigationRegion2D>("../TestPlacement/TestRegion").BakeNavigationPolygon();;
			GetNode<NavigationRegion2D>("../TestPlacement/NavRegion").BakeNavigationPolygon();;
		}
	}
	
	public void TestFinished(){
		baking-=1;
		if(baking<0){
			baking=0;
		}
	}
}
