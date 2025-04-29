using Godot;
using System;

public abstract partial class Extinguishable : Node2D
{
	public void Extinguish(){
		QueueFree();
		OnExtinguish();
	}

	public abstract void OnExtinguish();
}
