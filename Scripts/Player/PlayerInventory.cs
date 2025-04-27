using Godot;
using System;
using System.Collections.Generic;

public enum ItemTypes
{
	None = 0,
	FireAxe = 1,
	WetCloth = 2,
	FireExtinguisher = 3
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

	[Signal] public delegate void ItemAddedEventHandler(int item);
	[Signal] public delegate void ItemChangedEventHandler(int item);

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
		EmitSignal("ItemAdded", (int)item);
		changeItem(item);
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
				currentItem = new FireAxeTool();
				break;
			}
			case ItemTypes.WetCloth:{
				currentItem = new WetClotchTool();
				break;
			}
			case ItemTypes.FireExtinguisher:{
				currentItem = new FireExtinguisherTool();
				break;
			} default: {
				break;
			}
		}

		// EmitSignal("ItemSwitched", (int)item);
	}
}
