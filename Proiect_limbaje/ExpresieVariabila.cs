using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieVariabila:Expresie
    {
        public AtomLexical Variabila { get; }
        public TipDate TipAtom { get; set; }

        public ExpresieVariabila(AtomLexical variabila)
        {
            Variabila = variabila;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieVariabila;

        public override IEnumerable<Nod> GetCopii()
        {
            yield return Variabila;
        }
    }
}
