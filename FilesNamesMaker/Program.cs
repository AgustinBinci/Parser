using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace FilesNamesMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Proceso archivos
            DirectoryInfo unDirectorio = new DirectoryInfo(@"C:\Users\agustin.binci\Documents\Directv");

            //foreach (var unArchivo in unDirectorio.GetFiles())
            //{
                //Parseo
                String unString = " sa nexus asfa -09.1fdsf ";
                String expresionRegular = @"[0-9]+";

                Regex regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                Match regExpMatch = regExp.Match(unString);

                while (regExpMatch.Success)
                {
                    Console.WriteLine(regExpMatch.Value);
                    regExpMatch = regExpMatch.NextMatch();
                }

                //Imprimo
                /*Console.WriteLine(unArchivo.Name);
                Console.WriteLine(unArchivo.Name);

                Console.WriteLine("\n");
                
                //Renombro
                //unArchivo.MoveTo(@"C:\Users\agustin.binci\Documents\Directv\" + unArchivo.Name + "1");*/
            //}
        }
    }
}
