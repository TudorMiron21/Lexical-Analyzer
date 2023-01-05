using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieBinara : Expresie
    {
        public Expresie Stanga { get; }

        public AtomLexical OperatorExpresie { get; }
        public Expresie Dreapta { get; }

        public ExpresieBinara(Expresie stanga, AtomLexical operatorExpresie, Expresie dreapta)
        {
            Stanga = stanga;
            OperatorExpresie = operatorExpresie;
            Dreapta = dreapta;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieBinaraAtomLexical;

        public override IEnumerable<Nod> GetCopii()
        {
           yield return Stanga;
            yield return OperatorExpresie;
            yield return Dreapta;
        }

    }
}
