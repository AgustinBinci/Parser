using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    class DecoSinMirror : Deco
    {
        public DecoSinMirror deco { get; set; }

        public DecoSinMirror()
            : base() 
        {
            this.deco = null;
        }

        public override string ToString()
        {
            if (this.tipoDePago == null) throw new Exception(this.descripcion + " no tengo tipo de pago");

            String unaDescripcion = this.getDescripcionCompleta();
            unaDescripcion += " - ";
            unaDescripcion += this.tipoDePago.descripcion;
            return unaDescripcion;
        }

        public String getDescripcionCompleta()
        {
            String unaDescripcion = this.cantidad.ToString();
            unaDescripcion += " ";
            unaDescripcion += this.descripcion;

            if (this.deco != null)
            {
                unaDescripcion += " + ";
                unaDescripcion += this.deco.getDescripcionCompleta();
            }

            return unaDescripcion;
        }

        public override void setCantidad(Int32 unaCantidad)
        {
            if (this.deco != null) this.deco.setCantidad(unaCantidad);
            else this.cantidad = unaCantidad;
        }

        public override void setDescripcion(String unaDescripcion)
        {
            if (this.deco != null) this.deco.setDescripcion(unaDescripcion);
            else this.descripcion = unaDescripcion;
        }

        public void addDeco()
        {
            if (this.deco != null) this.deco.addDeco();
            else this.deco = new DecoSinMirror();
        }

    }
}
