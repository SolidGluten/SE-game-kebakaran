using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] public float CurrentSpeed = 0.0f;

	[Export] public float MaxSpeed = 400.0f;
	private float Acceleration = 10.0f; //10 /s
	private float Deceleration = 3.0f; //3 /s

	[Export] public float AccelTime = 1f; // Time to reach max speed
	[Export] public float DecelTime = 1f; // Time to stop

	[Export] public float JumpVelocity = -400.0f;
	[Export] public float InvicibilityTime = 0.2f;
	[Export] public float HurtKnockback = 100.0f;

	private bool isMoveRight = true;

	public override void _Ready()
	{
		Acceleration = MaxSpeed/AccelTime;
		Deceleration = MaxSpeed/DecelTime;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = this.Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump_action") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			var _isMoveRight = direction.X > 0;
			if (isMoveRight != _isMoveRight)
			{
				CurrentSpeed = 0f; //resets velocity if direction changes
			}
			else
			{
				CurrentSpeed = Mathf.MoveToward(CurrentSpeed, MaxSpeed * direction.X, (float)delta * Acceleration);
			}
			isMoveRight = _isMoveRight;
		}
		else
		{
			CurrentSpeed = Mathf.MoveToward(CurrentSpeed, 0, (float)delta * Deceleration);
		}

		velocity.X = CurrentSpeed;
		this.Velocity = velocity;
		MoveAndSlide();

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node2D collider = collision.GetCollider() as Node2D;

			if (collider.IsInGroup("Hurtable"))
			{
				HealthManager.Instance.TakeDamage(1);

				var knockbackDirection = (collision.GetPosition() - this.GlobalPosition).Normalized();
				GD.Print(knockbackDirection);
				var knockbackVelocity = knockbackDirection * HurtKnockback;
				this.Velocity = -this.Velocity * knockbackDirection;
			}
		}
	}
}
