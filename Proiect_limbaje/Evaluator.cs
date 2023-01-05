using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class Evaluator
    {
        public Expresie Expresie { get; }
        public Evaluator(Expresie expresie)
        {
            Expresie = expresie;
        }

        public int Evalueaza()
        {
            return EvalueazaExpresie(this.Expresie);
        }
        private int EvalueazaExpresie(Expresie expresie)
        {
            if(expresie is ExpresieNumerica n)
            {
                return (int)n.Numar.Valoare;
            }

            if (expresie is ExpresieBinara b)
            {
                if(b.OperatorExpresie.Tip == TipAtomLexical.PlusAtomLexical)
                {
                    return EvalueazaExpresie(b.Stanga) + EvalueazaExpresie(b.Dreapta);
                }
                if (b.OperatorExpresie.Tip == TipAtomLexical.MinusAtomLexical)
                {
                    return EvalueazaExpresie(b.Stanga) - EvalueazaExpresie(b.Dreapta);
                }
                if (b.OperatorExpresie.Tip == TipAtomLexical.StarAtomLexical)
                {
                    return EvalueazaExpresie(b.Stanga) * EvalueazaExpresie(b.Dreapta);
                }
                if (b.OperatorExpresie.Tip == TipAtomLexical.SlashAtomLexical)
                {
                    if (EvalueazaExpresie(b.Dreapta) == 0)
                        throw new Exception("Impartire la 0");
                    return EvalueazaExpresie(b.Stanga) / EvalueazaExpresie(b.Dreapta);
                }

            }
            if (expresie is ExpresieParanteza p)
                return EvalueazaExpresie(p.Expresie);

            throw new Exception($"expresie necunoscuta <{expresie.Tip}>.");
        }
    }
}
