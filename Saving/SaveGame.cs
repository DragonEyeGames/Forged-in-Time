using Godot;
using System;

[GlobalClass]
public partial class SaveGame : Resource
{
	[Export] public Godot.Collections.Array<int> Highscores { get; set; } = new() {1000, 800, 230};
}
