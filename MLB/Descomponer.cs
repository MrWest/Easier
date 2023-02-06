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
    public partial class Descomponer : Form
    {
        private String moneda, unidad, date;
        private double oricant, newcant, restcant;
        private int percent;
        private bool grid;
        private MLB.Form1 main;
        private  MLB.DBControl dbc;

        public Descomponer(String producto, String um, String price, String cantiad, String importe, String cuenta, String moneda, String unidad,MLB.Form1 main , String date)
        {
            InitializeComponent();
            dataGridView2.Rows.Add();
            dataGridView2.Rows[0].Cells[0].Value = producto;
            dataGridView2.Rows[0].Cells[1].Value = um;
            dataGridView2.Rows[0].Cells[2].Value = price;
            dataGridView2.Rows[0].Cells[3].Value = cantiad;
            dataGridView2.Rows[0].Cells[4].Value = importe;
            dataGridView2.Rows[0].Cells[5].Value = cuenta;
            dataGridView2.Rows[0].Tag = "100";

            dataGridView2.Rows.Add();
            dataGridView2.Rows[1].Cells[0].Value = producto;
            dataGridView2.Rows[1].Cells[1].Value = um;
            dataGridView2.Rows[1].Cells[2].Value = price;
            dataGridView2.Rows[1].Cells[3].Value = main.CantFixer3(um,"0.00");
            dataGridView2.Rows[1].Cells[4].Value = importe;
            dataGridView2.Rows[1].Cells[5].Value = cuenta;
            dataGridView2.Rows[1].Tag = "0";

            dataGridView2.Rows.Add();
            dataGridView2.Rows[2].Cells[0].Value = producto;
            dataGridView2.Rows[2].Cells[1].Value = um;
            dataGridView2.Rows[2].Cells[2].Value = price;
            dataGridView2.Rows[2].Cells[3].Value = cantiad;
            dataGridView2.Rows[2].Cells[4].Value = importe;
            dataGridView2.Rows[2].Cells[5].Value = cuenta;
            dataGridView2.Rows[2].Tag = "100";

            Tooltipper();

            this.moneda = moneda;
            this.unidad = unidad;
            this.date = date;
            groupBox1.Text = groupBox1.Text + " (Moneda: " + this.moneda+")";


            dataGridView1.Rows[0].Cells[1].ReadOnly = true;
            dataGridView1.Rows[0].Cells[2].ReadOnly = true;
            dataGridView1.Rows[0].Cells[3].ReadOnly = true;
            dataGridView1.Rows[0].Cells[4].ReadOnly = true;
            dataGridView1.Rows[0].Cells[5].ReadOnly = true;

           
            percent = 100;

            this.main = main;

             dbc = new MLB.DBControl();
             grid = true;
            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                tableLayoutPanel2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                groupBox1.ForeColor = System.Drawing.SystemColors.Window;
                groupBox2.ForeColor = System.Drawing.SystemColors.Window;
                dataGridView1.ForeColor = System.Drawing.SystemColors.ControlText;
                dataGridView1.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
                dataGridView2.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
                dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
                dataGridView2.BackgroundColor = System.Drawing.SystemColors.Info;
                dataGridView2.ForeColor = System.Drawing.SystemColors.ControlText;
                menuStrip1.ForeColor = System.Drawing.SystemColors.Window;
                menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                statusStrip1.ForeColor = System.Drawing.SystemColors.Window;
                statusStrip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.Window;
                button1.ForeColor = System.Drawing.SystemColors.ControlText;
                button2.ForeColor = System.Drawing.SystemColors.ControlText;

            }
           
        }

        private void Descomponer_Load(object sender, EventArgs e)
        {
           // numericUpDown2.Value = 100;
            //this.reportViewer1.RefreshReport();
            System.Data.DataSet dts = dbc.SelectQuerryFixed("SELECT DISTINCT([UMSimb])FROM [MLB].[dbo].[UM]");
            singleProd.Items.Clear();
            multiProd.Items.Clear();
            comboBox4.Items.Clear();

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                singleProd.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                multiProd.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                comboBox4.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         //   MLB.Form1 f = new MLB.Form1();
        //    f.Show();
            Close();
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try{

            if (numericUpDown1.Value<100)
            {

                 oricant = System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString());

                 restcant = (oricant * System.Convert.ToDouble(100 - numericUpDown1.Value)) / 100;
                 newcant = (oricant * System.Convert.ToDouble( numericUpDown1.Value.ToString())) / 100;

                if (dataGridView2.RowCount==3)
                {
                    dataGridView2.Rows.RemoveAt(1);
                    dataGridView2.Rows.RemoveAt(1);
                }
                    dataGridView2.Rows.Add(dataGridView2.Rows[0].Cells[0].Value.ToString(), dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(),restcant, dataGridView2.Rows[0].Cells[4].Value.ToString(), dataGridView2.Rows[0].Cells[5].Value.ToString());
                    dataGridView2.Rows.Add(dataGridView2.Rows[0].Cells[0].Value.ToString(), dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(), newcant, dataGridView2.Rows[0].Cells[4].Value.ToString(), dataGridView2.Rows[0].Cells[5].Value.ToString());

                    dataGridView2.Rows[1].Cells[3].Value = main.CantFixer3(dataGridView2.Rows[1].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round(restcant, 2)));
                    dataGridView2.Rows[1].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[1].Cells[3].Value.ToString()) * System.Convert.ToDouble(dataGridView2.Rows[1].Cells[2].Value.ToString()), 2));


                    dataGridView2.Rows[2].Cells[3].Value = main.CantFixer3(dataGridView2.Rows[2].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round(newcant, 2)));
                    dataGridView2.Rows[2].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[2].Cells[3].Value.ToString()) * System.Convert.ToDouble(dataGridView2.Rows[2].Cells[2].Value.ToString()), 2));


                    Tooltipper();

                  }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
                   // dataGridView2.Rows[1].Cells[2].Style.BackColor = Color.AliceBlue;
                //                 
                
                
