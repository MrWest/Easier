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
    public partial class FCReport : Form
    {
        private String unidad, moneda;
        private DateTime date;
        private MLB.DBControl dbc;
        private Random robj;

        public FCReport(String unidad, DateTime date, String moneda)
        {
            InitializeComponent();
            this.unidad = unidad;
            this.date = date;
            this.moneda = moneda;
            dbc = new MLB.DBControl();
            numericUpDown1.Value = date.Year+1;
            dateTimePicker1.Value = date;
            dateTimePicker2.Value = date;
            robj = new Random();

            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                groupBox1.ForeColor = System.Drawing.SystemColors.Window;
                groupBox2.ForeColor = System.Drawing.SystemColors.Window;
                button1.ForeColor = System.Drawing.SystemColors.ControlText;
                button2.ForeColor = System.Drawing.SystemColors.ControlText;
                button3.ForeColor = System.Drawing.SystemColors.ControlText;
                button4.ForeColor = System.Drawing.SystemColors.ControlText;
                button5.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void FCReport_Load(object sender, EventArgs e)
        {
            Readier();
            // TODO: This line of code loads data into the 'MLBFCEstandar.FCEstandar' table. You can move, or remove it, as needed.

            this.FCEstandarTableAdapter.Connection = dbc.GetConnection();
            this.FCEstandarTableAdapter.Fill(this.MLBFCEstandar.FCEstandar);
            // TODO: This line of code loads data into the 'MLBPFlujo.PFlujo' table. You can move, or remove it, as needed.
           // this.PFlujoTableAdapter.Fill(this.MLBPFlujo.PFlujo);

            this.reportViewer1.RefreshReport();
        }

        private void Readier()
        {
            String querry = "SELECT Flujo.UName, Moneda, Flujo.Tipo,Flujo.[Concepto],Grupo FROM [MLB].[dbo].[Flujo] INNER JOIN Concepto ON Flujo.Concepto = Concepto.Concepto  Where Flujo.UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' And Flujo.Tipo = Concepto.Tipo And Concepto.UName = '" + unidad + "' Group By Flujo.UName,Flujo.Moneda, Flujo.Tipo,Concepto.Grupo,Flujo.Concepto Order By Concepto.Grupo";

            dbc.SimplePlan("DELETE FROM FCEstandar WHERE [MachineName]= '" + System.Environment.MachineName + "'");

            System.Data.DataSet dts = dbc.SelectQuerryFixed(querry);
            if (dts.Tables[0].Rows.Count > 0)
            {

                double enero = 0;
                double febrero = 0;
                double marzo = 0;
                double abril = 0;
                double mayo = 0;
                double junio = 0;
                double julio = 0;
                double agosto = 0;
                double septiembre = 0;
                double octubre = 0;
                double noviembre = 0;
                double diciembre = 0;
                System.Data.DataSet dts2;

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {
                    enero = 0;
                    febrero = 0;
                    marzo = 0;
                    abril = 0;
                    mayo = 0;
                    junio = 0;
                    julio = 0;
                    agosto = 0;
                    septiembre = 0;
                    octubre = 0;
                    noviembre = 0;
                    diciembre = 0;
                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Enero FROM [MLB].[dbo].[Flujo] where Month(Date)= '1' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        enero = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Febrero FROM [MLB].[dbo].[Flujo] where Month(Date)= '2' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        febrero = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Marzo FROM [MLB].[dbo].[Flujo] where Month(Date)= '3' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        marzo = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Abril FROM [MLB].[dbo].[Flujo] where Month(Date)= '4' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        abril = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Mayo FROM [MLB].[dbo].[Flujo] where Month(Date)= '5' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        mayo = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Junio FROM [MLB].[dbo].[Flujo] where Month(Date)= '6' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        junio = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Julio FROM [MLB].[dbo].[Flujo] where Month(Date)= '7' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        julio = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Agosto FROM [MLB].[dbo].[Flujo] where Month(Date)= '8' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        agosto = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Septiembre FROM [MLB].[dbo].[Flujo] where Month(Date)= '9' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        septiembre = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Octubre FROM [MLB].[dbo].[Flujo] where Month(Date)= '10' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        octubre = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Noviembre FROM [MLB].[dbo].[Flujo] where Month(Date)= '11' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        noviembre = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());
                    dts2 = dbc.SelectQuerryFixed("SELECT Sum([Importe]) As Diciembre FROM [MLB].[dbo].[Flujo] where Month(Date)= '12' and Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Group by UName,Moneda,Tipo,Concepto ");

                    if (dts2.Tables[0].Rows.Count > 0)
                        diciembre = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());

                    if (dts.Tables[0].Rows[w].ItemArray[2].ToString()=="Entradas")
                    {
                                    dbc.SimplePlan("INSERT INTO [MLB].[dbo].[FCEstandar]([Id],[UName],[Moneda],[Tipo],[Grupo],[Concepto]" +
                      ",[Enero],[Febrero],[Marzo],[Abril],[Mayo],[Junio],[Julio],[Agosto],[Septiembre] ,[Octubre] ,[Noviembre],[Diciembre],[Clasif],[Date],[MachineName])" +
                      "VALUES('" + dbc.MaxQuerry("FCEstandar") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" +
                      System.Convert.ToString(enero) + "','" + System.Convert.ToString(febrero) + "','" + System.Convert.ToString(marzo) + "','" + System.Convert.ToString(abril) + "','" + System.Convert.ToString(mayo) + "','" + System.Convert.ToString(junio) + "','" + System.Convert.ToString(julio) + "','" +
                               System.Convert.ToString(agosto) + "','" + System.Convert.ToString(septiembre) + "','" + System.Convert.ToString(octubre) + "','" + System.Convert.ToString(noviembre) + "','" + System.Convert.ToString(diciembre) + "','" + "Flujo de Caja Real" + "','" + date.Year.ToString() + "','" + System.Environment.MachineName + "')");

                    } 
                    else
                    {
                                        dbc.SimplePlan("INSERT INTO [MLB].[dbo].[FCEstandar]([Id],[UName],[Moneda],[Tipo],[Grupo],[Concepto]" +
                          ",[Enero],[Febrero],[Marzo],[Abril],[Mayo],[Junio],[Julio],[Agosto],[Septiembre] ,[Octubre] ,[Noviembre],[Diciembre],[Clasif],[Date],[MachineName])" +
                          "VALUES('" + dbc.MaxQuerry("FCEstandar") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" +
                          System.Convert.ToString(enero * -1) + "','" + System.Convert.ToString(febrero * -1) + "','" + System.Convert.ToString(marzo * -1) + "','" + System.Convert.ToString(abril * -1) + "','" + System.Convert.ToString(mayo * -1) + "','" + System.Convert.ToString(junio * -1) + "','" + System.Convert.ToString(julio * -1) + "','" +
                                   System.Convert.ToString(agosto * -1) + "','" + System.Convert.ToString(septiembre * -1) + "','" + System.Convert.ToString(octubre * -1) + "','" + System.Convert.ToString(noviembre * -1) + "','" + System.Convert.ToString(diciembre * -1) + "','" + "Flujo de Caja Real" + "','" + date.Year.ToString() + "','" + System.Environment.MachineName + "')");

                    }
                   
                }

            }
           


        }
        private System.DateTime dayCatcherYear(System.DateTime dtt, bool ops)
        {
            System.DateTime fetch = dtt;
            int year = fetch.Year;
            if (ops)
            {

                while (year == fetch.Year)
                {
                    fetch = fetch.AddDays(-1);
                }
                fetch = fetch.AddDays(1);
                // fetch=fetch.ToUniversalTime();
                //return fetch.Month.ToString()+"/"+fetch.Day+"/"+fetch.Year;
                return fetch;

            }
            else
            {
                while (year == fetch.Year)
                {
                    fetch = fetch.AddDays(1);
                }
                fetch = fetch.AddDays(-1);
                return fetch;
            }


        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (meses.SelectedIndex != -1 && meses.SelectedIndex < meses.Items.Count)
            {
                if (!IsInLB(bajos, meses.SelectedItem.ToString()) && !IsInLB(altos, meses.SelectedItem.ToString()))
                altos.Items.Add(meses.SelectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (altos.SelectedIndex != -1 && altos.SelectedIndex<altos.Items.Count)
            {
                altos.Items.RemoveAt(altos.SelectedIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (meses.SelectedIndex != -1 && meses.SelectedIndex < meses.Items.Count)
            {
                if (!IsInLB(altos, meses.SelectedItem.ToString()) && !IsInLB(bajos, meses.SelectedItem.ToString()))
                bajos.Items.Add(meses.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bajos.SelectedIndex != -1 && bajos.SelectedIndex < bajos.Items.Count)
            {
                bajos.Items.RemoveAt(bajos.SelectedIndex);
            }
        }

        private bool IsInLB(ListBox lb,String mes)
        {

            for (int w = 0; w < lb.Items.Count; w++ )
            {
                if (lb.Items[w].ToString()== mes)
                {
                    return true;
                }
            }

            return false;

        }
        private void Readier2()
        {
            String querry = "SELECT Flujo.UName, Moneda, Flujo.Tipo,Flujo.[Concepto],Grupo FROM [MLB].[dbo].[Flujo] INNER JOIN Concepto ON Flujo.Concepto = Concepto.Concepto  Where Flujo.UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dateTimePicker1.Value.ToShortDateString() + "' and '" + dateTimePicker2.Value.ToShortDateString() + "' And Flujo.Tipo = Concepto.Tipo And Concepto.UName = '" + unidad + "' Group By Flujo.UName,Flujo.Moneda, Flujo.Tipo,Concepto.Grupo,Flujo.Concepto Order By Concepto.Grupo";

            dbc.SimplePlan("DELETE FROM FCEstandar WHERE [MachineName]= '" + System.Environment.MachineName + "'");

            if (dbc.ExistQuerry(querry))
            {

                System.Data.DataSet dts = dbc.SelectQuerryFixed(querry);

               
                if (dts.Tables[0].Rows.Count > 0)
                {

                    double avg = 0;
                    double enero = 0;
                    double febrero = 0;
                    double marzo = 0;
                    double abril = 0;
                    double mayo = 0;
                    double junio = 0;
                    double julio = 0;
                    double agosto = 0;
                    double septiembre = 0;
                    double octubre = 0;
                    double noviembre = 0;
                    double diciembre = 0;
                    System.Data.DataSet dts2;

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        avg = 0;
                        enero = 0;
                        febrero = 0;
                        marzo = 0;
                        abril = 0;
                        mayo = 0;
                        junio = 0;
                        julio = 0;
                        agosto = 0;
                        septiembre = 0;
                        octubre = 0;
                        noviembre = 0;
                        diciembre = 0;

                        dts2 = dbc.SelectQuerryFixed("SELECT Avg([Importe]) As PTotal FROM [MLB].[dbo].[Flujo] Where  Concepto = '" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "' and UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dateTimePicker1.Value.ToShortDateString() + "' and '" + dateTimePicker2.Value.ToShortDateString() + "'");


                        if (dts2.Tables[0].Rows.Count > 0)
                            avg = System.Convert.ToDouble(dts2.Tables[0].Rows[0].ItemArray[0].ToString());



                        enero = Randommer(avg, "Enero");
                        febrero = Randommer(avg, "Febrero");
                        marzo = Randommer(avg, "Marzo");
                        abril = Randommer(avg, "Abril");
                        mayo = Randommer(avg, "Mayo");
                        junio = Randommer(avg, "Junio");
                        julio = Randommer(avg, "Julio");
                        agosto = Randommer(avg, "Agosto");
                        septiembre = Randommer(avg, "Septiembre");
                        octubre = Randommer(avg, "Octubre");
                        noviembre = Randommer(avg, "Noviembre");
                        diciembre = Randommer(avg, "Diciembre");

                       
                        if (dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Entradas")
                        {
                            dbc.SimplePlan("INSERT INTO [MLB].[dbo].[FCEstandar]([Id],[UName],[Moneda],[Tipo],[Grupo],[Concepto]" +
              ",[Enero],[Febrero],[Marzo],[Abril],[Mayo],[Junio],[Julio],[Agosto],[Septiembre] ,[Octubre] ,[Noviembre],[Diciembre],[Clasif],[Date],[MachineName])" +
              "VALUES('" + dbc.MaxQuerry("FCEstandar") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" +
              System.Convert.ToString(enero) + "','" + System.Convert.ToString(febrero) + "','" + System.Convert.ToString(marzo) + "','" + System.Convert.ToString(abril) + "','" + System.Convert.ToString(mayo) + "','" + System.Convert.ToString(junio) + "','" + System.Convert.ToString(julio) + "','" +
                       System.Convert.ToString(agosto) + "','" + System.Convert.ToString(septiembre) + "','" + System.Convert.ToString(octubre) + "','" + System.Convert.ToString(noviembre) + "','" + System.Convert.ToString(diciembre) + "','" + "Flujo de Caja Predictivo" + "','" + numericUpDown1.Value.ToString() + "','" + System.Environment.MachineName + "')");

                        }
                        else
                        {
                            dbc.SimplePlan("INSERT INTO [MLB].[dbo].[FCEstandar]([Id],[UName],[Moneda],[Tipo],[Grupo],[Concepto]" +
              ",[Enero],[Febrero],[Marzo],[Abril],[Mayo],[Junio],[Julio],[Agosto],[Septiembre] ,[Octubre] ,[Noviembre],[Diciembre],[Clasif],[Date],[MachineName])" +
              "VALUES('" + dbc.MaxQuerry("FCEstandar") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" +
              System.Convert.ToString(enero * -1) + "','" + System.Convert.ToString(febrero * -1) + "','" + System.Convert.ToString(marzo * -1) + "','" + System.Convert.ToString(abril * -1) + "','" + System.Convert.ToString(mayo * -1) + "','" + System.Convert.ToString(junio * -1) + "','" + System.Convert.ToString(julio * -1) + "','" +
                       System.Convert.ToString(agosto * -1) + "','" + System.Convert.ToString(septiembre * -1) + "','" + System.Convert.ToString(octubre * -1) + "','" + System.Convert.ToString(noviembre * -1) + "','" + System.Convert.ToString(diciembre * -1) + "','" + "Flujo de Caja Predictivo" + "','" + numericUpDown1.Value.ToString() + "','" + System.Environment.MachineName + "')");

                        }

                    }

                }

            }
            else
                System.Windows.Forms.MessageBox.Show(this, "No existen registros de Flujo de Caja para el Período de " + dateTimePicker1.Value.ToShortDateString() + " a " + dateTimePicker2.Value.ToShortDateString() + ".");

        }
        private double Randommer(double avg, String mes)
        {
            double percent = (avg * System.Convert.ToDouble(numericUpDown2.Value)) / 100;
           
            if (IsInLB(altos,mes))
            {
               // double percent = (avg*System.Convert.ToDouble(numericUpDown2.Value))/100;
               // Random robj = new Random(1212);
                return robj.Next(System.Convert.ToInt32(avg),System.Convert.ToInt32(avg+percent))+System.Math.Round(robj.NextDouble(),2);
            }

            if (IsInLB(bajos, mes))
            {
               
               // Random robj = new Random(1212);
                return robj.Next(System.Convert.ToInt32(avg - percent), System.Convert.ToInt32(avg)) + System.Math.Round(robj.NextDouble(), 2); 
            }

            return robj.Next(System.Convert.ToInt32(avg - (percent / 2)), System.Convert.ToInt32(avg + (percent / 2))) + System.Math.Round(robj.NextDouble(), 2); 
           

        }
        private void button1_Click(object sender, EventArgs e)
        {

            Readier2();
            // TODO: This line of code loads data into the 'MLBFCEstandar.FCEstandar' table. You can move, or remove it, as needed.
            this.FCEstandarTableAdapter.Connection = dbc.GetConnection();
            this.FCEstandarTableAdapter.Fill(this.MLBFCEstandar.FCEstandar);
            reportViewer1.RefreshReport();
        }

        private void FCReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbc.SimplePlan("DELETE FROM FCEstandar WHERE [MachineName]= '" + System.Environment.MachineName + "'");

        }
    }
}
