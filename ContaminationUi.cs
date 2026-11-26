using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    internal partial class ContaminationUi : VBoxContainer
    {
        Contamination Contamination;
        VBoxContainer List = new VBoxContainer();

        public ContaminationUi(Contamination contamination)
        {
            Contamination = contamination;

            AddChild(new Label { Text = $"Contamination: " });
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
            foreach (Node c in List.GetChildren())
                List.RemoveChild(c);

            foreach (KeyValuePair<ContaminationType, float> kv in Contamination.Amounts)
            {
                List.AddChild(new Label { Text = $"  {kv.Key}: {kv.Value.ToString("0.00")}" });
            }
        }
    }
}
