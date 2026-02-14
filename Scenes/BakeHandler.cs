using Godot;
using System;

public partial class BakeHandler : Node2D
{
	private int baking=0;
	
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
