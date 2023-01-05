using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    abstract class Nod
    {
        public abstract TipAtomLexical Tip { get; }

        public abstract IEnumerable<Nod> GetCopii();
    }
}
