using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilesNamesMaker
{
    static class Fecha
    {

        public static String getAnio() 
        {
            return DateTime.Today.Year.ToString();
        }

        public static String getMes()
        {
            String unMes = DateTime.Today.Month.ToString();
            if (unMes.Count() == 1) unMes = "0" + unMes;
            return unMes;
        }

    }
}
