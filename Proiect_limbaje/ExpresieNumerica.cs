using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieNumerica : Expresie
    {

        public AtomLexical NumarInt { get; }

        public ExpresieNumerica(AtomLexical numar)
        {
            NumarInt = numar;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieNUmericaAtomLexical;

        public override IEnumerable<Nod> GetCopii()
        {
            yield return NumarInt;
        }
    }
}
