using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Placement : TileMapLayer
{
	private Vector2I hoveredCell = new Vector2I(0, 0);
	[Export] public TestValid tester;
	
	// Called when the node enters the scene tree for the first time.

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		Vector2I invalidCoords = new Vector2I(1, 0);
		if(Input.IsActionJustPressed("Click") && GameManager.placing){
			Click();
		}
	}
	
	private void Click(){
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		Vector2I invalidCoords = new Vector2I(1, 0);
		Vector2I atlasCoords = GetCellAtlasCoords(cell);
		if(atlasCoords!=placedCoords){
			SetCell(cell, 0, placedCoords);
			GameManager.validPlacement=true;
		} else{
			GameManager.validPlacement=false;
		}
		
	}
	
	Vector2 SnapToTopLeft(Vector2 position)
	{
		const int cellSize = 16;

		float x = Mathf.Floor(position.X / cellSize) * cellSize;
		float y = Mathf.Floor(position.Y / cellSize) * cellSize;

		return new Vector2(x, y);
	}
		
}
