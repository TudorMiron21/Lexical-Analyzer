using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_limbaje
{
    internal class ReadChoice
    {

  

        static public void read()
        {
           
 
            var choice = 3;
            bool fileread = false;
        
            while (true)
            {
               
                Console.WriteLine("ce tip de citire doriti?:\n(1)citire din fisier\n(2)citire de la tastatura");
                choice =Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    fileread = true;
                    break;
                }

                else
                    if (choice == 2)
                {
                    fileread = false;
                    break;
                }
                else
                {
                    Console.WriteLine("optiune invalida");

                }                
            }

            while (true)
            {

                Console.Write("> ");
                string text;
                if (fileread == false)

                    text = Console.ReadLine();
                else
                {
                    text = System.IO.File.ReadAllText("filetext.txt");                    
                }
                if (string.IsNullOrWhiteSpace(text))
                    return;

                var parser = new Parser(text);
                var arboreSintactic = parser.Parseaza();

                var culoare = Console.ForegroundColor;
                if (parser.erori.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var eroare in parser.erori)
                        Console.WriteLine(eroare);

                    Console.ForegroundColor = culoare;
                }
                else
                {
                    if (parser.pass_through_evalutaor)
                    {

                        Evaluator e = new Evaluator(arboreSintactic);
                        var res = e.Evalueaza();
                        Console.WriteLine(res);
                        AfiseazaArbore(arboreSintactic);
                    }
                    else
                    {
                        Console.WriteLine("hello");
                    }
                }

                fileread = false;
            }
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



}

