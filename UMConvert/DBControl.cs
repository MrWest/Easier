using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace UMConvert
{
    public class DBControl
    {
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        //private System.String fileName;
        private System.String Conex;

        public DBControl()
        {

            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();

            Conex = "Data Source=ENRIKE\\SQLEXPRESS;Initial Catalog=MLB;Integrated Security=False; User Id=killer; Password=killer";


            Conex = LoadOnInit();

            this.sqlConnection1.ConnectionString = Conex;


            this.sqlCommand1.Connection = this.sqlConnection1;
            sqlCommand1.Parameters.Clear();
            sqlCommand1.CommandType = System.Data.CommandType.Text;

            sqlConnection1.Close();
            // alow=true;
        }

        public System.String LoadOnInit()
        {
            System.String cxn = "";
            try
            {
                StreamReader sr = File.OpenText( Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName)+"\\fileAddress.cfg");
            sr.BaseStream.Position = 0;
            //bool can=sr.BaseStream.CanRead;
            if (sr.ReadToEnd() != "")
            {
                sr.BaseStream.Position = 0;
                cxn = sr.ReadToEnd();

            }
            sr.Close();
            }
            catch (System.Exception ex)
            {
                
             // System.Windows.Forms.MessageBox.Show( ex.Message, "Easier " + ex.Source , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
              
            }
            
           
            return cxn;

        }
        public System.Data.DataSet SelectQuerryFixed(System.String sel)
        {
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataSet dts = new System.Data.DataSet();
            try
            {      
            
                sqlCommand1.Parameters.Clear();

                sqlCommand1.CommandText = sel;

                sqlCommand1.Prepare();

                sqlConnection1.Open();


               
                
                adapter.SelectCommand = sqlCommand1;
                adapter.Fill(dts);

                sqlConnection1.Close();
                
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return dts;
        }
      public  bool ExistQuerry( System.String   sel )
        {
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataSet dts = new System.Data.DataSet();
          try
          {
              sqlCommand1.Parameters.Clear();

              sqlCommand1.CommandText = sel;

              sqlCommand1.Prepare();

              sqlConnection1.Open();


             

              adapter.SelectCommand = sqlCommand1;
              adapter.Fill(dts);

              sqlConnection1.Close();

              if (dts.Tables[0].Rows.Count > 0)
              {
                  return true;
              }

          }
          catch (System.Exception ex)
          {
             // System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
          }
         
          return false; 
        }
   

        public void SimplePlan( System.String  querry )
        {
            try
            {
            System.Data.SqlClient.SqlDataAdapter   adapter = new System.Data.SqlClient.SqlDataAdapter() ;

	        sqlCommand1.Parameters.Clear();

	        sqlCommand1.CommandText  ="SELECT * FROM Clase";
	        adapter.SelectCommand =sqlCommand1;

	        sqlCommand1.CommandText  = querry;

	        sqlCommand1.Prepare();

	        sqlConnection1.Open();

	        //sqlCommand1.ExecuteReader();

	        System.Data.Common.DataTableMapping   tableMapping = new System.Data.Common.DataTableMapping() ;
	        tableMapping.SourceTable = "Table";
	        tableMapping.DataSetTable = "myTable";


	        adapter.UpdateCommand = sqlCommand1;
	        adapter.UpdateCommand.Connection = sqlConnection1;
	        adapter.UpdateCommand.CommandType= System.Data.CommandType.Text;

	        System.Data.DataSet   dts = new System.Data.DataSet();

	        adapter.InsertCommand = sqlCommand1;


	        adapter.Fill(dts);

	        adapter.TableMappings.Add(tableMapping);

	        //int result = adapter.Update(dts.Tables[0]);

	        sqlConnection1.Close();
            }
            catch (System.Exception ex)
            {
              //  System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
	       

        }
        public System.Data.SqlClient.SqlConnection GetConnection()
        {
            return sqlConnection1;
        }
    public System.String  MaxQuerry(System.String table)
        {

        try
        {
            sqlCommand1.Parameters.Clear();

            sqlCommand1.CommandText = "SELECT MAX(Id) FROM [MLB].[dbo].[" + table + "]";

            sqlCommand1.Prepare();

            sqlConnection1.Open();


            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataSet dts = new System.Data.DataSet();

            adapter.SelectCommand = sqlCommand1;
            adapter.Fill(dts);
            System.String number = dts.Tables[0].Rows[0].ItemArray[0].ToString();

            sqlConnection1.Close();

            if (number == "")
            {
                int n = 1;
                return n.ToString();
            }
            int num = System.Convert.ToInt32(number) + 1;

            return num.ToString();
        }
        catch (System.Exception ex)
        {
           // System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        return "0";


        }

       

    }
}
