using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace MLB
{
    
    public partial class Componer : Form
    {
        private MLB.Form1 main;
        private String moneda, unidad, date, cuenta;
        private ArrayList pl;
        private MLB.DBControl dbc;
        private System.Data.DataSet dts;
        private ArrayList ramal;
        public Componer(ArrayList pl,String cuenta, String moneda, String unidad, MLB.Form1 main, String date, ArrayList ramal20)
        {
            InitializeComponent();

            this.moneda = moneda;
            this.cuenta = cuenta;
            this.date = date;
            this.unidad = unidad;
            this.main = main;
            this.pl = pl;
            dbc = new MLB.DBControl();
            ramal = new ArrayList();
            foreach (ValueSaver val in ramal20)
            {
                if (val.row == 5 && val.UName == unidad && val.moneda == moneda && System.Convert.ToDateTime(val.Date) == System.Convert.ToDateTime(date) )
                {
                    ramal.Add(val);
                }
            }

            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                menuStrip1.ForeColor = System.Drawing.SystemColors.Window;
                tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                groupBox1.ForeColor = System.Drawing.SystemColors.Window;
                groupBox2.ForeColor = System.Drawing.SystemColors.Window;
                dataGridView1.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridView2.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridView4.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridView1.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
                dataGridView2.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
                dataGridView4.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
                dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
                dataGridView2.BackgroundColor = System.Drawing.SystemColors.Info;
                dataGridView4.BackgroundColor = System.Drawing.SystemColors.Info;
                button1.ForeColor = System.Drawing.SystemColors.ControlText;
                
            }
        }

        private void Componer_Load(object sender, EventArgs e)
        {
             dts = dbc.SelectQuerryFixed("SELECT DISTINCT([UMSimb])FROM [MLB].[dbo].[UM]");
            singleProd.Items.Clear();
            multiProd.Items.Clear();
          //  comboBox4.Items.Clear();

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                singleProd.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                multiProd.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
              //  comboBox4.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            }
            LoadComoBox();
            dataGridView2.Rows.Add();
            LoadDataGrid();

        }
        private void LoadDataGrid()
        {
            dataGridView1.Rows.Clear();

           foreach (ValueSaver vs in pl)
           {
               dataGridView1.Rows.Add(vs.producto,vs.Cuenta,vs.Date,vs.UName,vs.moneda,cuenta);

               dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].ReadOnly = true;
               dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].ReadOnly = true;
               dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].ReadOnly = true;
               dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].ReadOnly = true;
               dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].ReadOnly = true;
           }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView2.Rows[0].Cells[5].Value = cuenta;

                if (dataGridView2.Rows[0].Cells[0].Value != null)
                {
                    Sumatoria();
                    comboBox2.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                }
               


            }
            if (e.ColumnIndex == 3 && dataGridView2.Rows[e.RowIndex].Cells[4].Value != null)
            {

                dataGridView2.Rows[0].Cells[2].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[0].Cells[4].Value.ToString()) / System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString()),5));

                if (dataGridView2.Rows[0].Cells[0].Value != null && dbc.ExistQuerry("Select Id From Preset Where NNombre = '" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'") && dataGridView1.Rows.Count>0)
                {
                    for (int w = 0; w < dataGridView1.RowCount;w++ )
                    {

                        dts = dbc.SelectQuerryFixed("Select [PCant],[Cantidad] From Preset Where NNombre = '" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "' And Producto = '" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "'");
                        if (dts.Tables[0].Rows.Count>0)
                        {
                            dataGridView1.Rows[w].Cells[3].Value = System.Convert.ToString(System.Math.Round((System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString()) * System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString())) / System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString()), 2));
                        }
                    }
                }
            }
            if (e.ColumnIndex == 3 && dataGridView1.Rows.Count>0)
            {
                Sumatoria();
            }
        }

        private void Sumatoria()
        {
            try
            {
                double suma = 0;
                for (int w = 0; w < dataGridView1.RowCount; w++)
                {
                    suma += System.Convert.ToDouble(dataGridView1.Rows[w].Cells[4].Value.ToString());
                }
                dataGridView2.Rows[0].Cells[4].Value = System.Convert.ToString(System.Math.Round(suma, 2));

                if (dataGridView2.Rows[0].Cells[0].Value != null && dataGridView2.Rows[0].Cells[3].Value != null && dataGridView2.Rows[0].Cells[4].Value != null)
                    dataGridView2.Rows[0].Cells[2].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[0].Cells[4].Value.ToString()) / System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString()), 5));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
            {

                dataGridView1.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()),2));
              if( dataGridView2.Rows[0].Cells[0].Value != null)
                Sumatoria();
            }
        }
        private void LoadComoBox()
        {
            try { 

                 dts = dbc.SelectQuerryFixed("SELECT DISTINCT(UniCuenta.Cuenta) FROM Producto INNER JOIN UniCuenta ON Producto.Cuenta=UniCuenta.Cuenta WHERE Producto.Moneda ='" + moneda + "' AND Unidad = '" + unidad + "'");

                comboBox1.Items.Clear();

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {
                    comboBox1.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                  //  combocuenta2.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());

                }



                dts = dbc.SelectQuerryFixed("Select Distinct(NNombre) From Preset");

                comboBox2.Items.Clear();
                comboBox2.Items.Add("Nuevo...");
                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {
                    comboBox2.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                  //  combocuenta2.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());

                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

           

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try{

            
                if (e.KeyCode == System.Windows.Forms.Keys.Delete && dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount )
                {
                    //toolStripStatusLabel2.Text = " Eliminando...";
                    //                 if (conceptos.Rows[conceptos.SelectedCells[0].RowIndex].Tag != null)
                    //                 {
                    //dbc.SimplePlan("DELETE [Concepto] WHERE [Id] = '" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag.ToString() + "'");
                    //  dbc.SimplePlan("DELETE [UndProd] WHERE [Producto] = '" + MainProducto.Rows[MainProducto.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'");

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
                    if (dataGridView2.Rows[0].Cells[0].Value != null)
                        Sumatoria();
                    /*   }*/
                    // toolStripStatusLabel2.Text = " Eliminado...";
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void some(String cta)
        {
               
            try
            {
           
            //  bool put = false;
                    //dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],Producto.[PrecIn] ,[FCantidad],[FImporte]FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(System.Convert.ToDateTime(date).Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Not Like '%(Defensa)%' ORDER BY Ramal20.Id");
                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[FCantidad],Producto.[PrecIn] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + unidad + "' AND  Ramal20.Cuenta='" + cta + "' AND Date='" + main.DateCultureConverter(System.Convert.ToDateTime(date).AddDays(-1)) + "'  AND Ramal20.Moneda = '" + moneda + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Not Like '%(Defensa)%' ORDER BY Ramal20.Id");
                    
                    ArrayList myal = new ArrayList();

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {

                       // int idx = Search4(ramal20Base, dts.Tables[0].Rows[w].ItemArray[0].ToString());
                        dataGridView4.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), main.CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString()));
                        dataGridView4.Rows[dataGridView4.RowCount - 1].Tag = dts.Tables[0].Rows[w].ItemArray[1].ToString();
                        dataGridView4.Rows[dataGridView4.RowCount - 1].Cells[0].Tag = dts.Tables[0].Rows[w].ItemArray[3].ToString();
                        myal.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                        //ramal20Base.Rows.RemoveAt(idx + 1);
                        //// CantFixer(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);

                        //if (idx > ramal20Base.RowCount - 10)
                        //    ramal20Base.Rows.Insert(ramal20Base.RowCount - 1);

                        //put = true;
                    }

                    dts = dbc.SelectQuerryFixed("Select Producto,DUM,PrecIn From UndProd Inner Join Producto On Producto.Nombre = UndProd.Producto Where UndProd.UName = '" + unidad + "' and Producto.Cuenta = '" + cta + "' and UndProd.Producto Not In (SELECT Ramal20.[Producto]FROM Ramal20  WHERE Ramal20.UName ='" + unidad + "' AND  Ramal20.Cuenta='" + cta + "' AND Date='" + main.DateCultureConverter(System.Convert.ToDateTime(date).AddDays(-1)) + "'  AND Ramal20.Moneda = '" + moneda + "'  AND Ramal20.Producto Not Like '%(Defensa)%')");
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        dataGridView4.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), "0.00");
                        dataGridView4.Rows[dataGridView4.RowCount - 1].Tag = dts.Tables[0].Rows[w].ItemArray[1].ToString();
                        dataGridView4.Rows[dataGridView4.RowCount - 1].Cells[0].Tag = dts.Tables[0].Rows[w].ItemArray[2].ToString();

                        myal.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                    }

                    myal = main.GetEntradas(myal,cta);

                    for (int w = 0; w < dataGridView4.RowCount;w++ )
                    {
                        for (int k = 0; k < myal.Count;k+=2 )
                        {
                            if(dataGridView4.Rows[w].Cells[0].Value.ToString() == myal[k].ToString())
                                dataGridView4.Rows[w].Cells[1].Value = System.Convert.ToString(System.Convert.ToDouble(dataGridView4.Rows[w].Cells[1].Value.ToString())+System.Convert.ToDouble(myal[k+1].ToString()));
                        }
                    }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        //private double GetEntradas(String prod)
        //{
        //    double entrada = 0;
        //    if (ramal != null)
        //    {

        //        foreach (ValueSaver val in ramal)
        //        {
        //            if (val.producto == prod && val.col == 5 && val.Cuenta == cuenta && val.UName ==unidad && val.Date == date&& val.moneda == moneda )
        //            {

        //                entrada = System.Convert.ToDouble(val.cant);

        //            }
        //        }
        //    }
        //    return entrada;
        //}
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            some(comboBox1.Text);

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try{

                    if (comboBox1.Text != "" && Procede(dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString(), comboBox1.Text) && System.Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString())>0)
                    {
                        dataGridView1.Rows.Add(dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView4.Rows[e.RowIndex].Tag, dataGridView4.Rows[e.RowIndex].Cells[0].Tag, dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString()) * System.Convert.ToDouble(dataGridView4.Rows[e.RowIndex].Cells[0].Tag),2)),comboBox1.Text);
                        dataGridView4.Rows.RemoveAt(e.RowIndex);
                        if (dataGridView2.Rows[0].Cells[0].Value != null)
                        Sumatoria();
                    }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private bool Procede(String prod, String cue)
        {
            for (int w = 0; w < dataGridView1.RowCount;w++ )
            {
                if (dataGridView1.Rows[w].Cells[0].Value.ToString() == prod && dataGridView1.Rows[w].Cells[5].Value.ToString()==cue)
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (Filled(0, dataGridView2) && NewProd())
                {

                    System.Collections.ArrayList desc = new System.Collections.ArrayList();

                    System.Collections.ArrayList parts = new System.Collections.ArrayList();


                    for (int w = 0; w < dataGridView2.RowCount; w++)
                    {
                        parts.Add(new MLB.InBetween(dataGridView2.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView2.Rows[w].Cells[4].Value.ToString()), "Des", dataGridView2.Rows[w].Cells[5].Value.ToString(), dataGridView2.Rows[w].Cells[0].Value.ToString(), dataGridView2.Rows[w].Cells[3].Value.ToString(), date, unidad));
                    }

                    for (int w = 0; w < dataGridView1.RowCount; w++)
                    {
                        desc.Add(new MLB.InBetween(dataGridView1.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView1.Rows[w].Cells[4].Value.ToString()), "Out", dataGridView2.Rows[0].Cells[5].Value.ToString(), dataGridView1.Rows[w].Cells[0].Value.ToString(), dataGridView1.Rows[w].Cells[3].Value.ToString(), date, unidad));

                    }




                    main.Ramaller2(desc, parts);

                    Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(this, "Por favor, revise los datos del nuevo Producto.");


                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private bool NewProd()
        {
            //if (Filled(0, dataGridView2))
            //{
            try{
                for (int w = 0; w < dataGridView2.RowCount; w++)
                {
                    if (!dbc.ExistQuerry("Select id From Producto Where Nombre ='" + dataGridView2.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView2.Rows[w].Cells[5].Value.ToString() + "'"))
                    {
                        dbc.SimplePlan("INSERT INTO [Producto]([Id],[Nombre],[PrecIn],[PrecOut],[DUM],[Cuenta],[Moneda])VALUES" +
                   "('" + dbc.MaxQuerry("Producto") + "','" + dataGridView2.Rows[w].Cells[0].Value.ToString() + "','" +
                   dataGridView2.Rows[w].Cells[2].Value.ToString() + "','" + "0.00" + "','" + dataGridView2.Rows[w].Cells[1].Value.ToString() + "','" +
                   dataGridView2.Rows[w].Cells[5].Value.ToString() + "','" + moneda + "')");

                    }
                    else
                    {

                        if (dbc.ExistQuerry("Select Producto.Id From Producto INNER JOIN UndProd ON Producto.Nombre = UndProd.Producto Where Nombre ='" + dataGridView2.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView2.Rows[w].Cells[5].Value.ToString() + "'"))
                        { 
                        
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			        	System.Windows.Forms.DialogResult result;


              
                             result = MessageBox.Show(this, "Desea Agregar esta Composición al producto: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + " ya existente en la cuenta: " + dataGridView2.Rows[0].Cells[5].Value.ToString() + "?", "Advertencia", buttons, MessageBoxIcon.Warning);
                              if (result == DialogResult.Yes)
                              {
                                  return true;
                              }
                        }
          
                        System.Windows.Forms.MessageBox.Show(this, "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".");
                        return false;
                    }

                }
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show(this, "Por favor, llene todos los campos del Producto a Componer.");
            //    return false;
            //}

             
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
          return true;
        }
        private bool Filled(int row, DataGridView dtgv)
        {
            bool rslt = true;

            for (int w = 0; w < dtgv.ColumnCount; w++)
                if (GetData(dtgv.Rows[row].Cells[w].Value) == " ")
                    rslt = false;

            return rslt;
        }
        private String GetData(Object val)
        {
            if (val != null)
            {
                return val.ToString();
            }
            return " ";

        }
        private void PrepareDes()
        {
                try{

                dbc.SimplePlan("DELETE FROM PDescomponer WHERE [MachineName]= '" + System.Environment.MachineName + "'");
                for (int w = 0; w < dataGridView2.RowCount; w++)
                {
                    if (dataGridView2.Rows[w].Cells[0].Value == null)
                        break;

                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PDescomponer]([Id],[Producto]" +
                 ",[UM],[PrecIn],[Cantidad],[Importe],[Cuenta],[Descrip],[Porcient],[Unidad]" +
                 ",[Moneda], [Date],[MachineName]) " +
                   "VALUES ('" + dbc.MaxQuerry("PDescomponer") + "','" + GetData(dataGridView2.Rows[w].Cells[0].Value) + "','" + GetData(dataGridView2.Rows[w].Cells[1].Value) + "','" + GetData(dataGridView2.Rows[w].Cells[2].Value) + "'" +
                   ",'" + GetData(dataGridView2.Rows[w].Cells[3].Value) + "','" + GetData(dataGridView2.Rows[w].Cells[4].Value) + "','" + GetData(dataGridView2.Rows[w].Cells[5].Value) + "'" +
                   ",'" + "Nuevo Producto luego de la Composición" + "','" + GetData(dataGridView2.Rows[w].Tag) + "%" + "','" + unidad + "','" + moneda + "','" + date + "','"
                   + System.Environment.MachineName + "')");

                }

                for (int w = 0; w < dataGridView1.RowCount; w++)
                {
                    if (dataGridView1.Rows[w].Cells[0].Value == null)
                        break;

                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PDescomponer]([Id],[Producto]" +
                 ",[UM],[PrecIn],[Cantidad],[Importe],[Cuenta],[Descrip],[Porcient],[Unidad]" +
                 ",[Moneda] , [Date],[MachineName]) " +
                   "VALUES ('" + dbc.MaxQuerry("PDescomponer") + "','" + GetData(dataGridView1.Rows[w].Cells[0].Value) + "','" + GetData(dataGridView1.Rows[w].Cells[1].Value) + "','" + GetData(dataGridView1.Rows[w].Cells[2].Value) + "'" +
                   ",'" + GetData(dataGridView1.Rows[w].Cells[3].Value) + "','" + GetData(dataGridView1.Rows[w].Cells[4].Value) + "','" + GetData(dataGridView1.Rows[w].Cells[5].Value) + "'" +
                   ",'" + GetData("Producto usado para la Composición") + "','" + GetData(dataGridView1.Rows[w].Tag) + "%" + "','" + unidad + "','" + moneda + "','" + date + "','"
                   + System.Environment.MachineName + "')");

                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aceptarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
           
            
                    if (Filled(0, dataGridView2) && NewProd())
                    {

                        System.Collections.ArrayList desc = new System.Collections.ArrayList();

                        System.Collections.ArrayList parts = new System.Collections.ArrayList();


                        for (int w = 0; w < dataGridView2.RowCount; w++)
                        {
                            parts.Add(new MLB.InBetween(dataGridView2.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView2.Rows[w].Cells[4].Value.ToString()), "Des", dataGridView2.Rows[w].Cells[5].Value.ToString(), dataGridView2.Rows[w].Cells[0].Value.ToString(), dataGridView2.Rows[w].Cells[3].Value.ToString(), "", ""));
                        }

                        for (int w = 0; w < dataGridView1.RowCount; w++)
                        {
                            desc.Add(new MLB.InBetween(dataGridView1.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView1.Rows[w].Cells[4].Value.ToString()), "Out", dataGridView2.Rows[0].Cells[5].Value.ToString(), dataGridView1.Rows[w].Cells[0].Value.ToString(), dataGridView1.Rows[w].Cells[3].Value.ToString(), "", ""));

                        }




                        main.Ramaller2(desc, parts);

                        Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(this, "Por favor, revise los datos del nuevo Producto.");


                    }

            }
            catch (System.Exception ex)
           {
               System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
           }

        }
        private void Validar()
        {
            try
            {
                String rslt = "";
                if (Filled(0, dataGridView2))
                {
                    for (int w = 0; w < dataGridView2.RowCount; w++)
                    {
                        if (dbc.ExistQuerry("Select id From Producto Where Nombre ='" + dataGridView2.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView2.Rows[w].Cells[5].Value.ToString() + "'"))
                        {

                            // System.Windows.Forms.MessageBox.Show(this, "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".");
                            rslt = rslt + "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".\n";
                            // return false;
                        }

                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(this, "Por favor, llene todos los campos del Producto a Componer.");
                    rslt = rslt + "Faltan campos por llenar para el Producto a Componer.\n";
                    // return false;
                }

                if (dataGridView1.RowCount > 1 && dataGridView2.Rows[0].Cells[0].Value != null && dataGridView2.Rows[0].Cells[5].Value != null)
                {
                    for (int w = 0; w < dataGridView1.RowCount; w++)
                    {
                        if (!dbc.ExistQuerry("Select UndProd.Id From UndProd INNER JOIN Producto On UndProd.Producto = Producto.Nombre Where Producto ='" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView1.Rows[w].Cells[5].Value.ToString() + "' and UName = '" + unidad + "'"))
                        {

                            // System.Windows.Forms.MessageBox.Show(this, "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".");
                            rslt = rslt + "No existe en las bases de datos un Producto con el nombre: " + dataGridView1.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[0].Cells[5].Value.ToString() + ", en la Unidad: " + unidad + ".\n";
                            // return false;
                        }

                    }
                }
                else
                {
                    rslt = rslt + "No se puede Componer un Producto a partir de 0 productos.\n";
                    // System.Windows.Forms.MessageBox.Show(this, "Por favor, llene todos los campos del Producto a Componer.");
                    // return false;
                }

                if (rslt != "")
                {
                    System.Windows.Forms.MessageBox.Show(this, "Errores encontrados:\n" + rslt);
                    //return false;
                }
                // return true;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void validarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Validar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrepareDes();
            MLB.Imprimir imp = new MLB.Imprimir();
            imp.table = "Descomponer";
            imp.Show(this);
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0&&e.ColumnIndex == 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                comboBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
           
                if (comboBox2.Text!=""&&comboBox2.Text!="Nuevo...")
                {
                    
                    dts = dbc.SelectQuerryFixed("SELECT [NNombre],[PUM],[PCant],Preset.[Producto],Preset.[UM],Producto.PrecIn,[Cantidad],Preset.[Cuenta] FROM [MLB].[dbo].[Preset]INNER JOIN UndProd On Preset.Producto=UndProd.Producto INNER JOIN Producto On Producto.Nombre = UndProd.Producto Where NNombre = '"+comboBox2.Text+"' and UndProd.UName = '"+unidad+"'");

                    if (dts.Tables[0].Rows.Count>0)
                    {

                        dataGridView2.Rows[0].Cells[0].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                        dataGridView2.Rows[0].Cells[1].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                        dataGridView2.Rows[0].Cells[3].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();
                        dataGridView2.Rows[0].Cells[5].Value = cuenta;

                        dataGridView1.Rows.Clear();
                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++ )
                        {
                            dataGridView1.Rows.Add(dts.Tables[0].Rows[w].ItemArray[3].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[6].ToString(), System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[5].ToString()) * System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[6].ToString()), 2)), dts.Tables[0].Rows[w].ItemArray[7].ToString());
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private bool BValidar()
        {
            try{
       
                String rslt = "";
                if (Filled(0, dataGridView2))
                {
                    //for (int w = 0; w < dataGridView2.RowCount; w++)
                    //{
                    //    if (dbc.ExistQuerry("Select id From Producto Where Nombre ='" + dataGridView2.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView2.Rows[w].Cells[5].Value.ToString() + "'"))
                    //    {
                      
                    //       // System.Windows.Forms.MessageBox.Show(this, "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".");
                    //        rslt = rslt + "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".\n";
                    //        // return false;
                    //    }

                    //}
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(this, "Por favor, llene todos los campos del Producto a Componer.");
                    rslt = rslt + "Faltan campos por llenar para el Producto a Componer.\n";
                    // return false;
                }

                if (dataGridView1.RowCount>1&&dataGridView2.Rows[0].Cells[0].Value!=null&&dataGridView2.Rows[0].Cells[5].Value!=null)
                {
                    for (int w = 0; w < dataGridView1.RowCount; w++)
                    {
                        if (!dbc.ExistQuerry("Select UndProd.Id From UndProd INNER JOIN Producto ON UndProd.Producto = Producto.Nombre Where Producto ='" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView1.Rows[w].Cells[5].Value.ToString() + "' and UName = '" + unidad + "'"))
                        {

                            // System.Windows.Forms.MessageBox.Show(this, "Ya existe en las bases de datos un Producto con el nombre: " + dataGridView2.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[w].Cells[5].Value.ToString() + ".");
                            rslt = rslt + "No existe en las bases de datos un Producto con el nombre: " + dataGridView1.Rows[w].Cells[0].Value.ToString() + ", asicionado a la Cuenta: " + dataGridView2.Rows[0].Cells[5].Value.ToString() + ", en la Unidad: "+unidad+".\n";
                            // return false;
                        }

                    }
                }
                else
                {
                    if (dataGridView1.RowCount < 1)
                    {
                        if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[Preset] WHERE [NNombre] ='" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'"))
                        {
                          MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			        	    System.Windows.Forms.DialogResult result;


                    // quit logo 
                    //loaded=true;
                    // Displays the MessageBox.
                    //connected=false;
                                  result = MessageBox.Show(this, "Esta Acción eliminará los la configuración para: "+dataGridView2.Rows[0].Cells[0].Value.ToString()+". Está seguro de que desea Continuar?", "Advertencia", buttons, MessageBoxIcon.Warning);
                                if (result == DialogResult.Yes)
                                {
                                   
                                        dbc.SimplePlan("DELETE FROM [MLB].[dbo].[Preset] WHERE [NNombre] ='" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'");

                                 }
                          }
                        else
                              rslt = rslt + "No se puede Componer un Producto a partir de 0 productos.\n";
                       }
                
                    
                  
                    // System.Windows.Forms.MessageBox.Show(this, "Por favor, llene todos los campos del Producto a Componer.");
                    // return false;
                }

                if (rslt!="")
                {
                    System.Windows.Forms.MessageBox.Show(this, "Errores encontrados:\n"+rslt);
                    return false;
                }
                
             }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
           return true;
        }
        private void gToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
           
            
            if (BValidar())
            {
                if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[Preset] WHERE [NNombre] ='" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'"))
                 {
                     dbc.SimplePlan("DELETE FROM [MLB].[dbo].[Preset] WHERE [NNombre] ='" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'");
                
                 }
                  for (int w = 0; w < dataGridView1.RowCount; w++)
                 {
                     dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Preset]([Id],[NNombre],[PUM],[PCant],[Producto],[UM]" +
                     ",[Cantidad],[Cuenta]) VALUES('" + dbc.MaxQuerry("Preset") + "','" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "'" +
                     ",'" + dataGridView2.Rows[0].Cells[1].Value.ToString() + "','" + dataGridView2.Rows[0].Cells[3].Value.ToString() + "'" +
                    " ,'" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[w].Cells[1].Value.ToString() + "'" +
                     ",'" + dataGridView1.Rows[w].Cells[3].Value.ToString() + "','" + dataGridView1.Rows[w].Cells[5].Value.ToString() + "')");
                 }

                  LoadComoBox();
            }
           }
            catch (System.Exception ex)
           {
               System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
           }

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text!="")
            {
                dataGridView2.Rows[0].Cells[0].Value = comboBox2.Text;
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            try{
                    if (dataGridView2.Rows[0].Cells[5].Value==null)
                    {
                        dataGridView2.Rows[0].Cells[5].Value = cuenta;
                    }

                    if (dataGridView2.Rows[0].Cells[4].Value == null)
                    {
                        if (dataGridView2.Rows[0].Cells[0].Value != null)
                        {
                            Sumatoria();
                           // comboBox2.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                        }
                    }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try{
                if (e.RowIndex>=0&&e.ColumnIndex == 3 && dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
                {

                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()), 2));
                    if (dataGridView2.Rows[0].Cells[0].Value != null)
                        Sumatoria();
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void opcionesToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            opcionesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void opcionesToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
             if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
                 opcionesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }
    }
}
