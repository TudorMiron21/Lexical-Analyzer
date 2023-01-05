
using Proiect_limbaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace Proiect_limbaje
{


    internal class Parser //analizator sintactic (verifica ordinea atomilor lexicali)
    {
        //public Dictionary<string, string> string_vars;
        //public  Dictionary<string, int> int_vars;
        //public Dictionary<string, double> double_vars;
        
        public List<Variable> vec_variables; 
        private readonly AtomLexical[] atomilexicali;
        private int index;
        public List<string> erori = new List<string>();
        private AtomLexical datatype;
        private AtomLexical var_name;

        public Parser(string text)
        {
            vec_variables = new List<Variable>();
            var atomi = new List<AtomLexical>();

            Lexer l = new Lexer(text);

            AtomLexical atom;
            do
            {
                atom = l.Atom();
                if (atom.Tip != TipAtomLexical.SpatiuAtomLexical && 
                    atom.Tip != TipAtomLexical.InvalidAtomLexical &&
                    atom.Tip != TipAtomLexical.GhilimeleAtomLexical &&
                    atom.Tip!=TipAtomLexical.VirgulaAtomLexical)
                    //atom.Tip != TipAtomLexical.TipDateInt &&
                    //atom.Tip != TipAtomLexical.TipDateDouble&&
                    //atom.Tip != TipAtomLexical.TipDateString&&
                    //atom.Tip != TipAtomLexical.VariabilaAtomLexical)
                    atomi.Add(atom);

            } while (atom.Tip != TipAtomLexical.TerminatorSirAtomLexical);

            atomilexicali = atomi.ToArray();
            erori.AddRange(l.erori);
        }

        private AtomLexical Avans(int k)
        {
            if (index + k >= atomilexicali.Length)
                return atomilexicali[atomilexicali.Length - 1];

            return atomilexicali[index + k];
        }

        private AtomLexical AtomCurent => Avans(0);
        private AtomLexical AtomAnterior => Avans(-1);


        private AtomLexical AtomCurentSiIncrementeaza()
        {
            var atom = AtomCurent;
            index++;
            return atom;
        }

        private AtomLexical VerificareTip(TipAtomLexical tip)
        {
            if (AtomCurent.Tip == tip)
                return AtomCurentSiIncrementeaza();

            else
            {
                erori.Add($"Atom Lexical invalid <{AtomCurent.Tip}> .Se astepta  tipul {tip}");
                return new AtomLexical(tip, null, null, index);
            }
        }

        private Expresie ParseazaTermen()
        {
            var stanga = ParseazaFactor();
            while (AtomCurent.Tip == TipAtomLexical.PlusAtomLexical ||
                AtomCurent.Tip== TipAtomLexical.MinusAtomLexical )
            {
                var operatorExpresie = AtomCurentSiIncrementeaza();
                var dreapta = ParseazaFactor();
                stanga = new ExpresieBinara(stanga, operatorExpresie, dreapta);
            }
            return stanga;
        }

        public Expresie Parseaza()
        {
            if (atomilexicali.First().Tip == TipAtomLexical.TipDateInt ||
                atomilexicali.First().Tip == TipAtomLexical.TipDateDouble ||
                atomilexicali.First().Tip == TipAtomLexical.TipDateString)
            
                return ParseazaExpresieLiterara();
            else
                return ParseazaTermen();
        }
        private Expresie ParseazaFactor()
        {
            var stanga = ParseazaExpresie();
            while (AtomCurent.Tip == TipAtomLexical.SlashAtomLexical ||
                AtomCurent.Tip == TipAtomLexical.StarAtomLexical)
            {
                var operatorExpresie = AtomCurentSiIncrementeaza();
                var dreapta = ParseazaExpresie();
                stanga = new ExpresieBinara(stanga, operatorExpresie, dreapta);
            }
            return stanga;
        }

        private Expresie ParseazaExpresie()
        {
            if(AtomCurent.Tip== TipAtomLexical.ParantezaDeschisaAtomLexical)
            {
                var parantezaDeschisa = AtomCurentSiIncrementeaza();
                var expresie = Parseaza();
                var parantezaInchisa = VerificareTip(TipAtomLexical.ParantezaInchisaAtomLexical);
                return new ExpresieParanteza(parantezaDeschisa, expresie, parantezaInchisa);
            }
            var numar = VerificareTip(TipAtomLexical.NumarAtomLexical);
            return new ExpresieNumerica(numar);
        }


        private Expresie ParseazaExpresieLiterara()
        {

            IntVariable new_var_int = default;
            StringVariable new_string_var = default;
            DoubleVariable new_double_var = default;
            List<Variable> new_vars = new List<Variable>();
            ExpresieLiterara expresie = new ExpresieLiterara();
            bool int_init = false;
            bool string_init = false;
            bool double_init = false;

            bool after_equal = false;
            bool has_final_atom = false;
            //foreach(var atom in this.atomilexicali)
            while (index < atomilexicali.Length)
            {
                if(!after_equal)
                {
                    if (AtomCurent.Tip == TipAtomLexical.TipDateString || AtomCurent.Tip == TipAtomLexical.TipDateDouble || AtomCurent.Tip == TipAtomLexical.TipDateInt)
                    {
                        this.datatype = AtomCurent;

                        if (AtomCurent.Tip == TipAtomLexical.TipDateString)
                            string_init = true;
                        if (AtomCurent.Tip == TipAtomLexical.TipDateDouble)
                            double_init = true;
                        if (AtomCurent.Tip == TipAtomLexical.TipDateInt)
                            int_init = true;
                    }

                    if (AtomCurent.Tip == TipAtomLexical.VariabilaAtomLexical)
                    {

                        if (this.datatype.Tip == TipAtomLexical.TipDateInt)
                        {
                            // vec_variables.Append(new IntVariable(AtomCurent.Text, false));
                            new_var_int = new IntVariable(AtomCurent.Text, false);
                        }
                        else
                            if (this.datatype.Tip == TipAtomLexical.TipDateString )
                        {
                            //vec_variables.Append(new StringVariable(AtomCurent.Text, false));
                            new_string_var = new StringVariable(AtomCurent.Text, false);
                        }
                        else
                            if (this.datatype.Tip == TipAtomLexical.TipDateDouble)
                        {
                            //vec_variables.Append(new DoubleVariable(AtomCurent.Text, false));
                            new_double_var = new DoubleVariable(AtomCurent.Text, false);
                        }

                        this.var_name = AtomCurent;
                    }

                    if(AtomCurent.Tip == TipAtomLexical.EgalAtomLexical)
                    {
                        after_equal = true;
                    }

                }
                else
                {                   
              
                    if (this.datatype.Tip == TipAtomLexical.TipDateInt)
                    {
                        int value;
                        Int32.TryParse(AtomCurent.Text, out value);
                        new_var_int.Value = value;
                    }
                    else

                    if (this.datatype.Tip == TipAtomLexical.TipDateString || this.datatype.Tip == TipAtomLexical.EgalAtomLexical)
                    {
                        if (AtomCurent.Text != ";")
                            new_string_var.Value += AtomCurent.Text;
                    }
                    else
                    if (this.datatype.Tip == TipAtomLexical.TipDateDouble)
                    {
                        new_double_var.Value = Double.Parse(AtomCurent.Text);
                    }


                    if (AtomCurent.Tip == TipAtomLexical.VirgulaAtomLexical)
                    {
                        if (int_init)
                            new_vars.Add(new_var_int);
                        if (string_init)
                            new_vars.Add(new_string_var);
                        if (double_init)
                            new_vars.Add(new_double_var);

                        after_equal = false;
                    }

                    if (AtomCurent.Tip == TipAtomLexical.PunctSiVirgulaAtomLexical)
                    {
                        if (int_init)
                            new_vars.Add(new_var_int);
                        if (string_init)
                            new_vars.Add(new_string_var);
                        if (double_init)
                            new_vars.Add(new_double_var);

                        vec_variables.AddRange(new_vars);
                        has_final_atom = true;

                    }
                    
                }

                if (AtomCurent.Tip == TipAtomLexical.PunctSiVirgulaAtomLexical)
                    has_final_atom = true;

                expresie.Expresie.Add(AtomCurent);

                index++;
            }

            if (!has_final_atom)
                erori.Add("missing ';'");

            return expresie;
        }
    }
}
