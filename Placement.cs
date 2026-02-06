using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Placement : TileMapLayer
{
	private Vector2I hoveredCell = new Vector2I(0, 0);
	private NavigationRegion2D navRegion;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		navRegion=GetNode<NavigationRegion2D>("../NavigationRegion2D");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		if(cell!=hoveredCell && GetCellSourceId(cell) != -1){
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
					GD.Print("preClick");
					SetCell(cell, 0, placedCoords);
					GD.Print("REad");
					UpdateNav();
				}
			} else if (hoveredCell!=cell) {
				Vector2I baseCoords = new Vector2I(0, 0);
				SetCell(hoveredCell, 0, baseCoords);
			}
		}
		if(Input.IsActionPressed("Click")){
			SetCell(cell, 0, placedCoords);
			GD.Print("REad");
			UpdateNav();
		}
	}
	
	public void UpdateNav(){
		Polygon2D newPolygon = GetNode<Polygon2D>("../NavigationRegion2D/Polygon2D").Duplicate() as Polygon2D;
		GetNode("../NavigationRegion2D").AddChild(newPolygon);
		newPolygon.GlobalPosition=SnapToTopLeft(GetGlobalMousePosition());
		navRegion.BakeNavigationPolygon();
		GD.Print("Made It");
	}
	
	Vector2 SnapToTopLeft(Vector2 position)
	{
		const int cellSize = 16;

		float x = Mathf.Floor(position.X / cellSize) * cellSize;
		float y = Mathf.Floor(position.Y / cellSize) * cellSize;

		return new Vector2(x, y);
	}
		
}
