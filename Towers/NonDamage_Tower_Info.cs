using Godot;
using System;


public partial class NonDamage_Tower_Info : ColorRect
{
	private RichTextLabel sellLabel;
	private RichTextLabel towerName;
	
	
	public override void _Ready(){
		//Sell Label
		sellLabel=GetNode<RichTextLabel>("sellPrice");
		sellLabel.Text="Frog";
		
		//Tower Name
		towerName=GetNode<RichTextLabel>("towerName");
		towerName.Text = (string)GetParent().GetParent().Name;
	}
	
	public void on_sell_pressed()
		{
			GD.Print("work");
			QueueFree();
		}

}
