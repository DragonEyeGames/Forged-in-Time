using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	
	public enum Towers{
		Turret,
		Wall,
		Watch_Tower,
		Spikes,
		Melee,
		Ranged,
		Brute,
		Healer
	}
	
	public enum UpgradeTypes{
		Speed,
		Health,
		Damage
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
	
	public static Vector4 upgradeTroop(Towers troopType, UpgradeTypes upgradeType, int player){
		foreach (TroopUpgrade troop in player1Upgrades){
			if(troop.troopType==troopType){
				if(upgradeType==UpgradeTypes.Speed){
					troop.speedLevel+=1;
				}
				if(upgradeType==UpgradeTypes.Health){
					troop.healthLevel+=1;
				}
				if(upgradeType==UpgradeTypes.Damage){
					troop.damageLevel+=1;
				}
				troop.upgradeLevel+=1;
				return fetchUpgrades(player, troopType);
			}
		}
		TroopUpgrade newTroop = new TroopUpgrade();
		player1Upgrades.Add(newTroop);
		newTroop.troopType=troopType;
		newTroop.speedLevel=0;
		newTroop.damageLevel=0;
		newTroop.healthLevel=0;
		newTroop.upgradeLevel=1;
		
		if(upgradeType==UpgradeTypes.Speed){
			newTroop.speedLevel+=1;
		}
		if(upgradeType==UpgradeTypes.Damage){
			newTroop.damageLevel+=1;
		}
		if(upgradeType==UpgradeTypes.Health){
			newTroop.healthLevel+=1;
		}
		
		return fetchUpgrades(player, troopType);
	}
	
	public static Vector4 timeAdvance(Towers troopType, int player){
		foreach (TroopUpgrade troop in player1Upgrades){
			if(troop.troopType==troopType){
				troop.speedLevel+=1;
				troop.healthLevel+=1;
				troop.damageLevel+=1;
				troop.upgradeLevel+=1;
				return fetchUpgrades(player, troopType);
			}
		}
		return fetchUpgrades(player, troopType);
	}
}
