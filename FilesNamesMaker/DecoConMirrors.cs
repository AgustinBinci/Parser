using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    class DecoConMirrors : Deco
    {

        public Int32 cantidadDeMirrors { get; set; }
        public TipoDePago tipoDePago { get; set; }

        public DecoConMirrors()
            : base()
        {
            this.cantidadDeMirrors = 0;
        }

        public void setMirror()
        {
            cantidadDeMirrors++;
        }

        public override string ToString()
        {
            String unaDescripcion = this.descripcion;
            unaDescripcion += " ";
            unaDescripcion += this.tipoDePago.descripcion;
            unaDescripcion += " ";
            unaDescripcion += this.cantidad.ToString() + "." + this.cantidadDeMirrors.ToString();

            return unaDescripcion;
        }

    }
}
