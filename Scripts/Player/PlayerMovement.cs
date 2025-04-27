using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	public bool isFacingRight = true;
	public const float JumpVelocity = -400.0f;

	AnimatedSprite2D animatedSprite;
	
  public override void _Ready()
  {
    animatedSprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
  }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			animatedSprite.Play("jump");
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump_action") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		isFacingRight = direction.X > 0;

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			animatedSprite.FlipH = !isFacingRight;
			if(IsOnFloor()) animatedSprite.Play("walking");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if(IsOnFloor()) animatedSprite.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
