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
    public partial class Estadisticas : Form
    {
        private String unidad, moneda;
        private DateTime date;
        private MLB.DBControl dbc;
        private String querry;
        public Estadisticas(String unidad, DateTime date, String moneda)
        {
            InitializeComponent();
            this.unidad = unidad;
            this.date = date;
            this.moneda = moneda;
            dbc = new MLB.DBControl();
            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                groupBox1.ForeColor = System.Drawing.SystemColors.Window;
                groupBox2.ForeColor = System.Drawing.SystemColors.Window;
            }
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'MLBResIng.PResIng' table. You can move, or remove it, as needed.
         //   this.PResIngTableAdapter.Fill(this.MLBResIng.PResIng);
            // TODO: This line of code loads data into the 'MLBSubMayor.PSubMayor' table. You can move, or remove it, as needed.
            //this.PSubMayorTableAdapter.Fill(this.MLBSubMayor.PSubMayor);
            // TODO: This line of code loads data into the 'MLBResIng.PResIng' table. You can move, or remove it, as needed.

          //  MLB.DBControl dbc = new MLB.DBControl();
            querry = "SELECT   Id, Day, Month, InHoy, InHastaHoy, CostHoy, CostHastaHoy, IngAcum, CostAcum, UName, RDate, Cuenta, Moneda,UName FROM  ResIng" +
               " Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";

            if (comboBox2.Text == "Entradas-Salidas")
                querry = "SELECT [Id],[Day],[Month],[SaldoInicial] ,[Entrada] ,[EntInt],[Salida],[SalInt],[Traslado] ,[SaldoFinal] ,[CompRamal],[RDate],[UName]as MachineName,[UName],[RDate] as Date ,[Cuenta]" +
                  ",[Moneda] FROM [MLB].[dbo].[SubMayor] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";
           
            if (comboBox2.Text == "Flujo de Caja")
                querry = "SELECT [Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial] FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By Date";
           // bool crap = this.reportViewer1.Created;
            //Readier();
           Reporter();


          // this.reportViewer2.RefreshReport();
           //this.reportViewer3.RefreshReport();
          // this.reportViewer4.RefreshReport();
        }

        private void Reporter()
        {

            comboBox1.Enabled = true;
            if (comboBox2.Text == "Entradas-Salidas")
            {
                //InitializeComponent();
                //comboBox2.Text = "Entradas-Salidas";

                FirstPrepare2(unidad, date);


               // Microsoft.Reporting.WinForms.ReportViewer reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();

              //  this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
               // this.reportViewer2.Location = new System.Drawing.Point(203, 3);
               // this.reportViewer2.Size = new System.Drawing.Size(615, 337);

                if (!this.reportViewer2.Visible)
                {
                    this.reportViewer2.Visible = true;
                    this.reportViewer1.Visible = false;
                    this.reportViewer3.Visible = false;
                    //this.reportViewer4.Visible = false;

                   // this.groupBox1.Controls.Add(this.reportViewer1);
                  //  this.groupBox1.Controls.Add(this.reportViewer3);
                   // this.groupBox1.Controls.Add(this.reportViewer4);
                  //  this.tableLayoutPanel1.Controls.Add(this.reportViewer2, 1, 0);
                
                }
               
              

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource4.Name = "MLBSubMayor_PSubMayor";
                reportDataSource4.Value = this.PSubMayorBindingSource;
                reportViewer2.LocalReport.DataSources.Add(reportDataSource4);
                reportViewer2.LocalReport.ReportEmbeddedResource = "MLB.EstEntSal.rdlc";
              

               
                this.PSubMayorTableAdapter.Connection = dbc.GetConnection();
                this.PSubMayorTableAdapter.Fill2(this.MLBSubMayor.PSubMayor);

              
                this.reportViewer2.RefreshReport();
                
            }

            if (comboBox2.Text == "Flujo de Caja")
            {
                //InitializeComponent();
                //comboBox2.Text = "Entradas-Salidas";

                FirstPrepare3(unidad, date);


                // Microsoft.Reporting.WinForms.ReportViewer reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();

               // this.reportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
              //  this.reportViewer3.Location = new System.Drawing.Point(203, 3);
              //  this.reportViewer3.Size = new System.Drawing.Size(615, 337);

                if (!this.reportViewer3.Visible)
                {
                    this.reportViewer3.Visible = true;
                    this.reportViewer1.Visible = false;
                    this.reportViewer2.Visible = false;
                   // this.reportViewer4.Visible = false;

                  ///  this.groupBox1.Controls.Add(this.reportViewer1);
                 //   this.groupBox1.Controls.Add(this.reportViewer2);
                    //this.groupBox1.Controls.Add(this.reportViewer2);
                  //  this.tableLayoutPanel1.Controls.Add(this.reportViewer3, 1, 0);

                }



                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource4.Name = "MLBPFlujo_PFlujo";
                reportDataSource4.Value = this.PSubMayorBindingSource;
                reportViewer3.LocalReport.DataSources.Add(reportDataSource4);
                reportViewer3.LocalReport.ReportEmbeddedResource = "MLB.EstFlujoCaja.rdlc";



                this.PFlujoTableAdapter.Connection = dbc.GetConnection();
                this.PFlujoTableAdapter.Fill(this.MLBPFlujo.PFlujo);


                this.reportViewer3.RefreshReport();

            }
            //if (comboBox2.Text == "Flujo Estandar")
            //{
            //    comboBox1.Enabled = false;
            //    //InitializeComponent();
            //    //comboBox2.Text = "Entradas-Salidas";

            //    FirstPrepare3(unidad, date);


            //    // Microsoft.Reporting.WinForms.ReportViewer reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();

            //    this.reportViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            //    this.reportViewer4.Location = new System.Drawing.Point(203, 3);
            //    this.reportViewer4.Size = new System.Drawing.Size(615, 337);

            //    if (!this.reportViewer4.Visible)
            //    {
            //        this.reportViewer4.Visible = true;
            //        this.reportViewer1.Visible = false;
            //        this.reportViewer2.Visible = false;
            //        this.reportViewer3.Visible = false;

            //        this.groupBox1.Controls.Add(this.reportViewer1);
            //        this.groupBox1.Controls.Add(this.reportViewer2);
            //        this.groupBox1.Controls.Add(this.reportViewer3);
            //        this.tableLayoutPanel1.Controls.Add(this.reportViewer4, 1, 0);

            //    }



            //    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            //    reportDataSource4.Name = "MLBPFlujo_PFlujo";
            //    reportDataSource4.Value = this.PSubMayorBindingSource;
            //    reportViewer4.LocalReport.DataSources.Add(reportDataSource4);
            //   // reportViewer4.LocalReport.ReportEmbeddedResource = "MLB.FlujoEstandar.rdlc";



            //    this.PFlujoTableAdapter.Connection = dbc.GetConnection();
            //    this.PFlujoTableAdapter.Fill2(this.MLBPFlujo.PFlujo);


            //    this.reportViewer4.RefreshReport();

            //}

            if (comboBox2.Text == "Ingresos-Costos")
            {

                //InitializeComponent();
                //comboBox2.Text = "Ingresos-Costos";

                FirstPrepare(unidad, date);

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource2.Name = "MLBResIng_PResIng";
                reportDataSource2.Value = this.PSubMayorBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.EstCosIng.rdlc";


                

                if (!this.reportViewer1.Visible)
                {
                   // this.reportViewer4.Visible = false;
                    this.reportViewer2.Visible = false;
                    this.reportViewer3.Visible = false;
                    this.reportViewer1.Visible = true;
              //  this.groupBox1.Controls.Add(this.reportViewer2);
             //   this.groupBox1.Controls.Add(this.reportViewer3);
               // this.groupBox1.Controls.Add(this.reportViewer4);
             //   this.tableLayoutPanel1.Controls.Add(this.reportViewer1, 1, 0);
                }
                this.PResIngTableAdapter.Connection = dbc.GetConnection();
                this.PResIngTableAdapter.Fill2(this.MLBResIng.PResIng);

                
                this.reportViewer1.RefreshReport();
            }


           // this.reportViewer1.LocalReport.Refresh();
          
        }
        private void FirstPrepare(String unidad, DateTime date)
        {
          //  dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dbc.SimplePlan("DELETE FROM PResIng WHERE [MachineName]= '" + System.Environment.MachineName + "'");
           
            //for (int w = 0; w < ResIngBase.RowCount; w++)
            //{

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PResIng]([Id],[Day],[Month],[InHoy]" +
             ",[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[UName],[Date],[Cuenta],[Moneda],[MachineName]) " + querry);

                dbc.SimplePlan(" UPDATE  [MLB].[dbo].[PResIng] SET [MachineName]= '" + System.Environment.MachineName + "' WHERE  [MachineName]= UName");
              
            //}
        }

        private void FirstPrepare2(String unidad, DateTime date)
        {
           // dbc.SimplePlan("DELETE FROM PResIng WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");

            //for (int w = 0; w < ResIngBase.RowCount; w++)
            //{

            dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PSubMayor]([Id],[Day],[Month],[SaldoInicial],[Entrada],[EntInt]"+
            ",[Salida],[SalInt],[Traslado] ,[SaldoFinal],[CompRamal],[RDate],[MachineName] ,[UName] ,[Date] ,[Cuenta],[Moneda])" + querry);

            

            dbc.SimplePlan(" UPDATE  [MLB].[dbo].[PSubMayor] SET [MachineName]= '" + System.Environment.MachineName + "' WHERE  [MachineName]= UName");

            //}
        }
        private void FirstPrepare3(String unidad, DateTime date)
        {
           // dbc.SimplePlan("DELETE FROM PResIng WHERE [MachineName]= '" + System.Environment.MachineName + "'");
           // dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dbc.SimplePlan("DELETE FROM PFlujo WHERE [MachineName]= '" + System.Environment.MachineName + "'");

            //for (int w = 0; w < ResIngBase.RowCount; w++)
            //{
            
            dbc.SimplePlan("INSERT INTO [MLB].[dbo].[PFlujo]([Id],[IDate],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial])" + querry);

             System.Data.DataSet dst = new System.Data.DataSet();
             if (comboBox1.Text == "Anual")
            dst = dbc.SelectQuerryFixed("SELECT Sum([Importe])FROM [MLB].[dbo].[Flujo]  Where Tipo = 'Entradas' And UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "'");
            if (comboBox1.Text == "Mensual")
             dst = dbc.SelectQuerryFixed("SELECT Sum([Importe])FROM [MLB].[dbo].[Flujo]  Where Tipo = 'Entradas' And UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "'");

            double ingreso = 0;

            if (dst.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                ingreso = System.Convert.ToDouble(dst.Tables[0].Rows[0].ItemArray[0].ToString());
            
            if (comboBox1.Text == "Anual")
             dst = dbc.SelectQuerryFixed("SELECT Sum([Importe])FROM [MLB].[dbo].[Flujo]  Where Tipo = 'Salidas' And UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "'");
            if (comboBox1.Text == "Mensual")
                dst = dbc.SelectQuerryFixed("SELECT Sum([Importe])FROM [MLB].[dbo].[Flujo]  Where Tipo = 'Salidas' And UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "'");

            double gasto = 0;
            if (dst.Tables[0].Rows[0].ItemArray[0].ToString() != "")
            gasto = System.Convert.ToDouble(dst.Tables[0].Rows[0].ItemArray[0].ToString());
         
            if (comboBox1.Text == "Anual")
                     dst = dbc.SelectQuerryFixed("SELECT Top 1 [IInicial]FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "'");
            if (comboBox1.Text == "Mensual")
                     dst = dbc.SelectQuerryFixed("SELECT Top 1 [IInicial]FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "'");

            double inicio = 0;
            if (dst.Tables[0].Rows.Count>0)
            inicio = System.Convert.ToDouble(dst.Tables[0].Rows[0].ItemArray[0].ToString());

            dbc.SimplePlan(" UPDATE  [MLB].[dbo].[PFlujo] SET [MachineName]= '" + System.Environment.MachineName + "' WHERE  [MachineName] IS NULL");

            dbc.SimplePlan(" UPDATE  [MLB].[dbo].[PFlujo] SET [IFinal]= '" + System.Convert.ToString(inicio+(ingreso-gasto))+ "', [Balance]='"+System.Convert.ToString(ingreso-gasto)+"' WHERE  [MachineName]= '"+System.Environment.MachineName+"'");

            //}
        }
          private System.DateTime dayCatcherYear(System.DateTime dtt, bool ops)
				   {
					   System.DateTime fetch=dtt;
					   int year=fetch.Year;
					   if (ops)
					   {

						   while(year==fetch.Year)
						   {
							   fetch=fetch.AddDays(-1);
						   }
						   fetch=fetch.AddDays(1);
						   // fetch=fetch.ToUniversalTime();
						   //return fetch.Month.ToString()+"/"+fetch.Day+"/"+fetch.Year;
						   return fetch;
					   
					   }
					   else
					   {
						   while(year==fetch.Year)
						   {
							   fetch=fetch.AddDays(1);
						   }
						   fetch=fetch.AddDays(-1);
						   return fetch;
					   }


				   }
          private System.DateTime dayCatcherMonth(System.DateTime dtt, bool ops)
          {
              System.DateTime fetch = dtt;
              int month = fetch.Month;
              if (ops)
              {

                  while (month == fetch.Month)
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
                  while (month == fetch.Month)
                  {
                      fetch = fetch.AddDays(1);
                  }
                  fetch = fetch.AddDays(-1);
                  return fetch;
              }


          }

          private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
          {

          }

          private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
          {
           

          }
         

          private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
          {
              if (comboBox1.Text == "Anual")
              {
                  querry = "SELECT   Id, Day, Month, InHoy, InHastaHoy, CostHoy, CostHastaHoy, IngAcum, CostAcum, UName, RDate, Cuenta, Moneda,UName FROM  ResIng" +
               " Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By RDate";


                  if (comboBox2.Text == "Entradas-Salidas")
                      querry = "SELECT [Id],[Day],[Month],[SaldoInicial] ,[Entrada] ,[EntInt],[Salida],[SalInt],[Traslado] ,[SaldoFinal] ,[CompRamal],[RDate],[UName]as MachineName,[UName],[RDate] as Date ,[Cuenta]" +
                        ",[Moneda] FROM [MLB].[dbo].[SubMayor] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Flujo de Caja")
                      querry = "SELECT [Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial] FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By Date";
                 
                  Reporter();
              }
              if (comboBox1.Text == "Mensual")
              {
                  querry = "SELECT   Id, Day, Month, InHoy, InHastaHoy, CostHoy, CostHastaHoy, IngAcum, CostAcum, UName, RDate, Cuenta, Moneda,UName FROM  ResIng" +
               " Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Entradas-Salidas")
                      querry = "SELECT [Id],[Day],[Month],[SaldoInicial] ,[Entrada] ,[EntInt],[Salida],[SalInt],[Traslado] ,[SaldoFinal] ,[CompRamal],[RDate],[UName]as MachineName,[UName],[RDate] as Date ,[Cuenta]" +
                        ",[Moneda] FROM [MLB].[dbo].[SubMayor] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Flujo de Caja")
                      querry = "SELECT [Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial] FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By Date";

                //  Readier();
                  Reporter();
              }
          }

          private void reportViewer1_Load(object sender, EventArgs e)
          {

          }

          private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
          {
              if (comboBox1.Text == "Anual")
              {
                  querry = "SELECT   Id, Day, Month, InHoy, InHastaHoy, CostHoy, CostHastaHoy, IngAcum, CostAcum, UName, RDate, Cuenta, Moneda,UName FROM  ResIng" +
               " Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By RDate";


                  if (comboBox2.Text == "Entradas-Salidas")
                      querry = "SELECT [Id],[Day],[Month],[SaldoInicial] ,[Entrada] ,[EntInt],[Salida],[SalInt],[Traslado] ,[SaldoFinal] ,[CompRamal],[RDate],[UName]as MachineName,[UName],[RDate] as Date ,[Cuenta]" +
                        ",[Moneda] FROM [MLB].[dbo].[SubMayor] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Flujo de Caja")
                      querry = "SELECT [Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial] FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherYear(date, true) + "' and '" + dayCatcherYear(date, false) + "' Order By Date";
                  Reporter();
              }
              if (comboBox1.Text == "Mensual")
              {
                  querry = "SELECT   Id, Day, Month, InHoy, InHastaHoy, CostHoy, CostHastaHoy, IngAcum, CostAcum, UName, RDate, Cuenta, Moneda,UName FROM  ResIng" +
               " Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Entradas-Salidas")
                      querry = "SELECT [Id],[Day],[Month],[SaldoInicial] ,[Entrada] ,[EntInt],[Salida],[SalInt],[Traslado] ,[SaldoFinal] ,[CompRamal],[RDate],[UName]as MachineName,[UName],[RDate] as Date ,[Cuenta]" +
                        ",[Moneda] FROM [MLB].[dbo].[SubMayor] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and RDate Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By RDate";

                  if (comboBox2.Text == "Flujo de Caja")
                      querry = "SELECT [Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial] FROM [MLB].[dbo].[Flujo] Where UName = '" + unidad + "' and Moneda = '" + moneda + "' and Date Between '" + dayCatcherMonth(date, true) + "' and '" + dayCatcherMonth(date, false) + "' Order By Date";

                  //  Readier();
                  Reporter();
              }
          }

          private void Estadisticas_FormClosing(object sender, FormClosingEventArgs e)
          {
              dbc.SimplePlan("DELETE FROM PResIng WHERE [MachineName]= '" + System.Environment.MachineName + "'");
              dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");
              dbc.SimplePlan("DELETE FROM PFlujo WHERE [MachineName]= '" + System.Environment.MachineName + "'");

               this.reportViewer1.Dispose();
               this.reportViewer2.Dispose();
               this.reportViewer3.Dispose();
          }

          private void reportViewer1_Load_1(object sender, EventArgs e)
          {

          }
    }
}
