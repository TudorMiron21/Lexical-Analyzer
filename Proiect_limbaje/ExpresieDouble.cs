using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieDouble: Expresie
    {
        public AtomLexical NumarDouble { get; }

        public ExpresieDouble(AtomLexical numarDouble)
        {
            NumarDouble = numarDouble;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieDoubleAtomLexical;

        public override IEnumerable<Nod> GetCopii()
        {
            yield return NumarDouble;
        }

    }
}
