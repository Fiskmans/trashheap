using Godot;
using System;
using System.Drawing;

public partial class TrashPileSlot : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Size = new Vector2(30,30);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
