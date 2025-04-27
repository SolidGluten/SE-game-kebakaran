using Godot;
using System;

public abstract partial class Pickable : Node2D
{
	public abstract void OnPickup();
}
