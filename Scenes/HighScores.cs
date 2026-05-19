using Godot;
using System;

public partial class HighScores : Node2D
{
	private string _savePath = "user://highscores.tres";
	private Godot.Collections.Array<int> Highscores = new();
	
	public override void _Ready()
	{
		Highscores=LoadData().Highscores;
		GD.Print(Highscores);
		if(Highscores.Count<0){
			GD.Print("NADA");
			GetNode<ScrollContainer>("ScrollContainer").Visible=false;
		} else {
			int index=0;
			foreach (Control control in GetNode<VBoxContainer>("ScrollContainer/VBoxContainer").GetChildren()){
				if(Highscores.Count>index){
					control.GetChild<RichTextLabel>(0).Text=(index+1).ToString() + " - " + Highscores[index];
					index+=1;
				} else {
					control.Visible=false;
				}
				
			}
		}
	}
	
	public void SaveData(int health, Vector2 pos)
	{
		var saveGame = new SaveGame();

		Error error = ResourceSaver.Save(saveGame, _savePath);
		if (error != Error.Ok)
		{
			GD.Print("Save failed: ", error);
		}
	}

	public SaveGame LoadData()
	{
		if (!FileAccess.FileExists(_savePath))
		{
			return new SaveGame(); // Return default data if no save exists
		}

		return ResourceLoader.Load<SaveGame>(_savePath, cacheMode: ResourceLoader.CacheMode.Ignore);
	}
	
	private void ToMain(){
		GetTree().ChangeSceneToFile("res://Scenes/StartScreen.tscn");
	}
}
