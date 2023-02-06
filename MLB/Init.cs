using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MLB
{
    public partial class Init : Form
    {
        private int ticks;
       // private int state;
        public Init(int rest)
        {
            InitializeComponent();
            ticks = rest;
           // state = 0;
        }

        private void Init_Shown(object sender, EventArgs e)
        {
            timer1.Interval = (10000*ticks)/100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 96)
            {
                progressBar1.Value = progressBar1.Value + 4;
            }
            else
                progressBar1.Value = 100;

        }
    }
}
