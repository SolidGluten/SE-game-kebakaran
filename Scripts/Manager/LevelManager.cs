using Godot;
using System;

public partial class LevelManager : Node
{
	public PlayerHealth playerHealth;

	public override void _Ready()
	{
		playerHealth = GetNode<PlayerHealth>("/root/PlayerHealth");
		playerHealth.OnDeath += ResetCurrentScene;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("dev_reset"))
		{
			ResetCurrentScene();
		}
	}

	private void ResetCurrentScene()
	{
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);	
	}

	public override void _ExitTree()
	{
		if (playerHealth != null)
			playerHealth.OnDeath -= ResetCurrentScene;
	}

}
