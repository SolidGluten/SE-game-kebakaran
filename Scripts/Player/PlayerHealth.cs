using Godot;
using System;

public partial class PlayerHealth : Node
{
	public const int maxHealth = 3;
	public int currentHealth;

	[Signal] public delegate void OnHealthChangeEventHandler(int dmg);

  public override void _Ready()
  {
    currentHealth = maxHealth;
  }

	public void Hurt(int dmg){
		currentHealth = Mathf.Max(0, currentHealth - dmg);
		EmitSignal("OnHealthChange", currentHealth);		
	}

	public void Heal(int hp){
		currentHealth = Mathf.Min(maxHealth, currentHealth + hp);
		EmitSignal("OnHealthChange", currentHealth);		
	}
}
