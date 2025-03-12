using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InventorySystem : Node
{
	public InventorySystem Instance { get; private set; }
	
	public List<IConsumable> consumables { get; private set; } = new List<IConsumable>();
	private IConsumable consumableInHand = null;

	public override void _Ready()
	{
		Instance = this;
	}

	public bool addConsumable(IConsumable consumable)
	{
		if (consumables.Any((item) => item == consumable)) return false; // ensures no duplicate
		consumables.Add(consumable);
		return true;
	}

	public void switchConsumable(int index)
	{
		if(index < 0 && index > consumables.Count - 1){
			return;
		}
		consumableInHand = consumables[index];
	}
}
