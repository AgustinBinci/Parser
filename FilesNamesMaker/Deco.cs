using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    abstract class Deco
    {

        public Int32 cantidad { get; set; }
        public String descripcion { get; set; }
        public TipoDePago tipoDePago { get; set; }

        public Deco()
        {
            this.cantidad = 0;
        }

        public virtual void setCantidad(Int32 unaCantidad)
        {
            this.cantidad = unaCantidad;
        }
        
        public virtual void setDescripcion(String unaDescripcion)
        {
            this.descripcion = unaDescripcion;
        }

    }
}
