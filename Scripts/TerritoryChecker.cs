using Godot;
using System;

public partial class TerritoryChecker : TileMapLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//recalculate();
		GameManager.territory=this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public async override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Territory")){
			await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
			recalculate();
		}
	}
	
	public void recalculate(){
		clearNonexistent();
		betterTerritory(3, 0);
		betterTerritory(4, 1);
	}
	
	private void clearNonexistent(){
		int TILE_SIZE = 16;
		int WIDTH = 72;
		int HEIGHT = 40;

		var space = GetWorld2D().DirectSpaceState;

		RectangleShape2D tileShape = new RectangleShape2D
		{
			Size = new Vector2(TILE_SIZE, TILE_SIZE)
		};

		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				Vector2 worldPos = new Vector2(
					x * TILE_SIZE + TILE_SIZE / 2,
					y * TILE_SIZE + TILE_SIZE / 2
				);

				var query = new PhysicsShapeQueryParameters2D
				{
					Shape = tileShape,
					Transform = new Transform2D(0, worldPos),
					CollisionMask = 1u << 5,
					CollideWithAreas = true
				};
				if (space.IntersectShape(query).Count > 0)
				{
					EraseCell(new Vector2I(x, y));
				}
			}
		}
	}
	
	private void betterTerritory(int layer, int pos){
		int TILE_SIZE = 16;
		int WIDTH = 72;
		int HEIGHT = 40;

		var space = GetWorld2D().DirectSpaceState;

		RectangleShape2D tileShape = new RectangleShape2D
		{
			Size = new Vector2(TILE_SIZE, TILE_SIZE)
		};

		for (int y = 0; y < HEIGHT; y++)
		{
			for (int x = 0; x < WIDTH; x++)
			{
				Vector2 worldPos = new Vector2(
					x * TILE_SIZE + TILE_SIZE / 2,
					y * TILE_SIZE + TILE_SIZE / 2
				);

				var query = new PhysicsShapeQueryParameters2D
				{
					Shape = tileShape,
					Transform = new Transform2D(0, worldPos),
					CollisionMask = 1u << layer,
					CollideWithAreas = true
				};
				Vector2I territoryCoords = new Vector2I(pos, 0);
				if (space.IntersectShape(query).Count > 0)
				{
					if(GetCellSourceId(new Vector2I(x, y))!=0){
						SetCell(new Vector2I(x, y), 0, territoryCoords);
					}
				}
			}
		}

	}
	
	public bool IsTerritory(Vector2 position, int pos){
		Vector2I cell = LocalToMap(position);
		Vector2I atlasCoords = GetCellAtlasCoords(cell);
		return atlasCoords==new Vector2I(pos, 0);
	}
	
}
