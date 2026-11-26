using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    public enum ContaminationType
    {
        Liquid,
        Gaseous
    }

    public class Contamination
    {
        public Dictionary<ContaminationType, float> Amounts = new Dictionary<ContaminationType, float>();
        public void Add(ContaminationType type, float amount)
        {
            if (!Amounts.ContainsKey(type))
                Amounts.Add(type, amount);
            else
                Amounts[type] += amount;
        }

        public void Merge(Contamination contam)
        {
            foreach (KeyValuePair<ContaminationType, float> kv in contam.Amounts)
                Add(kv.Key, kv.Value);
        }
    }
}
