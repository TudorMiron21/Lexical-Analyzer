using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class AtomLexical: Nod
    {
       public override TipAtomLexical Tip{ get;}
       
       public string Text { get; set; }

       public object Valoare { get; set; }

        public int Index { get; set;  } //pozitie in sir a atomului lexical pe care il analizam

       // public override TipAtomLexical Tip => TipAtom;

        public AtomLexical(TipAtomLexical tip, string text, object valoare, int index)
        {
            Tip = tip;
            Index = index;
            Text = text;
            Valoare = valoare;
        }

        public override IEnumerable<Nod> GetCopii()
        {
            return Enumerable.Empty<Nod>();
        }
    }
}
