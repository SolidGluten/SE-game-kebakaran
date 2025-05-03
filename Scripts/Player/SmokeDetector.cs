using Godot;
using System;

public partial class SmokeDetector : Area2D
{
	[Signal] public delegate void InSmokeEventHandler();
	[Signal] public delegate void OutSmokeEventHandler();

	public PlayerHealth playerHealth;
	public PlayerInventory playerInventory;

	[Export] public float hurtTimer = 1.0f;
	private float currentTime;
	private bool startTimer = false;

	public override void _Ready()
	{
		playerHealth = GetNode<PlayerHealth>("/root/PlayerHealth");
		playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");

		this.BodyEntered += OnBodyEnter;
		this.BodyExited += OnBodyExited;
	}

  public override void _Process(double delta)	
  {
		if (startTimer && playerInventory.getCurrentItem() != ItemTypes.WetCloth){

			if (currentTime > 0){
				currentTime -= (float)delta;
			} else {
				playerHealth.Hurt(1);
				currentTime = hurtTimer;
			}

		} else {
			currentTime = hurtTimer;
		}
  }


	public void OnBodyEnter(Node node){
		if(node is TileMapLayer tileMap && tileMap.Name == "SmokeLayer"){
			EmitSignal("InSmoke");
			GD.Print("In Smoke");
			startTimer = true;
		}
	}
	public void OnBodyExited(Node node){
		if(node is TileMapLayer tileMap && tileMap.Name == "SmokeLayer"){
			EmitSignal("OutSmoke");
			GD.Print("Out Smoke");
			startTimer = false;
		}
	}
}
