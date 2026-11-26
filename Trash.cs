using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    public enum TrashType
    {
        Plastic,
        Cardboard,
        Paper,
        Organics,
        Electronics,
        Batteries,
        Radioactive,
        Metal,
        Chemical
    }

    public class Decomposition
    {
        public float BaseRate;
        public Dictionary<TrashType, float> Into;
        public Dictionary<ContaminationType, float> Contamination;

        public float Decompose(float delta, Conditions conditions, Contamination contam, float value, Trash target)
        {
            float decomposed = value * delta * BaseRate; // TODO: apply multiplier based on conditions
            if (decomposed > value)
                decomposed = value;

            foreach (KeyValuePair<TrashType, float> kv in Into)
                target.Add(kv.Key, kv.Value * decomposed);

            foreach (KeyValuePair<ContaminationType, float> kv in Contamination)
                contam.Add(kv.Key, kv.Value * decomposed);

            return decomposed;
        }
    }

    public class Trash
    {
        static readonly Dictionary<TrashType, Decomposition> Decomposing = new Dictionary<TrashType, Decomposition>
        {
            { TrashType.Batteries, new Decomposition{
                BaseRate = 1.0f/600.0f,
                Into = new Dictionary<TrashType, float>
                {
                    { TrashType.Metal, 0.2f },
                    { TrashType.Plastic, 0.1f },
                    { TrashType.Chemical, 0.7f }
                },
                Contamination = new Dictionary<ContaminationType, float>
                {
                    { ContaminationType.Liquid, 10.0f }
                }
            } },
            { TrashType.Organics, new Decomposition
            {
                BaseRate = 1.0f/60.0f,
                Into = new Dictionary<TrashType, float> { },
                Contamination = new Dictionary<ContaminationType, float>
                {
                    { ContaminationType.Liquid, 0.1f },
                    { ContaminationType.Gaseous, 0.5f }
                }
            } }
        };

        public Dictionary<TrashType, float> Amounts = new Dictionary<TrashType, float>();
        public float Total => Amounts.Values.Sum();

        public Contamination Decompose(float delta, Conditions conditions)
        {
            Contamination contam = new Contamination();
            foreach (KeyValuePair<TrashType, Decomposition> kv in Decomposing)
            {
                if (Amounts.ContainsKey(kv.Key))
                    Amounts[kv.Key] -= kv.Value.Decompose(delta, conditions, contam, Amounts[kv.Key], this);
            }

            return contam;
        }

        public void Add(TrashType type, float amount)
        {
            if (!Amounts.ContainsKey(type))
                Amounts.Add(type, amount);
            else
                Amounts[type] += amount;
        }

        public void Merge(Trash other)
        {
            foreach (KeyValuePair<TrashType, float> kv in other.Amounts)
                Add(kv.Key, kv.Value);
        }

        public Trash TakeFraction(float fraction)
        {
            Trash t = new Trash();

            foreach (KeyValuePair<TrashType, float> kv in Amounts)
            {
                float a = kv.Value * fraction;
                t.Add(kv.Key, a);
                Amounts[kv.Key] -= a;
            }

            return t;
        }

        public Trash TakeAmount(float amount)
        {
            if (amount > Total)
                return TakeAll();

            return TakeFraction(amount / Total);
        }

        public Trash TakeAll()
        {
            Trash t = new Trash();
            (t.Amounts, Amounts) = (Amounts, t.Amounts);
            return t;
        }
    }
}
