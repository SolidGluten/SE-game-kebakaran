using Godot;
using System;
using System.Collections.Generic;
using System.Xml.XPath;

public partial class PlayerInteraction : Node2D
{
	private ShapeCast2D shapeCast;
	private PlayerMovement playerMovement;
	private PlayerInventory playerInventory;
	public float rayCastLength = 25f;

	public override void _Ready()
	{
		shapeCast = GetNode<ShapeCast2D>("./ShapeCast2D");
		playerMovement = GetNode<PlayerMovement>("./..");
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
	}

	public override void _Process(double delta)
	{
		Vector2 newTargetPos = new Vector2();
		newTargetPos.X = rayCastLength * (playerMovement.isFacingRight ? 1 : -1);
		shapeCast.TargetPosition = newTargetPos;
		
		if (
			Input.IsActionJustPressed("interact")
			&& shapeCast.IsColliding()
			)
		{
			// Object collider = rayCast.GetCollider();
			var collision_count = shapeCast.GetCollisionCount();
			for (int i = 0; i < collision_count; i++)
			{
				Object collider = shapeCast.GetCollider(i);
				GD.Print((collider as Node).Name);
				HandleCollision(collider);
			}
		}
	}

	public void HandleCollision(Object collider)
	{
		switch (playerInventory.getCurrentItem())
			{
				case ItemTypes.FireAxe:
					{
						if (collider is Breakable obj) obj.Break();
						GD.Print("Using Fire Axe");
						break;
					}
				case ItemTypes.FireExtinguisher:
					{
						if (collider is Extinguishable obj) obj.Extinguish();
						GD.Print("Using Fire Extinguisher");
						break;
					}
				case ItemTypes.WetCloth:
					{
						GD.Print("Using Wet Cloth");
						break;
					}
				default: break;
			}
	}
}


