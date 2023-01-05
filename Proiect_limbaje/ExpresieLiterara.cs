using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieLiterara:Expresie
    {
        public List<AtomLexical> Expresie { get; set; }

        //public ExpresieLiterara(AtomLexical []expresie)
        //{

        //    Expresie = expresie;
        //}

        public ExpresieLiterara() { 
        
            Expresie = new List<AtomLexical>();
        }


        public override TipAtomLexical Tip => TipAtomLexical.ExpresieLiterara;

 

        public override IEnumerable<Nod> GetCopii()
        {
            throw new NotImplementedException();
        }
    }
}
