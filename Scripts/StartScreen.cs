using Godot;
using System;

public partial class StartScreen : Node2D         
{
	private Button startButton;
	private Button optionsButton;
	private Button exitButton;

	private const string LevelSelectScenePath = "res://Scenes/level_select.tscn";

	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		//optionsButton = GetNode<Button>("VBoxContainer/OptionsButton");
		//exitButton = GetNode<Button>("VBoxContainer/ExitButton");

		// Focus the first button
		startButton.GrabFocus();


	}

	private void OnStartButtonPressed()
	{
		var LevelSelectScene = ResourceLoader.Load<PackedScene>(LevelSelectScenePath);
		if (LevelSelectScene != null)
			GetTree().ChangeSceneToPacked(LevelSelectScene);
		else
			GD.Print("Error: Game scene not found at " + LevelSelectScene);
	}

	private void OnOptionsButtonPressed()
	{
		GD.Print("Options clicked!");
	}

	private void OnScoresPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/highScores.tscn");
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
