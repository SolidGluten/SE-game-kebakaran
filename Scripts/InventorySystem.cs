using Godot;
using System;

public partial class InventorySystem : Node
{
	public InventorySystem Instance { get; private set; }

	public IConsumable consumables;

	public override void _Ready()
	{
		Instance = this;
	}
}
