using Godot;
using System;

public partial class Pause : CanvasLayer
{
	private Button resumeButton;
	private Button optionsButton;
	private Button exitButton;
	private VBoxContainer vbox;

	public override void _Ready()
	{
		vbox = GetNode<VBoxContainer>("VBoxContainer");
		vbox.MouseFilter = Control.MouseFilterEnum.Stop;

		resumeButton = GetNode<Button>("VBoxContainer/resume");
		optionsButton = GetNode<Button>("VBoxContainer/options");
		exitButton = GetNode<Button>("VBoxContainer/exit");

		resumeButton.Pressed += OnResumePressed;
		optionsButton.Pressed += OnOptionsPressed;
		exitButton.Pressed += OnExitPressed;

		Visible = false;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		Visible = !Visible;
		GetTree().Paused = Visible;

		if (Visible)
			resumeButton.GrabFocus();
	}

	private void OnResumePressed()
	{
		GD.Print("resumeButton = " + resumeButton);
		GD.Print(Visible);
		if(Visible){
			TogglePause();

		}
		GD.Print(Visible);
	}

	private void OnOptionsPressed()
	{
		GD.Print("Options clicked!");
	}

	private void OnExitPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Scenes/StartScreen.tscn");
	}
}
