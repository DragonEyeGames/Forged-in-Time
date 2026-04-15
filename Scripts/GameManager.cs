using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	
	public enum Towers{
		Turret,
		Plasma_Turret,
		Wall,
		Watch_Tower,
		Spikes,
		Melee,
		Ranged,
		Brute,
		Healer
	}
	
	public enum Upgrades{
		Plasma_Turret
	}
	
	public static Base player1Base;
	public static Base player2Base;
	public static TerritoryChecker territory;
	public static bool player1=true;
	public static bool keyboard=false;
	
	public static bool player1HUDOpen=false;
	public static bool player2HUDOpen=false;
	
	public static Placement player1Placement;
	public static Placement player2Placement;
	
	public static BakeHandler baker;
	
	public static List<TroopUpgrade> player1Upgrades = new List<TroopUpgrade>();
	public static List<TroopUpgrade> player2Upgrades = new List<TroopUpgrade>();
	
	public static Vector4 fetchUpgrades(int player, Towers troopType){
		if(player==1){
			foreach (TroopUpgrade troop in player1Upgrades){
				if(troop.troopType==troopType){
					return new Vector4(troop.upgradeLevel, troop.speedLevel, troop.healthLevel, troop.damageLevel);
				}
			}
			return new Vector4(0, 0, 0, 0);
		}
		else{
			foreach (TroopUpgrade troop in player2Upgrades){
				if(troop.troopType==troopType){
					return new Vector4(troop.upgradeLevel, troop.speedLevel, troop.healthLevel, troop.damageLevel);
				}
			}
			return new Vector4(0, 0, 0, 0);
		}
	}
}
