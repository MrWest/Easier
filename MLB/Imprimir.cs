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
    public partial class Imprimir : Form
    {
        public String table { get; set; }
        public Imprimir()
        {
            InitializeComponent();
           
        }

        private void Imprimir_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'MLBVSalida.PValeSalida' table. You can move, or remove it, as needed.
            //this.PValeSalidaTableAdapter.Fill(this.MLBVSalida.PValeSalida);
            //// TODO: This line of code loads data into the 'MLBFCosto.PFichaCosto' table. You can move, or remove it, as needed.
            //this.PFichaCostoTableAdapter.Fill(this.MLBFCosto.PFichaCosto);
            //// TODO: This line of code loads data into the 'MLBResIng.PResIng' table. You can move, or remove it, as needed.
            //this.PResIngTableAdapter.Fill(this.MLBResIng.PResIng);
            //// TODO: This line of code loads data into the 'MLBSubMayor.PSubMayor' table. You can move, or remove it, as needed.
            //this.PSubMayorTableAdapter.Fill(this.MLBSubMayor.PSubMayor);
            //// TODO: This line of code loads data into the 'MLBIPVs.PIPV' table. You can move, or remove it, as needed.
            //this.PIPVTableAdapter.Fill(this.MLBIPVs.PIPV);
            //// TODO: This line of code loads data into the 'MLBRamal.PRamal' table. You can move, or remove it, as needed.
            //this.PRamalTableAdapter.Fill(this.MLBRamal.PRamal);


            // TODO: This line of code loads data into the 'MLBFichaCosto.PFichaCosto' table. You can move, or remove it, as needed.

            if (table == "SubMayor")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBSubMayor_PSubMayor";
                reportDataSource1.Value = this.PSubMayorBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.SubMayor.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PSubMayorTableAdapter.Connection = dbc.GetConnection();
                this.PSubMayorTableAdapter.Fill(this.MLBSubMayor.PSubMayor);

                // this.reportViewer1.LocalReport.ReportPath = "SubMayor.rdlc";

            }

            // TODO: This line of code loads data into the 'MLBPRamal20.PRamal' table. You can move, or remove it, as needed.
            if (table == "Ramal20")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBRamal_PRamal";
                reportDataSource1.Value = this.PRamalBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource2.Name = "Defensa";
                reportDataSource2.Value = this.BSDefensa;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource3.Name = "Resumen";
                reportDataSource3.Value = this.BSResumen;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);

                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.Ramal20.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PRamalTableAdapter.Connection = dbc.GetConnection();
                this.PRamalTableAdapter.Fill(this.MLBRamal.PRamal);
                this.PRamalTableAdapter2.Fill(this.defensa.PRamal);
                this.ResumenTableAdapter.Fill(this.resumen.PRamal);


                //this.reportViewer1.LocalReport.ReportPath = "Ramal20.rdlc";
            }

            // TODO: This line of code loads data into the 'MLBPIPV.PIPV' table. You can move, or remove it, as needed.
            if (table == "IPV")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBIPVs_PIPV";
                reportDataSource1.Value = this.PIPVBindingSource;
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.IPV_Report.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PIPVTableAdapter.Connection = dbc.GetConnection();
                this.PIPVTableAdapter.Fill(this.MLBIPVs.PIPV);

                //this.reportViewer1.LocalReport.ReportPath = "IPV_Report.rdlc";
            }

            // TODO: This line of code loads data into the 'MLBPResIng.PResIng' table. You can move, or remove it, as needed.
            if (table == "ResIng")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBResIng_PResIng";
                reportDataSource1.Value = this.PResIngBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.ResumenIngresos.rdlc";


                MLB.DBControl dbc = new MLB.DBControl();
                this.PResIngTableAdapter.Connection = dbc.GetConnection();
                this.PResIngTableAdapter.Fill(this.MLBResIng.PResIng);
                //this.reportViewer1.LocalReport.ReportPath = "IngresosGastos.rdlc";
            }

            if (table == "FichaCosto")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBFCosto_PFichaCosto";
                reportDataSource1.Value = this.PFichaCostoBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.FichaCosto.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PFichaCostoTableAdapter.Connection = dbc.GetConnection();
                this.PFichaCostoTableAdapter.Fill(this.MLBFCosto.PFichaCosto);
            }
            // TODO: This line of code loads data into the 'MLBValeSalida.PValeSalida' table. You can move, or remove it, as needed.

            if (table == "ValeSalida")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBVSalida_PValeSalida";
                reportDataSource1.Value = this.PValeSalidaBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.ValeSalida.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PValeSalidaTableAdapter.Connection = dbc.GetConnection();
                this.PValeSalidaTableAdapter.Fill(this.MLBVSalida.PValeSalida);
            }


            if (table == "Descomponer")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBDescomponer_PDescomponer";
                reportDataSource1.Value = this.PDescomponerBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.Descomponer.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PDescomponerTableAdapter.Connection = dbc.GetConnection();
                this.PDescomponerTableAdapter.Fill(this.MLBDescomponer.PDescomponer);
            }
            if (table == "Flujo")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBPFlujo_PFlujo";
                reportDataSource1.Value = this.PFlujoBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.FlujoCaja.rdlc";

                MLB.DBControl dbc = new MLB.DBControl();
                this.PFlujoTableAdapter.Connection = dbc.GetConnection();
                this.PFlujoTableAdapter.Fill(this.MLBPFlujo.PFlujo);
            }
            // TODO: This line of code loads data into the 'MLBPSubMayor.PSubMayor' table. You can move, or remove it, as needed.
          
            if (table == "ALL")
            {
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource1.Name = "MLBRamal_PRamal";
                    reportDataSource1.Value = this.PRamalBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource2.Name = "Defensa";
                    reportDataSource2.Value = this.BSDefensa;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);

                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource3.Name = "Resumen";
                    reportDataSource3.Value = this.BSResumen;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);

                   

                    MLB.DBControl dbc = new MLB.DBControl();
                    this.PRamalTableAdapter.Connection = dbc.GetConnection();
                    this.PRamalTableAdapter.Fill(this.MLBRamal.PRamal);
                    this.PRamalTableAdapter2.Fill(this.defensa.PRamal);
                    this.ResumenTableAdapter.Fill(this.resumen.PRamal);


                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource4.Name = "MLBIPVs_PIPV";
                    reportDataSource4.Value = this.PIPVBindingSource;
                   // this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.IPV_Report.rdlc";

                   // MLB.DBControl dbc = new MLB.DBControl();
                    this.PIPVTableAdapter.Connection = dbc.GetConnection();
                    this.PIPVTableAdapter.Fill(this.MLBIPVs.PIPV);

                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource5.Name = "MLBSubMayor_PSubMayor";
                    reportDataSource5.Value = this.PSubMayorBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
                    this.PSubMayorTableAdapter.Connection = dbc.GetConnection();
                    this.PSubMayorTableAdapter.Fill(this.MLBSubMayor.PSubMayor);

            

                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource7.Name = "MLBVSalida_PValeSalida";
                    reportDataSource7.Value = this.PValeSalidaBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
                   // this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.ValeSalida.rdlc";

                    //MLB.DBControl dbc = new MLB.DBControl();
                    this.PValeSalidaTableAdapter.Connection = dbc.GetConnection();
                    this.PValeSalidaTableAdapter.Fill(this.MLBVSalida.PValeSalida);

                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
                    reportDataSource10.Name = "MLBFCosto_PFichaCosto";
                    reportDataSource10.Value = this.PFichaCostoBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
                    //this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.FichaCosto.rdlc";

                    //MLB.DBControl dbc = new MLB.DBControl();
                    this.PFichaCostoTableAdapter.Connection = dbc.GetConnection();
                    this.PFichaCostoTableAdapter.Fill(this.MLBFCosto.PFichaCosto);
            


                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.FullReport.rdlc";

                    //this.reportViewer1.LocalReport.ReportPath = "Ramal20.rdlc";
                

            }

            if (table == "FullRamal")
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                reportDataSource1.Name = "MLBRamal_PRamal";
                reportDataSource1.Value = this.PRamalBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                MLB.DBControl dbc = new MLB.DBControl();
                this.PRamalTableAdapter.Connection = dbc.GetConnection();
                this.PRamalTableAdapter.Fill(this.MLBRamal.PRamal);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.Existencias.rdlc";
                

            }
            

            this.reportViewer1.RefreshReport();
        }

        private void Imprimir_FormClosing(object sender, FormClosingEventArgs e)
        {
            MLB.DBControl dbc = new MLB.DBControl();
            if (table == "Ramal20")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PRamal]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "FullRamal")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PRamal]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "SubMayor")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PSubMayor]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "ValeSalida")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PValeSalida]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "ResIng")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PResIng]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "FichaCosto")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PFichaCosto]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "IPV")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PIPV]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "Descomponer")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PDescomponer]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }

            if (table == "Flujo")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PFlujo]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
            if (table == "ALL")
            {
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PRamal]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PIPV]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PSubMayor]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PFichaCosto]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
                dbc.SimplePlan("DELETE FROM [MLB].[dbo].[PValeSalida]" + " WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            }
        }
    }
}
