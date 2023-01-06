using Proiect_limbaje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace Proiect_limbaje
{

    enum Operatii { Initializare, Asignare, OperatieMatematica }
    internal class Parser //analizator sintactic (verifica ordinea atomilor lexicali)
    {
        private readonly AtomLexical[] atomilexicali;
        private int index;
        public List<string> erori = new List<string>();
        private AtomLexical datatype;
        private AtomLexical var_name;
        private Operatii Operatie;

        public Parser(string text)
        {
            
            var atomi = new List<AtomLexical>();
            bool equal_found = false;
            bool datatype_found = false;
            Lexer l = new Lexer(text);

            

            AtomLexical atom;
            do
            {
                atom = l.Atom();
                if (atom.Tip != TipAtomLexical.InvalidAtomLexical
                    //atom.Tip != TipAtomLexical.GhilimeleAtomLexical)
                    //atom.Tip != TipAtomLexical.TipDateInt &&
                    //atom.Tip != TipAtomLexical.TipDateDouble&&
                    //atom.Tip != TipAtomLexical.TipDateString&&
                    //atom.Tip != TipAtomLexical.VariabilaAtomLexical
                    )

                    if(TipAtomLexical.EgalAtomLexical == atom.Tip && !datatype_found)
                        equal_found = true;
                    if (TipAtomLexical.TipDateInt == atom.Tip || TipAtomLexical.TipDateString == atom.Tip || TipAtomLexical.TipDateDouble == atom.Tip )
                        datatype_found = true;
                   
                    
                    atomi.Add(atom);

            } while (atom.Tip != TipAtomLexical.TerminatorSirAtomLexical);

            if (datatype_found)
                this.Operatie = Operatii.Initializare;
            else
                if (equal_found)
                this.Operatie = Operatii.Asignare;
            else
                this.Operatie = Operatii.OperatieMatematica;

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
            if (this.Operatie == Operatii.Initializare)
                return ParseazaExpresieLiterara();
            else
                if (this.Operatie == Operatii.Asignare)
                return ParseazaExpresieLiterara_search();
            else
                if (this.Operatie == Operatii.OperatieMatematica)
                return ParseazaTermen();

            return null;
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
            var numar = VerificareTip(TipAtomLexical.NumarIntAtomLexical);//modifica
            return new ExpresieNumerica(numar);
        }


        private Expresie ParseazaExpresieLiterara()
        {

            IntVariable new_var_int = default;
            StringVariable new_string_var = default;
            DoubleVariable new_double_var = default;
            List<Variable> new_vars = new List<Variable>();
            ExpresieLiterara expresie = new ExpresieLiterara();
            int ghilimele_count = 0;
            bool int_init = false;
            bool string_init = false;
            bool double_init = false;
            bool first_ghilimea = false;
            bool last_ghilimea = true;

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
                            new_var_int = new IntVariable(AtomCurent.Text, false, TipDate.Int);
                        }
                        else
                            if (this.datatype.Tip == TipAtomLexical.TipDateString )
                        {
                            //vec_variables.Append(new StringVariable(AtomCurent.Text, false));
                            new_string_var = new StringVariable(AtomCurent.Text, false,TipDate.String);
                            //ghilimele_count = 0;
                        }
                        else
                            if (this.datatype.Tip == TipAtomLexical.TipDateDouble)
                        {
                            //vec_variables.Append(new DoubleVariable(AtomCurent.Text, false));
                            new_double_var = new DoubleVariable(AtomCurent.Text, false,TipDate.Double);
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
                    //last_ghilimea = false;
              
                    if (this.datatype.Tip == TipAtomLexical.TipDateInt && AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                    {
                        
                        
                        //int value;
                        //Int32.TryParse(AtomCurent.Text, out value);
                        if (AtomCurent.Tip == TipAtomLexical.NumarIntAtomLexical)
                        {
                            new_var_int.Value = (int)AtomCurent.Valoare;
                            new_var_int.IsInitialised = true;
                        }
                        else
                        {
                            erori.Add("eroare asignare");
                            return null;
                        }
                      
                    }
                    else

                    if (this.datatype.Tip == TipAtomLexical.TipDateString )/*|| this.datatype.Tip == TipAtomLexical.EgalAtomLexical)*/
                    {

                        if (ghilimele_count == 2 )
                           if (AtomCurent.Tip != TipAtomLexical.SpatiuAtomLexical && AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                                last_ghilimea = false;

                        if (AtomCurent.Tip == TipAtomLexical.GhilimeleAtomLexical)
                        {
                            ghilimele_count++;
                            first_ghilimea = true;
                        }

                        if (ghilimele_count == 1 && AtomCurent.Tip != TipAtomLexical.GhilimeleAtomLexical && first_ghilimea == true)
                        {
                            new_string_var.Value += AtomCurent.Text;
                            new_string_var.IsInitialised = true;
                        }
                        else
                            if(first_ghilimea != true && AtomCurent.Tip == TipAtomLexical.VariabilaAtomLexical)
                        {
                            erori.Add("eroare ghilimele 1");
                            return null;
                        }

                        //if (AtomCurent.Tip == TipAtomLexical.GhilimeleAtomLexical || 
                        //    AtomCurent.Tip == TipAtomLexical.SpatiuAtomLexical ||
                        //    AtomCurent.Tip == TipAtomLexical.PunctSiVirgulaAtomLexical)
                        //    last_ghilimea = true;

                    }
                    else
                    if (this.datatype.Tip == TipAtomLexical.TipDateDouble && AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                    {
                        if (AtomCurent.Tip == TipAtomLexical.NumarDoubleAtomLexical)
                        { //new_double_var.Value = Double.Parse(AtomCurent.Text);
                            new_double_var.Value = (double)AtomCurent.Valoare;
                            new_double_var.IsInitialised = true;
                        }
                        else
                        {
                            erori.Add("eroare asignare");
                            return null;
                        }
                    }
                    
                }

                if (AtomCurent.Tip == TipAtomLexical.VirgulaAtomLexical)
                {
                    if (int_init)
                        new_vars.Add(new_var_int);
                    if (string_init)
                    {
                        if(ghilimele_count == 2)
                            new_vars.Add(new_string_var);
                        else
                        {
                            erori.Add("eroare ghilimele2");
                            return null;
                        }
                    }
                    if (double_init)
                        new_vars.Add(new_double_var);

                    after_equal = false;
                }

                if (AtomCurent.Tip == TipAtomLexical.PunctSiVirgulaAtomLexical)
                {
                    if (int_init)
                        new_vars.Add(new_var_int);
                    if (string_init)
                    {
                        if (ghilimele_count == 2 && last_ghilimea == true)
                            new_vars.Add(new_string_var);
                        else
                        {
                            erori.Add("eroare ghilimele2");
                            return null;
                        }
                    }
                        
                    if (double_init)
                        new_vars.Add(new_double_var);

                    // vec_variables.AddRange(new_vars);
                    GlobalVars.vec_vars.AddRange(new_vars);
                    has_final_atom = true;

                }


                expresie.Expresie.Add(AtomCurent);

                index++;
            }

            if (!has_final_atom)
                erori.Add("missing ';'");

            return expresie;
        }

        private Expresie ParseazaExpresieLiterara_search()
        {
            bool after_equal = false;
            bool has_final_atom = false;
            ExpresieLiterara expresie = new ExpresieLiterara();

            IntVariable new_var_int = default;
            StringVariable new_string_var = default;
            DoubleVariable new_double_var = default;

            while (index < atomilexicali.Length)
            {
                if(!after_equal)
                {
                    if(AtomCurent.Tip == TipAtomLexical.VariabilaAtomLexical)
                    {
                        foreach (var variable in GlobalVars.vec_vars)
                        {
                            if (variable.Name == AtomCurent.Text)
                            {
                                if(variable.tipDate == TipDate.Int)
                                {
                                    new_var_int = (IntVariable)variable;
                                    new_var_int.IsInitialised = true;
                                    this.datatype = new AtomLexical(TipAtomLexical.TipDateInt, "", null, 0);
                                }
                                else
                                if(variable.tipDate == TipDate.String)
                                {
                                    new_string_var = (StringVariable)variable;
                                    new_string_var.IsInitialised = true;
                                    new_string_var.Value = "";
                                    this.datatype = new AtomLexical(TipAtomLexical.TipDateString, "", null, 0);

                                }
                                else
                                if(variable.tipDate == TipDate.Double)
                                {
                                    new_double_var = (DoubleVariable)variable;
                                    new_double_var.IsInitialised= true;
                                    this.datatype = new AtomLexical(TipAtomLexical.TipDateDouble,"",null,0);

                                }
                                
                                GlobalVars.vec_vars.Remove(variable);
                                break;
                            }

                        }

                    }

                    if (AtomCurent.Tip == TipAtomLexical.EgalAtomLexical)
                        after_equal = true;
                }
                else
                {

                    if (this.datatype.Tip == TipAtomLexical.TipDateInt)
                    {
                        if (AtomCurent.Tip == TipAtomLexical.NumarIntAtomLexical && AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                        {
                            //int value;
                            //Int32.TryParse(AtomCurent.Text, out value);
                              
                            new_var_int.Value = (int)AtomCurent.Valoare;
                            new_var_int.IsInitialised = true;
                            
                        }
                       
                    }
                    else

                    if (this.datatype.Tip == TipAtomLexical.TipDateString || this.datatype.Tip == TipAtomLexical.EgalAtomLexical)
                    {
                        if (AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                        {

                            new_string_var.Value += AtomCurent.Text;
                            new_string_var.IsInitialised = true;
                        }
                 

                    }
                    else
                    if (this.datatype.Tip == TipAtomLexical.TipDateDouble && AtomCurent.Tip != TipAtomLexical.PunctSiVirgulaAtomLexical)
                    {
                        if (AtomCurent.Tip == TipAtomLexical.NumarDoubleAtomLexical)
                        { //new_double_var.Value = Double.Parse(AtomCurent.Text);
                            new_double_var.Value = (double)AtomCurent.Valoare;
                            new_double_var.IsInitialised = true;
                        }
                 
                    }

                    if (AtomCurent.Tip == TipAtomLexical.PunctSiVirgulaAtomLexical)
                    {
                        if (this.datatype.Tip == TipAtomLexical.TipDateInt)
                            GlobalVars.vec_vars.Add(new_var_int);
                        if (this.datatype.Tip == TipAtomLexical.TipDateDouble)
                            GlobalVars.vec_vars.Add(new_double_var);
                        if (this.datatype.Tip == TipAtomLexical.TipDateString)
                            GlobalVars.vec_vars.Add(new_string_var);
                        has_final_atom = true;
                    }
                }

                index++;
            }
            if (!has_final_atom)
                erori.Add("missing ';'");

            return expresie;
        }
    }
}
