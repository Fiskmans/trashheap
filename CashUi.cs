using Godot;
using System;
using System.ComponentModel;

public partial class CashUi : Label
{
    [Export]
    public float Amount = 0;

    public override void _Ready()
    {
        base._Ready();
        Refresh();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Refresh();
    }

    public void Refresh() => Text = $"Cash: €{Amount:0.00}";
}
