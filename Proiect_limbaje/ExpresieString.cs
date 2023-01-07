using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ExpresieString : Expresie
    {
        public AtomLexical Text { get; }

        public ExpresieString(AtomLexical text)
        {
            Text = text;
        }

        public override TipAtomLexical Tip => TipAtomLexical.ExpresieStringAtomLexical;

        public override IEnumerable<Nod> GetCopii()
        {
            yield return Text;
        }
    }
}
