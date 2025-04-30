using Godot;
using System;

public partial class PlayerHealth : Node
{
	public const int maxHealth = 3;
	public int currentHealth;

  public override void _Ready()
  {
    currentHealth = maxHealth;
  }

	public void Hurt(int dmg){
		currentHealth = Mathf.Max(0, currentHealth - dmg);
		GD.Print(currentHealth);
	}

	public void Heal(int hp){
		currentHealth = Mathf.Min(maxHealth, currentHealth + hp);
	}
}
