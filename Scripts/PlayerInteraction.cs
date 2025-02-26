using Godot;
using System;

public partial class PlayerInteraction : Node
{
	[Export]
	private float rayLength = 200f;
	
	RayCast2D raycast;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		raycast = this.GetNode<RayCast2D>("./RayCast2D");
		raycast.TargetPosition = new Vector2(200f, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("move_left")){
			raycast.TargetPosition = new Vector2(-200f, 0);
		} else if(Input.IsActionJustPressed("move_right")){
			raycast.TargetPosition = new Vector2(200f, 0);
		}

		var collider = raycast.GetCollider();
		if(raycast.IsColliding()){
			if(collider.HasMethod("Break") && Input.IsActionJustPressed("interact")){
				collider.Call("Break");
			}
		}
	}
}
