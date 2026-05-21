using Godot;
using System;

public partial class LevelSelect : Node2D         
{
	private Button Level1Button;
	private Button Level2Button;
	private Button Level3Button;
	private Button Level4Button;
	
	private const string Level1Path = "res://Scenes/plains_level.tscn";
	private const string Level2Path = "res://Scenes/desert_level.tscn";
	private const string Level3Path = "res://Scenes/forest_level.tscn";
	private const string Level4Path = "res://Scenes/tundra_level.tscn";

	public override void _Ready()
	{
		Level1Button = GetNode<Button>("ColorRect/VBoxContainer/Level_1_Button");
		Level2Button = GetNode<Button>("ColorRect/VBoxContainer/Level_2_Button");
		Level3Button = GetNode<Button>("ColorRect/VBoxContainer/Level_3_Button");
		Level4Button = GetNode<Button>("ColorRect/VBoxContainer/Level_4_Button");

		// Focus the first button
		Level1Button.GrabFocus();

	}
	
	public void OnLevel1ButtonPressed()
	{
		var Level1Scene = ResourceLoader.Load<PackedScene>(Level1Path);
		if (Level1Scene != null)
			GetTree().ChangeSceneToPacked(Level1Scene);
		else
			GD.Print("Error: Game scene not found at " + Level1Scene);
	}
	public void OnLevel2ButtonPressed()
	{
		var Level2Scene = ResourceLoader.Load<PackedScene>(Level2Path);
		if (Level2Scene != null)
			GetTree().ChangeSceneToPacked(Level2Scene);
		else
			GD.Print("Error: Game scene not found at " + Level2Scene);
	}
	public void OnLevel3ButtonPressed()
	{
		var Level3Scene = ResourceLoader.Load<PackedScene>(Level3Path);
		if (Level3Scene != null)
			GetTree().ChangeSceneToPacked(Level3Scene);
		else
			GD.Print("Error: Game scene not found at " + Level3Scene);
	}
	public void OnLevel4ButtonPressed()
	{
		var Level4Scene = ResourceLoader.Load<PackedScene>(Level4Path);
		if (Level4Scene != null)
			GetTree().ChangeSceneToPacked(Level4Scene);
		else
			GD.Print("Error: Game scene not found at " + Level4Scene);
	}
}
