using Godot;
using System;

public partial class PlayerInteraction : Node2D
{
	private RayCast2D rayCast;
	private PlayerMovement playerMovement;
	private PlayerInventory playerInventory;
	public float rayCastLength = 25f;

	public override void _Ready()
	{
		rayCast = GetNode<RayCast2D>("./RayCast2D");
		playerMovement = GetNode<PlayerMovement>("./..");
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
	}

	public override void _Process(double delta)
	{
		Vector2 newTargetPos = new Vector2();
		newTargetPos.X = rayCastLength * (playerMovement.isFacingRight ? 1 : -1);
		rayCast.TargetPosition = newTargetPos;

		if (
			Input.IsActionJustPressed("interact") &&
			rayCast.IsColliding() && 
			playerInventory.getCurrentItem() == ItemTypes.FireAxe)
		{
			Object collider = rayCast.GetCollider();
			if (collider is Breakable obj) obj.Break();	
		}
	}
}
