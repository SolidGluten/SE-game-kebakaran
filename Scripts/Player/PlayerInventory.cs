using Godot;
using System;
using System.Collections.Generic;

public enum ItemTypes
{
	FireAxe,
	WetCloth,
	FireExtinguisher
}

public partial class PlayerInventory : Node2D
{
	private Dictionary<ItemTypes, bool> itemList = new Dictionary<ItemTypes, bool>{
		{ItemTypes.FireAxe, false},
		{ItemTypes.WetCloth, false},
		{ItemTypes.FireExtinguisher, false},
	};

	private ItemTypes currentItemType;
	private Usable currentItem;

  public override void _Process(double delta)
  {
		if(Input.IsActionJustPressed("switch_axe")){
			changeItem(ItemTypes.FireAxe);
		} else if(Input.IsActionJustPressed("switch_cloth")){
			changeItem(ItemTypes.WetCloth);
		} else if(Input.IsActionJustPressed("switch_extinguisher")){
			changeItem(ItemTypes.FireExtinguisher);
		}
  }

	public ItemTypes getCurrentItem(){
		return currentItemType;
	}

	public void addItem(ItemTypes item){
		itemList[item] = true;
	}

	public void useItem(ItemTypes item){
		if(itemList[item] == false) return;

		itemList[item] = false;
		currentItem.Use();
	}

	public void changeItem(ItemTypes item){
		if(itemList[item] == false) return;

		currentItemType = item;
		switch(item){
			case ItemTypes.FireAxe:{
				currentItem = (Usable) new FireAxe();
				break;
			}
			case ItemTypes.WetCloth:{
				currentItem = (Usable) new WetCloth();
				break;
			}
			case ItemTypes.FireExtinguisher:{
				currentItem = (Usable) new WetCloth();
				break;
			}
		}
	}
}
