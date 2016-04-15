using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    class Registro
    {
        public TipoDePago tipoDePago { get; set; }
        public Deco deco { get; set; }
        public Ciclo ciclo { get; set; }


        public String getNombreFormateado()
        {
            String unNombre = Fecha.getAnio() + Fecha.getMes();
            unNombre += " - ";
            unNombre += this.ciclo.descripcion;
            unNombre += " - ";
            unNombre += this.deco.ToString();

            return unNombre;
        }
     
    }
}
