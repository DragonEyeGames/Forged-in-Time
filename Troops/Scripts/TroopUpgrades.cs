using Godot;
using System;
using System.Collections.Generic;


public partial class TroopUpgrades : Node
{
    public List<int> Health = new List<int> { 0, 1, 3, 4};
    public List<int> Damage = new List<int> { 0, 1, 2, 3};
    public List<int> Speed = new List<int> { 0, 10, 20, 30};
}
