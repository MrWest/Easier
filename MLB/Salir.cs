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
    public partial class Salir : Form
    {
        private String dsg;
        private MLB.Form1 mf;
        public Salir(String docsinguardar, MLB.Form1 main)
        {
            InitializeComponent();
            dsg = docsinguardar;
            mf = main;
        }

        private void Salir_Load(object sender, EventArgs e)
        {

            button1.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(this, dsg, "Documentos sin Guardar");
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  mf.salir = false;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Close();
            mf.OutYa();
        }
    }
}
