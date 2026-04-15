using Godot;
using System;

public partial class UpgradeSlot : ColorRect
{
	public enum UpgradeTypes{
		Speed,
		Defense,
		Attack
	}
	
	[Export] private UpgradeTypes upgradeType;
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node2D child in GetNode<Node2D>("Icons").GetChildren())
		{
			child.Visible=false;
		}
		if(upgradeType==UpgradeTypes.Speed){
			GetNode<Sprite2D>("Icons/Speed").Visible=true;
			GetNode<RichTextLabel>("Type").Text="Speed";
		}
		else if(upgradeType==UpgradeTypes.Attack){
			GetNode<Sprite2D>("Icons/Attack").Visible=true;
			GetNode<RichTextLabel>("Type").Text="Attack";
		}
		else if(upgradeType==UpgradeTypes.Defense){
			GetNode<Sprite2D>("Icons/Defense").Visible=true;
			GetNode<RichTextLabel>("Type").Text="Defense";
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void upgrade(){
		GD.Print(upgradeType);
	}
}
