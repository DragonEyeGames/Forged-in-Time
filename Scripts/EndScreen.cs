using Godot;
using System;

public partial class EndScreen: Node2D         
{
	private Button mainButton;
	private Button scoresButton;
	private Button exitButton;

	private const string MainScenePath = "res://Scenes/Main.tscn";
	private string _savePath = "user://highscores.tres";
	
	public override void _Ready()
	{
		GD.Print("eneded");
		var saveGame = new SaveGame();
		SaveGame data = LoadData();
		if(GameManager.winner==1){
			GetNode<RichTextLabel>("Winner").Text="Player 2 Wins!";
			data.Highscores.Add(Player2Manager.score);
		} else {
			GetNode<RichTextLabel>("Winner").Text="Player 1 Wins!";
			data.Highscores.Add(Player1Manager.score);
		}
		saveGame.Highscores=data.Highscores;
		Error error = ResourceSaver.Save(saveGame, _savePath);
		if (error != Error.Ok)
		{
			GD.Print("Save failed: ", error);
		}
		
		GetNode<RichTextLabel>("Player1/Score").Text=Player1Manager.score.ToString();
		GetNode<RichTextLabel>("Player2/Score").Text=Player2Manager.score.ToString();
		mainButton = GetNode<Button>("VBoxContainer/MainButton");
		scoresButton = GetNode<Button>("VBoxContainer/ScoresButton");
		exitButton = GetNode<Button>("VBoxContainer/ExitButton");

		// Focus the first button
		mainButton.GrabFocus();

		// Connect signals
		mainButton.Pressed += OnMainButtonPressed;
		scoresButton.Pressed += OnScoresButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
	}

	public SaveGame LoadData()
	{
		if (!FileAccess.FileExists(_savePath))
		{
			return new SaveGame(); // Return default data if no save exists
		}

		return ResourceLoader.Load<SaveGame>(_savePath, cacheMode: ResourceLoader.CacheMode.Ignore);
	}

	private void OnMainButtonPressed()
	{
		var mainScene = ResourceLoader.Load<PackedScene>(MainScenePath);
		if (mainScene != null)
			GetTree().ChangeSceneToFile("res://Scenes/StartScreen.tscn");
		else
			GD.Print("Error: Game scene not found at " + MainScenePath);
	}

	private void OnScoresButtonPressed()
	{
		GD.Print("Scores clicked!");
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
