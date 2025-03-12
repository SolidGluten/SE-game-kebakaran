using Godot;
using System;

public enum GameState { Win, Death, Running, Pause }

public partial class GameManager : Node
{
	public static GameManager Instance {get; private set;}
	public GameState currentState = GameState.Running;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void Resume(){
		currentState = GameState.Running;
	}

	public void Pause(){
		currentState = GameState.Pause;
	}

	public void Win(){
		currentState = GameState.Win;
	}

	public void Death(){
		currentState = GameState.Death;
	}
}
