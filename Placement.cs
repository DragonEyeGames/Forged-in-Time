using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Placement : TileMapLayer
{
	private Vector2I hoveredCell = new Vector2I(0, 0);
	private NavigationRegion2D navRegion;
	private NavigationRegion2D testRegion;
	private Polygon2D selectedPolygon = null;
	[Export] public PackedScene polygon;
	private int baking = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		navRegion=GetNode<NavigationRegion2D>("../NavRegion");
		testRegion=GetNode<NavigationRegion2D>("../TestRegion");
		BakePoly();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(selectedPolygon==null) {
			selectedPolygon = polygon.Instantiate() as Polygon2D;
			testRegion.AddChild(selectedPolygon);
		}
		if(Input.IsActionJustPressed("Poly")){
			BakePoly();
		}
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		if(cell!=hoveredCell){
			selectedPolygon.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
		}
		if(cell!=hoveredCell && GetCellSourceId(cell) != -1){
			BakePoly();
			Vector2I atlasCoords = GetCellAtlasCoords(cell);
			if(atlasCoords!=placedCoords){
				Vector2I baseCoords = new Vector2I(0, 0);
				Vector2I lastCoords = GetCellAtlasCoords(hoveredCell);
				if(lastCoords!=placedCoords){
					SetCell(hoveredCell, 0, baseCoords);
				}
				SetCell(cell, 0, hoverCoords);
				hoveredCell=cell;
				if(Input.IsActionPressed("Click")){
					SetCell(cell, 0, placedCoords);
					UpdateNav();
				}
			} else if (hoveredCell!=cell) {
				Vector2I baseCoords = new Vector2I(0, 0);
				Vector2I lastCoords = GetCellAtlasCoords(hoveredCell);
				if(lastCoords!=placedCoords){
					SetCell(hoveredCell, 0, baseCoords);
				}
				if(Input.IsActionPressed("Click")){
					SetCell(hoveredCell, 0, placedCoords);
					UpdateNav();
				}
			}
		}
		else if(Input.IsActionJustPressed("Click")){
			SetCell(cell, 0, placedCoords);
			UpdateNav();
		}
	}
	
	public void UpdateNav(){
		Polygon2D newPolygon = polygon.Instantiate() as Polygon2D;
		navRegion.AddChild(newPolygon);
		newPolygon.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
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
			navRegion.BakeNavigationPolygon();
			testRegion.BakeNavigationPolygon();
		}
	}
	
	public void NavFinished(){
		baking-=1;
		if(baking<0){
			baking=0;
		}
	}
	
	public void TestFinished(){
		baking-=1;
		if(baking<0){
			baking=0;
		}
	}
		
}
