using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLB
{
    class InBetween
    {
        public String Cuenta { get; set; }
        public double Saldo { get; set; }
        public String Tipo { get; set; }
        public String FromTo { get; set; }
        public String producto { get; set; }
        public String cant { get; set; }
        public String eprod { get; set; } 
        public String ipv { get; set; }

        public InBetween(String Cuenta, double Saldo, String Tipo, String FromTo, String producto, String cant, String eprod, String ipv)
        {
            this.Cuenta = Cuenta;
            this.Saldo = Saldo;
            this.Tipo = Tipo;
            this.FromTo = FromTo;
            this.producto = producto;
            this.cant = cant;
            this.eprod = eprod;
            this.ipv = ipv;

      


        }


        
    }
}
