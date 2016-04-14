using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    static class Fecha
    {

        public static Int32 getAnio() 
        {
            return DateTime.Today.Year;
        }

        public static Int32 getMes()
        {
            return DateTime.Today.Month;
        }

    }
}
