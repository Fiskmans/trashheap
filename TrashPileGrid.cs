using Godot;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using TrashHeap;

public partial class TrashPileGrid : Control
{
	[Export]
	Node ContaminationUiOwner;

	Contamination Contamination = new Contamination();

	[Export]
	int Width = 15;

	[Export]
	int Height = 5;

	TrashPileSlot[,] Grid;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetSize(new Vector2(30 * Width, 30 * Height));
		SetAnchorsAndOffsetsPreset(LayoutPreset.CenterBottom, LayoutPresetMode.KeepSize);
		Grid = new TrashPileSlot[Width,Height];
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				Populate(x,y);
			}
		}

		if (ContaminationUiOwner != null)
			ContaminationUiOwner.AddChild(new ContaminationUi(Contamination));

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				Contamination.Merge(Grid[x, y].Simulate((float)delta, x, y, this));
			}
		}
	}

	public TrashPileSlot TryGet(int x, int y)
	{
		if (x < 0)
			return null;

		if (y < 0)
			return null;

		if (x >= Width)
			return null;

		if (y >= Height)
			return null;

		return Grid[x, y];
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
