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
            try
            {
                this.registro.tipoDePago = this.getTipoDePago();
                this.registro.deco = this.getDeco();
                this.registro.deco.tipoDePago = this.registro.tipoDePago;
                this.registro.ciclo = this.getCiclo();
            }
            catch (Exception unaExcepcion)
            {
                throw new Exception(unaExcepcion.Message + " -- " + this.cadena);
            }
        }

        //**************************************************************************************************************************
        //*******************************************************METODOS PRIVADOS***************************************************
        //**************************************************************************************************************************

        //Tipo de pago
        private TipoDePago getTipoDePago()
        {
            try
            {
                TipoDePago unTipoDePago = new TipoDePago();

                String expresionRegular = @"([^a-z]|^)((ef)|(tc))([^a-z]|$)";
                Regex regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                Match regExpMatch = regExp.Match(this.cadena);

                if (regExpMatch.Success)
                {
                    expresionRegular = @"(ef)|(tc)";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(regExpMatch.Value);

                    if (regExpMatch.Success) unTipoDePago.descripcion = regExpMatch.Value;
                    else throw new Exception("No hay tipo de pago");
                }
                else throw new Exception("No hay tipo de pago");

                return unTipoDePago;
            }
            catch (Exception unaExcepcion)
            {
                throw unaExcepcion;
            }
        }

        //Ciclo
        private Ciclo getCiclo()
        {
            try
            {
                Ciclo unCiclo = new Ciclo();

                //Corporativo directa
                String expresionRegular = @"directa";
                Regex regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                Match regExpMatch = regExp.Match(this.cadena);

                if (regExpMatch.Success) 
                {
                    //Chequeo indirecta
                    expresionRegular = @"indirecta";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);

                    if (regExpMatch.Success) unCiclo.descripcion = "CORPORATIVO INDIRECTA";
                    else unCiclo.descripcion = "CORPORATIVO DIRECTA";
                }            

                else
                {
                    //Regular
                    expresionRegular = @"regular";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);

                    if (regExpMatch.Success) unCiclo.descripcion = "REGULAR";

                    else
                    {
                        //Empleados red
                        expresionRegular = @"empleado[s]? red";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(this.cadena);

                        if (regExpMatch.Success) unCiclo.descripcion = "EMPLEADOS RED";

                        else
                        {
                            //Empleados mam
                            expresionRegular = @"empleado[s]? mam";
                            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                            regExpMatch = regExp.Match(this.cadena);

                            if (regExpMatch.Success) unCiclo.descripcion = "EMPLEADOS MAM";

                            else
                            {
                                //Mdu
                                expresionRegular = @"mdu";
                                regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                                regExpMatch = regExp.Match(this.cadena);

                                if (regExpMatch.Success) unCiclo.descripcion = "MDU";

                                else
                                {
                                    //referido
                                    expresionRegular = @"referido";
                                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                                    regExpMatch = regExp.Match(this.cadena);

                                    if (regExpMatch.Success) unCiclo.descripcion = "REFERIDO";

                                    else throw new Exception("No hay ningun ciclo");
                                }
                            }
                        }
                    }
                }
                

                return unCiclo;

            }
            catch (Exception unaExcepcion)
            {
                throw unaExcepcion;
            }
        }

        //Deco
        private Deco getDeco()
        {
            try
            {
                Deco unDeco = null;

                //Chequeo si el deco es con mirror o sin
                String expresionRegular = @"[0-9]+\.[0-9]+";
                Regex regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);

                Match regExpMatch = regExp.Match(this.cadena);

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

                    if (regExp.Match(this.cadena).Success) unDecoConMirrors.descripcion = "HD ORO NEXUS";

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
                    DecoSinMirror unDecoSinMirror = (DecoSinMirror)(unDeco);
                    Boolean decoEncontrado = false;
                    String matchPrincipal = "";

                    //Hd only
                    expresionRegular = @"[0-9]+.?(hd )?only";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);

                    if (regExpMatch.Success)
                    {
                        unDeco.descripcion = "HD ONLY";
                        decoEncontrado = true;

                        //Cantidad
                        expresionRegular = @"[0-9]+";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(regExpMatch.Value);

                        if (regExpMatch.Success) unDeco.cantidad = Int32.Parse(regExpMatch.Value);
                        else throw new Exception("Hd only sin cantidad");
                    }

                    //Plata
                    expresionRegular = @"[0-9]+.?ird";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);
                    Boolean plataEncontrado = false;

                    if (regExpMatch.Success)
                    {
                        matchPrincipal = regExpMatch.Value;

                        expresionRegular = @"plata";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        Match regExpMatchAux = regExp.Match(this.cadena);

                        if (regExpMatchAux.Success)
                        {
                            if (decoEncontrado) unDecoSinMirror.addDeco();
                            unDecoSinMirror.setDescripcion("IRD PLATA");
                            decoEncontrado = true;

                            //Cantidad
                            expresionRegular = @"[0-9]+";
                            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                            regExpMatchAux = regExp.Match(matchPrincipal);

                            if (regExpMatchAux.Success) unDecoSinMirror.setCantidad(Int32.Parse(regExpMatchAux.Value));
                            else throw new Exception("Plata sin cantidad");

                        }

               
                    }

                    if (!plataEncontrado)
                    {
                        expresionRegular = @"[0-9]+.?plata";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(this.cadena);

                        if (regExpMatch.Success)
                        {
                            matchPrincipal = regExpMatch.Value;

                            if (decoEncontrado) unDecoSinMirror.addDeco();
                            unDecoSinMirror.setDescripcion("IRD PLATA");
                            decoEncontrado = true;

                            //Cantidad
                            expresionRegular = @"[0-9]+";
                            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                            regExpMatch = regExp.Match(matchPrincipal);

                            if (regExpMatch.Success) unDecoSinMirror.setCantidad(Int32.Parse(regExpMatch.Value));
                            else throw new Exception("Plata sin cantidad");
                        }
                    }

                    //Mix
                    expresionRegular = @"[0-9]+.?ird";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);
                    Boolean mixEncontrado = false;

                    if (regExpMatch.Success)
                    {
                        matchPrincipal = regExpMatch.Value;

                        expresionRegular = @"mix";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        Match regExpMatchAux = regExp.Match(this.cadena);

                        if (regExpMatchAux.Success)
                        {
                            mixEncontrado = true;

                            if (decoEncontrado) unDecoSinMirror.addDeco();
                            unDecoSinMirror.setDescripcion("IRD MIX");
                            decoEncontrado = true;

                            //Cantidad
                            expresionRegular = @"[0-9]+";
                            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                            regExpMatchAux = regExp.Match(matchPrincipal);

                            if (regExpMatchAux.Success) unDecoSinMirror.setCantidad(Int32.Parse(regExpMatchAux.Value));
                            else throw new Exception("Mix sin cantidad");

                        }

                    }

                    if (!mixEncontrado)
                    {
                        expresionRegular = @"[0-9]+.?mix";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(this.cadena);

                        if (regExpMatch.Success)
                        {
                            matchPrincipal = regExpMatch.Value;

                            if (decoEncontrado) unDecoSinMirror.addDeco();
                            unDecoSinMirror.setDescripcion("IRD MIX");
                            decoEncontrado = true;

                            //Cantidad
                            expresionRegular = @"[0-9]+";
                            regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                            regExpMatch = regExp.Match(matchPrincipal);

                            if (regExpMatch.Success) unDecoSinMirror.setCantidad(Int32.Parse(regExpMatch.Value));
                            else throw new Exception("Mix sin cantidad");
                        }
                    }

                    //Plus
                    expresionRegular = @"[0-9]+.?plus";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);

                    if (regExpMatch.Success)
                    {
                        if (decoEncontrado) unDecoSinMirror.addDeco();
                        unDecoSinMirror.setDescripcion("PLUS HD");
                        decoEncontrado = true;

                        //Cantidad
                        expresionRegular = @"[0-9]+";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(regExpMatch.Value);

                        if (regExpMatch.Success) unDecoSinMirror.setCantidad(Int32.Parse(regExpMatch.Value));
                        else throw new Exception("Plus sin cantidad");           

                    }

                    //Ird solo
                    expresionRegular = @"\+ ?[0-9]+.?ird";
                    regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                    regExpMatch = regExp.Match(this.cadena);

                    if (regExpMatch.Success)
                    {

                        expresionRegular = @"[0-9]+";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        Match regExpMatchAux = regExp.Match(regExpMatch.Value);

                        if (regExpMatchAux.Success)
                        {
                            if (decoEncontrado) unDecoSinMirror.addDeco();

                            unDecoSinMirror.setDescripcion("IRD");
                            unDecoSinMirror.setCantidad(Int32.Parse(regExpMatchAux.Value));                                                
                        }
                        else throw new Exception("Ird sin cantidad");

                    }
                    else
                    {
                        expresionRegular = @"\+ ?ird";
                        regExp = new Regex(expresionRegular, RegexOptions.IgnoreCase);
                        regExpMatch = regExp.Match(this.cadena);

                        if (regExpMatch.Success)
                        {
                            if (decoEncontrado) unDecoSinMirror.addDeco();

                            unDecoSinMirror.setDescripcion("IRD");
                            unDecoSinMirror.setCantidad(1);        
                        }
                    }
                }

                return unDeco;
            }
            catch (Exception unaExcepcion)
            {
                throw unaExcepcion;
            }      
        }

        //Fin de la clase
    }
}
