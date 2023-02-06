using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using System.Text;

namespace MLB
{
    class PElab
    {
        public String cuenta { get; set; }
        public String unidad { get; set; }
        public String moneda { get; set; }
        public DateTime date { get; set; }
        public String Nombre { get; set; }
        private ArrayList norma;


        PElab()
        {
            norma = new ArrayList();
        }
    }
}
