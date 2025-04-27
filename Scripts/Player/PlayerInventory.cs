using Godot;
using System;

public enum ItemTypes
{
	FireAxe,
	WetClotch,
	FireExtinguisher
}

public partial class PlayerInventory : Node2D
{
	public bool hasFireAxe = false;
	public bool hasWetCloth = false;
	public bool hasFireExtinguisher = false;

	public void setItem(ItemTypes item){
		switch(item){
			case ItemTypes.FireAxe:{
				hasFireAxe = true;
				break;
			}
			case ItemTypes.WetClotch:{
				hasWetCloth = true;
				break;
			}
			case ItemTypes.FireExtinguisher:{
				hasFireExtinguisher = true;
				break;
			}
		}
	}

	public void useItem(ItemTypes item){
		switch(item){
			case ItemTypes.FireAxe:{
				hasFireAxe = false;
				break;
			}
			case ItemTypes.WetClotch:{
				hasWetCloth = false;
				break;
			}
			case ItemTypes.FireExtinguisher:{
				hasFireExtinguisher = false;
				break;
			}
		}
	}
}
