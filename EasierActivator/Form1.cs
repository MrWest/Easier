using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EasierActivator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            EasierActivator.Properties.Resources.Culture = new System.Globalization.CultureInfo("en-US");
            Application.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                int secs = -1;
                System.DateTime expdate = System.DateTime.Today;
                if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key"))
                {
                    EasierActivator.Encripter enc = new EasierActivator.Encripter();
                    String fulltextdata = enc.DecryptFile2(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key", System.Environment.MachineName);

                    String date = fulltextdata.Substring(fulltextdata.LastIndexOf(" (w) ") + 5, (fulltextdata.Length) - (fulltextdata.LastIndexOf(" (w) ") + 5));

                    expdate = System.Convert.ToDateTime(date);

                    secs = System.DateTime.Compare(expdate, System.DateTime.Today);
                }
                if (secs > 0)
                {
                    label1.Text = "Su periodo de Reactivacion culmina el dia " + expdate.Day.ToString() + " de " + MonthConverter(expdate.Month) + " del " + expdate.Year.ToString();//".\n Cuenta con "+System.Convert.ToString(secs)+" para Reactivar el Software.";
                }
                else if (secs == 0)
                    label1.Text = "Su periodo de Reactivacion termina hoy.";
                else
                {

                    label1.Text = "Por favor especifique la ruta del archivo llave para pa activacion del Easier.";
                }

                EasierActivator.RC2Crypt rc = new EasierActivator.RC2Crypt();
                //rc.Sample();
                textBox1.Text = rc.Encrypt(System.Convert.ToString(System.DateTime.Now) + " | - | " + System.Environment.MachineName);

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            

        }
        private String MonthConverter(int month)
        {
            if (month == 1)
            {
                return "Enero";
            }
            if (month == 2)
            {
                return "Febrero";
            }
            if (month == 3)
            {
                return "Marzo";
            }
            if (month == 4)
            {
                return "Abril";
            }
            if (month == 5)
            {
                return "Mayo";
            }
            if (month == 6)
            {
                return "Junio";
            }
            if (month == 7)
            {
                return "Julio";
            }
            if (month == 8)
            {
                return "Agosto";
            }
            if (month == 9)
            {
                return "Septiembre";
            }
            if (month == 10)
            {
                return "Octubre";
            }
            if (month == 11)
            {
                return "Noviembre";
            }

            return "Diciembre";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                if (File.Exists(openFileDialog1.FileName))
                {
                    FileInfo fi = new FileInfo(openFileDialog1.FileName);

                    if (Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\"))
                    {
                        fi.CopyTo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\" + openFileDialog1.SafeFileName, true);
                    }
                    else
                    {
                        Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier");
                        fi.CopyTo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\" + openFileDialog1.SafeFileName, true);

                    }
                }


                int secs = -1;
                System.DateTime expdate = System.DateTime.Today;
                if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key"))
                {
                    EasierActivator.Encripter enc = new EasierActivator.Encripter();
                    String fulltextdata = enc.DecryptFile2(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key", System.Environment.MachineName);

                    String date = fulltextdata.Substring(fulltextdata.LastIndexOf(" (w) ") + 5, (fulltextdata.Length) - (fulltextdata.LastIndexOf(" (w) ") + 5));

                    expdate = System.Convert.ToDateTime(date);

                    secs = System.DateTime.Compare(expdate, System.DateTime.Today);
                }
                if (secs > 0)
                {
                    label1.Text = "Su periodo de Reactivacion culmina el dia " + expdate.Day.ToString() + " de " + MonthConverter(expdate.Month) + " del " + expdate.Year.ToString();//".\n Cuenta con "+System.Convert.ToString(secs)+" para Reactivar el Software.";
                }
                else if (secs == 0)
                    label1.Text = "Su periodo de Reactivacion termina hoy.";
                else
                {

                    label1.Text = "Por favor especifique la ruta del archivo llave para pa activacion del Easier.";
                }


            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
          
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            textBox1.Width = this.Width - 104;

            if (this.Width < 417)
            {
                this.Width = 417;
            }
            if (this.Height < 176)
            {
                this.Height = 176;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
