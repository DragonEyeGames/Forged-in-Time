using Godot;
using System;


public partial class TowerInfo : ColorRect
{
	private RichTextLabel sellLabel;
	private RichTextLabel towerName;
	public GameManager.Towers towerType;
	
	public async override void _Ready(){
		//Sell Label
		GameManager.Towers towerType=GetParent<Tower>().towerType;
		sellLabel=GetNode<RichTextLabel>("sellPrice");
		sellLabel.Text="Sell For: " +  (Prices.towerPrices[towerType]/2).ToString();
		
		//Tower Name
		towerName=GetNode<RichTextLabel>("towerName");
		towerName.Text = Enum.GetNames<GameManager.Towers>()[(int)towerType].ToString();
		towerName.Text=towerName.Text.Replace("_", " ");
		GetNode<Sprite2D>("Sprite").Texture = (Texture2D)GD.Load(Cosmetics.towerDisplays[towerType]);
		while (GetParent<Tower>().hovering){
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		}
		if(!GetParent<Tower>().Player1){
			GetNode<Area2D>("SellButton/Player1").QueueFree();
		}else{
			GetNode<Area2D>("SellButton/Player2").QueueFree();
		}
		
	}
	
	public void open(){
		if(!GetParent<Tower>().hovering){
			Visible=!Visible;
			GetNode<Timer>("Timer").Start();
			GetNode<Button>("SellButton").GetChild(0).GetChild<CollisionPolygon2D>(0).SetDeferred("disabled", !Visible);
		}
		GD.Print(Visible);
	}
	
	public void timerTimeout(){
		GD.Print("TimedOut");
		Visible=true;
		open();
	}
	
	public void on_area_entered(Area2D Enter){
		GD.Print("AAAAAAAHHHHHHHHH"); 
	}
	
	public void sellPress()
		{
			GD.Print("work");
			if(GetParent<Tower>().Player1 == true){
				Player1Manager.money+=Prices.towerPrices[towerType]/2;
				GetParent<Tower>().sell();
			}
			
			else if(GetParent<Tower>().Player1 == false){
				Player2Manager.money+=Prices.towerPrices[towerType]/2;
				GetParent<Tower>().sell();
			}
			
		}

}
