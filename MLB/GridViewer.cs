using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLB
{
    class MyGridViewer
    {
        public System.Windows.Forms.DataGridView dtgv { get; set; }
       // public int kk;
        public MyGridViewer(System.Windows.Forms.DataGridView d)
        {
            dtgv = d;
        }
    }
}
