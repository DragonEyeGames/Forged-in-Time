using Godot;
using System;


public partial class NonDamage_Tower_Info : ColorRect
{
	private RichTextLabel sellLabel;
	private RichTextLabel towerName;
	[Export] public GameManager.Towers towerType;
	
	public override void _Ready(){
		//Sell Label
		sellLabel=GetNode<RichTextLabel>("sellPrice");
		sellLabel.Text=(Prices.towerPrices[towerType]/2).ToString();
		
		//Tower Name
		towerName=GetNode<RichTextLabel>("towerName");
		towerName.Text = (string)GetParent<Tower>().name;
	}
	
	
	public void on_area_entered(Area2D Enter){
		GD.Print("AAAAAAAHHHHHHHHH"); 
	}
	public void sellPress()
		{
			GD.Print("work");
			if(GetParent<Tower>().Player1 == true){
				Player1Manager.money+=Prices.towerPrices[towerType]/2;
				GetParent().QueueFree();
			}
			
			else if(GetParent<Tower>().Player1 == false){
				Player2Manager.money+=Prices.towerPrices[towerType]/2;
				GetParent().QueueFree();
			}
			
		}

}
