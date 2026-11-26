using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    internal partial class TrashBag : ColorRect, ITrashHaver, IConvertableToTrash
    {

        public Trash Trash { get; set; } = new Trash();

        public TrashBag()
        {
            CustomMinimumSize = new Vector2(90, 120);
            Size = new Vector2(90, 120);
        }

        public override void _Ready()
        {
            base._Ready();
        }

        public override GodotObject _MakeCustomTooltip(string forText)
        {
            Control toolTip = new VBoxContainer();

            toolTip.AddChild(new Label { Text = "Bag" });
            toolTip.AddChild(new TrashToolTip(this));

            return toolTip;
        }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            return Variant.CreateFrom(this.GetPath());
        }

        public Trash ConvertToTrash()
        {
            Trash t = new Trash();
            t.Add(TrashType.Plastic, 0.001f);
            QueueFree();
            return t;
        }
    }
}
