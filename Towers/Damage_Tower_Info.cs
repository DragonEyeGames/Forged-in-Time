using Godot;
using System;


public partial class Damage_Tower_Info : ColorRect
{
	private RichTextLabel sellLabel;
	private RichTextLabel killsLabel;
	private RichTextLabel towerName;
	
	
	public override void _Ready(){
		//Sell Label
		sellLabel=GetNode<RichTextLabel>("sellPrice");
		sellLabel.Text="Frog";
		
		//kills Label
		killsLabel=GetNode<RichTextLabel>("lifetimeKills");
		killsLabel.Text="Frog";
		
		//Tower Name
		towerName=GetNode<RichTextLabel>("towerName");
		towerName.Text = (string)GetParent().GetParent<Tower>().name;
	}
	
	public void on_sell_pressed()
		{
			GD.Print("work");
			GetParent().GetParent().QueueFree();
		}

}
