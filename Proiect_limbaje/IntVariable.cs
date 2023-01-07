using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class IntVariable:Variable
    {
        public int Value { get; set; }

        public IntVariable(string Name, int Value, bool IsInitialised, TipDate tipDate) : base(Name, IsInitialised, tipDate)
        {
            this.Value = Value;
        }

        public IntVariable(string Name, bool IsInitialised, TipDate tipDate) : base(Name, IsInitialised,  tipDate) { }

        public IntVariable():base(){}

        public override dynamic get_value()
        {
            return Value;
        }
    }
}
