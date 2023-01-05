using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class StringVariable:Variable
    {
        public string Value{ get; set; }

        public StringVariable(string Name,string Value,bool IsInitialised) : base(Name, IsInitialised)
        {
            this.Value = Value;
        }
        public StringVariable(string Name, bool IsInitialised) : base(Name, IsInitialised) { }

        public StringVariable() : base() { }
    }
}
