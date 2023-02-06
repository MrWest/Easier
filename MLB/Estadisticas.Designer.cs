namespace MLB
{
    partial class Estadisticas
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Estadisticas));
            this.PResIngBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBResIng = new MLB.MLBResIng();
            this.PFlujoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBPFlujo = new MLB.MLBPFlujo();
            this.PSubMayorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MLBSubMayor = new MLB.MLBSubMayor();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.PResIngTableAdapter = new MLB.MLBResIngTableAdapters.PResIngTableAdapter();
            this.PSubMayorTableAdapter = new MLB.MLBSubMayorTableAdapters.PSubMayorTableAdapter();
            this.PFlujoTableAdapter = new MLB.MLBPFlujoTableAdapters.PFlujoTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.PResIngBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBResIng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFlujoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBPFlujo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSubMayorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBSubMayor)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PResIngBindingSource
            // 
            this.PResIngBindingSource.DataMember = "PResIng";
            this.PResIngBindingSource.DataSource = this.MLBResIng;
            // 
            // MLBResIng
            // 
            this.MLBResIng.DataSetName = "MLBResIng";
            this.MLBResIng.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 343F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(821, 343);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 337);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Período:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tipo:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Ingresos-Costos",
            "Entradas-Salidas",
            "Flujo de Caja"});
            this.comboBox2.Location = new System.Drawing.Point(65, 37);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.Text = "Ingresos-Costos";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Mensual",
            "Anual"});
            this.comboBox1.Location = new System.Drawing.Point(65, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Mensual";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // PResIngTableAdapter
            // 
            this.PResIngTableAdapter.ClearBeforeFill = true;
            // 
            // PSubMayorTableAdapter
            // 
            this.PSubMayorTableAdapter.ClearBeforeFill = true;
            // 
            // PFlujoTableAdapter
            // 
            this.PFlujoTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reportViewer3);
            this.groupBox2.Controls.Add(this.reportViewer2);
            this.groupBox2.Controls.Add(this.reportViewer1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(203, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(615, 337);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reporte Resultante";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "MLBResIng_PResIng";
            reportDataSource3.Value = this.PResIngBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MLB.EstCosIng.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 16);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(609, 318);
            this.reportViewer1.TabIndex = 11;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "MLBSubMayor_PSubMayor";
            reportDataSource2.Value = this.PSubMayorBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "MLB.EstEntSal.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(3, 16);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(609, 318);
            this.reportViewer2.TabIndex = 12;
            this.reportViewer2.Visible = false;
            // 
            // reportViewer3
            // 
            this.reportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MLBPFlujo_PFlujo";
            reportDataSource1.Value = this.PFlujoBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "MLB.FujoEstandar.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(3, 16);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.Size = new System.Drawing.Size(609, 318);
            this.reportViewer3.TabIndex = 13;
            this.reportViewer3.Visible = false;
            // 
            // Estadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(821, 343);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Estadisticas";
            this.Text = "Estadisticas";
            this.Load += new System.EventHandler(this.Estadisticas_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Estadisticas_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PResIngBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBResIng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PFlujoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBPFlujo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSubMayorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MLBSubMayor)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.BindingSource PResIngBindingSource;
        private MLBResIng MLBResIng;
        private MLB.MLBResIngTableAdapters.PResIngTableAdapter PResIngTableAdapter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource PSubMayorBindingSource;
        private MLBSubMayor MLBSubMayor;
        private MLB.MLBSubMayorTableAdapters.PSubMayorTableAdapter PSubMayorTableAdapter;
        private System.Windows.Forms.BindingSource PFlujoBindingSource;
        private MLBPFlujo MLBPFlujo;
        private MLB.MLBPFlujoTableAdapters.PFlujoTableAdapter PFlujoTableAdapter;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        
    }
}