//                 dataGridView2.Rows.Clear();
//                 dataGridView1.Rows.Add(dataGridView2.Rows[0].Cells[0].Value.ToString(), dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(), dataGridView2.Rows[0].Cells[3].Value.ToString(), dataGridView2.Rows[0].Cells[4].Value.ToString(), dataGridView2.Rows[0].Cells[5].Value.ToString());
//                 dataGridView1.Rows[0].Cells[3].Value = main.CantFixer3(dataGridView1.Rows[0].Cells[1].Value.ToString(),System.Convert.ToString(System.Math.Round(newcant, 2)));
//                 dataGridView1.Rows[0].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[0].Cells[3].Value.ToString()) * System.Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value.ToString()), 2));
//                 if (dataGridView1.Rows[0].Cells[0].Value.ToString()== dataGridView2.Rows[0].Cells[0].Value.ToString())
//                 {
// 
//                 }
            }
       
        private void Tooltipper()
        {
            try{

            for (int w = 0; w < 6; w++)
            {
                //dataGridView2.Rows[0].Cells[w].Style.BackColor = Color.wi;
                dataGridView2.Rows[0].Cells[w].ToolTipText = "Datos del Producto Original que se va Descomponer";
            }
            for (int w = 0; w < 6; w++)
            {
                dataGridView2.Rows[1].Cells[w].Style.BackColor = Color.AliceBlue;
                dataGridView2.Rows[1].Cells[w].ToolTipText = "Datos del Producto Restante luego de Descomponer";
            }
            for (int w = 0; w < 6; w++)
            {
                dataGridView2.Rows[2].Cells[w].Style.BackColor = Color.LightBlue;
                dataGridView2.Rows[2].Cells[w].ToolTipText = "Datos del Producto que se va a Descomponer";
            }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try{
            if (e.ColumnIndex==0&&e.RowIndex==dataGridView1.RowCount-2)
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataGridView2.Rows[0].Cells[1].Value;
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = "0.00";
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = main.CantFixer3(dataGridView1.Rows[0].Cells[1].Value.ToString(), "0.00");
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = "0.00";
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = dataGridView2.Rows[0].Cells[5].Value;

                dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                dataGridView1.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                dataGridView1.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                dataGridView1.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                dataGridView1.Rows[e.RowIndex].Cells[5].ReadOnly = false;

                dataGridView1.Rows[e.RowIndex+1].Cells[1].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex+1].Cells[2].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex+1].Cells[3].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex+1].Cells[4].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex+1].Cells[5].ReadOnly = true;
               
            }

            if (e.ColumnIndex == 4 && System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString())!=0)
            {
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) / System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()),6));
               
            }


           

            if (e.ColumnIndex==2||e.ColumnIndex==3)
            {
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()),2)); 
            }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        //private String main.CantFixer3(String um, String cant)
        //{
        //    if (um != "Kg" && um != "Lt" && um != "Lb" && (um != "Rac" || (um == "Rac" && TheMothod(um, cant))))
        //    {
        //       // go = true;



        //        double num = System.Convert.ToDouble(GetNumData(cant));


        //        return System.Convert.ToString(System.Math.Round(num));


        //    }

        //    return cant;

        //}
        private String GetNumData(Object val)
        {
            if (val != null&&val.ToString() !="")
            {
                //String aux = val.ToString().Replace("$", "");
                //aux = aux.Replace("(", "");
                //aux = aux.Replace(")", "");

                String aux = System.Convert.ToString(System.Convert.ToDouble(val));
                // aux = aux.Replace(",", "");
                if (aux == "")
                    aux = "0";
                return aux;

            }
            return "0.00";

        }
        private bool TheMothod(String um, String cant)
        {
            if (um == "Rac")
            {
                double var = System.Convert.ToDouble(cant);
                int kk = System.Convert.ToInt32(System.Math.Round(var));
                if (var == 0 || var % kk == 0)
                {
                    // double num = System.Convert.ToDouble(GetNumData(cant));


                    return true;

                }
            }
            return false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
//             if (e.Row.Cells[0].Value!=null&&e.Row.Cells[0].Value.ToString()!="")
//             {
//                 e.Row.Cells[1].Value = dataGridView1.Rows[0].Cells[1].Value;
//                 e.Row.Cells[2].Value = "0.00";
//                 e.Row.Cells[3].Value = main.CantFixer3( dataGridView1.Rows[0].Cells[1].Value.ToString(),"0.00" );
//                 e.Row.Cells[4].Value = "0.00";
//                 e.Row.Cells[5].Value = dataGridView1.Rows[0].Cells[5].Value; 
//             }
        }

       

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Enabled = true;
            comboBox4.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox6.Enabled = true;
            comboBox3.Enabled = true;   

            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            else
                textBox5.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[1].Value != null)
                comboBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            else
                comboBox4.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            else
                textBox7.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            else
                textBox8.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value != null)
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            else
                textBox6.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[5].Value != null)
            comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            else
            comboBox3.Text = "";

            if (dataGridView1.Rows[e.RowIndex].Cells[5].Value == null && dataGridView1.Rows[e.RowIndex].Cells[4].Value != null && dataGridView1.Rows[e.RowIndex].Cells[3].Value != null && dataGridView1.Rows[e.RowIndex].Cells[2].Value != null && dataGridView1.Rows[e.RowIndex].Cells[1].Value != null && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                comboBox3.Enabled = false;
                textBox8.Enabled = false;
                textBox7.Enabled = false;
                textBox6.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                comboBox3.Enabled = true;
                textBox8.Enabled = true;
                textBox7.Enabled = true;
                textBox6.Enabled = true;
                comboBox4.Enabled = true;
            }


            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null && dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() != ""&& System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
            {
                if (dataGridView2.RowCount == 3)
                {
                    decimal check = System.Convert.ToDecimal(System.Math.Round((System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())*100)/System.Convert.ToDouble(dataGridView2.Rows[2].Cells[3].Value.ToString()),2));
                    if (check <= 100 && check > 0)
                    {
                        numericUpDown2.Value = check;
                        numericUpDown2.BackColor = Color.White;
                    }
                    else
                        numericUpDown2.BackColor = Color.LightSalmon;
                }
                if (dataGridView2.RowCount == 1)
                {
                    decimal check = System.Convert.ToDecimal(System.Math.Round((System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100) / System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString()),2));

                    if (check <= 100 && check > 0)
                    {
                        numericUpDown2.Value = check;
                        numericUpDown2.BackColor = Color.White;
                    }
                    else
                        numericUpDown2.BackColor = Color.LightSalmon;
                }

            }

            grid = false;

        }

        private void LoadComoBox()
        {
          
            System.Data.DataSet dts = dbc.SelectQuerryFixed("SELECT DISTINCT(UniCuenta.Cuenta) FROM Producto INNER JOIN UniCuenta ON Producto.Cuenta=UniCuenta.Cuenta WHERE Producto.Moneda ='" + moneda + "' AND Unidad = '" + unidad+ "'");

            comboBox3.Items.Clear();
            combocuenta.Items.Clear();
            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                comboBox3.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                combocuenta.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            }
           

        }

   

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
           if (e.RowIndex!=-1)
           {
           
            
                   if (e.ColumnIndex ==0)
                   {
                       textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                       
                   }
                   if (e.ColumnIndex == 1)
                   {
                       comboBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                   }
                   if (e.ColumnIndex == 2)
                   {
                       textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                   }
                   if (e.ColumnIndex == 3)
                   {
                       textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                   }
                   if (e.ColumnIndex == 4)
                   {
                       textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                   }
                   if (e.ColumnIndex == 5)
                   {
                       comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                   }


               if (e.ColumnIndex == 4)
               {
                   Validar1(0);
               }

               if (e.ColumnIndex == 3 && dataGridView1.Rows[e.RowIndex].Cells[3].Value != null && dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()!=""&&System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
               {
                   if (dataGridView2.RowCount == 3)
                   {
                       decimal check = System.Convert.ToDecimal(System.Math.Round((System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100) / System.Convert.ToDouble(dataGridView2.Rows[2].Cells[3].Value.ToString()),2));
                       
                       if (check<=100&&check>0)
                       {
                           numericUpDown2.Value = check;
                       }
                      
                   }
                   if (dataGridView2.RowCount == 1)
                   {
                       decimal check = System.Convert.ToDecimal(System.Math.Round((System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100) / System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString()),2));

                       if (check <= 100 && check > 0)
                           numericUpDown2.Value = check;
                   }

                   if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() != "" && dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()!="")
                   {
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()) * System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()),2)); 
                   }

               }
           }
        }

        private bool Validar1(int choice)
        {
            double imptotal = 0;
            for (int r = 0; r < dataGridView1.RowCount - 1; r++ )
            {
                imptotal += System.Convert.ToDouble(GetNumData(dataGridView1.Rows[r].Cells[4].Value));
            }
            imptotal = System.Math.Round(imptotal, 2);
            if (choice==0)
            {
                double exact = 0;
                    if (dataGridView2.RowCount==3)
                    {
                         exact = System.Convert.ToDouble(dataGridView2.Rows[2].Cells[4].Value.ToString());
                    }
                    if (dataGridView2.RowCount == 1)
                    {
                        exact = System.Convert.ToDouble(dataGridView2.Rows[0].Cells[4].Value.ToString());
                    }  
                
                if (imptotal > exact)
                        {
                            toolStripStatusLabel1.Text = "El Importe de la descomposicion actual excede el importe correcto en: $"+System.Math.Round(imptotal-exact,2).ToString();
                            return false;
                        }

                        if (imptotal < exact)
                        {
                            toolStripStatusLabel1.Text = "El Importe de la descomposicion actual es menor que el Importe correcto en: $" + System.Math.Round((exact - imptotal),2).ToString();
                            return false;
                        }
                        if (imptotal == exact)
                        {
                            toolStripStatusLabel1.Text = "La descomposicion actual es correcta";

                            return true;
                        }
                   
            }
                if (choice==1)
                {
                    double exact = 0;
                    if (dataGridView2.RowCount == 3)
                    {
                        exact = System.Convert.ToDouble(dataGridView2.Rows[2].Cells[4].Value.ToString());
                    }
                    if (dataGridView2.RowCount == 1)
                    {
                        exact = System.Convert.ToDouble(dataGridView2.Rows[0].Cells[4].Value.ToString());
                    }

                    if (imptotal > exact)
                    {
                        System.Windows.Forms.MessageBox.Show("El Importe de la descomposicion actual excede el importe correcto en: $" + System.Math.Round((imptotal - exact),2).ToString());
                        return false;
                    }

                    if (imptotal < exact)
                    {
                        System.Windows.Forms.MessageBox.Show("El Importe de la descomposicion actual es menor que el Importe correcto en: $" +System.Math.Round( (exact - imptotal),2).ToString());
                        return false;

                    }
                    if (imptotal == exact)
                    {
                       System.Windows.Forms.MessageBox.Show("La descomposicion actual es correcta");
                       return true;
                    }
                }
                return false;
        }

        private String Validar2()
        {
            double imptotal = 0;
            for (int r = 0; r < dataGridView1.RowCount - 1; r++)
            {
                imptotal += System.Convert.ToDouble(GetNumData(dataGridView1.Rows[r].Cells[4].Value));
            }

               imptotal = System.Math.Round(imptotal, 2);
                double exact = 0;
                if (dataGridView2.RowCount == 3)
                {
                    exact = System.Convert.ToDouble(dataGridView2.Rows[2].Cells[4].Value.ToString());
                }
                if (dataGridView2.RowCount == 1)
                {
                    exact = System.Convert.ToDouble(dataGridView2.Rows[0].Cells[4].Value.ToString());
                }

                if (imptotal > exact)
                {
                    return "El Importe de la descomposicion actual excede el importe correcto en: $" + System.Math.Round(imptotal - exact, 2).ToString();
                   
                }

                if (imptotal < exact)
                {
                    return "El Importe de la descomposicion actual es menor que el Importe correcto en: $" + System.Math.Round((exact - imptotal), 2).ToString();
                   
                }
                if (imptotal == exact)
                {
                    return "La descomposicion actual es correcta";

                  
                }
                return "";

         
        }



        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value = textBox5.Text;
            }

