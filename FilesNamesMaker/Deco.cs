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

        public Deco()
        {
            this.cantidad = 0;
        }

    }
}
