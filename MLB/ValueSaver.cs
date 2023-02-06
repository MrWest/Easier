using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLB
{
    class ValueSaver
    {
        public int col { get; set; }
        public int row { get; set; }
        public String cant { get; set; }
        public String IPVName { get; set; }
        public String UName { get; set; }
        public String Cuenta { get; set; }
        public String Date { get; set; }
        public String producto { get; set; }
        public String  moneda { get; set; }

        public System.Windows.Forms.DataGridView dtgv { get; set; }

        public ValueSaver(String IPVName, String cant, int row, int col, String UName, String Cuenta, String Date, String producto, String moneda)
        {
            
            this.IPVName = IPVName;
            this.cant = cant;
            this.row = row;
            this.col = col;
            this.UName = UName;
            this.Cuenta = Cuenta;
            this.Date = Date;
            this.producto = producto;
            this.moneda = moneda;

        }

    }
}
