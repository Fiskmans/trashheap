using Godot;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

public partial class TrashPileGrid : Control
{

	[Export]
	int Width = 15;

	[Export]
	int Height = 5;

	TrashPileSlot[,] Grid;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Grid = new TrashPileSlot[Width,Height];
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				Populate(x,y);
			}
		}
		Size = new Vector2(30 * Width, 30 * Height);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void Populate(int x, int y)
	{
		TrashPileSlot slot = new TrashPileSlot();

		slot.Name = $"Slot_{x}_{y}";
		slot.Position = new Vector2(x * 30, y * 30);

		AddChild(slot);

		Grid[x,y] = slot;
	}
}
