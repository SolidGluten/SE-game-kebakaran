using Godot;
using System;

public partial class HealthPack : Pickable
{
	PlayerHealth playerHealth;

	public override void _Ready(){
		base._Ready();
		playerHealth = GetNode<PlayerHealth>("/root/PlayerHealth");
	}

	public override void OnPickup(){
		playerHealth.Heal(1);
		this.QueueFree();
	}
}
