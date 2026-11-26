using Godot;
using System;
using System.Collections.Generic;
using TrashHeap;

public partial class TakeOnTrash : Button
{
    [Export]
    public Node Target;

    [Export]
    public Godot.Collections.Dictionary<TrashType, float> Contents = new Godot.Collections.Dictionary<TrashType, float>();

    [Export]
    public float Price = 1;

    [Export]
    public string BagName = "";

    public override void _Ready()
    {
        base._Ready();
        Text = $"Take on a bag of {BagName} for €{-Price:0.00}";
        Pressed += BuyTrash;
    }

    private void BuyTrash()
    {
        TrashBag bag = new TrashBag();

        foreach (KeyValuePair<TrashType, float> kv in Contents)
            bag.Trash.Add(kv.Key, kv.Value);

        Target?.AddChild(bag);
    }
}
