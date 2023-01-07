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

        public DoubleVariable(string Name, double Value, bool IsInitialised,TipDate tipDate) : base(Name, IsInitialised,tipDate)
        {
            this.Value = Value;
        }

        public DoubleVariable(string Name, bool IsInitialised, TipDate tipDate) : base(Name, IsInitialised, tipDate) { }

        public DoubleVariable() : base() { }

        public override dynamic get_value()
        {
            return Value;
        }
    }
}
