using Godot;
using System;

public partial class ExitDoor : Area2D
{
	[Export] public string nextLevelPath;
	public LevelManager levelManager;
	
	public override void _Ready()
	{
		levelManager = GetNode<LevelManager>("/root/LevelManager");
		this.BodyEntered += OpenDoor;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("dev_next_level"))
		{
			levelManager.NextLevel(nextLevelPath);
		}
  }

	public void OpenDoor(Node2D node)
	{
		GD.Print(node.Name);

		if (nextLevelPath == null || levelManager == null) return;
		if (node.Name != "Player") return;
		levelManager.NextLevel(nextLevelPath);
	}

}
