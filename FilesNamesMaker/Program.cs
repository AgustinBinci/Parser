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
                //Carpeta de archivos
                String unaCarpeta = @"C:\Users\agustin.binci\Documents\Directv\";

                //Proceso archivos
                DirectoryInfo unDirectorio = new DirectoryInfo(unaCarpeta);
                Parser unParser = new Parser();

                foreach (var unArchivo in unDirectorio.GetFiles())
                {
                    //Parseo
                    unParser.cadena = unArchivo.Name;
                    unParser.parsear();

                    //Obtengo registro
                    Registro unRegistro = unParser.registro;

                    //Renombro
                    unArchivo.MoveTo(unaCarpeta + unRegistro.getNombreFormateado() + ".pdf");
                }

                Console.WriteLine("Mapeo finalizado");
                
            }
            catch(Exception unaExcepcion){
                Console.WriteLine(unaExcepcion.ToString());
            }         

        }
    }
}
