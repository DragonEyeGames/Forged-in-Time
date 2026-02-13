using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Placement : Node2D
{
	[Export] public bool player1=false;
	private Vector2I hoveredCell = new Vector2I(0, 0);
	[Export] public TestValid tester;
	private TileMapLayer layer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		layer=GetParent<TileMapLayer>();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell =layer.LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		Vector2I invalidCoords = new Vector2I(1, 0);
		if(player1){
			if(Input.IsActionJustPressed("Click") && Player1Manager.placing){
				Click();
			}
		} else if(!player1){
			if(Input.IsActionJustPressed("Click") && Player2Manager.placing){
				Click();
			}
		}
		
	}
	
	private void Click(){
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = layer.LocalToMap(mouseWorldPos);
		Vector2I hoverCoords = new Vector2I(0, 1);
		Vector2I placedCoords = new Vector2I(1, 1);
		Vector2I invalidCoords = new Vector2I(1, 0);
		Vector2I atlasCoords = layer.GetCellAtlasCoords(cell);
		if(player1){
			if(atlasCoords!=placedCoords){
				layer.SetCell(cell, 0, placedCoords);
				Player1Manager.validPlacement=true;
			} else{
				Player1Manager.validPlacement=false;
			}
		 } else if(!player1){
			if(atlasCoords!=placedCoords){
				layer.SetCell(cell, 0, placedCoords);
				Player2Manager.validPlacement=true;
			} else{
				Player2Manager.validPlacement=false;
				GD.Print(atlasCoords);
				GD.Print(placedCoords);
			}
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
