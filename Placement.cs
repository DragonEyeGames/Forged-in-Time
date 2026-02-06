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
			GD.Print(atlasCoords);
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
				}
			} else if (hoveredCell!=cell) {
				Vector2I baseCoords = new Vector2I(0, 0);
				SetCell(hoveredCell, 0, baseCoords);
			}
		}
		if(Input.IsActionPressed("Click")){
			SetCell(cell, 0, placedCoords);
			UpdateNav();
		}
	}
	
	public void UpdateNav(){
		var polygon = navRegion.GetNavigationPolygon();

		if (polygon == null)
		{
			polygon = new NavigationPolygon();
			navRegion.NavigationPolygon = polygon;
		}

		// Local coordinates for the hole
		Vector2 localMouse = navRegion.ToLocal(GetGlobalMousePosition());
		float halfSize = 10f;

		// Create a new hole
		Vector2[] newHole = new Vector2[]
   		{
			new Vector2(localMouse.X - halfSize, localMouse.Y - halfSize),
			new Vector2(localMouse.X + halfSize, localMouse.Y - halfSize),
			new Vector2(localMouse.X + halfSize, localMouse.Y + halfSize),
			new Vector2(localMouse.X - halfSize, localMouse.Y + halfSize)
		};

		// Add the hole
		polygon.AddOutline(newHole);

		// Update polygon
		polygon.MakePolygonsFromOutlines();
		navRegion.NavigationPolygon = polygon;
	}
		
}
