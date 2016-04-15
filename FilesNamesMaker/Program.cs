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
            try
            {
                Int32 i = 0;
                //Proceso archivos
                DirectoryInfo unDirectorio = new DirectoryInfo(@"C:\Users\Eduardo\Documents\Directv");
                Parser unParser = new Parser();

                foreach (var unArchivo in unDirectorio.GetFiles())
                {
                    //Parseo
                    unParser.cadena = unArchivo.Name;
                    unParser.parsear();

                    //
                    Console.WriteLine(i.ToString());
                    Console.WriteLine(unArchivo.Name);
                    Console.WriteLine(unParser.registro.getNombreFormateado());

                    Console.WriteLine("\n");
                    i++;
                
                //Renombro
                //unArchivo.MoveTo(@"C:\Users\agustin.binci\Documents\Directv\" + unArchivo.Name + "1");
                }

               /* String expresionRegular = @".*((ef)|(tc)).*";
                Regex regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                Match regExpMatch = regExp.Match("fseffds");

                while (regExpMatch.Success)
                {
                    Console.WriteLine(regExpMatch);
                    regExpMatch = regExpMatch.NextMatch();
                }*/

            }
            catch(Exception unaExcepcion){
                Console.WriteLine(unaExcepcion.ToString());
            }         

        }
    }
}
