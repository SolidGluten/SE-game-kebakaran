using Godot;
using System;

public abstract partial class Breakable : Node2D
{
	public void Break(){
		QueueFree();
		OnBreak();
	}

	public abstract void OnBreak();
}
