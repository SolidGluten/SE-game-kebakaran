using Godot;
using System;

public abstract partial class Pickable : Area2D
{
	protected PlayerInventory playerInventory;
	[Export] public ItemTypes type;

  public override void _Ready()
  {
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
		this.BodyEntered += OnPlayerEnter;
  }

	public void OnPlayerEnter(Node2D node){
		playerInventory.addItem(type);
		OnPickup();
	}

	public virtual void OnPickup(){
		QueueFree();
	}
}
