using Godot;
using System;

public partial class EndScreen: Node2D         
{
	private Button mainButton;
	private Button scoresButton;
	private Button exitButton;

	private const string MainScenePath = "res://Scenes/Main.tscn";

	public override void _Ready()
	{
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
