using Godot;
using System;
using System.Drawing;
using TrashHeap;

public partial class TrashPileSlot : ColorRect, ITrashHaver
{
	Conditions Conditions = new Conditions();

	public Trash Trash { get; set; } = new Trash();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Size = new Vector2(30,30);
	}

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
		if (data.VariantType == Variant.Type.NodePath)
		{
			Node n = GetNode(data.AsNodePath());

			if (n == null)
				return false;

			if (n is ITrashHaver)
				return true;

			if (n is IConvertableToTrash)
				return true;
		}

        return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.VariantType == Variant.Type.NodePath)
        {
            Node n = GetNode(data.AsNodePath());

            if (n == null)
                return;

            if (n is ITrashHaver trashHaver)
                Trash.Merge(trashHaver.Trash);

            if (n is IConvertableToTrash trashable)
                Trash.Merge(trashable.ConvertToTrash());
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Contamination Simulate(float delta, int x, int y, TrashPileGrid grid)
	{
        bool sentDown = false;
		TrashPileSlot below = grid.TryGet(x, y + 1);
		if (below != null)
		{
			float space = 1 - below.Trash.Total;
            if (space > 0.001)
			{
				below.Trash.Merge(Trash.TakeAmount(space));
                sentDown = true;
			}
		}

        if (!sentDown)
        {
            TrashPileSlot belowleft = grid.TryGet(x - 1, y + 1);
            if (belowleft != null)
            {
                float space = 1 - belowleft.Trash.Total;
                if (space > 0.001)
                {
                    belowleft.Trash.Merge(Trash.TakeAmount(space * 0.1f));
                }
            }
            TrashPileSlot belowRight = grid.TryGet(x + 1, y + 1);
            if (belowRight != null)
            {
                float space = 1 - belowRight.Trash.Total;
                if (space > 0.001)
                {
                    belowRight.Trash.Merge(Trash.TakeAmount(space * 0.1f));
                }
            }
        }

        Conditions.AirExposure = 0.1f;

		if ((grid.TryGet(x, y - 1)?.Trash.Total ?? 0) < 0.3f)
            Conditions.AirExposure += 0.5f;

		if ((grid.TryGet(x + 1, y - 1)?.Trash.Total ?? 0) < 0.2f)
            Conditions.AirExposure += 0.2f;

		if ((grid.TryGet(x - 1, y - 1)?.Trash.Total ?? 0) < 0.2f)
            Conditions.AirExposure += 0.2f;

		Contamination contam = Trash.Decompose(delta, Conditions);

        Color = new Godot.Color(0, 0, 0, Math.Min(1.0f, Trash.Total));

		return contam;
    }

    public override GodotObject _MakeCustomTooltip(string forText)
    {
		Control toolTip = new VBoxContainer();

		toolTip.AddChild(new TrashToolTip(this));
		toolTip.AddChild(new ConditionsToolTip(Conditions));

		return toolTip;
    }
}
