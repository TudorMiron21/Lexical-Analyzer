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

        public IntVariable(string Name, int Value, bool IsInitialised) : base(Name, IsInitialised)
        {
            this.Value = Value;
        }

        public IntVariable(string Name, bool IsInitialised) : base(Name, IsInitialised) { }

        public IntVariable():base(){}


    }
}
