using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class DoubleVariable:Variable
    {
        public double Value { get; set; }

        public DoubleVariable(string Name, double Value, bool IsInitialised) : base(Name, IsInitialised)
        {
            this.Value = Value;
        }

        public DoubleVariable(string Name, bool IsInitialised) : base(Name, IsInitialised) { }

        public DoubleVariable() : base() { }
    }
}
