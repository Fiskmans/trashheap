using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashHeap
{
    internal interface IConvertableToTrash
    {
        public Trash ConvertToTrash();
    }
}
