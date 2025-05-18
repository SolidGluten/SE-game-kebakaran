using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;
	public bool isFacingRight = true;

	[Export] public float cayoteeDuration = 0.2f; //seconds
	private float cayoteeTimer;

	private Vector2 knockVelocity;
	[Export] public float knockDuration = 0.1f; //seconds
	private float knockTimer;

	AnimatedSprite2D animatedSprite;

	private PlayerInventory playerInventory;

	public override void _Ready()
	{
		animatedSprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			cayoteeTimer -= (float)delta;
			velocity += GetGravity() * (float)delta;
			PlayJumpAnim();
		}
		else
		{
			cayoteeTimer = cayoteeDuration;
		}

		GD.Print(cayoteeTimer);

		if (Input.IsActionJustPressed("jump_action") && (IsOnFloor() || cayoteeTimer > 0))
		{
			velocity.Y = JumpVelocity;
		}

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");


		if (knockTimer > 0)
		{
			knockTimer -= (float)delta;
		}
		else
		{
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
				isFacingRight = direction.X > 0;
				animatedSprite.FlipH = !isFacingRight;
				PlayWalkAnim();
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				PlayIdleAnim();
			}
		}

		this.Velocity = velocity;
		MoveAndSlide();
	}


	public void ApplyKnockback(Vector2 sourcePos, float strength)
	{
		Vector2 direction = (GlobalPosition - sourcePos).Normalized();
		knockVelocity = direction * strength;
		Velocity = knockVelocity; // Set once
		knockTimer = knockDuration;
	}

	public void PlayWalkAnim()
	{
		if (!IsOnFloor()) return;

		var item = playerInventory.getCurrentItem();
		switch (item)
		{
			case ItemTypes.FireAxe:
				{
					animatedSprite.Play("walk_axe");
					break;
				}
			case ItemTypes.WetCloth:
				{
					animatedSprite.Play("walk_cloth");
					break;
				}
			case ItemTypes.FireExtinguisher:
				{
					animatedSprite.Play("walk_exting");
					break;
				}
			default:
				{
					animatedSprite.Play("walk");
					break;
				}
		}
	}

	public void PlayIdleAnim()
	{
		if (!IsOnFloor()) return;

		var item = playerInventory.getCurrentItem();
		switch (item)
		{
			case ItemTypes.FireAxe:
				{
					animatedSprite.Play("idle_axe");
					break;
				}
			case ItemTypes.WetCloth:
				{
					animatedSprite.Play("idle_cloth");
					break;
				}
			case ItemTypes.FireExtinguisher:
				{
					animatedSprite.Play("idle_exting");
					break;
				}
			default:
				{
					animatedSprite.Play("idle");
					break;
				}
		}
	}

	public void PlayJumpAnim()
	{
		var item = playerInventory.getCurrentItem();
		// if(animatedSprite.Animation.ToString().Contains("jump")) return; // returns if already jumping

		switch (item)
		{
			case ItemTypes.FireAxe:
				{
					animatedSprite.Play("jump_axe");
					break;
				}
			case ItemTypes.WetCloth:
				{
					animatedSprite.Play("jump_cloth");
					break;
				}
			case ItemTypes.FireExtinguisher:
				{
					animatedSprite.Play("jump_exting");
					break;
				}
			default:
				{
					animatedSprite.Play("jump");
					break;
				}
		}
	}
}
