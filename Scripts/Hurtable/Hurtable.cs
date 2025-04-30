using Godot;
using System;

public partial class Hurtable : Area2D
{
	private PlayerHealth playerHealth;
	[Export] public int damage = 1;
	[Export] public float knockbackForce = 300f;

	public override void _Ready()
	{
		playerHealth = GetNode<PlayerHealth>("/root/PlayerHealth");
		this.BodyEntered += Hurt;
	}

	public void Hurt(Node body)
	{
		if(body is PlayerMovement player){
			player.ApplyKnockback(GlobalPosition, knockbackForce);
			playerHealth.Hurt(damage);
		}

	}
}
