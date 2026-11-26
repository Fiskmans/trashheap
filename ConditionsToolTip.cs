using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    internal partial class ConditionsToolTip : VBoxContainer
    {
        Conditions Conditions;

        Label Temperature = new Label();
        Label Pressure = new Label();
        Label AirExposure = new Label();
        Label Humidity = new Label();

        public ConditionsToolTip(Conditions conditions)
        {
            Conditions = conditions;

            AddChild(new Label { Text = "Conditions:" });

            AddChild(Temperature);
            AddChild(Pressure);
            AddChild(AirExposure);
            AddChild(Humidity);

            Refresh();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            Refresh();
        }

        public void Refresh()
        {
            Temperature.Text = $"  Temperature: {Conditions.Temperature.ToString("0.00")}°C";
            Pressure.Text = $"  Pressure: {Conditions.Pressure.ToString("0.00")}atm";
            AirExposure.Text = $"  Air exposure: {(Conditions.AirExposure * 100).ToString("0")}%";
            Humidity.Text = $"  Humidity: {(Conditions.Humidity * 100).ToString("0")}%";
        }
    }
}
