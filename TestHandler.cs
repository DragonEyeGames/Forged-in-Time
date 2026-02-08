using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class TestHandler : Node2D
{
	private Vector2I hoveredCell = new Vector2I(0, 0);
	private NavigationRegion2D testRegion;
	private NavigationRegion2D navRegion;
	private Polygon2D selectedPolygon = null;
	[Export] public PackedScene polygon;
	private int baking = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		testRegion=GetNode<NavigationRegion2D>("TestRegion");
		navRegion=GetNode<NavigationRegion2D>("NavRegion");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(selectedPolygon==null) {
			selectedPolygon = polygon.Instantiate() as Polygon2D;
			testRegion.AddChild(selectedPolygon);
		}
		selectedPolygon.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
		selectedPolygon.Position+=new Vector2(1300, 0);
		if(Input.IsActionPressed("Click")){
			Polygon2D selectedPolygon2 = polygon.Instantiate() as Polygon2D;
			navRegion.AddChild(selectedPolygon2);
			selectedPolygon2.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
			selectedPolygon=null;
		}
		BakePoly();
	}
	
	public void UpdateNav(){
		Polygon2D newPolygon2 = polygon.Instantiate() as Polygon2D;
		testRegion.AddChild(newPolygon2);
		newPolygon2.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
		BakePoly();
	}
	
	Vector2 SnapToTopLeft(Vector2 position)
	{
		const int cellSize = 16;

		float x = Mathf.Floor(position.X / cellSize) * cellSize;
		float y = Mathf.Floor(position.Y / cellSize) * cellSize;

		return new Vector2(x, y);
	}
	
	public void BakePoly(){
		if(baking==0){
			baking=2;
			testRegion.BakeNavigationPolygon();
			navRegion.BakeNavigationPolygon();
		}
	}
	
	public void TestFinished(){
		baking-=1;
		if(baking<0){
			baking=0;
		}
	}
		
}