//             if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex == dataGridView1.RowCount - 1)
//             {
// 
//                 
//                 dataGridView1.Rows.Insert(0,1);
//                 dataGridView1.Rows[dataGridView1.RowCount-2].Cells[0].Selected = true;
//                 dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value = textBox5.Text;
//                 DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(dataGridView1.SelectedCells[0].ColumnIndex, dataGridView1.SelectedCells[0].RowIndex);
//                 
//             
//                 dataGridView1_CellEndEdit(sender, ee);
//             }
             
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value = comboBox4.Text;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value = textBox7.Text;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = textBox8.Text;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = textBox6.Text;

                if (textBox6.Text!=""&&System.Convert.ToDouble(textBox6.Text)!=0)
                textBox7.Text = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(textBox6.Text) / System.Convert.ToDouble(textBox8.Text), 6));
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = comboBox3.Text;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Descomponer_Shown(object sender, EventArgs e)
        {
            LoadComoBox();
            numericUpDown2.Value = 100;
            
        }

        private void validarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HardValidar2();
           // Validar1(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (HardValidar())
            {

                System.Collections.ArrayList desc = new System.Collections.ArrayList();

                System.Collections.ArrayList parts = new System.Collections.ArrayList();


                for (int w = 0; w < dataGridView2.RowCount; w++)
                {
                    //if()
                    desc.Add(new MLB.InBetween(dataGridView2.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView2.Rows[w].Cells[4].Value.ToString()), "Out", dataGridView2.Rows[w].Cells[5].Value.ToString(), dataGridView2.Rows[w].Cells[0].Value.ToString(), dataGridView2.Rows[w].Cells[3].Value.ToString(), date, unidad));
                }

                for (int w = 0; w < dataGridView1.RowCount - 1; w++)
                {
                    parts.Add(new MLB.InBetween(dataGridView1.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView1.Rows[w].Cells[4].Value.ToString()), "Des", dataGridView2.Rows[0].Cells[5].Value.ToString(), dataGridView1.Rows[w].Cells[0].Value.ToString(), dataGridView1.Rows[w].Cells[3].Value.ToString(), date, unidad));

                }




                main.Ramaller(desc, parts);

                Close();
            }


        }

        private bool  HardValidar()
        {
            bool check = false;
            String msg = "";
            int grave = 0;
            int mgrave = 0;
            int warning = 0;


            int rept = 0;

            msg = Validar2() + "\n";

            for (int w= 0;w<dataGridView1.RowCount-1;w++)
            {

                for (int k = 0; k < dataGridView1.RowCount - 1;k++ )
                {
                    if (k!=w)
                    {
                        if (dataGridView1.Rows[w].Cells[0].Value.ToString() == dataGridView1.Rows[k].Cells[0].Value.ToString() && (!msg.Contains("- Nombre de productos repetidos en la lista (" + (k+1) + " y " + (w+1) + ")")))
                        {
                            rept++;
                            msg = msg + "- Nombre de productos repetidos en la lista (" + (w +1) + " y " + (k+1)+ ")\n";
                        }
                    }
                }

                 if (dbc.ExistQuerry("Select id From Producto Where Nombre ='"+dataGridView1.Rows[w].Cells[0].Value.ToString()+"' and Cuenta ='"+dataGridView1.Rows[w].Cells[5].Value.ToString()+"' and PrecIn='"+dataGridView1.Rows[w].Cells[2].Value.ToString()+"'"))
                 {
                     msg = msg+ "-Ya existe un Porducto en la base de datos con el Nombre: "+dataGridView1.Rows[w].Cells[0].Value.ToString()+" asociado a la Cuenta: "+dataGridView1.Rows[w].Cells[5].Value.ToString()+" con Precio: "+dataGridView1.Rows[w].Cells[2].Value.ToString()+"\n";
                     grave++;
                 }
                else
                 if (dbc.ExistQuerry("Select id From Producto Where Nombre ='"+dataGridView1.Rows[w].Cells[0].Value.ToString()+"' and Cuenta ='"+dataGridView1.Rows[w].Cells[5].Value.ToString()+"'"))
                 {
                     msg = msg + "-Ya existe un Porducto en la base de datos con el Nombre: " + dataGridView1.Rows[w].Cells[0].Value.ToString() + " asociado a la Cuenta: " + dataGridView1.Rows[w].Cells[5].Value.ToString() + "\n";
                     mgrave++;
                 }
                else
                 if (dbc.ExistQuerry("Select id From Producto Where Nombre ='"+dataGridView1.Rows[w].Cells[0].Value.ToString()+"'"))
                 {
                     msg = msg + "-Ya existe un Porducto en la base de datos con el Nombre: " + dataGridView1.Rows[w].Cells[0].Value.ToString() + "\n";
                     warning++;
                 }              
           
                 

            }



            if (grave==0&&mgrave==0&&rept==0&&Validar1(0))
            {
                if (warning>0)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				System.Windows.Forms.DialogResult result;


                result = MessageBox.Show(this, "Se han detectado las Advertencias siguientes: \n" + msg+"Desea continuar de todas formas?...", "Advertencia", buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                
                    for (int w = 0; w < dataGridView1.RowCount - 1; w++)
                    {
                        if (!dbc.ExistQuerry("Select id From Producto Where Nombre ='" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "' and Cuenta ='" + dataGridView1.Rows[w].Cells[5].Value.ToString() + "'"))
                        {
                            dbc.SimplePlan("INSERT INTO [Producto]([Id],[Nombre],[PrecIn],[PrecOut],[DUM],[Cuenta],[Moneda])VALUES" +
                       "('" + dbc.MaxQuerry("Producto") + "','" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "','" +
                       dataGridView1.Rows[w].Cells[2].Value.ToString() + "','" + "0.00" + "','" + dataGridView1.Rows[w].Cells[1].Value.ToString() + "','" +
                       dataGridView1.Rows[w].Cells[5].Value.ToString() + "','" + moneda + "')");

                        }

                    }
                    check = true;
                 }
                }
                else
                {
                    for (int w = 0; w < dataGridView1.RowCount - 1; w++)
                    {
                        if (!dbc.ExistQuerry("Select id From Producto Where Nombre ='" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "'"))
                        {
                              dbc.SimplePlan("INSERT INTO [Producto]([Id],[Nombre],[PrecIn],[PrecOut],[DUM],[Cuenta],[Moneda])VALUES" +
                         "('" + dbc.MaxQuerry("Producto") + "','" + dataGridView1.Rows[w].Cells[0].Value.ToString() + "','" +
                         dataGridView1.Rows[w].Cells[2].Value.ToString() + "','" + "0.00" + "','" + dataGridView1.Rows[w].Cells[1].Value.ToString() + "','" +
                         dataGridView1.Rows[w].Cells[5].Value.ToString() + "','" + moneda+ "')");
         
                        }



                    }

                    check = true;
                }
            }
            else{

                System.Windows.Forms.MessageBox.Show("Se han detectado los siguientes Errores al validar la operacion: \n"+msg+"Corrígalos para continuar");
                check = false;
            }


            return check;

        }

        private bool HardValidar2()
        {
            bool check = false;
            String msg = "";
            int grave = 0;
            int mgrave = 0;
            int warning = 0;


            int rept = 0;

            msg = Validar2() + "\n";

            for (int w = 0; w < dataGridView1.RowCount - 1; w++)
            {

                for (int k = 0; k < dataGridView1.RowCount - 1; k++)
                {
                    if (k != w)
                    {
                        if (GetData(dataGridView1.Rows[w].Cells[0].Value) == GetData(dataGridView1.Rows[k].Cells[0].Value) && (!msg.Contains("- Nombre de productos repetidos en la lista (" + (k + 1) + " y " + (w + 1) + ")")))
                        {
                            rept++;
                            msg = msg + "- Nombre de productos repetidos en la lista (" + (w + 1) + " y " + (k + 1) + ")\n";
                        }
                    }
                }

                if (dbc.ExistQuerry("Select id From Producto Where Nombre ='" + GetData(dataGridView1.Rows[w].Cells[0].Value) + "' and Cuenta ='" + GetData(dataGridView1.Rows[w].Cells[5].Value) + "' and PrecIn='" + GetData(dataGridView1.Rows[w].Cells[2].Value)+ "'"))
                {
                    msg = msg + "-Ya existe un Porducto en la base de datos con el Nombre: " + GetData(dataGridView1.Rows[w].Cells[0].Value) + " asociado a la Cuenta: " + GetData(dataGridView1.Rows[w].Cells[5].Value) + " con Precio: " + GetData(dataGridView1.Rows[w].Cells[2].Value) + "\n";
                    grave++;
                }
                else
                    if (dbc.ExistQuerry("Select id From Producto Where Nombre ='" + GetData(dataGridView1.Rows[w].Cells[0].Value) + "' and Cuenta ='" + GetData(dataGridView1.Rows[w].Cells[5].Value) + "'"))
                    {
                        msg = msg + "-Ya existe un Porducto en la base de datos con el Nombre: " + GetData(dataGridView1.Rows[w].Cells[0].Value) + " asociado a la Cuenta: " + GetData(dataGridView1.Rows[w].Cells[5].Value) + "\n";
                        mgrave++;
                    }
                    else
                        if (dbc.ExistQuerry("Select id From Producto Where Nombre ='" + GetData(dataGridView1.Rows[w].Cells[0].Value) + "'"))
                        {
                            msg = msg + "-Ya existe un Porducto en la base de datos con el Nombre: " + GetData(dataGridView1.Rows[w].Cells[0].Value) + "\n";
                            warning++;
                            
                        }



            }


            if (grave == 0 && mgrave == 0 && rept == 0 && Validar1(0))
            {
                check = true;
                if (warning > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Se han detectado los siguientes Advertencias al validar la operacion: \n" + msg + "Se Recomienda Corregirlos para Continuar");
                }
                else
                    System.Windows.Forms.MessageBox.Show("No se han detectado Errores ni Advertencias: \n" + msg + "Listo para Continuar");
            }
            else{
                System.Windows.Forms.MessageBox.Show("Se han detectado los siguientes Errores al validar la operacion: \n" + msg + "Corrígalos para continuar");
                check = false;
          

            }
            
               


            return check;

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (grid&&numericUpDown2.Value <= 100&&numericUpDown2.Value >=1)
            {
                percent = System.Convert.ToInt32(numericUpDown2.Value);

                oricant = System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString());

                restcant = (oricant * System.Convert.ToDouble(100 - numericUpDown2.Value)) / 100;
                newcant = (oricant * System.Convert.ToDouble(numericUpDown2.Value.ToString())) / 100;

                numericUpDown2.BackColor = Color.White;

//                if (dataGridView2.RowCount == 1)
//                {

//                    dataGridView2.Rows.Add(dataGridView2.Rows[0].Cells[0].Value.ToString(), dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(), restcant, dataGridView2.Rows[0].Cells[4].Value.ToString(), dataGridView2.Rows[0].Cells[5].Value.ToString());
//                    dataGridView2.Rows.Add(dataGridView2.Rows[0].Cells[0].Value.ToString(), dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(), newcant, dataGridView2.Rows[0].Cells[4].Value.ToString(), dataGridView2.Rows[0].Cells[5].Value.ToString());
//                   // grid = false;
////                     dataGridView2.Rows.RemoveAt(1);
////                     dataGridView2.Rows.RemoveAt(1);
//                }
               
                dataGridView2.Rows[1].Cells[3].Value = main.CantFixer3(dataGridView2.Rows[1].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round(restcant, 2)));
                dataGridView2.Rows[1].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[1].Cells[3].Value.ToString()) * System.Convert.ToDouble(dataGridView2.Rows[1].Cells[2].Value.ToString()), 2));


                dataGridView2.Rows[2].Cells[3].Value = main.CantFixer3(dataGridView2.Rows[2].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round(newcant, 2)));
                dataGridView2.Rows[2].Cells[4].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(dataGridView2.Rows[2].Cells[3].Value.ToString()) * System.Convert.ToDouble(dataGridView2.Rows[2].Cells[2].Value.ToString()), 2));


                for (int w = 0; w < 6; w++)
                {
                    //dataGridView2.Rows[0].Cells[w].Style.BackColor = Color.wi;
                    dataGridView2.Rows[0].Cells[w].ToolTipText = "Datos del Producto Original que se va Descomponer";
                }
                for (int w = 0; w < 6; w++)
                {
                    dataGridView2.Rows[1].Cells[w].Style.BackColor = Color.AliceBlue;
                    dataGridView2.Rows[1].Cells[w].ToolTipText = "Datos del Producto Restante luego de Descomponer";
                }
                for (int w = 0; w < 6; w++)
                {
                    dataGridView2.Rows[2].Cells[w].Style.BackColor = Color.LightBlue;
                    dataGridView2.Rows[2].Cells[w].ToolTipText = "Datos del Producto que se va a Descomponer";
                }

                dataGridView2.Rows[0].Tag = "100";
                dataGridView2.Rows[1].Tag = (100-percent).ToString();
                dataGridView2.Rows[2].Tag = percent.ToString();



            }

            if (!grid&&dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {

                if (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value != null)
                {
                    if (dataGridView2.RowCount == 3)
                    {
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = main.CantFixer3(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString(), System.Convert.ToString(System.Math.Round((System.Convert.ToDouble(dataGridView2.Rows[2].Cells[3].Value.ToString()) * System.Convert.ToDouble(numericUpDown2.Value)) / 100, 2)));
                        dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag = numericUpDown2.Value;
                    }
                    if (dataGridView2.RowCount == 1)
                    {
                      dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value  = main.CantFixer3(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString(),System.Convert.ToString(System.Math.Round((System.Convert.ToDouble(dataGridView2.Rows[0].Cells[3].Value.ToString()) * System.Convert.ToDouble(numericUpDown2.Value) ) / 100,2)));
                      dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag = numericUpDown2.Value;
                    }

                }
            }
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

                numericUpDown2.Value = System.Convert.ToDecimal(percent);

                grid = true;
                textBox5.Enabled = false;
                comboBox4.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox6.Enabled = false;
                comboBox3.Enabled = false;   
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PrepareDes();
            MLB.Imprimir imp = new MLB.Imprimir();
            imp.table = "Descomponer";
            imp.Show(this);

           
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
               ",'" + GetData(dataGridView2.Rows[w].Cells[0].ToolTipText) + "','" + GetData(dataGridView2.Rows[w].Tag)+"%" + "','" + unidad + "','" + moneda + "','" + date + "','"
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
               ",'" + GetData("Producto resultante de la Descomposicion") + "','" + GetData(dataGridView1.Rows[w].Tag)+"%" + "','" + unidad + "','" + moneda + "','" + date + "','"
               + System.Environment.MachineName + "')");

            }

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aceptarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HardValidar())
            {

                System.Collections.ArrayList desc = new System.Collections.ArrayList();

                System.Collections.ArrayList parts = new System.Collections.ArrayList();


                for (int w = 0; w < dataGridView2.RowCount; w++)
                {
                    desc.Add(new MLB.InBetween(dataGridView2.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView2.Rows[w].Cells[4].Value.ToString()), "Out", dataGridView2.Rows[w].Cells[5].Value.ToString(), dataGridView2.Rows[w].Cells[0].Value.ToString(), dataGridView2.Rows[w].Cells[3].Value.ToString(), "", ""));
                }

                for (int w = 0; w < dataGridView1.RowCount - 1; w++)
                {
                    parts.Add(new MLB.InBetween(dataGridView1.Rows[w].Cells[5].Value.ToString(), System.Convert.ToDouble(dataGridView1.Rows[w].Cells[4].Value.ToString()), "Des", dataGridView2.Rows[0].Cells[5].Value.ToString(), dataGridView1.Rows[w].Cells[0].Value.ToString(), dataGridView1.Rows[w].Cells[3].Value.ToString(), "", ""));

                }




                main.Ramaller(desc, parts);

                Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete && dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedCells[0].RowIndex < dataGridView1.RowCount - 1)
            {
                //toolStripStatusLabel2.Text = " Eliminando...";
//                 if (conceptos.Rows[conceptos.SelectedCells[0].RowIndex].Tag != null)
//                 {
                //dbc.SimplePlan("DELETE [Concepto] WHERE [Id] = '" + dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag.ToString() + "'");
                    //  dbc.SimplePlan("DELETE [UndProd] WHERE [Producto] = '" + MainProducto.Rows[MainProducto.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "'");

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
             /*   }*/
               // toolStripStatusLabel2.Text = " Eliminado...";
            }
        }

        private void menúToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            menúToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void menúToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
                menúToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }
       

      
    }
}
