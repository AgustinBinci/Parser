using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    class DecoSinMirror : Deco
    {
        public Deco deco { get; set; }

        public DecoSinMirror()
            : base() 
        {
            this.deco = null;
        }

        public override string ToString()
        {
            String unaDescripcion = this.getDescripcionCompletaDe(this);

            if (this.deco != null)
            {
                unaDescripcion += " + ";
                unaDescripcion += this.getDescripcionCompletaDe(this.deco);
            }

            return unaDescripcion;
        }

        private String getDescripcionCompletaDe(Deco unDeco)
        {
            String unaDescripcion = unDeco.cantidad.ToString();
            unaDescripcion += " ";
            unaDescripcion += unDeco.descripcion;
            return unaDescripcion;
        }

    }
}
