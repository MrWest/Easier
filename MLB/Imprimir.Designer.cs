namespace MLB
{
    partial class Imprimir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Imprimir));
            this.PIPVBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBIPVs = new MLB.MLBIPVs();
            this.PFlujoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBPFlujo = new MLB.MLBPFlujo();
            this.PDescomponerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBDescomponer = new MLB.MLBDescomponer();
            this.PSubMayorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBSubMayor = new MLB.MLBSubMayor();
            this.PRamalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBRamal = new MLB.MLBRamal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PRamalTableAdapter = new MLB.MLBRamalTableAdapters.PRamalTableAdapter();
            this.MLBFCosto = new MLB.MLBFCosto();
            this.PFichaCostoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PFichaCostoTableAdapter = new MLB.MLBFCostoTableAdapters.PFichaCostoTableAdapter();
            this.MLBResIng = new MLB.MLBResIng();
            this.PResIngBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PResIngTableAdapter = new MLB.MLBResIngTableAdapters.PResIngTableAdapter();
            this.PSubMayorTableAdapter = new MLB.MLBSubMayorTableAdapters.PSubMayorTableAdapter();
            this.MLBVSalida = new MLB.MLBVSalida();
            this.PValeSalidaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PValeSalidaTableAdapter = new MLB.MLBVSalidaTableAdapters.PValeSalidaTableAdapter();
            this.PIPVTableAdapter = new MLB.MLBIPVsTableAdapters.PIPVTableAdapter();
            this.defensa = new MLB.Defensa();
            this.resumen = new MLB.Resumen();
            this.PRamalTableAdapter2 = new MLB.DefensaTableAdapters.PRamalTableAdapter();
            this.ResumenTableAdapter = new MLB.ResumenTableAdapters.ResumenTableAdapter();
            this.BSDefensa = new System.Windows.Forms.BindingSource(this.components);
            this.BSResumen = new System.Windows.Forms.BindingSource(this.components);
            this.PDescomponerTableAdapter = new MLB.MLBDescomponerTableAdapters.PDescomponerTableAdapter();
            this.PFlujoTableAdapter = new MLB.MLBPFlujoTableAdapters.PFlujoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PIPVBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBIPVs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFlujoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBPFlujo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PDescomponerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBDescomponer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSubMayorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBSubMayor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRamalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBRamal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBFCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFichaCostoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBResIng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PResIngBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBVSalida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PValeSalidaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSDefensa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSResumen)).BeginInit();
            this.SuspendLayout();
            // 
            // PIPVBindingSource
            // 
            this.PIPVBindingSource.DataMember = "PIPV";
            this.PIPVBindingSource.DataSource = this.MLBIPVs;
            // 
            // MLBIPVs
            // 
            this.MLBIPVs.DataSetName = "MLBIPVs";
            this.MLBIPVs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PFlujoBindingSource
            // 
            this.PFlujoBindingSource.DataMember = "PFlujo";
            this.PFlujoBindingSource.DataSource = this.MLBPFlujo;
            // 
            // MLBPFlujo
            // 
            this.MLBPFlujo.DataSetName = "MLBPFlujo";
            this.MLBPFlujo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PDescomponerBindingSource
            // 
            this.PDescomponerBindingSource.DataMember = "PDescomponer";
            this.PDescomponerBindingSource.DataSource = this.MLBDescomponer;
            // 
            // MLBDescomponer
            // 
            this.MLBDescomponer.DataSetName = "MLBDescomponer";
            this.MLBDescomponer.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PSubMayorBindingSource
            // 
            this.PSubMayorBindingSource.DataMember = "PSubMayor";
            this.PSubMayorBindingSource.DataSource = this.MLBSubMayor;
            // 
            // MLBSubMayor
            // 
            this.MLBSubMayor.DataSetName = "MLBSubMayor";
            this.MLBSubMayor.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PRamalBindingSource
            // 
            this.PRamalBindingSource.DataMember = "PRamal";
            this.PRamalBindingSource.DataSource = this.MLBRamal;
            // 
            // MLBRamal
            // 
            this.MLBRamal.DataSetName = "MLBRamal";
            this.MLBRamal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MLBIPVs_PIPV";
            reportDataSource1.Value = this.PIPVBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.IPV_Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(512, 332);
            this.reportViewer1.TabIndex = 0;
            // 
            // PRamalTableAdapter
            // 
            this.PRamalTableAdapter.ClearBeforeFill = true;
            // 
            // MLBFCosto
            // 
            this.MLBFCosto.DataSetName = "MLBFCosto";
            this.MLBFCosto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PFichaCostoBindingSource
            // 
            this.PFichaCostoBindingSource.DataMember = "PFichaCosto";
            this.PFichaCostoBindingSource.DataSource = this.MLBFCosto;
            // 
            // PFichaCostoTableAdapter
            // 
            this.PFichaCostoTableAdapter.ClearBeforeFill = true;
            // 
            // MLBResIng
            // 
            this.MLBResIng.DataSetName = "MLBResIng";
            this.MLBResIng.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PResIngBindingSource
            // 
            this.PResIngBindingSource.DataMember = "PResIng";
            this.PResIngBindingSource.DataSource = this.MLBResIng;
            // 
            // PResIngTableAdapter
            // 
            this.PResIngTableAdapter.ClearBeforeFill = true;
            // 
            // PSubMayorTableAdapter
            // 
            this.PSubMayorTableAdapter.ClearBeforeFill = true;
            // 
            // MLBVSalida
            // 
            this.MLBVSalida.DataSetName = "MLBVSalida";
            this.MLBVSalida.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PValeSalidaBindingSource
            // 
            this.PValeSalidaBindingSource.DataMember = "PValeSalida";
            this.PValeSalidaBindingSource.DataSource = this.MLBVSalida;
            // 
            // PValeSalidaTableAdapter
            // 
            this.PValeSalidaTableAdapter.ClearBeforeFill = true;
            // 
            // PIPVTableAdapter
            // 
            this.PIPVTableAdapter.ClearBeforeFill = true;
            // 
            // defensa
            // 
            this.defensa.DataSetName = "Defensa";
            this.defensa.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // resumen
            // 
            this.resumen.DataSetName = "Resumen";
            this.resumen.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PRamalTableAdapter2
            // 
            this.PRamalTableAdapter2.ClearBeforeFill = true;
            // 
            // ResumenTableAdapter
            // 
            this.ResumenTableAdapter.ClearBeforeFill = true;
            // 
            // BSDefensa
            // 
            this.BSDefensa.DataMember = "PRamal";
            this.BSDefensa.DataSource = this.defensa;
            // 
            // BSResumen
            // 
            this.BSResumen.DataMember = "PRamal";
            this.BSResumen.DataSource = this.resumen;
            // 
            // PDescomponerTableAdapter
            // 
            this.PDescomponerTableAdapter.ClearBeforeFill = true;
            // 
            // PFlujoTableAdapter
            // 
            this.PFlujoTableAdapter.ClearBeforeFill = true;
            // 
            // Imprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 332);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Imprimir";
            this.Text = "Imprimir";
            this.Load += new System.EventHandler(this.Imprimir_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Imprimir_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PIPVBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBIPVs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFlujoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBPFlujo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PDescomponerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBDescomponer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSubMayorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBSubMayor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRamalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBRamal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBFCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFichaCostoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBResIng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PResIngBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBVSalida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PValeSalidaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defensa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSDefensa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BSResumen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PRamalBindingSource;
        private MLBRamal MLBRamal;
        private Defensa defensa;
        private Resumen resumen;
        private MLB.MLBRamalTableAdapters.PRamalTableAdapter PRamalTableAdapter;
        private MLB.DefensaTableAdapters.PRamalTableAdapter PRamalTableAdapter2;
        private MLB.ResumenTableAdapters.ResumenTableAdapter ResumenTableAdapter;
        private System.Windows.Forms.BindingSource PFichaCostoBindingSource;
        private MLBFCosto MLBFCosto;
        private MLB.MLBFCostoTableAdapters.PFichaCostoTableAdapter PFichaCostoTableAdapter;
        private System.Windows.Forms.BindingSource PIPVBindingSource;
        private MLBIPVs MLBIPVs;
        private MLB.MLBIPVsTableAdapters.PIPVTableAdapter PIPVTableAdapter;
        private System.Windows.Forms.BindingSource PResIngBindingSource;
        private MLBResIng MLBResIng;
        private MLB.MLBResIngTableAdapters.PResIngTableAdapter PResIngTableAdapter;
        private System.Windows.Forms.BindingSource PSubMayorBindingSource;
        private MLBSubMayor MLBSubMayor;
        private MLB.MLBSubMayorTableAdapters.PSubMayorTableAdapter PSubMayorTableAdapter;
        private System.Windows.Forms.BindingSource PValeSalidaBindingSource;
        private MLBVSalida MLBVSalida;
        private MLB.MLBVSalidaTableAdapters.PValeSalidaTableAdapter PValeSalidaTableAdapter;
        private System.Windows.Forms.BindingSource BSDefensa;
        private System.Windows.Forms.BindingSource BSResumen;
        private System.Windows.Forms.BindingSource PDescomponerBindingSource;
        private MLBDescomponer MLBDescomponer;
        private MLB.MLBDescomponerTableAdapters.PDescomponerTableAdapter PDescomponerTableAdapter;
        private System.Windows.Forms.BindingSource PFlujoBindingSource;
        private MLBPFlujo MLBPFlujo;
        private MLB.MLBPFlujoTableAdapters.PFlujoTableAdapter PFlujoTableAdapter;


    }
}