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
    public partial class Authorization : Form
    {
        private Form1 main;
        private DBControl dbc;
        public Authorization( Form1 main)
        {
            InitializeComponent();
            textBox1.Focus();
            this.main = main;
            dbc = new DBControl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                this.BackColor  = System.Drawing.SystemColors.InactiveCaptionText;
                label1.ForeColor = System.Drawing.SystemColors.Window;
                textBox1.BackColor = System.Drawing.SystemColors.Info;
                
            }
            textBox1.Focus();

            textBox1.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dbc.ExistQuerry("Select Password From Administrador Where Password = '" + textBox1.Text + "'"))
            {
                main.editarVariablesToolStripMenuItem.Enabled = true;
                main.Forccer();
                Close();
            }
            else
            {
                textBox1.BackColor = Color.LightCoral;
                textBox1.Focus();
                textBox1.SelectAll();
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void Authorization_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                button2_Click(sender, e);
            }
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                Close();
            }
        }
    }
}
