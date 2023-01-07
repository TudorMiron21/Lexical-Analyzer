using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proiect_limbaje
{
    public enum TipDate { Int, Double, String};
    public abstract class Variable
    {
        public TipDate tipDate { get; set; }
        public string Name { get; set; } 
        public bool IsInitialised { get; set; }

        public Variable(string name ,bool isInitialised,TipDate tipDate)
        {
            Name = name;
       
            IsInitialised = isInitialised;
            this.tipDate = tipDate;
        }

        public abstract dynamic get_value();

        public Variable() { }
    }
}
