using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            //{
            //    Console.Write("> ");
            //    var text = Console.ReadLine();
            //    if (string.IsNullOrWhiteSpace(text))
            //        return;

            //    var parser = new Parser(text);
            //    var arboreSintactic = parser.Parseaza();

            //    var culoare = Console.ForegroundColor;
            //    if (parser.erori.Any())
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        foreach (var eroare in parser.erori)
            //            Console.WriteLine(eroare);

            //        Console.ForegroundColor = culoare;
            //    }
            //    else
            //    {
            //        if (parser.pass_through_evalutaor)
            //        {

            //            Evaluator e = new Evaluator(arboreSintactic);
            //            var res = e.Evalueaza();
            //            Console.WriteLine(res);
            //            AfiseazaArbore(arboreSintactic);
            //        }
            //        else
            //        {
            //            Console.WriteLine("hello");
            //        }
            //    }

            //}
            ReadChoice.read();

        }
        static void AfiseazaArbore(Nod nod, string indentare = "", bool ultimulNod = true)
        {
            var prefix = ultimulNod ? "└──" : "├──";
            Console.Write(indentare);
            Console.Write(prefix);
            Console.Write(nod.Tip);



            if (nod is AtomLexical t && t.Valoare != null)
            {
                Console.Write(" ");
                Console.Write(t.Valoare);
            }



            Console.WriteLine();



            indentare += ultimulNod ? "    " : "|   ";



            var ultimulCopil = nod.GetCopii().LastOrDefault();



            foreach (var c in nod.GetCopii())
            {
                AfiseazaArbore(c, indentare, c == ultimulCopil);
            }
        }
    }

    enum TipAtomLexical
    {
        NumarIntAtomLexical,
        NumarDoubleAtomLexical,
        PlusAtomLexical,
        MinusAtomLexical,
        StarAtomLexical,
        SlashAtomLexical,
        ParantezaDeschisaAtomLexical,
        ParantezaInchisaAtomLexical,
        TerminatorSirAtomLexical,
        SpatiuAtomLexical,
        InvalidAtomLexical,

        ExpresieBinaraAtomLexical,
        ExpresieNUmericaAtomLexical,
        ExpresieDoubleAtomLexical,
        ExpresieParantezeAtomLexical,
        ExpresieLiterara,
        ExpresieVariabila,
        ExpresieStringAtomLexical,

        TipDateInt,
        TipDateDouble,
        TipDateString,

        VariabilaAtomLexical,
        EgalAtomLexical,
        VirgulaAtomLexical,
        PunctSiVirgulaAtomLexical,
        PunctAtomLexical,
        GhilimeleAtomLexical
    }


}
