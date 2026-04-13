using Godot;
using System;

public partial class StartScreen : Node2D         
{
	private Button startButton;
	private Button optionsButton;
	private Button exitButton;

	private const string MainScenePath = "res://Scenes/main.tscn";

	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		optionsButton = GetNode<Button>("VBoxContainer/OptionsButton");
		exitButton = GetNode<Button>("VBoxContainer/ExitButton");

		// Focus the first button
		startButton.GrabFocus();

		// Connect signals
		startButton.Pressed += OnStartButtonPressed;
		optionsButton.Pressed += OnOptionsButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		var mainScene = ResourceLoader.Load<PackedScene>(MainScenePath);
		if (mainScene != null)
			GetTree().ChangeSceneToPacked(mainScene);
		else
			GD.Print("Error: Game scene not found at " + MainScenePath);
	}

	private void OnOptionsButtonPressed()
	{
		GD.Print("Options clicked!");
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
