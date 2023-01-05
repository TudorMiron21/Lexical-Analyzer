using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieParanteza : Expresie
    {
        public AtomLexical ParantezaDeschisa { get; }
        public Expresie Expresie { get; }
        public AtomLexical ParantezaInchisa {get;}

        public ExpresieParanteza(AtomLexical parantezaDeschisa, Expresie expresie, AtomLexical parantezaInchisa)
        {
            this.ParantezaDeschisa = parantezaDeschisa;
            this.Expresie = expresie;
            this.ParantezaInchisa = parantezaInchisa;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieParantezeAtomLexical;

        public override IEnumerable<Nod> GetCopii()
        {
            yield return ParantezaDeschisa;
            yield return Expresie;
            yield return ParantezaInchisa; 
        }
    }
}
