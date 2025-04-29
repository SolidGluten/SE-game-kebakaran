using Godot;
using System;

public partial class Crate : Breakable
{
	public override void OnBreak(){
		GD.Print("I've been broken!");
	}
}
