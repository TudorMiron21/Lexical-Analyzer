using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    abstract class Variable
    {
        private string Name { get; set; } 

        private bool IsInitialised { get; set; }

        public Variable(string name ,bool isInitialised)
        {
            Name = name;
       
            IsInitialised = isInitialised;
        }

        public Variable() { }
    }
}
