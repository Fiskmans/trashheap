using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    internal partial class TrashToolTip : VBoxContainer
    {
        Trash Trash;
        Label Title = new Label();
        VBoxContainer List = new VBoxContainer { OffsetLeft = 10 };
        public TrashToolTip(ITrashHaver trashHaver)
        {
            Trash = trashHaver.Trash;

            AddChild(Title);
            AddChild(List);

            Refresh();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            Refresh();
        }

        public void Refresh()
        {
            Title.Text = $"{Trash.Total.ToString("0.0")}t of trash:";

            foreach (Node c in List.GetChildren())
                List.RemoveChild(c);

            foreach (KeyValuePair<TrashType, float> kv in Trash.Amounts)
            {
                List.AddChild(new Label { Text = $"  {kv.Key}: {kv.Value.ToString("0.00")}t" });
            }
        }
    }
}
