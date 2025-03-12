using Godot;
using System;

public partial class HealthManager : Node
{
	public static HealthManager Instance { get; private set; }

	public int maxHealth = 5;

	[Export]
	public int currentHealth { get; private set; } = 0;

	[Signal]
	public delegate void HealthDepletedEventHandler(int dmg);

	public override void _Ready()
	{
		Instance = this;
		
		maxHealth = currentHealth;
		HealthDepleted += TakeDamage;
	}

	public override void _Process(double delta)
	{

	}

	public void TakeDamage(int dmg)
	{
		GD.Print("Player took " + dmg + " Damage.");
		currentHealth -= dmg;
	}

}
