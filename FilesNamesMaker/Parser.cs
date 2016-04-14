using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FilesNamesMaker
{
    class Parser
    {
        public Registro registro { get; set; }
        public String cadena { get; set; }

        public Parser()
        {
            this.registro = new Registro();
        }

        public void parsear()
        {
            Int32 unaCantidadDeDecos = 0;
            Int32 unaCantidadDeMirrors = 0;

            Deco unDeco = null;
            TipoDePago unTipoDePago = new TipoDePago();
            Ciclo unCiclo;
            Int32 i = 0;
            Regex regExp;

            //Obtengo modo de pago
            String expresionRegular = @"[t,c][e,f]";
            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
            
            Match regExpMatch = regExp.Match(this.cadena);
            Boolean encontrado = false;

            while (regExpMatch.Success)
            {
                if (regExpMatch.Value.Count() == 2) 
                {
                    encontrado = true;
                    unTipoDePago.descripcion = regExpMatch.Value;

                    //Extraigo el termino del string
                    this.cadena = this.cadena.Substring(0, regExpMatch.Index) + this.cadena.Substring(regExpMatch.Index + regExpMatch.Value.Count(), this.cadena.Count() - 1 - regExpMatch.Index + regExpMatch.Value.Count());
                    break;
                }
                regExpMatch = regExpMatch.NextMatch();
            }

            if (!encontrado) throw new Exception("No hay medio de pago");

            encontrado = false;

            //Chequeo si el deco es con mirror o sin
            expresionRegular = @"[0-9]+\.[0-9]+";
            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

            regExpMatch = regExp.Match(this.cadena);

            //Decos con mirrors
            if (regExpMatch.Success)
            {
                unDeco = new DecoConMirrors();
                DecoConMirrors unDecoConMirrors = (DecoConMirrors)(unDeco);

                //Cantidad y mirrors
                unDecoConMirrors.cantidad = Int32.Parse(regExpMatch.Value.ElementAt(0).ToString());
                unDecoConMirrors.cantidadDeMirrors = Int32.Parse(regExpMatch.Value.ElementAt(2).ToString());

                //Nexus
                expresionRegular = @"nexus";
                regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                if (regExp.Match(this.cadena).Success)
                {
                    unDecoConMirrors.descripcion = "HD ORO NEXUS";
                }

                else
                {
                    //Platino
                    expresionRegular = @"platino";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                    if (regExp.Match(this.cadena).Success)
                    {
                        unDecoConMirrors.descripcion = "PLATINO";
                    }
                    else throw new Exception("No es ni platino ni nexus pero tiene mirrors");
                }

            }

            //Decos sin mirrors
            else
            {
                unDeco = new DecoSinMirror();
                DecoSinMirror unDecoSinMirrors = (DecoSinMirror)(unDeco);

                //Cantidad
                expresionRegular = @"[0-9]+";
                regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                regExpMatch = regExp.Match(this.cadena);

                //Cantidad y mirrors
                /*unDecoSinMirrors.cantidad = Int32.Parse(regExpMatch.Value.ElementAt(0).ToString());

                //Nexus
                expresionRegular = @"nexus";
                regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                if (regExp.Match(this.cadena).NextMatch().Success)
                {
                    unDecoConMirrors.descripcion = "HD ORO NEXUS";
                }

                else
                {
                    //Platino
                    expresionRegular = @"platino";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                    if (regExp.Match(this.cadena).NextMatch().Success)
                    {
                        unDecoConMirrors.descripcion = "PLATINO";
                    }
                    else throw new Exception("No es ni platino ni nexus pero tiene mirrors");
                }*/
                
            }

  
        }
    }
}
