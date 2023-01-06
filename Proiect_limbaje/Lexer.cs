using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class Lexer //analizator lexical
    {
        private readonly string text;
        private int index;
        public List<string> erori = new List<string>();


        public Lexer(string text)
        {
            this.text = text;
            this.index = 0;
        }

        private char get_SimbolCurent
        {
            get
            {
                if (index >= text.Length)
                    return '\0';
                return text[index];
            }
        }

        private void IncrementIndex()
        {
            index++;
        }

        public AtomLexical Atom()
        {
            //terminator sir
            if(index >= text.Length)
            {
                return new AtomLexical(TipAtomLexical.TerminatorSirAtomLexical, "\0" ,null,index);
            }

            //operatori matematici

            if (get_SimbolCurent == '+') 
                return new AtomLexical(TipAtomLexical.PlusAtomLexical, "+", null, index++);
            if (get_SimbolCurent == '-')
                return new AtomLexical(TipAtomLexical.MinusAtomLexical, "-", null, index++);
            if (get_SimbolCurent == '*')
                return new AtomLexical(TipAtomLexical.StarAtomLexical, "*", null, index++);
            if (get_SimbolCurent == '/')
                return new AtomLexical(TipAtomLexical.SlashAtomLexical, "/", null, index++);
            if (get_SimbolCurent == '(')
                  return new AtomLexical(TipAtomLexical.ParantezaDeschisaAtomLexical, "(", null, index++);
            if (get_SimbolCurent == ')')
                return new AtomLexical(TipAtomLexical.ParantezaInchisaAtomLexical, ")", null, index++);


            //numere
            if (char.IsDigit(get_SimbolCurent))
            {
                bool dot_detected = false;
                bool conversion_error = false;
                int start = index;
                do
                {
                    if (get_SimbolCurent == '.')
                        dot_detected = true;    
                    if (get_SimbolCurent == ';')
                        break;
                    IncrementIndex();
                } while (char.IsDigit(get_SimbolCurent) || get_SimbolCurent == '.');

                string val = text.Substring(start, index - start);
                int res_int;
                double res_double;

                if (dot_detected)
                    if (Double.TryParse(val, out res_double))
                        return new AtomLexical(TipAtomLexical.NumarDoubleAtomLexical, val, res_double, start);
                    else
                        conversion_error = true;

                if (Int32.TryParse(val, out res_int)) // facem conversia de la string la int
                    return new AtomLexical(TipAtomLexical.NumarIntAtomLexical, val, res_int, start);
                else
                    conversion_error = true;

                if(conversion_error)
                {
                    erori.Add($"sirul de caract {val} nu a putut fi parsat la int32");
                }

            }

            //tipuri de date si variabile 
            if(char.IsLetter(get_SimbolCurent))
            {
                int start = index;
                do
                {
                    IncrementIndex();
                    if (get_SimbolCurent == '\0' || get_SimbolCurent =='=' || get_SimbolCurent ==';' || get_SimbolCurent == '"' || get_SimbolCurent==',')
                        break;
                }while (!char.IsWhiteSpace(get_SimbolCurent));

                string val = text.Substring(start, index - start);
                if (val == "int")
                    return new AtomLexical(TipAtomLexical.TipDateInt, val, val, start);
                else
                    if (val == "double")
                        return new AtomLexical(TipAtomLexical.TipDateDouble, val, val, start);
                    else
                        if (val == "string")
                            return new AtomLexical(TipAtomLexical.TipDateString, val, val, start);
                        else
                            return new AtomLexical(TipAtomLexical.VariabilaAtomLexical, val, val, start);              
            }

            //verificare asignare variabile
            if(get_SimbolCurent == '=')
            {
                string val = "=";
                IncrementIndex();
                return new AtomLexical(TipAtomLexical.EgalAtomLexical, val, val, index - 1);

            }

            //virgula 
            if (get_SimbolCurent == ',')
            {
                string val = ",";
                IncrementIndex();
                return new AtomLexical(TipAtomLexical.VirgulaAtomLexical, val, val, index - 1);

            }

            //punct si virgula 
            if (get_SimbolCurent == ';')
            {
                string val = ";";
                IncrementIndex();
                return new AtomLexical(TipAtomLexical.PunctSiVirgulaAtomLexical, val, val, index - 1);

            }

            //ghilimele
            if (get_SimbolCurent == '"')
            {
                string val = "\"";
                IncrementIndex();
                return new AtomLexical(TipAtomLexical.GhilimeleAtomLexical, val, val, index - 1);

            }

            //punct
            if (get_SimbolCurent == '.')
            {
                string val = ".";
                IncrementIndex();
                return new AtomLexical(TipAtomLexical.PunctSiVirgulaAtomLexical, val, val, index - 1);

            }

            //spatii
            if (get_SimbolCurent == ' ')
            {
                int start = index;
                do
                {
                    IncrementIndex();
                } while (get_SimbolCurent == ' ');

                string val = text.Substring(start, index - start);
                
                return new AtomLexical(TipAtomLexical.SpatiuAtomLexical, val, val, start);
          
            }


            erori.Add($"simbol invalid <{get_SimbolCurent}>");
            return new AtomLexical(TipAtomLexical.InvalidAtomLexical, get_SimbolCurent.ToString(), null, index++);


        }
    }
}
