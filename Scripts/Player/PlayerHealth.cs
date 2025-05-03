using Godot;
using System;

public partial class PlayerHealth : Node
{
	public const int maxHealth = 3;
	public int currentHealth;

	[Signal] public delegate void OnHealthChangeEventHandler(int dmg);
	[Signal] public delegate void OnDeathEventHandler();

  public override void _Ready()
  {
    currentHealth = maxHealth;
  }

	public void Hurt(int dmg){
		currentHealth = Mathf.Max(0, currentHealth - dmg);
		EmitSignal("OnHealthChange", currentHealth);		
		
		if(currentHealth <= 0){
			EmitSignal("OnDeath");
			currentHealth = maxHealth;
		} else {
		}
	}

	public void Heal(int hp){
		currentHealth = Mathf.Min(maxHealth, currentHealth + hp);
		EmitSignal("OnHealthChange", currentHealth);		
	}
}
