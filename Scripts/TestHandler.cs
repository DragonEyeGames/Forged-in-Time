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
	private Tower selectedTower = null;
	[Export] public bool player1=false;
	[Export] public TestValid tester;
	private int baking = 0;
	private bool initializePlace=false;
	private BakeHandler parentNode;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parentNode=GetParent<BakeHandler>();
		testRegion=GetNode<NavigationRegion2D>("../../TestPlacement/TestRegion");
		navRegion=GetNode<NavigationRegion2D>("../../TestPlacement/NavRegion");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		BakePoly();
		if(player1){
			if(!Player1Manager.placing){
				return;
			}
		} else if (!player1){
			if(!Player2Manager.placing){
				return;
			}
		}
		
		if(initializePlace && player1){
			initializePlace=false;
			if(Player1Manager.validPlacement && tester.isValid() && Player1Manager.placing){
				Player1Manager.placing=false;
				Polygon2D selectedPolygon2 = parentNode.polygon.Instantiate() as Polygon2D;;
				if(Player1Manager.toPlace==GameManager.Towers.Spikes){
					selectedPolygon2 = parentNode.bigPolygon.Instantiate() as Polygon2D;
				}
				navRegion.AddChild(selectedPolygon2);
				selectedPolygon2.GlobalPosition=SnapToTopLeft(Player1Manager.cursor.GlobalPosition);
				selectedPolygon=null;
				selectedTower.hovering=false;
				selectedTower=null;
				return;
			}
		} else if(initializePlace && !player1){
			initializePlace=false;
			if(Player2Manager.validPlacement && tester.isValid() && Player2Manager.placing){
				Player2Manager.placing=false;
				Polygon2D selectedPolygon2 = parentNode.polygon.Instantiate() as Polygon2D;;
				if(Player2Manager.toPlace==GameManager.Towers.Spikes){
					selectedPolygon2 = parentNode.bigPolygon.Instantiate() as Polygon2D;
				}
				
				navRegion.AddChild(selectedPolygon2);
				selectedPolygon2.GlobalPosition=SnapToTopLeft(Player2Manager.cursor.GlobalPosition);
				selectedPolygon=null;
				selectedTower.hovering=false;
				selectedTower=null;
				return;
			}
		}
		if(selectedTower==null && player1){
			if(Player1Manager.toPlace==GameManager.Towers.Spikes){
				selectedTower=parentNode.spikes.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
				selectedPolygon = parentNode.bigPolygon.Instantiate() as Polygon2D;
				testRegion.AddChild(selectedPolygon);
			}
			if(selectedPolygon==null) {
				selectedPolygon = parentNode.polygon.Instantiate() as Polygon2D;
				testRegion.AddChild(selectedPolygon);
			}
			if(Player1Manager.toPlace==GameManager.Towers.Turret){
				selectedTower=parentNode.turret.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player1Manager.toPlace==GameManager.Towers.Wall){
				selectedTower=parentNode.wall.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player1Manager.toPlace==GameManager.Towers.Plasma_Turret){
				selectedTower=parentNode.plasmaTurret.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player1Manager.toPlace==GameManager.Towers.Watch_Tower){
				selectedTower=parentNode.tower.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
		} else if(selectedTower==null && !player1){
			if(Player2Manager.toPlace==GameManager.Towers.Spikes){
				selectedTower=parentNode.spikes.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
				selectedPolygon = parentNode.bigPolygon.Instantiate() as Polygon2D;
				testRegion.AddChild(selectedPolygon);
			}
			if(selectedPolygon==null) {
				selectedPolygon = parentNode.polygon.Instantiate() as Polygon2D;
				testRegion.AddChild(selectedPolygon);
			}
			if(Player2Manager.toPlace==GameManager.Towers.Turret){
				selectedTower=parentNode.turret.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player2Manager.toPlace==GameManager.Towers.Wall){
				selectedTower=parentNode.wall.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player2Manager.toPlace==GameManager.Towers.Plasma_Turret){
				selectedTower=parentNode.plasmaTurret.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
			if(Player2Manager.toPlace==GameManager.Towers.Watch_Tower){
				selectedTower=parentNode.tower.Instantiate() as Tower;
				GetParent().AddChild(selectedTower);
			}
		}
		selectedTower.Player1=player1;
		if(player1){
			selectedPolygon.GlobalPosition=SnapToTopLeft(Player1Manager.cursor.GlobalPosition);
			selectedTower.GlobalPosition=SnapToTopLeft(Player1Manager.cursor.GlobalPosition);
		}
		if(!player1){
			selectedPolygon.GlobalPosition=SnapToTopLeft(Player2Manager.cursor.GlobalPosition);
			selectedTower.GlobalPosition=SnapToTopLeft(Player2Manager.cursor.GlobalPosition);
		}
		selectedPolygon.Position+=new Vector2(1300, 0);
		
		BakePoly();
	}
	
	public void UpdateNav(){
		Polygon2D newPolygon2 = parentNode.polygon.Instantiate() as Polygon2D;
		testRegion.AddChild(newPolygon2);
		if(player1){
			newPolygon2.GlobalPosition=SnapToTopLeft(Player1Manager.cursor.GlobalPosition);
		}
		if(player1){
			newPolygon2.GlobalPosition=SnapToTopLeft(Player2Manager.cursor.GlobalPosition);
		}
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
		GetParent<BakeHandler>().BakePoly();
	}
	
	public override void _Input(InputEvent @event)
	{
		if(!GameManager.keyboard){
			if(player1 && 0==@event.Device){
				if(@event.IsActionPressed("Select") && tester.isValid() && Player1Manager.placing && !initializePlace && GetNode<TerritoryChecker>("../../Territory").IsTerritory(SnapToTopLeft(Player1Manager.cursor.GlobalPosition), 0)){
					initializePlace=true;
					//GetViewport().SetInputAsHandled();
				}
			} else if(!player1 && 1==@event.Device){
				if(@event.IsActionPressed("Select") && tester.isValid() && Player2Manager.placing && !initializePlace && GetNode<TerritoryChecker>("../../Territory").IsTerritory(SnapToTopLeft(Player2Manager.cursor.GlobalPosition), 1)){
					initializePlace=true;
					//GetViewport().SetInputAsHandled();
				}
			}
		} else if(GameManager.keyboard){
			if(player1){
				if(@event.IsActionPressed("Click-1") && tester.isValid() && Player1Manager.placing && !initializePlace && GetNode<TerritoryChecker>("../../Territory").IsTerritory(SnapToTopLeft(Player1Manager.cursor.GlobalPosition), 0)){
					initializePlace=true;
					//GetViewport().SetInputAsHandled();
				}
			} else if(!player1){
				if(@event.IsActionPressed("Click-2") && tester.isValid() && Player2Manager.placing && !initializePlace && GetNode<TerritoryChecker>("../../Territory").IsTerritory(SnapToTopLeft(Player2Manager.cursor.GlobalPosition), 1)){
					initializePlace=true;
					//GetViewport().SetInputAsHandled();
				}
			}
		}
		
		
	}
		
}
