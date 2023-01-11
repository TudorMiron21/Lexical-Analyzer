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

        public dynamic Evalueaza()
        {
            return EvalueazaExpresie(this.Expresie);
        }
        private dynamic EvalueazaExpresie(Expresie expresie)
        {
            if(expresie is ExpresieNumerica n)
            {
                return (int)n.NumarInt.Valoare;
            }

            if (expresie is ExpresieDouble m)
            {
                return (double)m.NumarDouble.Valoare;
            }

            if (expresie is ExpresieVariabila v)
            {
                foreach(var variable in GlobalVars.vec_vars)
                {
                    if (variable.Name == v.Variabila.Text)
                    {


                        if (variable.IsInitialised)
                        {
                            //if (variable.tipDate == TipDate.Int)
                            //    v.TipAtom = TipDate.Int;
                            //else
                            //if (variable.tipDate == TipDate.Double)
                            //    v.TipAtom = TipDate.Double;
                            //else
                            //if (variable.tipDate == TipDate.String)
                            //    v.TipAtom = TipDate.String;
                        
                            return variable.get_value();
                        }
                    }

                }
                throw new Exception($"Variabila{v.Variabila.Text} nu exista sau nu este initializata");
            }

            if (expresie is ExpresieString s)
            {
                return (string)s.Text.Valoare;
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
