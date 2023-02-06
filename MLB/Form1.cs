using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Security.Policy;
using System.Security.Permissions;
//using UMConvert;



namespace MLB
{
     struct HlpMgr 
    {
        public String topic;
        public int tnumber;
        public HelpKind hlpknd;
        public ErrorKind errknd;
        public ArrayList atext;

    }

    public partial class Form1 : Form
    {
        private MLB.DBControl dbc;
        private System.Data.DataSet dts;
        //private bool saved;
        // private bool change;
        private ArrayList IPVSaver;
        private ArrayList RamalSaver;
        private ArrayList SMSaver;
        private ArrayList RISaver;
        private ArrayList Transfers;
        private ArrayList Bkptransf;
        private ArrayList FlujoSaver;


        private ArrayList FCSaver;
        private ArrayList Existences;
        private ArrayList Ajustes;
        private ArrayList AjustesInRamal;
        private ArrayList CopyPaste;
        private Init init;
        private int secs;

        private bool go;
        private bool godeep;

        private bool go2;
        //private bool godeep2;

        private bool ldipv;
        private int rower;
        private int counter;
        private bool ajinram;
        private System.Globalization.NumberFormatInfo provider;
        private System.Globalization.CultureInfo cinf;
        private System.Drawing.Point mouse;
        private MessageBoxButtons buttons;
        private System.Windows.Forms.DialogResult result;
        public bool salir;

        // Ayuda con Eve

        private MLB.EAHelp ea;
        public bool hlpeve;
        public int hlptimer;
        public int tnumber;
        public String hlptopic;
        private HlpMgr hlpManager;
        private System.Windows.Forms.StatusBarPanel helpsb;

        private bool failed;

        //private double rango;
        public Form1()
        {
            InitializeComponent();
            dbc = new MLB.DBControl();
            //IPVSaver = new IList<MLB.ValueSaver>();
            IPVSaver = new ArrayList();
            RamalSaver = new ArrayList();
            SMSaver = new ArrayList();
            RISaver = new ArrayList();
            Transfers = new ArrayList();
            Bkptransf = new ArrayList();
            FlujoSaver = new ArrayList();
            FCSaver = new ArrayList();
            Existences = new ArrayList();
            Ajustes = new ArrayList();
            AjustesInRamal = new ArrayList();
            CopyPaste = new ArrayList();
            init = new Init(3);
            go = true;
            godeep = true;

            ajinram = false;
            salir = false;

            go2 = true;
            //  godeep2 = true;
            rower = 0;
            provider = new System.Globalization.NumberFormatInfo();
            cinf = new System.Globalization.CultureInfo("en-US");
            provider.CurrencyDecimalSeparator = ".";
            provider.CurrencyGroupSeparator = ",";
            provider.CurrencyGroupSizes = new int[] { 3 };
            cinf.NumberFormat = provider;

            MLB.Properties.Resources.Culture = cinf;
            Application.CurrentCulture = cinf;

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            hlpeve = false;
            hlptimer = 0;
            tnumber = 1;
            hlptopic = "Saludo";
            helpsb = new System.Windows.Forms.StatusBarPanel();

            failed = false;
            
          //  System.Windows.Forms.DialogResult result;

            // rango = 0.5;


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;

            init.Visible = true;
            secs = 6;
            timer1.Enabled = true;

            //if ( UserRigths() && File.Exists(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName.Replace("Easier.exe", "") + "Modules\\sqlexpr32.exe"))
            //{
            //    FileInfo fi = new FileInfo(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName.Replace("Easier.exe", "") + "Modules\\sqlexpr32.exe");
            //    fi.Delete();
            //}

            //int idx =tableLayoutPanel6.Controls.IndexOf(helpcomm);
            //helpsb.Dock = DockStyle.Fill;

            //tableLayoutPanel6.GetCellPosition(helpcomm);
            

            //this.helpProvider1.SetShowHelp(this.button2, true);
            //this.helpProvider1.SetHelpString(this.button2, "Para Guardar el Documento Seleccionado, Click en este botón.");

            //this.helpProvider1.SetShowHelp(this.button3, true);
            //this.helpProvider1.SetHelpString(this.button3, "Para Imprimir el Documento Seleccionado, Click en este botón.");

            //this.helpProvider1.SetShowHelp(this.button1, true);
            //this.helpProvider1.SetHelpString(this.button1, "Para Borrar el Documento Seleccionado, Click en este botón.");

            //this.helpProvider1.HelpNamespace = System.Environment.CurrentDirectory + "\\Help\\index.html";



        }
        private bool UserRigths()
        {
            
            //  System.Security.Principal.WindowsIdentity

            // System.Security.Principal.NTAccount tacc = new System.Security.Principal.NTAccount(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            //  String k = tacc.Value;

            //  lresultados.Items.Clear();
            AppDomain myDomain = System.Threading.Thread.GetDomain();

            myDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);

            System.Security.Principal.WindowsPrincipal myPrincipal = (System.Security.Principal.WindowsPrincipal)System.Threading.Thread.CurrentPrincipal;
            Array wbirFields = Enum.GetValues(typeof(System.Security.Principal.WindowsBuiltInRole));
            bool admin = false;
            foreach (object roleName in wbirFields)
            {
                try
                {
                    // Cast the role name to a RID represented by the WindowsBuildInRole value.
                    if (roleName.ToString() == "Administrator")
                        if (myPrincipal.IsInRole((System.Security.Principal.WindowsBuiltInRole)roleName))
                        {
                            admin = true;
                            break;
                        }

                }
                catch (Exception)
                {
                    dbc.CloseConnection();
                    // lresultados.Items.Add("{0}: Could not obtain role for this RID."+roleName);
                }
            }

            // System.Security.Principal.GenericPrincipal gp = new System.Security.Principal.GenericPrincipal(myPrincipal,)

          
            //if (!System.Security.SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.ControlPrincipal)))
            //{
            //    lbit.Add("[W03] -El Sistema no se esta ejecutando con privilegios de Administración...");

            //}
            return admin;

        }

        public String DateCultureConverter(System.DateTime mtime)
        {

            return mtime.Month.ToString() + "/" + mtime.Day.ToString() + "/" + mtime.Year.ToString();
        }
        private void LoadRamal20()
        {
            try
            {
                rower = 0;
                ramal20Base.Rows.Clear();
                //  System.Data.DataSet dts;
                if (!IsCadena(tabControl2.SelectedTab.Text))
                {

                    if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
                    {
                        //Fill
                        dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte]" +
                       ",[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Id] FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' Order by [Id]");

                        godeep = false;
                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            ramal20Base.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString()), dts.Tables[0].Rows[w].ItemArray[6].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString()),
                                dts.Tables[0].Rows[w].ItemArray[8].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[9].ToString()), dts.Tables[0].Rows[w].ItemArray[10].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[11].ToString()), dts.Tables[0].Rows[w].ItemArray[12].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[13].ToString()), dts.Tables[0].Rows[w].ItemArray[14].ToString(), dts.Tables[0].Rows[w].ItemArray[15].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[16].ToString()), dts.Tables[0].Rows[w].ItemArray[17].ToString());

                            //   CantFixer(dts.Tables[0].Rows[w].ItemArray[1].ToString(), ramal20Base.Rows.Count - 1);

                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[18].ToString();

                        }

                        RamalIPVAdjust(ramal20Base);



                        dts = dbc.SelectQuerryFixed("SELECT SUM([IImporte]),SUM([EImporte]), SUM([EIImporte]),SUM([SImporte]), SUM([SIImporte])," +
                      "SUM([TImporte]),SUM([FImporte]) FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[4].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[6].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[8].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[10].Value = dts.Tables[0].Rows[0].ItemArray[3].ToString();
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[12].Value = dts.Tables[0].Rows[0].ItemArray[4].ToString();
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[14].Value = dts.Tables[0].Rows[0].ItemArray[5].ToString();
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value = dts.Tables[0].Rows[0].ItemArray[6].ToString();

                        godeep = true;
                        // IntTransf2();
                        //  Transfer2Ramal();
                        PutIn(RamalSaver, ramal20Base);
                        ReBindValuesSaved(ramal20Base);
                        if (tabControl1.SelectedTab.Text == "Ramal 20")
                            tabControl1.TabPages[0].BackColor = Color.LightGray;
                       
                    }
                    else
                    {
                        dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecIn] FROM [MLB].[dbo].[Producto] INNER JOIN [MLB].[dbo].[UndProd] ON Producto.Nombre = UndProd.Producto WHERE [Cuenta] = '" + comboBox1.Text + "' AND Moneda = '" + GetMoneda() + "' AND UName = '" + tabControl2.SelectedTab.Text + "' AND Producto.Nombre Not Like '%(Defensa)%' ORDER BY UndProd.Id");

                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            ramal20Base.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00");
                        }

                        dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecIn] FROM [MLB].[dbo].[Producto] INNER JOIN [MLB].[dbo].[UndProd] ON Producto.Nombre = UndProd.Producto WHERE [Cuenta] = '" + comboBox1.Text + "' AND Moneda = '" + GetMoneda() + "' AND UName = '" + tabControl2.SelectedTab.Text + "'AND Producto.Nombre Like '%(Defensa)%' ORDER BY UndProd.Id");

                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            ramal20Base.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00");
                        }

                        RamalIPVAdjust(ramal20Base);

                        bool put = false;
                        dts = dbc.SelectQuerryFixed("SELECT Ramal20.[Producto],[UM],Producto.[PrecIn] ,[FCantidad],[FImporte]FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre INNER JOIN [UndProd] ON [UndProd].Producto = Producto.Nombre WHERE Ramal20.UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Not Like '%(Defensa)%' ORDER BY Ramal20.Id");

                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {

                            int idx = Search4(ramal20Base, dts.Tables[0].Rows[w].ItemArray[0].ToString());
                            ramal20Base.Rows.Insert(idx, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), System.Convert.ToString(dts.Tables[0].Rows[w].ItemArray[3].ToString(), provider)), dts.Tables[0].Rows[w].ItemArray[4].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString());
                            ramal20Base.Rows.RemoveAt(idx + 1);
                            // CantFixer(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);

                            if (idx > ramal20Base.RowCount - 10)
                                ramal20Base.Rows.Insert(ramal20Base.RowCount - 1);

                            put = true;
                        }
                        //// bool put = false;
                        dts = dbc.SelectQuerryFixed("SELECT Ramal20.[Producto],[UM],Producto.[PrecIn] ,[FCantidad],[FImporte]FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre INNER JOIN [UndProd] ON [UndProd].Producto = Producto.Nombre WHERE Ramal20.UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Like '%(Defensa)%' ORDER BY Ramal20.Id");

                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {

                            int idx = Search4(ramal20Base, dts.Tables[0].Rows[w].ItemArray[0].ToString());
                            ramal20Base.Rows.Insert(idx, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString());
                            ramal20Base.Rows.RemoveAt(idx + 1);
                            // CantFixer(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);

                            if (idx > ramal20Base.RowCount - 10)
                                ramal20Base.Rows.Insert(ramal20Base.RowCount - 1);

                            put = true;
                        }


                        IntTransf2();
                        Transfer2Ramal();

                        PutIn(RamalSaver, ramal20Base);




                        ReBindValues(ramal20Base);

                        int k = 0;
                        bool ya = true;
                        ajinram = false;
                        while (k < ramal20Base.RowCount - 2 && GetData(ramal20Base.Rows[k].Cells[0].Value) != " " && comboBox1.Text != "")
                        {
                            ramal20Base.Rows[k].Cells[0].ReadOnly = true;
                            if (ya && k > 0 && GetData(ramal20Base.Rows[k].Cells[0].Value).Contains("(Defensa)"))
                            {
                                ramal20Base.Rows[k - 1].DividerHeight = ramal20Base.Rows[k].DividerHeight + 3;
                                ya = false;
                            }
                            Brassier(GetData(ramal20Base.Rows[k].Cells[0].Value), GetNumData(ramal20Base.Rows[k].Cells[2].Value), k);
                            //  CargarCuentaRamal(GetData(ramal20Base.Rows[k].Cells[0].Value), k);
                            k++;
                            //ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);

                        }


                        //edit in ramal
                        ramal20Base.Rows[ramal20Base.Rows.Count - 2].DividerHeight = 2;
                        if (comboBox1.Text != "")
                            ramal20Base.Rows[k].Cells[0].ReadOnly = false;

                        if (ajinram)
                        {
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value = GetTotal(ramal20Base, 8);
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[10].Value = GetTotal(ramal20Base, 10);
                        }


                        if (put)
                        {
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[4].Value = GetTotal(ramal20Base, 4);
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[17].Value = GetTotal(ramal20Base, 17);
                        }
                        if (tabControl1.SelectedTab.Text == "Ramal 20")
                            tabControl1.TabPages[0].BackColor = Color.Transparent;
                        go = true;
                        // ReCheck();
                        forzar.Enabled = false;
                    }

                    
                }
                else
                {

                    if (comboBox1.Text != "")
                    {

                        System.Data.DataSet dts12 = dbc.SelectQuerryFixed("SELECT DISTINCT(UniCuenta.Unidad) FROM Producto INNER JOIN UniCuenta ON Producto.Cuenta=UniCuenta.Cuenta WHERE Producto.Moneda ='" + GetMoneda() + "'");

                        for (int kp = 0; kp < dts12.Tables[0].Rows.Count; kp++)
                        {

                            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + dts12.Tables[0].Rows[kp].ItemArray[0].ToString() + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
                            {
                                //Fill
                                dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte]" +
                               ",[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Id] FROM Ramal20 WHERE UName ='" + dts12.Tables[0].Rows[kp].ItemArray[0].ToString() + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' Order by [Id]");

                                godeep = false;
                                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                                {
                                    ramal20Base.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString()), dts.Tables[0].Rows[w].ItemArray[6].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString()),
                                        dts.Tables[0].Rows[w].ItemArray[8].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[9].ToString()), dts.Tables[0].Rows[w].ItemArray[10].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[11].ToString()), dts.Tables[0].Rows[w].ItemArray[12].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[13].ToString()), dts.Tables[0].Rows[w].ItemArray[14].ToString(), dts.Tables[0].Rows[w].ItemArray[15].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[16].ToString()), dts.Tables[0].Rows[w].ItemArray[17].ToString());

                                    //   CantFixer(dts.Tables[0].Rows[w].ItemArray[1].ToString(), ramal20Base.Rows.Count - 1);

                                    ramal20Base.Rows[ramal20Base.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[18].ToString();
                                    ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[0].ToolTipText = dts12.Tables[0].Rows[kp].ItemArray[0].ToString();

                                }

                                RamalIPVAdjustII(ramal20Base);



                                dts = dbc.SelectQuerryFixed("SELECT SUM([IImporte]),SUM([EImporte]), SUM([EIImporte]),SUM([SImporte]), SUM([SIImporte])," +
                              "SUM([TImporte]),SUM([FImporte]) FROM Ramal20 WHERE UName ='" + dts12.Tables[0].Rows[kp].ItemArray[0].ToString() + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[4].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[6].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[8].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();
                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[10].Value = dts.Tables[0].Rows[0].ItemArray[3].ToString();
                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[12].Value = dts.Tables[0].Rows[0].ItemArray[4].ToString();
                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[14].Value = dts.Tables[0].Rows[0].ItemArray[5].ToString();
                                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value = dts.Tables[0].Rows[0].ItemArray[6].ToString();

                                for (int w = 0; w < 3; w++)
                                {
                                    ramal20Base.Rows.Add();

                                    ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[0].ReadOnly = true;
                                }

                                godeep = true;
                                // IntTransf2();
                                //  Transfer2Ramal();

                            }
                        }
                        if (ramal20Base.Rows.Count > 0)
                        {

                            RamalIPVAdjustII(ramal20Base);
                            godeep = false;
                            dts = dbc.SelectQuerryFixed("SELECT SUM([IImporte]),SUM([EImporte]), SUM([EIImporte]),SUM([SImporte]), SUM([SIImporte])," +
                                      "SUM([TImporte]),SUM([FImporte]) FROM Ramal20 WHERE   Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[4].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[6].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[8].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();
                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[10].Value = dts.Tables[0].Rows[0].ItemArray[3].ToString();
                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[12].Value = dts.Tables[0].Rows[0].ItemArray[4].ToString();
                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[14].Value = dts.Tables[0].Rows[0].ItemArray[5].ToString();
                            ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value = dts.Tables[0].Rows[0].ItemArray[6].ToString();

                            godeep = true;

                            PutIn(RamalSaver, ramal20Base);
                            //  ReBindValuesSaved(ramal20Base);
                            if (tabControl1.SelectedTab.Text == "Ramal 20")
                                tabControl1.SelectedTab.BackColor = Color.LightGray;
                           if(tabControl1.SelectedIndex == 0&&editarVariablesToolStripMenuItem.Enabled == true)
                            forzar.Enabled = true;
                        }

                    }
                }

                if (!IsCadena(tabControl2.SelectedTab.Text))
                {
                    if (tabControl1.SelectedIndex == 0)
                        BigValidarRamal();
                }

                if (tabControl1.SelectedIndex == 0&&tabControl1.SelectedTab.BackColor == Color.LightGray)
                {
                     if (  editarVariablesToolStripMenuItem.Enabled)
                        {
                            forzar.Enabled = true;


                        }
                    LockRamal(true);

                       
                }
               
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }



        }

        private void LockRamal(bool lkey)
        {
            for (int w = 0; w < ramal20Base.Rows.Count; w++)
            {
                for (int k = 0; k < ramal20Base.ColumnCount; k++)
                {
                    if (k == 0 || k == 2 || k == 3 || k == 5 || k == 7 || k == 9 || k == 11 || k == 13 || k == 15 || k == 16)
                    {
                        ramal20Base.Rows[w].Cells[k].ReadOnly = lkey;
                    }

                }

                if (ramal20Base.Rows[w].Cells[0].Value == null)
                {
                    break;
                }
            }
        }
        public void Ramaller(ArrayList desc, ArrayList parts)
        {

            InBetween value;
            if (desc.Count > 1)
            {
                value = (InBetween)desc[2];
                //int idx = Search4(ramal20Base, value.producto);

                Bkptransf.Add(value);

                // Transfers.Add(new InBetween(value.Cuenta, 0, "In", value.Cuenta, value.producto, "0.00", value.eprod, value.ipv));


            }
            else
            {

                value = (InBetween)desc[0];
                // int idx = Search4(ramal20Base, value.producto);

                Bkptransf.Add(value);

                //   Transfers.Add(new InBetween(value.Cuenta, 0, "In", value.Cuenta, value.producto, "0.00", value.eprod, value.ipv));

            }


            foreach (InBetween ib in parts)
            {
                Bkptransf.Add(ib);
                //if (ib.Cuenta != value.Cuenta)
                //{
                //    Bkptransf.Add(new InBetween(ib.Cuenta, ib.Saldo, "Des", value.Cuenta, ib.producto, ib.cant, "", ""));
                //    Bkptransf.Add(new InBetween(value.Cuenta, ib.Saldo, "Out", ib.Cuenta, ib.producto, ib.cant, "", ""));
                //}
            }



            LoadRamal20();



        }
        public void Ramaller2(ArrayList desc, ArrayList parts)
        {


            foreach (InBetween value in desc)
            {
                Bkptransf.Add(value);
            }



            foreach (InBetween ib in parts)
            {
                Bkptransf.Add(ib);
            }



            LoadRamal20();



        }

        private void Brassier(String prod, String prec, int row)
        {
            foreach (ValueSaver val in AjustesInRamal)
            {
                if (val.producto == prod && val.cant == prec && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    ramal20Base.Rows[row].Cells[8].Value = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[8].Value)) + System.Math.Round(System.Convert.ToDouble(val.col) / 100, 2);
                    ramal20Base.Rows[row].Cells[10].Value = ramal20Base.Rows[row].Cells[8].Value;
                    ajinram = true;
                    // return true;
                }
            }
            // return false;
        }


        private void ReCheck()
        {
            int k = 0;
            go = true;

            while (k < ramal20Base.RowCount - 2 && ramal20Base.Rows[k].Cells[0].Value != null)
            {

                if (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) == GetRCant(GetData(ramal20Base.Rows[k].Cells[0].Value)))
                {
                    if (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[10].Value)) != GetRImp(GetData(ramal20Base.Rows[k].Cells[0].Value)))
                    {
                        ramal20Base.Rows[k].Cells[10].Value = System.Convert.ToString(GetRImp(GetData(ramal20Base.Rows[k].Cells[0].Value)));
                        ramal20Base.Rows[k].Cells[10].Style.ForeColor = Color.DarkBlue;
                        ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[10].Value = GetTotal(ramal20Base, 10);

                        if (System.Convert.ToString(ramal20Base.Rows[k].Cells[9].Value) == System.Convert.ToString(ramal20Base.Rows[k].Cells[7].Value))
                        {
                            ramal20Base.Rows[k].Cells[8].Value = System.Convert.ToString(GetRImp(GetData(ramal20Base.Rows[k].Cells[0].Value)));
                            ramal20Base.Rows[k].Cells[8].Style.ForeColor = Color.DarkBlue;
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value = GetTotal(ramal20Base, 8);

                        }

                        if (comboBox1.Text == "" && System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) == (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[16].Value)) * -1))
                        {
                            ramal20Base.Rows[k].Cells[17].Value = ramal20Base.Rows[k].Cells[10].Value;
                            ramal20Base.Rows[k].Cells[17].Style.ForeColor = Color.DarkBlue;
                            ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[17].Value = GetTotal(ramal20Base, 17);
                        }

                    }
                    else
                    {
                        ramal20Base.Rows[k].Cells[10].Style.ForeColor = Color.Black;
                        if (comboBox1.Text == "")
                        {
                            ramal20Base.Rows[k].Cells[17].Style.ForeColor = Color.Black;
                        }

                    }
                }

                k++;
            }



        }

        private void RePutIn()
        {
            if (RamalSaver != null)
            {
                // godeep = false;
                int max = -1;

                //  bool jet = false;
                for (int w = 0; w < RamalSaver.Count; w++)
                {
                    ValueSaver val = (ValueSaver)RamalSaver[w];
                    if (val.IPVName == tabControl1.SelectedTab.Text)
                    {
                        //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                        if (val.row < Search4(ramal20Base, "#$-") && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                        {
                            //go = false;
                            //if (val.cant == "15")
                            //{
                            //    go = false;
                            //}
                            ramal20Base.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                            if (ramal20Base.RowCount - 10 < val.row)
                                ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
                            //go = true;

                            //  jet = true;
                        }
                        if (val.row > max && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            max = val.row;
                    }
                }


            }
        }

        private void PutIn(ArrayList TbSaver, DataGridView dtgv)
        {
            if (TbSaver != null)
            {
                // godeep = false;
                int max = -1;

                //  bool jet = false;
                for (int w = 0; w < TbSaver.Count; w++)
                {
                    ValueSaver val = (ValueSaver)TbSaver[w];
                    String tabname;
                    if (dtgv.ColumnCount > 15)
                    {
                        tabname = tabControl1.TabPages[0].Text;
                    }
                    else
                        tabname = tabControl1.SelectedTab.Text;
                    if (val.IPVName == tabname)
                    {
                        //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                        if (comboBox1.Text == "")
                        {
                            if (val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            {
                                //go = false;
                                //if (val.cant == "15")
                                //{
                                //    go = false;
                                //}
                                //if (val.cant.Contains("50"))
                                //{
                                //    int y = 99;
                                //}
                                //if (!tabControl1.SelectedTab.Text.Contains("IPV "))
                                if (val.row != -1 && val.col != -1 && val.row < dtgv.RowCount)
                                {
                                    go = true;
                                    //}
                                    if (val.cant == "0")
                                    {
                                        if (val.producto != GetData(dtgv.Rows[val.row].Cells[0].Value) && comboBox1.Text != "")
                                        {
                                           // System.Windows.Forms.MessageBox.Show(this,"Ocurrió un error de sincronización, con el producto:"+val.producto+".\nPor favor Revise y Reintroduzca los Datos del producto.");
                                            dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        }else{
                                        val.cant = "0.00";
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        dtgv.Rows[val.row].Cells[val.col].Value = "0";
                                         }
                                    }
                                    else
                                    {
                                        if (val.producto != GetData(dtgv.Rows[val.row].Cells[0].Value) && comboBox1.Text != "")
                                        {
                                           // System.Windows.Forms.MessageBox.Show(this,"Ocurrió un error de sincronización, con el producto:"+val.producto+".\nPor favor Revise y Reintroduzca los Datos del producto.");
                                            dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        }
                                        else
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                    }

                                }


                                if (dtgv.RowCount - 10 < val.row)
                                    dtgv.Rows.Insert(dtgv.RowCount - 2);
                                go = false;
                                //go = true;

                                //  jet = true;
                            }
                        }
                        else
                        {
                            if ((val.row <= Search4(dtgv, "#$-") || Search4(dtgv, "#$-") == 0) && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            {

                                if (val.row != -1 && val.col != -1 && val.row < dtgv.RowCount &&(val.col>2||tabControl1.SelectedIndex!=0))
                                {
                                    go = true;
                                    //}
                                    if (val.cant == "0")
                                    {
                                        if (val.producto != GetData(dtgv.Rows[val.row].Cells[0].Value) && comboBox1.Text != "")
                                        {
                                           // System.Windows.Forms.MessageBox.Show(this, "Ocurrió un error de sincronización, con el producto:" + val.producto + ".\nPor favor Revise y Reintroduzca los Datos del producto.");
                                            dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        }
                                        else
                                        {
                                            val.cant = "0.00";
                                            dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                            dtgv.Rows[val.row].Cells[val.col].Value = "0";
                                        }
                                    }
                                    else
                                    {
                                        if (tabControl1.SelectedIndex==0&&(val.col == 9 || val.col == 10 || val.col == 7 || val.col == 8 || val.col == 11 || val.col == 12))
                                        {

                                            if (dbc.ExistQuerry("Select PrecOut From Producto INNER JOIN UndProd On Producto.Nombre = UndProd.Producto Where Nombre ='" + val.producto + "' And Cuenta = '" + comboBox1.Text + "' And PrecOut != 0"))
                                            {
                                                if (val.producto != GetData(dtgv.Rows[val.row].Cells[0].Value) && comboBox1.Text != "")
                                                {
                                                   // System.Windows.Forms.MessageBox.Show(this, "Ocurrió un error de sincronización, con el producto:" + val.producto + ".\nPor favor Revise y Reintroduzca los Datos del producto.");
                                                    dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                                }
                                                else
                                                dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                            }
                                            else{
                                                TbSaver.RemoveAt(w);
                                                w--;
                                            }
                                        }
                                        else{
                                            if (val.producto !=GetData( dtgv.Rows[val.row].Cells[0].Value)&&comboBox1.Text!="")
                                            {
                                               // System.Windows.Forms.MessageBox.Show(this, "Ocurrió un error de sincronización, con el producto:" + val.producto + ".\nPor favor Revise y Reintroduzca los Datos del producto.");
                                                dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                            }
                                            else
                                            dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);

                                        }

                                       
                                    }

                                }
                                if (dtgv.RowCount - 10 < val.row)
                                    dtgv.Rows.Insert(dtgv.RowCount - 2);

                                go = false;

                            }
                        }
                        if (val.row > max && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            max = val.row;
                    }
                }


            }
        }
        private void PutInExt(ArrayList TbSaver, DataGridView dtgv, int row, int col)
        {
            if (TbSaver != null)
            {
                // godeep = false;
                int max = -1;

                //  bool jet = false;
                for (int w = 0; w < TbSaver.Count; w++)
                {
                    ValueSaver val = (ValueSaver)TbSaver[w];
                    if (val.IPVName == tabControl1.SelectedTab.Text)
                    {
                        //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                        if (comboBox1.Text == "")
                        {
                            if (val.col == col && val.row >= row && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            {
                                //go = false;
                                //if (val.cant == "15")
                                //{
                                //    go = false;
                                //}
                                //if (val.cant.Contains("50"))
                                //{
                                //    int y = 99;
                                //}
                                //if (!tabControl1.SelectedTab.Text.Contains("IPV "))
                                if (val.row != -1 && val.col != -1)
                                {
                                    go = true;
                                    //}
                                    if (val.cant == "0")
                                    {
                                        val.cant = "0.00";
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        dtgv.Rows[val.row].Cells[val.col].Value = "0";
                                    }
                                    else
                                    {

                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                    }

                                }


                                if (dtgv.RowCount - 10 < val.row)
                                    dtgv.Rows.Insert(dtgv.RowCount - 2);
                                go = false;
                                //go = true;

                                //  jet = true;
                            }
                        }
                        else
                        {
                            if (val.col == col && val.row >= row && (val.row <= Search4(dtgv, "#$-") || Search4(dtgv, "#$-") == 0) && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            {
                                //go = false;
                                //if (val.cant == "15")
                                //{
                                //    go = false;
                                //}
                                //if (val.cant.Contains("50"))
                                //{
                                //    int y = 99;
                                //}
                                //if (!tabControl1.SelectedTab.Text.Contains("IPV "))
                                //{
                                if (val.row != -1 && val.col != -1)
                                {
                                    go = true;
                                    //}
                                    if (val.cant == "0")
                                    {
                                        val.cant = "0.00";
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                        dtgv.Rows[val.row].Cells[val.col].Value = "0";
                                    }
                                    else
                                    {
                                        double aux = System.Convert.ToDouble(val.cant);
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(System.Convert.ToDouble(System.Convert.ToDouble(val.cant) + 1));
                                        dtgv.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(aux);

                                    }

                                }
                                if (dtgv.RowCount - 10 < val.row)
                                    dtgv.Rows.Insert(dtgv.RowCount - 2);

                                go = false;
                                //go = true;

                                //  jet = true;
                            }
                        }
                        if (val.row > max && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                            max = val.row;
                    }
                }


            }
        }

        private void Transfer2Ramal()
        {
            //ramal20Base.Rows[k].Cells[9].Value = "0.00";
            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "In")
                {

                    int k = Search4(ramal20Base, ib.producto);
                    if (ramal20Base.Rows[k].Cells[0].Value == null)
                    {
                        dts = dbc.SelectQuerryFixed("SELECT Nombre, DUM ,PrecIn, EProducto.UM FROM Producto INNER JOIN  EProducto ON Producto.Nombre = Eproducto.Producto WHERE Nombre = '" + ib.producto + "' AND Producto.Cuenta = '" + ib.FromTo + "' AND EProducto.NNombre = '" + ib.eprod + "'");

                        ramal20Base.Rows.Insert(k, dts.Tables[0].Rows[0].ItemArray[0].ToString(), dts.Tables[0].Rows[0].ItemArray[1].ToString(), dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00");
                        ramal20Base.Rows.RemoveAt(k + 1);
                        ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
                        ramal20Base.Rows[k].ReadOnly = true;
                        ramal20Base.Rows[k + 1].ReadOnly = false;

                        //ramal20Base.Rows[k].Cells[7].Value = System.Math.Round(UMConverter(System.Convert.ToDouble(ib.cant), GetData(ramal20Base.Rows[k].Cells[1].Value), dts.Tables[0].Rows[0].ItemArray[3].ToString()), 2);
                        //ramal20Base.Rows[k].Cells[9].Value = System.Math.Round(UMConverter(System.Convert.ToDouble(ib.cant), GetData(ramal20Base.Rows[k].Cells[1].Value), dts.Tables[0].Rows[0].ItemArray[3].ToString()), 2);
                       // ramal20Base.Rows[k].Cells[1].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                      //  ramal20Base.Rows[k].Cells[2].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();

                        ramal20Base.Rows[k].Cells[7].Value = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        ramal20Base.Rows[k].Cells[9].Value = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);

                    }
                    else if (GetData(ramal20Base.Rows[k].Cells[0].Value) == ib.producto)
                    {
                        dts = dbc.SelectQuerryFixed("SELECT Nombre, DUM ,PrecIn, EProducto.UM FROM Producto INNER JOIN  EProducto ON Producto.Nombre = Eproducto.Producto WHERE Nombre = '" + ib.producto + "' AND Producto.Cuenta = '" + ib.FromTo + "' AND EProducto.NNombre = '" + ib.eprod + "'");

                        // ramal20Base.Rows.Insert(k, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00");
                        ramal20Base.Rows[k].Cells[7].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[7].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                    }

                }


                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "Out")
                {

                    int k = Search4(ramal20Base, ib.producto);
                    if (ramal20Base.Rows[k].Cells[0].Value == null)
                    {


                        System.Windows.Forms.MessageBox.Show("Producto: " + ib.producto + " no encontrado en esta Cuenta!..");

                    }
                    else if (GetData(ramal20Base.Rows[k].Cells[0].Value) == ib.producto)
                    {
                        dts = dbc.SelectQuerryFixed("SELECT Nombre, DUM ,PrecIn, EProducto.UM FROM Producto INNER JOIN  EProducto ON Producto.Nombre  = Eproducto.Producto WHERE Nombre = '" + ib.producto + "' AND Producto.Cuenta = '" + ib.Cuenta + "' AND EProducto.NNombre = '" + ib.eprod + "'");

                        // ramal20Base.Rows.Insert(k, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00");
                        ramal20Base.Rows[k].Cells[11].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[11].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        //ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Convert.ToDouble(ib.cant)); ;
                    }

                }


                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "Own")
                {

                    int k = Search4(ramal20Base, ib.producto);
                    if (ramal20Base.Rows[k].Cells[0].Value == null)
                    {

                        System.Windows.Forms.MessageBox.Show("Producto:" + ib.producto + " no encontrado en esta Cuenta!..");

                    }
                    else if (GetData(ramal20Base.Rows[k].Cells[0].Value) == ib.producto)
                    {
                        dts = dbc.SelectQuerryFixed("SELECT Nombre, DUM ,PrecIn, EProducto.UM FROM Producto INNER JOIN  EProducto ON Producto.Nombre  = Eproducto.Producto WHERE Nombre = '" + ib.producto + "' AND Producto.Cuenta = '" + ib.Cuenta + "' AND EProducto.NNombre = '" + ib.eprod + "'");


                        ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        //ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Convert.ToDouble(ib.cant)); ;
                    }

                }

                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "Des")
                {

                    int k = Search4(ramal20Base, ib.producto);




                    if (ramal20Base.Rows[k].Cells[0].Value == null)
                    {

                        if (dbc.ExistQuerry("Select Id From Producto Where Nombre = '" + ib.producto + "'  and Cuenta = '" + ib.Cuenta + "'"))
                        {

                            dts = dbc.SelectQuerryFixed("Select Nombre, DUM, PrecIn From Producto Where Nombre = '" + ib.producto + "' and Cuenta = '" + ib.Cuenta + "'");

                            if (!dbc.ExistQuerry("Select Id From UndProd Where UName = '" + tabControl2.SelectedTab.Text + "' and Producto = '" + ib.Cuenta + "'") && (ib.Cuenta == ib.FromTo))
                                dbc.SimplePlan("INSERT INTO UndProd([Id],[UName],[Producto]) VALUES ('" + dbc.MaxQuerry("UndProd") + "','" + tabControl2.SelectedTab.Text + "','" + ib.producto + "')");

                            //  ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                            ramal20Base.Rows.Insert(k, ib.producto, dts.Tables[0].Rows[0].ItemArray[1].ToString(), dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00");
                            ramal20Base.Rows.RemoveAt(k + 1);
                            ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
                            ramal20Base.Rows[k].Cells[0].ReadOnly = true;
                            ramal20Base.Rows[k + 1].Cells[0].ReadOnly = false;

                            ramal20Base.Rows[k].Cells[7].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[7].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        }

                        //System.Windows.Forms.MessageBox.Show("Producto:" + ib.producto + " no encontrado en esta Cuenta!..");

                    }
                    else if (GetData(ramal20Base.Rows[k].Cells[0].Value) == ib.producto)
                    {

                        ramal20Base.Rows[k].Cells[7].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[7].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));

                        //dts = dbc.SelectQuerryFixed("SELECT Nombre, DUM ,PrecIn, EProducto.UM FROM Producto INNER JOIN  EProducto ON Producto.Nombre  = Eproducto.Producto WHERE Nombre = '" + ib.producto + "' AND Producto.Cuenta = '" + ib.Cuenta + "' AND EProducto.NNombre = '" + ib.eprod + "'");

                        //// ramal20Base.Rows.Insert(k, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00");
                        //ramal20Base.Rows[k].Cells[11].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[11].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        //ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Convert.ToDouble(ib.cant)); ;
                    }

                }





            }
        }
        private int Search4(DataGridView dtgv, String prod)
        {
            int w = 0;
            while (w < dtgv.RowCount - 2 && dtgv.Rows[w].Cells[0].Value != null && GetData(dtgv.Rows[w].Cells[0].Value) != prod)
                w++;
            return w;

        }

        private void SalverFixer(int index, ArrayList salver)
        {
            // int mark = 0;
            ArrayList charge = new ArrayList();

            for (int w = 0; w < salver.Count; w++)
            {
                ValueSaver val = (ValueSaver)salver[w];
                if (val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {


                    if (val.row == index)
                    {
                        salver.RemoveAt(w);
                        w--;
                    }

                    if (val.row > index)
                    {
                        val.row = val.row - 1;
                    }
                }
            }




        }
        private void SalverFixer2(int index, ArrayList salver, String ipv)
        {
            // int mark = 0;
            ArrayList charge = new ArrayList();

            for (int w = 0; w < salver.Count; w++)
            {
                ValueSaver val = (ValueSaver)salver[w];
                if (val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {


                    if (val.row == index)
                    {
                        salver.RemoveAt(w);
                        w--;
                    }

                    if (val.row > index)
                    {
                        val.row = val.row - 1;
                    }
                }
            }




        }

        private void SalverFixerOnTop(String prod, ArrayList salver, String cuenta)
        {
            // int mark = 0;
            ArrayList charge = new ArrayList();

            dts = dbc.SelectQuerryFixed("SELECT count(IPV.Producto)FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "'");
            int count = System.Convert.ToInt32(dts.Tables[0].Rows[0].ItemArray[0].ToString());

            int index = -1;

            foreach (ValueSaver val in salver)
            {
                if (val.row >= count &&val.col == 3 && System.Convert.ToDouble(val.cant)>0 && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    count = val.row+1;
                }
            }
            for (int w = 0; w < salver.Count; w++)
            {
                ValueSaver val = (ValueSaver)salver[w];
                if (val.row >= count && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {


                    if (val.producto == prod)
                    {
                        index = val.row;
                        salver.RemoveAt(w);
                        w--;

                    }

                    if (val.row > index && index != -1)
                    {
                        val.row = val.row - 1;
                    }
                }
            }




        }




        private void CantFixer(String um, int row)
        {
            if (um != "Kg" && um != "Lt" && um != "Lb")
            {
                go = true;

                double num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[3].Value));


                ramal20Base.Rows[row].Cells[3].Value = System.Convert.ToString(System.Math.Round(num));

                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[5].Value));

                ramal20Base.Rows[row].Cells[5].Value = System.Convert.ToString(System.Math.Round(num));

                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[7].Value));

                ramal20Base.Rows[row].Cells[7].Value = System.Convert.ToString(System.Math.Round(num));


                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[9].Value));

                ramal20Base.Rows[row].Cells[9].Value = System.Convert.ToString(System.Math.Round(num));

                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[11].Value));

                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[11].Value = System.Convert.ToString(System.Math.Round(num));

                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[13].Value));

                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[13].Value = System.Convert.ToString(System.Math.Round(num));

                num = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[row].Cells[16].Value));

                ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[16].Value = System.Convert.ToString(System.Math.Round(num));

                go = false;
            }

        }

        public String CantFixer3(String um, String cant)
        {
            if (um != "Kg" && um != "Lt" && um != "Lb" && (um != "Rac" || (um == "Rac" && TheMothod(um, cant))))
            {
                go = true;



                double num = System.Convert.ToDouble(GetNumData(cant));
                if ((System.Math.Round(num) - num) != 0)
                    return System.Convert.ToString(num);


                return System.Convert.ToString(System.Math.Round(num));


            }

            return cant;

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
        private void CantFixer2(String um, int row)
        {
            if (um != "Kg" && um != "Lt")
            {
                String aux = GetNumData(IPVBase.Rows[row].Cells[3].Value);
                if (TheMothod(um, aux))
                {
                    if (aux.Contains("."))
                        aux = aux.Remove(aux.IndexOf("."));
                    IPVBase.Rows[row].Cells[3].Value = System.Convert.ToString(System.Convert.ToInt32(aux));

                }

                aux = GetNumData(IPVBase.Rows[row].Cells[5].Value);
                if (TheMothod(um, aux))
                {
                    if (aux.Contains("."))
                        aux = aux.Remove(aux.IndexOf("."));
                    IPVBase.Rows[row].Cells[5].Value = System.Convert.ToString(System.Convert.ToInt32(aux));
                }

                aux = GetNumData(IPVBase.Rows[row].Cells[7].Value);
                if (TheMothod(um, aux))
                {
                    if (aux.Contains("."))
                        aux = aux.Remove(aux.IndexOf("."));
                    IPVBase.Rows[row].Cells[7].Value = System.Convert.ToString(System.Convert.ToInt32(aux));
                }
                aux = GetNumData(IPVBase.Rows[row].Cells[11].Value);
                if (TheMothod(um, aux))
                {
                    if (aux.Contains("."))
                        aux = aux.Remove(aux.IndexOf("."));
                    IPVBase.Rows[row].Cells[11].Value = System.Convert.ToString(System.Convert.ToInt32(aux));
                }
            }

        }
        private String GetMoneda()
        {
            if (monedaNacionalToolStripMenuItem.Checked)
            {
                return "CUP";
            }
            return "CUC";
        }

        private void RamalIPVAdjust(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck
            godeep = false;
            for (int w = 0; w < 10; w++)
            {
                dtgv.Rows.Add();
                if (dtgv == ramal20Base)
                    dtgv.Rows[dtgv.Rows.Count - 1].Cells[0].ReadOnly = true;
            }
            //dtgv.Rows[chck].Cells[0].Style.BackColor = Color.LightYellow;
            dtgv.Rows[dtgv.Rows.Count - 1].Cells[0].Value = "Totales:";
           // dtgv.Height = dtgv.ColumnHeadersHeight * dtgv.Rows.Count;
            
            dtgv.Rows[0].Cells[0].Selected = false;
            godeep = true;
        }

        private void RamalIPVAdjustII(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck
            godeep = false;
            for (int w = 0; w < 2; w++)
            {
                dtgv.Rows.Add();
                if (dtgv == ramal20Base)
                    dtgv.Rows[dtgv.Rows.Count - 1].Cells[0].ReadOnly = true;
            }
            //dtgv.Rows[chck].Cells[0].Style.BackColor = Color.LightYellow;
            dtgv.Rows[dtgv.Rows.Count - 1].Cells[0].Value = "Totales:";
            dtgv.Height = dtgv.ColumnHeadersHeight * dtgv.Rows.Count;
            dtgv.Rows[dtgv.Rows.Count - 2].DividerHeight = 2;
            dtgv.Rows[0].Cells[0].Selected = false;


            godeep = true;
        }
        private void ReBindValues(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck

            int k = 0;
            go = true;

            while (k < dtgv.RowCount - 2 && dtgv.Rows[k].Cells[0].Value != null)
            {
                for (int w = 4; w < dtgv.ColumnCount - 2; w++)
                {
                    //(w!=3||dtgv.Columns[w-1].HeaderText.Contains("Venta")) &&
                    if (dtgv.Columns[w].HeaderText.Contains("Cantidad") && System.Convert.ToDouble(GetNumData(dtgv.Rows[k].Cells[w].Value)) != 0)
                    {


                        String val = GetNumData(dtgv.Rows[k].Cells[w].Value);

                        dtgv.Rows[k].Cells[w].Value = "0";

                        dtgv.Rows[k].Cells[w].Value = val;

                    }


                }
                k++;
            }
            go = false;

        }

        private void ReBindValuesSaved(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck

            int k = 0;
            go = true;

            while (k < dtgv.RowCount - 2 && dtgv.Rows[k].Cells[0].Value != null)
            {
                for (int w = 4; w < dtgv.ColumnCount - 2; w++)
                {
                    //(w!=3||dtgv.Columns[w-1].HeaderText.Contains("Venta")) &&
                    if (dtgv.Columns[w].HeaderText.Contains("Cantidad") && System.Convert.ToDouble(GetNumData(dtgv.Rows[k].Cells[w].Value)) != 0)
                    {

                        if (dtgv.ColumnCount < 16 || (w != 7 && w != 9 && w != 11))
                        {
                            String val = GetNumData(dtgv.Rows[k].Cells[w].Value);

                            dtgv.Rows[k].Cells[w].Value = "0";

                            dtgv.Rows[k].Cells[w].Value = val;

                        }
                    }


                }
                k++;
            }
            go = false;

        }

        private void ReBindValuesRamal(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck

            int k = 0;
            go = true;

            while (k < dtgv.RowCount - 2 && dtgv.Rows[k].Cells[0].Value != null)
            {
                for (int w = 3; w < dtgv.ColumnCount - 2; w++)
                {
                    //(w!=3||dtgv.Columns[w-1].HeaderText.Contains("Venta")) &&
                    if (dtgv.Columns[w].HeaderText.Contains("Cantidad") && System.Convert.ToDouble(GetNumData(dtgv.Rows[k].Cells[w].Value)) != 0)
                    {
                        String val = GetNumData(dtgv.Rows[k].Cells[w].Value);

                        dtgv.Rows[k].Cells[w].Value = "0";

                        dtgv.Rows[k].Cells[w].Value = val;
                    }


                }
                k++;
            }
            go = false;

        }

        private void ReBindValues2(System.Windows.Forms.DataGridView dtgv)
        {

            //, int chck

            int k = 0;
            go = true;

            while (k < dtgv.RowCount - 2 && dtgv.Rows[k].Cells[0].Value != null)
            {
                for (int w = 3; w < dtgv.ColumnCount - 2; w++)
                {
                    if ((w == 7 || w == 5) && dtgv.Columns[w].HeaderText.Contains("Cantidad") && System.Convert.ToDouble(GetNumData(dtgv.Rows[k].Cells[w].Value)) != 0)
                    {
                        String val = GetNumData(dtgv.Rows[k].Cells[w].Value);

                        dtgv.Rows[k].Cells[w].Value = "0";

                        dtgv.Rows[k].Cells[w].Value = val;
                    }


                }
                k++;
            }
            go = false;

        }

        private void ReSetValues(ArrayList Salver)
        {

            //, int chck

            //  int k = 0;

            for (int w = 0; w < Salver.Count; w++)
            {
                ValueSaver val = (ValueSaver)Salver[w];
                if (val.UName == tabControl2.SelectedTab.Text &&val.IPVName == tabControl1.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda() && comboBox1.Text != "")
                {
                    Salver.RemoveAt(w);
                    w--;
                }
            }

            //while (k<dtgv.RowCount-2 && dtgv.Rows[k].Cells[0].Value!=null )
            //{
            //    for (int w = 3; w < dtgv.ColumnCount-2; w++)
            //    {
            //       if (dtgv.Columns[w].HeaderText.Contains("Cantidad"))
            //       {
            //           //String val = GetNumData(dtgv.Rows[k].Cells[w].Value);

            //           dtgv.Rows[k].Cells[w].Value = "0";

            //         //  dtgv.Rows[k].Cells[w].Value = val;
            //       }

            //    }
            //    k++;     
            //}

        }



        private void LoadIPV(System.String IPVName)
        {
            try
            {
                rower = 0;
                counter = 0;
                ldipv = false;
                IntTransf2();

                IPVName = IPVName.Replace("IPV ", "");
                IPVBase.Rows.Clear();
                //  System.Data.DataSet dts;
                bool put = false;

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'  AND Moneda = '" + GetMoneda() + "'"))
                {
                    //Fill
                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[VPrice],[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad],[VIngreso],[CUnitario],[CVendido]" +
                    ",[FCantidad],[FCosto],[Id] FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "' AND Moneda = '" + GetMoneda() + "'");

                    //godeep2 = false;
                    int chck = 0;
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        IPVBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[4].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString()), dts.Tables[0].Rows[w].ItemArray[6].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString()),
                            dts.Tables[0].Rows[w].ItemArray[8].ToString(), dts.Tables[0].Rows[w].ItemArray[9].ToString(), dts.Tables[0].Rows[w].ItemArray[10].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[11].ToString()), dts.Tables[0].Rows[w].ItemArray[12].ToString());

                        //CantFixer2(dts.Tables[0].Rows[w].ItemArray[1].ToString(), IPVBase.Rows.Count - 1);
                        IPVBase.Rows[IPVBase.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[13].ToString();
                        chck = w;
                    }

                    RamalIPVAdjust(IPVBase);

                    dts = dbc.SelectQuerryFixed("SELECT SUM([ICosto]),SUM([EImporte]), SUM([VIngreso])," +
                 "SUM([CVendido]),SUM([FCosto]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'  AND Moneda = '" + GetMoneda() + "'");

                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[4].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[6].Value = dts.Tables[0].Rows[0].ItemArray[1].ToString();
                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[8].Value = dts.Tables[0].Rows[0].ItemArray[2].ToString();
                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[10].Value = dts.Tables[0].Rows[0].ItemArray[3].ToString();
                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value = dts.Tables[0].Rows[0].ItemArray[4].ToString();

                    // godeep2 = true;
                    PutIn(IPVSaver, IPVBase);
                    // ReBindValues(IPVBase);

                    if (tabControl1.SelectedTab.Text.Contains("IPV "))
                        tabControl1.SelectedTab.BackColor = Color.LightGray;

                   


                }
                else
                {
                    if(tabControl1.SelectedTab.Text.Contains("IPV "))
                    forzar.Enabled = false;
                    dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecOut],[PrecIn] FROM [MLB].[dbo].[Producto] INNER JOIN [MLB].[dbo].[UndProd] ON Producto.Nombre = UndProd.Producto WHERE [Cuenta] = '" + comboBox1.Text + "' AND Moneda = '" + GetMoneda() + "' AND UName = '" + tabControl2.SelectedTab.Text + "' AND [PrecOut] != '0' ORDER BY UndProd.Id");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        IPVBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[3].ToString(), "0.00", "0", "0.00");
                    }



                    // go = false;
                    RamalIPVAdjust(IPVBase);


                    //el bulto

                    dts = dbc.SelectQuerryFixed("SELECT DISTINCT IPV.Producto, IPV.UM, Producto.PrecOut, IPV.FCantidad, IPV.CUnitario, IPV.FCosto FROM IPV INNER JOIN Producto ON IPV.Producto = Producto.Nombre  WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND Producto.Cuenta = '"+textBox1.Text+"' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "' AND IPV.Producto NOT IN (Select NNombre From EProducto) AND (Producto NOT IN  (SELECT  Nombre  FROM  Producto))");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {



                        // int idx = Search4(IPVBase, dts.Tables[0].Rows[w].ItemArray[0].ToString());
                        int idx = Search4(IPVBase, "####");


                        IPVBase.Rows.Insert(idx, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString(), "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[4].ToString(), "0.00", CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString());

                        IPVBase.Rows[idx].Cells[5].ReadOnly = true;
                        IPVBase.Rows[idx].Cells[9].ReadOnly = true;
                        IPVBase.Rows[idx].Cells[3].ReadOnly = true;
                        // IPVBase.Rows.RemoveAt(idx + 1);
                        // CantFixer2(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);
                        put = true;

                    }


                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],Producto.PrecOut,[FCantidad],[CUnitario],[FCosto]FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 AND Producto.Cuenta = IPV.Cuenta");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {



                        int idx = Search4(IPVBase, dts.Tables[0].Rows[w].ItemArray[0].ToString());


                        IPVBase.Rows.Insert(idx, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString(), "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[4].ToString(), "0.00", CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString());
                        IPVBase.Rows.RemoveAt(idx + 1);
                        //  CantFixer2(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);
                        put = true;


                    }

                    dts = dbc.SelectQuerryFixed("SELECT distinct(IPV.Producto),IPV.UM,EProducto.Precio,[FCantidad],[CUnitario],[FCosto] FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "'");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {



                        // int idx = Search4(IPVBase, dts.Tables[0].Rows[w].ItemArray[0].ToString());
                        int idx = Search4(IPVBase, "####");


                        IPVBase.Rows.Insert(idx, dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString(), "0", "0.00", "0", "0.00", dts.Tables[0].Rows[w].ItemArray[4].ToString(), "0.00", CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()), dts.Tables[0].Rows[w].ItemArray[5].ToString());

                        IPVBase.Rows[idx].Cells[5].ReadOnly = true;
                        IPVBase.Rows[idx].Cells[9].ReadOnly = true;
                        IPVBase.Rows[idx].Cells[3].ReadOnly = true;
                        // IPVBase.Rows.RemoveAt(idx + 1);
                        // CantFixer2(dts.Tables[0].Rows[w].ItemArray[1].ToString(), idx);
                        put = true;

                    }




                    PutIn(IPVSaver, IPVBase);



                    for (int w = 0; w < IPVBase.RowCount; w++)
                    {
                        IPVBase.Rows[w].Cells[0].ReadOnly = true;
                        if (w > 0 && IPVBase.Rows[w].Cells[0].Value == null && IPVBase.Rows[w - 1].Cells[0].Value != null)
                            IPVBase.Rows[w].Cells[0].ReadOnly = false;
                        if (w == 0 && IPVBase.Rows[w].Cells[0].Value == null && IPVBase.Rows[w + 1].Cells[0].Value == null)
                            IPVBase.Rows[w].Cells[0].ReadOnly = false;

                    }


                    //   go = true;

                    if (tabControl1.SelectedTab.Text.Contains("IPV "))
                        tabControl1.SelectedTab.BackColor = Color.Transparent;

                }

                dts = dbc.SelectQuerryFixed("SELECT count(IPV.Producto)FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "'");

                PutInExt(IPVSaver, IPVBase, System.Convert.ToInt32(dts.Tables[0].Rows[0].ItemArray[0].ToString()), 3);
                //   ReBindValues(IPVBase);

                if (put)
                {
                    IPVBase.Rows[IPVBase.RowCount - 1].Cells[4].Value = GetTotal(IPVBase, 4);
                    //ldipv = true;
                    IPVBase.Rows[IPVBase.RowCount - 1].Cells[12].Value = GetTotal(IPVBase, 12);
                    if (!EstaIPVSaver4(GetNumData(IPVBase.Rows[IPVBase.RowCount - 1].Cells[IPVBase.ColumnCount - 1].Value)) && IPVBase.Rows[IPVBase.RowCount - 1].Cells[IPVBase.ColumnCount - 1].Value != null)
                    {
                        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[IPVBase.ColumnCount - 1].Value), IPVBase.Rows.Count - 1, IPVBase.ColumnCount - 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[0].Value), GetMoneda()));
                    }
                }
                //             rePutPrecinIPv();
                // 
                //ReBindValues2(IPVBase);
                IPVBase.Rows[IPVBase.Rows.Count - 2].DividerHeight = 2;
                ldipv = true;
                //if (tabControl1.SelectedTab.Text.Contains("IPV "))
                //{
                //    BigValidarIPV();
                //}

                if (tabControl1.SelectedTab.Text.Contains("IPV ") && tabControl1.SelectedTab.BackColor==Color.LightGray)
                {
               
                    
                    if (editarVariablesToolStripMenuItem.Enabled)
                    {
                        forzar.Enabled = true;
                       

                    }
                    LockIPV(true);
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }


        }

        private void LockIPV(bool lkey)
        {
            for (int w = 0; w < IPVBase.Rows.Count; w++)
            {
                for (int k = 0; k < IPVBase.ColumnCount; k++)
                {
                    if (k == 0 || k == 2 || k == 3 || k == 5 || k == 7 || k == 9)
                    {
                        IPVBase.Rows[w].Cells[k].ReadOnly = lkey;
                    }

                }

                if (IPVBase.Rows[w].Cells[0].Value == null)
                {
                    break;
                }
            }
        }
        private void LockFichaCosto(bool lkey)
        {
            for (int w = 0; w < FichaCostoBase.Rows.Count; w++)
            {
                for (int k = 0; k < FichaCostoBase.ColumnCount; k++)
                {
                    if (k == 5 || k == 4 || k == 6)
                    {
                        FichaCostoBase.Rows[w].Cells[k].ReadOnly = lkey;
                    }

                }

               
            }
        }
        private void rePutinIPV()
        {
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName == tabControl1.SelectedTab.Text)
                {
                    //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                    if (val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda()&&val.col!=2)
                    {
                        go2 = false;
                        IPVBase.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                        go2 = true;
                    }
                }
            }
        }
        private String DateConverter(System.DateTime sdt)
        {
            return sdt.Month.ToString() + sdt.Year.ToString();
        }
        private System.String MonthConverter(int month)
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
        private System.DateTime dayCatcher2(System.DateTime dtt, bool ops)
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
        private void RLResIng()
        {
            ResIngBase.Rows.Clear();
            for (int k = 1; k < comboBox1.Items.Count; k++)
            {
                NewResIng2(comboBox1.Items[k].ToString());
            }

        }


        private void LoadResIng()
        {
            try
            {
                rower = 0;
                ResIngBase.Rows.Clear();
                //  System.Data.DataSet dts;
                bool gray = true;
                if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "' AND Moneda ='" + GetMoneda() + "'"))
                {

                    //Fill


                    dts = dbc.SelectQuerryFixed("SELECT [Day],[Month],[InHoy],[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum], [Id],[Cuenta]" +
                 " FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate='" + DateCultureConverter(dateTimePicker1.Value) + "'");

                    bool paso = false;

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {

                        //if (System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[0].ToString()) > dateTimePicker1.Value.Day)
                        //    break;

                        ResIngBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[9].ToString(), dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(),
                            dts.Tables[0].Rows[w].ItemArray[4].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[6].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString());

                        ResIngBase.Rows[ResIngBase.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[8].ToString();

                        //if ((System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[0].ToString()) + 1) == dateTimePicker1.Value.Day)
                        //{

                        //    for (int k = 1; k < comboBox1.Items.Count; k++)
                        //    {
                        //        NewResIng2(comboBox1.Items[k].ToString());
                        //    }


                        //    break;
                        //}
                        //   gray = false;
                    }

                    if (ResIngBase.RowCount > 0)
                    {

                        if (!paso && (System.Convert.ToInt32(ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[1].Value.ToString()) < dateTimePicker1.Value.Day))
                        {
                            for (int k = 1; k < comboBox1.Items.Count; k++)
                            {
                                NewResIng2(comboBox1.Items[k].ToString());
                            }

                        }
                    }
                    else
                        for (int k = 1; k < comboBox1.Items.Count; k++)
                        {
                            NewResIng2(comboBox1.Items[k].ToString());
                        }

                }
                else
                {

                    //  NewResIng();
                    for (int k = 1; k < comboBox1.Items.Count; k++)
                    {
                        NewResIng2(comboBox1.Items[k].ToString());
                    }
                    gray = false;

                }
                //Totalizar;
                ResIngBase.Rows.Add("Totales: ", dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), GetTotal2(ResIngBase, 3), GetTotal2(ResIngBase, 4), GetTotal2(ResIngBase, 5), GetTotal2(ResIngBase, 6), GetTotal2(ResIngBase, 7), GetTotal2(ResIngBase, 8));
                ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[7].ReadOnly = true;
                ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[8].ReadOnly = true;


                if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                {
                    if (gray)
                        tabControl1.SelectedTab.BackColor = Color.LightGray;
                    else
                        tabControl1.SelectedTab.BackColor = Color.Transparent;

                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }


        }

        private void NewResIng2(String cuenta)
        {

            if (!IsCadena(tabControl2.SelectedTab.Text))
            {


                double ingAc = 0;

                dts = dbc.SelectQuerryFixed("SELECT [InHoy],[InHastaHoy] FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate = '" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'");

                if (dts.Tables[0].Rows.Count > 0)
                {
                    //System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString()) +
                    ingAc = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                }
                else
                    ingAc = GetIngAc(cuenta);

                double ingHoy = AmountIPV2(cuenta);

                double ingHH = ingAc + ingHoy;

                double costAc = 0;

                dts = dbc.SelectQuerryFixed("SELECT [CostHoy],[CostHastaHoy] FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate = '" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'");

                if (dts.Tables[0].Rows.Count > 0)
                {
                    //System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString()) +
                    costAc = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                }
                else
                    costAc = GetCosAc(cuenta);


                double costHoy = AmountIPV(cuenta);

                double costHH = costAc + costHoy;




                //  double saldofinal = (entrada + diaAnt) - (salida + traslado);
                ResIngBase.Rows.Add(cuenta, dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), System.Convert.ToString(ingHoy), System.Convert.ToString(ingHH), System.Convert.ToString(costHoy), System.Convert.ToString(costHH), System.Convert.ToString(ingAc), System.Convert.ToString(costAc));
            }
            else
            {
                dts = dbc.SelectQuerryFixed("SELECT SUM([InHoy]),SUM([InHastaHoy]),SUM([CostHoy]),SUM([CostHastaHoy]),SUM([IngAcum]),SUM([CostAcum])" +
                 " FROM [MLB].[dbo].[ResIng] WHERE Cuenta='" + cuenta + "' And MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate='" + DateCultureConverter(dateTimePicker1.Value) + "' And Moneda = '" + GetMoneda() + "'");



                if (dts.Tables[0].Rows.Count > 0)
                {


                    ResIngBase.Rows.Add(cuenta, dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), GetNumData(dts.Tables[0].Rows[0].ItemArray[0].ToString()), GetNumData(dts.Tables[0].Rows[0].ItemArray[1].ToString()), GetNumData(dts.Tables[0].Rows[0].ItemArray[2].ToString()), GetNumData(dts.Tables[0].Rows[0].ItemArray[3].ToString()),
                        GetNumData(dts.Tables[0].Rows[0].ItemArray[4].ToString()), GetNumData(dts.Tables[0].Rows[0].ItemArray[5].ToString()));


                }

            }

        }
        private void LoadSubMayor()
        {
            try
            {

                SubMayorBase.Rows.Clear();
                //  System.Data.DataSet dts;
                //dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'")
                bool gray = true;
                // Day = '" + dateTimePicker1.Value.Day.ToString() + "' 
                if (dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'"))
                {
                    //Fill



                    dts = dbc.SelectQuerryFixed("SELECT [Day],[Month],[SaldoInicial],[Entrada],[EntInt],[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[Id],[TTTEI],[TTTSI]" +
                 " FROM [MLB].[dbo].[SubMayor] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' ORDER BY [Day]");

                    //bool paso = false;

                    // int mark = 0;
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {

                        if ((System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[0].ToString())) > dateTimePicker1.Value.Day)
                            break;

                        SubMayorBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(),
                          dts.Tables[0].Rows[w].ItemArray[4].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[6].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString(), dts.Tables[0].Rows[w].ItemArray[8].ToString(), dts.Tables[0].Rows[w].ItemArray[9].ToString());

                        SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[10].ToString();
                        SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[4].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[11].ToString());
                        SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[6].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[12].ToString());
                    }
                    if (hlpeve)
                    {
                        hlpManager.hlpknd = HelpKind.Descripcion;
                        hlpManager.topic = "SubMayor";
                        hlpManager.tnumber = 1;
                    }
                    if ((SubMayorBase.RowCount > 0) && (System.Convert.ToInt32(SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[0].Value.ToString()) != dateTimePicker1.Value.Day))
                    {
                        NewSubMayor();
                        gray = false;
                    }
                    if (SubMayorBase.RowCount == 0)
                    {

                        NewSubMayor();
                        gray = false;
                    }

                   

                }
                else
                {


                    NewSubMayor();
                    gray = false;
                    // RamalIPVAdjust(ramal20Base);
                }

                if (tabControl1.SelectedTab.Text == "SubMayor")
                {
                    if (gray)
                        tabControl1.SelectedTab.BackColor = Color.LightGray;
                    else
                        tabControl1.SelectedTab.BackColor = Color.Transparent;

                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }



        }
        private double GetDiaAnt()
        {
            double diant = 0;

            foreach (ValueSaver val in SMSaver)
            {
                if (val.col == 2 && val.moneda == GetMoneda() && val.Cuenta == comboBox1.Text && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    diant = System.Convert.ToDouble(GetNumData(val.cant));//amount += (System.Convert.ToDouble(val.cant) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[val.row].Cells[9].Value)));
                }
            }

            return diant;
        }
        private double GetIngAc(String cta)
        {
            double ingac = 0;

            foreach (ValueSaver val in RISaver)
            {
                if (val.col == 7 && val.moneda == GetMoneda() && val.producto == cta && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    ingac = System.Convert.ToDouble(val.cant);//amount += (System.Convert.ToDouble(val.cant) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[val.row].Cells[9].Value)));
                }
            }

            return ingac;
        }
        private double GetCosAc(String cta)
        {
            double cosac = 0;

            foreach (ValueSaver val in RISaver)
            {
                if (val.col == 8 && val.moneda == GetMoneda() && val.producto == cta && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    cosac = System.Convert.ToDouble(val.cant);//amount += (System.Convert.ToDouble(val.cant) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[val.row].Cells[9].Value)));
                }
            }

            return cosac;
        }


        private void NewSubMayor()
        {
            if (!IsCadena(tabControl2.SelectedTab.Text))
            {

                double diaAnt = 0;

              //  dts = dbc.SelectQuerryFixed("SELECT [SaldoFinal] FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate = '" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND Moneda = '" + GetMoneda() + "'");
                dts = dbc.SelectQuerryFixed("SELECT [SaldoFinal] FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND RDate = '" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND Moneda = '" + GetMoneda() + "'");

                if (dts.Tables[0].Rows.Count > 0)
                {
                    diaAnt = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    diaAnt = GetDiaAnt();
                }
                //  diaAnt = GetDiaAnt();
                double entrada = 0;
                if (ramal20Base.RowCount>0&&ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[6].Value != null)
                {
                    entrada = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[6].Value.ToString());
                }


                double salida = AmountIPV(comboBox1.Text);
                double traslado = 0;
                if (ramal20Base.RowCount > 0 && ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[14].Value != null)
                {
                    traslado = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[14].Value.ToString());
                }



                double eint = 0;
                double sint = 0;
                //EntInt.Items.Clear();
                //SalInt.Items.Clear();

                if (ramal20Base.RowCount > 0 && ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value != null)
                {
                    eint = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value.ToString());
                }
                if (ramal20Base.RowCount > 0 && ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[12].Value != null)
                {
                    sint = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[12].Value.ToString());
                }

                //if (comboBox1.Text != "")
                //{
                //    IntTransf();
                //    eint = GenEntInt();
                //    sint = GenSalInt();
                //}

                double saldofinal = (entrada + diaAnt + eint) - (salida + traslado + sint);

                //  saldofinal = System.Math.Round(saldofinal, 3);


                double aux = 0;
                if (ramal20Base.RowCount>0)
                {
               
                ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[17].Value = GetTotal(ramal20Base, 17);
                if (ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value != null)
                {
                    aux = System.Convert.ToDouble(GetTotal(ramal20Base, 17));
                    //aux = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value.ToString());
                }
                }
                double sal2 = (entrada + diaAnt) - (salida + traslado);

                saldofinal = System.Math.Round(saldofinal, 2);

                double compramal = saldofinal - aux - (System.Math.Round(AmountIPV4(), 3));
                compramal = System.Math.Round(compramal, 2);
              //  if (System.Math.Round(compramal,2) < 0.01 && compramal > 0)
               //     compramal = 0;


                SubMayorBase.Rows.Add(dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), System.Convert.ToString(System.Math.Round(diaAnt, 2)), System.Convert.ToString(System.Math.Round(entrada, 2)), System.Convert.ToString(System.Math.Round(eint, 2)), System.Convert.ToString(System.Math.Round(salida, 2)), System.Convert.ToString(System.Math.Round(sint, 2)), System.Convert.ToString(System.Math.Round(traslado, 2)), System.Convert.ToString(System.Math.Round(saldofinal, 2)), System.Convert.ToString(System.Math.Round(compramal, 2)));
                SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[SubMayorBase.ColumnCount - 1].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[SubMayorBase.ColumnCount - 1].Value), 2);
                if (comboBox1.Text != "")
                {
                    SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].ToolTipText = GenEntIntTTT();
                    SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[6].ToolTipText = GenSalIntTTT();
                }
                if(hlpeve)
                CheckSubMayor4Help(compramal,diaAnt);
            }
            else
            {
                dts = dbc.SelectQuerryFixed("SELECT SUM([SaldoInicial]),SUM([Entrada]),SUM([EntInt]),SUM([Salida]),SUM([SalInt]),SUM([Traslado]),SUM([SaldoFinal]),SUM([CompRamal])" +
                    " FROM [MLB].[dbo].[SubMayor] WHERE  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' And Day = '" + dateTimePicker1.Value.Day.ToString() + "'");

                //bool paso = false;

                // int mark = 0;
                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {


                    SubMayorBase.Rows.Add(dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(),
                      dts.Tables[0].Rows[w].ItemArray[4].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[6].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString());

                    //  SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Tag = dts.Tables[0].Rows[w].ItemArray[10].ToString();

                }

            }

        }
        private void NewSubMayor2(String tag)
        {
            double diaAnt = 0;

            dts = dbc.SelectQuerryFixed("SELECT [SaldoFinal] FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND RDate = '" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND Moneda = '" + GetMoneda() + "'");

            if (dts.Tables[0].Rows.Count > 0)
            {
                diaAnt = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            else
            {
                diaAnt = GetDiaAnt();
            }

            double entrada = 0;
            if (ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[6].Value != null)
            {
                entrada = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[6].Value.ToString());
            }


            double salida = AmountIPV(comboBox1.Text);
            double traslado = 0;
            if (ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[14].Value != null)
            {
                traslado = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[14].Value.ToString());
            }



            double eint = 0;
            double sint = 0;
            //EntInt.Items.Clear();
            //SalInt.Items.Clear();

            if (ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value != null)
            {
                eint = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[8].Value.ToString());
            }
            if (ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[12].Value != null)
            {
                sint = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[12].Value.ToString());
            }

            //if (comboBox1.Text != "")
            //{
            //    IntTransf();
            //    eint = GenEntInt();
            //    sint = GenSalInt();
            //}

            double saldofinal = (entrada + diaAnt + eint) - (salida + traslado + sint);

            //  saldofinal = System.Math.Round(saldofinal, 3);


            double aux = 0;
            if (ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value != null)
            {
                aux = System.Convert.ToDouble(GetTotal(ramal20Base, 17));
                //aux = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value.ToString());
            }

            double sal2 = (entrada + diaAnt) - (salida + traslado);

            saldofinal = System.Math.Round(saldofinal, 2);

            double compramal = saldofinal - aux - (System.Math.Round(AmountIPV4(), 3));
            if (compramal < 0.01 && compramal > 0)
                compramal = 0;


            SubMayorBase.Rows.Add(dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), System.Convert.ToString(System.Math.Round(diaAnt, 2)), System.Convert.ToString(System.Math.Round(entrada, 2)), System.Convert.ToString(System.Math.Round(eint, 2)), System.Convert.ToString(System.Math.Round(salida, 2)), System.Convert.ToString(System.Math.Round(sint, 2)), System.Convert.ToString(System.Math.Round(traslado, 2)), System.Convert.ToString(System.Math.Round(saldofinal, 2)), System.Convert.ToString(System.Math.Round(compramal, 2)));
            SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[SubMayorBase.ColumnCount - 1].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[SubMayorBase.ColumnCount - 1].Value), 2);
            SubMayorBase.Rows[SubMayorBase.RowCount - 1].Tag = tag;

            if (comboBox1.Text != "")
            {
                SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].ToolTipText = GenEntIntTTT();
                SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[6].ToolTipText = GenSalIntTTT();
            }


        }

        private double GenEntInt()
        {


            double ent = 0;
            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "In")
                {
                    ent += System.Convert.ToDouble(ib.Saldo);
                    // ent+= System.Math.Round(System.Convert.ToDouble(ib.Saldo),2);
                    //EntInt.Items.Add("Desde la: "+ib.FromTo+" ("+ib.producto+") -Saldo: $"+ib.Saldo);
                }
            }

            // EntInt.Items.Insert(0, ent);

            return ent;
            // SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].Value = ent;
            // SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].Value = SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].FormattedValue;


        }

        private double GenSalInt()
        {



            double sal = 0;
            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "Out")
                {
                    sal += System.Math.Round(System.Convert.ToDouble(ib.Saldo));
                    // SalInt.Items.Add("Hacia la: " + ib.FromTo + " (" + ib.producto + ") -Saldo: $" + ib.Saldo);
                }
            }

            // SalInt.Items.Insert(0, sal);

            return sal;
            // SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].Value = sal;
            // SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].Value = SubMayorBase.Rows[SubMayorBase.RowCount - 1].Cells[4].FormattedValue;


        }

        private String GenEntIntTTT()
        {


            String ttt = "";
            foreach (InBetween ib in Transfers)
                if (ib.Cuenta == comboBox1.Text && ((ib.Tipo == "In") || (ib.Tipo == "Des")))
                    ttt += "Desde la: " + ib.FromTo + " (" + ib.producto + ") -Saldo: $" + System.Math.Round(ib.Saldo, 2) + "\n";




            if (ttt == "")
                ttt = "No hay Entradas...";

            return ttt;


        }

        private String GenSalIntTTT()
        {



            String ttt = "";
            foreach (InBetween ib in Transfers)
                if (ib.Cuenta == comboBox1.Text && ib.Tipo == "Out")
                    ttt += "Hacia la: " + ib.FromTo + " (" + ib.producto + ") -Saldo: $" + System.Math.Round(ib.Saldo, 2) + "\n";




            if (ttt == "")
                ttt = "No hay Salidas...";

            return ttt;


        }



        private double AmountIPV(String cuenta)
        {

            double amount = 0;
            ArrayList ipv = new ArrayList();
            for (int w = 1; w < tabControl1.TabCount - 2; w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([CVendido]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'");

                    if (dts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                        amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());

                    ipv.Add("IPV " + IPVName);


                }

                if (!tabControl1.TabPages[w+1].Text.Contains("IPV "))
                {
                    break;
                }
            }

            foreach (ValueSaver val in IPVSaver)
            {
                if (!NotIn(ipv, val.IPVName) && val.col == 10 && val.producto == "Totales:" && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.Cuenta == cuenta)
                {
                    amount += System.Math.Round(System.Convert.ToDouble(val.cant), 2);//* GetPrice(val.row, val.IPVName), 3);
                }
            }



            return amount;


        }
        private double AmountIPV2(String cuenta)
        {

            double amount = 0;
            //bool chck = true;

            ArrayList ipv = new ArrayList();
            for (int w = 1; w < tabControl1.TabCount - GetIPVTop(); w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([VIngreso]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'");

                    if (dts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                        amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());

                    ipv.Add(IPVName);
                }


            }


            foreach (ValueSaver val in IPVSaver)
            {
                if (!NotIn(ipv, val.IPVName.Replace("IPV ", "")) && val.col == 8 && val.producto == "Totales:" && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.Cuenta == cuenta)
                {
                    amount += System.Math.Round(System.Convert.ToDouble(val.cant), 2);//* GetPrice(val.row, val.IPVName), 3);
                }
            }

            ////foreach (ValueSaver val in IPVSaver)
            ////{
            ////    if (!NotIn(ipv, val.IPVName.Replace("IPV ", "")) && val.col == 7 && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.Cuenta == cuenta)
            ////    {
            ////        amount += System.Math.Round(System.Convert.ToDouble(val.cant) * GetPrice222(val.row, val.IPVName,cuenta), 2);
            ////    }
            ////}



            return amount;


        }

        private double AmountIPV5(String cuenta)
        {

            double amount = 0;
            ArrayList ipv = new ArrayList();
            for (int w = 1; w < tabControl1.TabCount - 2; w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([EImporte]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'");

                    if (dts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                        amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());

                    ipv.Add("IPV " + IPVName);


                }

                if (!tabControl1.TabPages[w + 1].Text.Contains("IPV "))
                {
                    break;
                }
            }

            foreach (ValueSaver val in IPVSaver)
            {
                if (!NotIn(ipv, val.IPVName) && val.col == 6 && val.producto == "Totales:" && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.Cuenta == cuenta)
                {
                    amount += System.Math.Round(System.Convert.ToDouble(val.cant), 2);//* GetPrice(val.row, val.IPVName), 3);
                }
            }



            return amount;


        }
        private bool NotIn(ArrayList lista, String ipv)
        {
            foreach (String i in lista)
            {
                if (i == ipv)
                {
                    return true;
                }
            }
            return false;
        }
        private double AmountIPV3(String cuenta)
        {

            double amount = 0;
            ArrayList itpass = new ArrayList();

            for (int w = 1; w < tabControl1.TabCount - GetIPVTop(); w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([VIngreso]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'");

                    amount = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                    itpass.Add(IPVName);

                }


            }
            foreach (ValueSaver val in IPVSaver)
            {
                if (!NotIn(itpass, val.IPVName) && val.col == 7 && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.Cuenta == cuenta)
                {
                    amount += System.Math.Round(System.Convert.ToDouble(val.cant) * GetPrice(val.row, val.IPVName), 2);
                }
            }

            return amount;


        }
        private int GetIPVTop()
        {
            int top = 2;
            if (GetMoneda() == "CUP")
                top = 4;
            return top;
        }
        private double AmountIPV4()
        {

            double amount = 0;
            ArrayList itpass = new ArrayList();

            for (int w = 1; w < tabControl1.TabCount - GetIPVTop(); w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([FCosto]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + IPVName + "'");

                    amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                    itpass.Add("IPV " + IPVName);


                }


            }




            foreach (ValueSaver val in IPVSaver)
            {
                if (!NotIn(itpass, val.IPVName) && val.col == 12 && val.producto == "Totales:" && val.Cuenta == comboBox1.Text && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    amount += System.Math.Round(System.Convert.ToDouble(val.cant), 3);
                    itpass.Add(val.IPVName);
                }


            }



            for (int w = 1; w < tabControl1.TabCount - GetIPVTop(); w++)
            {
                if (!NotIn(itpass, tabControl1.TabPages[w].Text))
                {

                    System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                    if (dbc.ExistQuerry("SELECT [Producto],[UM],Producto.PrecOut,[FCantidad],Producto.PrecIn FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + IPVName + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = IPV.Cuenta"))
                    {
                        dts = dbc.SelectQuerryFixed("SELECT SUM([FCosto]) FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + IPVName + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = IPV.Cuenta");
                        amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                    }

                }
            }





            return amount;


        }

        private double GetPrice(int row, String ipv)
        {
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == 9 && val.IPVName == ipv && val.Cuenta == comboBox1.Text && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    return System.Convert.ToDouble(val.cant);
                }


            }

            return 0;

        }
        private double GetPrice2(int row, String ipv)
        {
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == 2 && val.IPVName == ipv && val.Cuenta == comboBox1.Text && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    return System.Convert.ToDouble(val.cant);
                }


            }

            return 0;

        }

        private double GetPrice222(int row, String ipv, String cuenta)
        {
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == 2 && val.IPVName == ipv && val.Cuenta == cuenta && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                {
                    return System.Convert.ToDouble(val.cant);
                }


            }

            return 0;

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRamal20();
            LoadSubMayor();
            // LoadResIng();
            LoadFichaCosto();
            LoadValeSalida();


            if (tabControl1.SelectedIndex > 0 && tabControl1.SelectedIndex < tabControl1.TabCount)
            {
                LoadIPV(tabControl1.TabPages[tabControl1.SelectedIndex].Text.Replace("IPV ", ""));
            }

            Form1_Resize(sender, e);



        }

        private void LoadValeSalida()
        {
            try
            {
                ValeSalidaBase.Rows.Clear();
                bool gray = false;

                // if (comboBox1.Text =="")
                // {
                if (dbc.ExistQuerry("Select[Id]FROM ValeSalida WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "'"))
                {
                    dts = dbc.SelectQuerryFixed("Select [Producto],[UM],[Cantidad],[Precio],[Importe] FROM ValeSalida WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "' ORDER BY Id");
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        ValeSalidaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString());
                    }
                    gray = true;
                }
                else
                {

                    if (HayRamalSaver())
                    {


                        int w = 0;
                        double final = 0;

                        while (w < ramal20Base.RowCount - 2 && GetData(ramal20Base.Rows[w].Cells[0].Value) != " ")
                        {
                            if ((System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[9].Value)) != 0 || System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[11].Value)) != 0) && (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[7].Value)) == 0 || System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[9].Value)) == 0))
                            {
                                double cant = System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[9].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[11].Value)), 2);
                                double imp = System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[10].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[12].Value)), 2);
                                // double imp = System.Math.Round(cant * System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[2].Value)), 2);
                                ValeSalidaBase.Rows.Add(GetData(ramal20Base.Rows[w].Cells[0].Value), GetData(ramal20Base.Rows[w].Cells[1].Value), System.Convert.ToString(cant), GetData(ramal20Base.Rows[w].Cells[2].Value), System.Convert.ToString(imp));
                                final = final + imp;
                            }
                            //final = final + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[10].Value));
                            w++;
                        }
                        ValeSalidaBase.Rows.Add("Total", " ", " ", " ", System.Convert.ToDouble(System.Math.Round(final, 2)));


                        ValeSalidaBase.Rows[ValeSalidaBase.RowCount - 1].Cells[0].Style.Font = ramal20Base.Columns[3].DefaultCellStyle.Font;
                        ValeSalidaBase.Rows[ValeSalidaBase.RowCount - 1].Cells[ValeSalidaBase.ColumnCount - 1].Style.Font = ramal20Base.Columns[3].DefaultCellStyle.Font;


                    }
                }

                //  }

                if (gray)
                {
                    tabControl1.SelectedTab.BackColor = Color.LightGray;

                }
                else
                {
                    if (VSIndex() > 0)
                        tabControl1.TabPages[VSIndex()].BackColor = Color.Transparent;
                }



            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }




        }
        private double Cantidad2(String ipv, int row, int col)
        {
            foreach (ValueSaver val in IPVSaver)
            {

                //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                if (val.col == col && val.row == row && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    //go = false;
                    return System.Convert.ToDouble(val.cant);
                }

            }

            return 0;
        }

        private double Cantidad3(String ipv, String prod, String cuenta)
        {
            double cant = 0;
            foreach (ValueSaver val in IPVSaver)
            {

                //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                if (val.col == 5 && val.producto == prod && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    //go = false;
                    cant = System.Convert.ToDouble(val.cant);
                }

            }

            return cant;
        }
        private void IntTransf()
        {
            Transfers.Clear();


            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName.Contains("IPV "))
                {

                    if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value))
                    {
                        FillTrasfers(val.cant, System.Convert.ToDouble(Cantidad2(val.IPVName, val.row, 5).ToString()), val.IPVName);
                    }
                }
            }

        }
        private void IntTransf2()
        {
            Transfers.Clear();


            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName.Contains("IPV "))
                {

                    if (val.col == 0 && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value))
                    {
                        double canti = System.Convert.ToDouble(Cantidad3(val.IPVName, val.producto,"").ToString());
                        if (canti != 0)
                        {
                            FillTrasfers2(val.cant, canti, val.IPVName);
                        }

                    }
                    //if (val.col == 0 && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == GetCuenta(val.cant) && val.Date == DateCultureConverter(dateTimePicker1.Value))
                    //{
                    //    double canti = System.Convert.ToDouble(Cantidad3(val.IPVName, val.producto, GetCuenta(val.cant)).ToString());
                    //    if (canti!=0)
                    //    {
                    //        FillTrasfers2(val.cant,canti , val.IPVName);
                    //    }
                        
                    //}
                }
            }


            foreach (InBetween ib in Bkptransf)
            {
                if (ib.eprod == DateCultureConverter(dateTimePicker1.Value) && ib.ipv == tabControl2.SelectedTab.Text)
                    Transfers.Add(ib);
            }
            //if (Transfers.Count==0)
            //{

            //foreach (ValueSaver val in IPVSaver)
            //{
            //    if (val.IPVName.Contains("IPV "))
            //    {

            //        if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value))
            //        {
            //            FillTrasfers(val.cant, System.Convert.ToDouble(Cantidad2(val.IPVName, val.row, 5).ToString()));
            //        }
            //    }
            //}
            //}

        }

        private void LoadFichaCosto()
        {
            try
            {
                counter = 0;
                FichaCostoBase.Rows.Clear();
                bool gray = false;
                //  Transfers.Clear();
                //if (comboBox1.Text == "")
                //{



                if (dbc.ExistQuerry("Select[Id]FROM FichaCosto WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "'"))
                {
                    dts = dbc.SelectQuerryFixed("Select [EProducto],[Raciones],[Producto],[UM],[Norma],[Cantidad],[Precio],[Importe] FROM FichaCosto WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "' ORDER BY Id");
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        if (dts.Tables[0].Rows[w].ItemArray[0].ToString() == "" && dts.Tables[0].Rows[w].ItemArray[5].ToString() != "")
                        {
                            FichaCostoBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString(), System.Math.Round(System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[5].ToString()), 2), dts.Tables[0].Rows[w].ItemArray[6].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString());
                        }
                        else
                            FichaCostoBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString(), dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[6].ToString(), dts.Tables[0].Rows[w].ItemArray[7].ToString());

                    }
                    gray = true;
                }
                else if (HayRamalSaver())
                {
                    foreach (ValueSaver val in IPVSaver)
                    {
                        if (val.IPVName.Contains("IPV "))
                        {
                            //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                            if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                            {
                                //go = false;
                                //IPVBase.Rows[val.row].Cells[val.col].Value = System.Convert.ToString(val.cant);
                                FichaCosto(val.IPVName, val.cant, Cantidad2(val.IPVName, val.row, 5).ToString());
                                // FillTrasfers( val.cant, System.Convert.ToDouble(Cantidad2(val.IPVName, val.row, 5).ToString()));
                            }
                        }
                    }

                    rePutinFCosto();

                }
                if (tabControl1.SelectedTab.Text == "Ficha de Costo")
                {
                
                    if (gray)
                    {
                        tabControl1.TabPages[VSIndex()-1].BackColor = Color.LightGray;
                       if(editarVariablesToolStripMenuItem.Enabled)
                        forzar.Enabled = true;
                    }
                    else
                    {
                        if (VSIndex()-1 > 0)
                            tabControl1.TabPages[VSIndex()-1].BackColor = Color.Transparent;
                    }    
                    if (hlpeve)
                    {
                        if (FichaCostoBase.Rows.Count==0)
                        {
                            if (FCGetIfError())
                            {
                                hlpManager.hlpknd = HelpKind.Resolucion;
                                hlpManager.topic = "Ficha de Costo";
                                hlpManager.tnumber = 1;
                                
                                    hlpManager.errknd = ErrorKind.FC_CHK_IPV;
                                    hlpManager.atext.Clear();
                                    //hlpManager.atext.Add(dif.ToString());
                            }
                        }

                       


                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }

            // int kk = Transfers.Count;
        }
        private bool FCGetIfError()
        {
            
                    foreach (ValueSaver val in IPVSaver)
                        {
                            if (val.IPVName.Contains("IPV "))
                            {
                                //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                                if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value))
                                {
                                    if (GetCuenta(val.producto)== comboBox1.Text)
                                    {
                                        return true;
                                    }
                                    
                                }
                            }
                        }

            return false;
                       
        }
        private int VSIndex()
        {
            int idx = 0;
            for (int w = tabControl1.TabCount - 1; w > 0; w--)
            {
                if (tabControl1.TabPages[w].Text == "Vale de Salida")
                {
                    idx = w;
                    break;
                }
            }

            return idx;
        }
        private double GetRCant(String prod)
        {


            double rcant = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName.Contains("IPV "))
                {

                    if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                    {

                        rcant += CantGetter(val.IPVName, val.cant, Cantidad2(val.IPVName, val.row, 5).ToString(), prod);
                    }
                }
            }

            return rcant;
        }
        private double GetRImp(String prod)
        {


            double rimp = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName.Contains("IPV "))
                {

                    if (val.col == 0 && val.row < IPVBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                    {

                        rimp += ImpGetter(val.IPVName, val.cant, Cantidad2(val.IPVName, val.row, 5).ToString(), prod);
                    }
                }
            }

            return rimp;
        }

        private String GetCuenta(String eprod)
        {
            System.Data.DataSet dts = dbc.SelectQuerryFixed("SELECT  SUM(Producto.PrecIn*EProducto.Cantidad) as costo,Producto.Nombre, Producto.Cuenta FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' GROUP BY  Producto.Nombre, Producto.Cuenta ORDER BY costo DESC");
            if (dts.Tables[0].Rows.Count > 0)
            {
                return dts.Tables[0].Rows[0].ItemArray[2].ToString();
            }

            return "Not";

        }

        private String GetProd(String eprod)
        {
            System.Data.DataSet dts = dbc.SelectQuerryFixed("SELECT SUM(Producto.PrecIn*EProducto.Cantidad)  as costo,Producto.Nombre, Producto.Cuenta FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' GROUP BY  Producto.Nombre, Producto.Cuenta ORDER BY costo DESC");
            if (dts.Tables[0].Rows.Count > 0)
            {
                return dts.Tables[0].Rows[0].ItemArray[1].ToString();
            }

            return "Not";

        }

        private void FillTrasfers2(String eprod, double cant, String ipv)
        {
            //  double saldo = 0;
            System.Data.DataSet dts33 = dbc.SelectQuerryFixed("SELECT  Producto.PrecIn,Producto.Nombre, Producto.Cuenta, Producto.Id, EProducto.Id FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' AND Producto.Cuenta != '" + GetCuenta(eprod) + "'");
            for (int w = 0; w < dts33.Tables[0].Rows.Count; w++)
            {

                //  if (!CheckIsIn(GetCuenta(eprod), GetChikSaldo(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())), "In", dts33.Tables[0].Rows[w].ItemArray[2].ToString(), dts33.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod))
                // {
                Transfers.Add(new MLB.InBetween(GetCuenta(eprod), GetChikSaldo(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())), "In", dts33.Tables[0].Rows[w].ItemArray[2].ToString(), dts33.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod, ipv));
                Transfers.Add(new MLB.InBetween(dts33.Tables[0].Rows[w].ItemArray[2].ToString(), GetChikSaldo(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())), "Out", GetCuenta(eprod), dts33.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod, ipv));
                //  }
            }

            dts33 = dbc.SelectQuerryFixed("SELECT  Producto.PrecIn,Producto.Nombre, Producto.Cuenta, Producto.Id, EProducto.Id FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' AND Producto.Cuenta = '" + GetCuenta(eprod) + "'");
            for (int w = 0; w < dts33.Tables[0].Rows.Count; w++)
            {

                // if (!CheckIsIn(GetCuenta(eprod), GetChikSaldo(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())), "Own", dts33.Tables[0].Rows[w].ItemArray[2].ToString(), dts33.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod))
                //{
                Transfers.Add(new MLB.InBetween(GetCuenta(eprod), GetChikSaldo(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())), "Own", dts33.Tables[0].Rows[w].ItemArray[2].ToString(), dts33.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts33.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts33.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod, ipv));
                //Transfers.Add(new MLB.InBetween(dts.Tables[0].Rows[w].ItemArray[2].ToString(), GetChikSaldo(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())), "Out", GetCuenta(eprod), dts.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod));
                // }
            }




        }

        private void FillTrasfers(String eprod, double cant, String ipv)
        {
            //  double saldo = 0;
            System.Data.DataSet dts = dbc.SelectQuerryFixed("SELECT  Producto.PrecIn,Producto.Nombre, Producto.Cuenta, Producto.Id, EProducto.Id FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' AND Producto.Cuenta != '" + GetCuenta(eprod) + "'");
            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                if (!CheckIsIn(GetCuenta(eprod), GetChikSaldo(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())), "In", dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod))
                {
                    Transfers.Add(new MLB.InBetween(GetCuenta(eprod), GetChikSaldo(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())), "In", dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod, ipv));
                    Transfers.Add(new MLB.InBetween(dts.Tables[0].Rows[w].ItemArray[2].ToString(), GetChikSaldo(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())), "Out", GetCuenta(eprod), dts.Tables[0].Rows[w].ItemArray[1].ToString(), GetChikCant(eprod, dts.Tables[0].Rows[w].ItemArray[1].ToString(), cant, System.Convert.ToInt32(dts.Tables[0].Rows[w].ItemArray[4].ToString())).ToString(), eprod, ipv));

                }

            }




        }

        private bool CheckIsIn(String Cuenta, double Saldo, String Tipo, String FromTo, String producto, String cant, String eprod)
        {

            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == Cuenta && ib.Saldo == Saldo && ib.Tipo == Tipo && ib.FromTo == FromTo && ib.producto == producto && ib.cant == cant && ib.eprod == eprod)
                {
                    return true;
                }
            }

            return false;
        }

        private double GetChikSaldo(String eprod, String prod, double cant, int id)
        {
            // double saldo = 0;
            System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT  EProducto.Cantidad, EProducto.PUM,Producto.PrecIn, Producto.DUM,EProducto.UM, EProducto.Cuenta  FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "'AND Producto.Nombre = '" + prod + "' AND EProducto.Id = '" + id.ToString() + "'");
            if (dts88.Tables[0].Rows.Count > 0)
            {

                // 
                //  return (System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString()) * cant / System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString())) * System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[2].ToString());
                double kant = System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[0].ToString()) * cant / System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[1].ToString());

                kant = UMConverter(kant, dts88.Tables[0].Rows[0].ItemArray[3].ToString(), dts88.Tables[0].Rows[0].ItemArray[4].ToString());
                // IntTransf2();
                double ext = Existencia2(prod, dts88.Tables[0].Rows[0].ItemArray[5].ToString());
                double rango = GetRango(eprod, prod);
                if ((ext - System.Convert.ToDouble(kant)) <= rango && (ext - System.Convert.ToDouble(kant)) > (rango * -1))
                {
                    kant = ext;
                }
                return kant * System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[2].ToString());
            }

            return 0;

        }

        private double GetChikCant(String eprod, String prod, double cant, int id)
        {
            // double saldo = 0;
            System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT  EProducto.Cantidad, EProducto.PUM, Producto.DUM,EProducto.UM,EProducto.Cuenta FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "'AND Producto.Nombre = '" + prod + "' AND EProducto.Id = '" + id.ToString() + "'");
            if (dts88.Tables[0].Rows.Count > 0)
            {
                double kant = System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[0].ToString()) * cant / System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[1].ToString());

                kant = UMConverter(kant, dts88.Tables[0].Rows[0].ItemArray[2].ToString(), dts88.Tables[0].Rows[0].ItemArray[3].ToString());
                // IntTransf2();
                double ext = Existencia2(prod, dts88.Tables[0].Rows[0].ItemArray[4].ToString());
                double rango = GetRango(eprod, prod);
                if ((ext - System.Convert.ToDouble(kant)) <= rango && (ext - System.Convert.ToDouble(kant)) > (rango * -1))
                {
                    kant = ext;
                }
                return kant;
            }

            return 0;

        }
        private int GetKant(String eprod, String prod)
        {
            return System.Convert.ToInt32(dbc.SelectQuerryFixed("SELECT COUNT(EProducto.Producto) FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' AND Producto.Nombre = '" + prod + "'").Tables[0].Rows[0].ItemArray[0].ToString());
        }
        private bool CheckerDown(ArrayList al, String prod, String eprod)
        {
            al.Add(prod);
            return (GetKant(eprod, prod) == CountAL(al, prod));

        }
        private double Infamen(String ipv, String prod, String eprod, String cant, String uprec)
        {
            double ramal = CantGetter(ipv, eprod, cant, prod) * System.Convert.ToDouble(uprec);
            double fcosto = ImpGetter(ipv, eprod, cant, prod);

            ramal = System.Math.Round(ramal, 2);
            fcosto = System.Math.Round(fcosto, 2);
            return ramal - fcosto;

        }
        private int CountAL(ArrayList al, String prod)
        {
            int w = 0;
            foreach (String p in al)
            {
                if (p == prod)
                {
                    w++;
                }
            }
            return w;

        }

        private int HowMany(ArrayList pxa, String prod, String cuenta)
        {
            pxa.Add(prod);
            pxa.Add(cuenta);
            int count = 0;

            for (int w = 0; w < pxa.Count; w += 2)
            {
                String val1 = (String)pxa[w];
                String val2 = (String)pxa[w + 1];
                if (val1 == prod && val2 == cuenta)
                {
                    count++;
                }

            }

            return count;
        }
        private void FichaCosto(String ipv, String eprod, String cant)
        {
            if (System.Convert.ToDouble(cant)!=0)
            {
           
            FichaCostoBase.Rows.Add(eprod, cant, "-", "-", "IPV", ipv.Replace("IPV ", ""), "-", "-");
            System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT Producto.Nombre, EProducto.UM, Producto.PrecIn, Cantidad, PUM, Producto.DUM,Producto.Cuenta FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' ORDER BY Producto.Nombre");

            double final = 0;
            ArrayList al = new ArrayList();
            ArrayList pxa = new ArrayList();


            for (int w = 0; w < dts88.Tables[0].Rows.Count; w++)
            {
                double cant2 = System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[3].ToString()) * System.Convert.ToDouble(cant) / System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[4].ToString());
                double dealcant = cant2;
                cant2 = System.Math.Round(UMConverter(cant2, dts88.Tables[0].Rows[w].ItemArray[5].ToString(), dts88.Tables[0].Rows[w].ItemArray[1].ToString()), 2);
                //IntTransf2();

                double rcant = GetYourCant2(eprod, dts88.Tables[0].Rows[w].ItemArray[0].ToString(), dts88.Tables[0].Rows[w].ItemArray[6].ToString(), HowMany(pxa, dts88.Tables[0].Rows[w].ItemArray[0].ToString(), dts88.Tables[0].Rows[w].ItemArray[6].ToString()), ipv);
                double real = 0;
                bool pinter = false;
                if (cant2 != rcant)
                {
                    real = cant2;
                    cant2 = rcant;
                    pinter = true;
                }

                //bool pinter = false;
                //double real = 0;

                //double ext = Existencia(dts88.Tables[0].Rows[w].ItemArray[0].ToString(), dts88.Tables[0].Rows[w].ItemArray[6].ToString());
                //double rango = GetRango(eprod, dts88.Tables[0].Rows[w].ItemArray[0].ToString());
                //if ((ext - System.Convert.ToDouble(cant2)) <= rango && (ext - System.Convert.ToDouble(cant2)) > (rango * -1))
                //{
                //    real = cant2;
                //    cant2 = ext;
                //    pinter = true;
                //}

                double imp = System.Math.Round(System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[2].ToString()) * cant2, 2); // System.Math.Round(UMConverter(cant2, dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString()),2),2);

                bool check = false;
                if (CheckerDown(al, dts88.Tables[0].Rows[w].ItemArray[0].ToString(), eprod))
                {
                    double aux = Infamen(ipv, dts88.Tables[0].Rows[w].ItemArray[0].ToString(), eprod, cant, dts88.Tables[0].Rows[w].ItemArray[2].ToString());
                    imp = imp + aux;
                    if (aux != 0)
                        check = true;
                }
                final = final + imp;
                if (System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[4].ToString()) == 100)
                    FichaCostoBase.Rows.Add("", "-", dts88.Tables[0].Rows[w].ItemArray[0].ToString(), dts88.Tables[0].Rows[w].ItemArray[5].ToString(), dts88.Tables[0].Rows[w].ItemArray[3].ToString() + " (" + dts88.Tables[0].Rows[w].ItemArray[1].ToString() + ")" + "/" + dts88.Tables[0].Rows[w].ItemArray[4].ToString(), System.Convert.ToString(cant2), dts88.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp, 2)));
                else
                {
                    double myprop = System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[3].ToString()) * 100 / System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[4].ToString());
                    myprop = System.Math.Round(myprop, 2);

                    FichaCostoBase.Rows.Add("", "-", dts88.Tables[0].Rows[w].ItemArray[0].ToString(), dts88.Tables[0].Rows[w].ItemArray[5].ToString(), myprop.ToString() + " (" + dts88.Tables[0].Rows[w].ItemArray[1].ToString() + ")" + "/" + "100", System.Convert.ToString(cant2), dts88.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp, 2)));

                }
                // FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].ReadOnly = false;
                //  FichaCostoBase.Rows.Add("", "-", dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString() + "/" + dts.Tables[0].Rows[w].ItemArray[4].ToString(), System.Convert.ToString(cant2), dts.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp,2)));
                if (check)
                {
                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].Style.ForeColor = Color.DarkBlue;
                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].ToolTipText = System.Convert.ToString(System.Math.Round(cant2, 2) * System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[2].ToString()));
                }
                if (pinter)
                {
                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].Style.ForeColor = Color.DarkBlue;
                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 3].Style.ForeColor = Color.DarkBlue;

                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 3].ToolTipText = System.Convert.ToString(System.Math.Round(real, 2));
                    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].ToolTipText = System.Convert.ToString(System.Math.Round(real * System.Convert.ToDouble(dts88.Tables[0].Rows[w].ItemArray[2].ToString()), 2));
                }
            }

            // double aux = 0;
            // if (System.Convert.ToDouble(cant)>0)
            //{

            // double some = System.Math.Round(System.Convert.ToDouble(GetUnitPrice2(eprod, System.Convert.ToDouble(cant))) * System.Convert.ToDouble(cant), 2);
            //   if (System.Math.Round(final,2)!=some)
            //   {
            //       aux = final;
            //       final = some;
            //   }
            //}
            FichaCostoBase.Rows.Add("Final:", " ", " ", "-", " ", " ", " ", System.Convert.ToString(System.Math.Round(final, 2)));
            FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[0].Style.Font = ramal20Base.Columns[3].DefaultCellStyle.Font;
            FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].Style.Font = ramal20Base.Columns[3].DefaultCellStyle.Font;
            //if (aux>0)
            //{
            //    FichaCostoBase.Rows[FichaCostoBase.RowCount - 1].Cells[FichaCostoBase.ColumnCount - 1].ToolTipText = System.Convert.ToString(aux);
            //}
            FichaCostoBase.Rows.Add();

            }
        }
        private double CantGetter(String ipv, String eprod, String cant, String prod)
        {

            //  FichaCostoBase.Rows.Add(eprod, cant, "-", "-", "IPV", ipv.Replace("IPV ", ""), "-", "-");
            dts = dbc.SelectQuerryFixed("SELECT Producto.Nombre, EProducto.UM, Producto.PrecIn, Cantidad, PUM, Producto.DUM FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' ORDER BY Producto.Nombre");

            double final = 0;

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                if (prod == dts.Tables[0].Rows[w].ItemArray[0].ToString())
                {
                    double cant2 = System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[3].ToString()) * System.Convert.ToDouble(cant) / System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[4].ToString());

                    double cant3 = System.Math.Round(UMConverter(cant2, dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString()), 2);
                    final = final + cant3;
                }

                //  FichaCostoBase.Rows.Add("", "-", dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString() + "/" + dts.Tables[0].Rows[w].ItemArray[4].ToString(), System.Convert.ToString(cant2), dts.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp, 2)));
            }

            return final;


        }

        private double ImpGetter(String ipv, String eprod, String cant, String prod)
        {

            //  FichaCostoBase.Rows.Add(eprod, cant, "-", "-", "IPV", ipv.Replace("IPV ", ""), "-", "-");
            dts = dbc.SelectQuerryFixed("SELECT Producto.Nombre, EProducto.UM, Producto.PrecIn, Cantidad, PUM, Producto.DUM FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' ORDER BY Producto.Nombre");

            double final = 0;

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                if (prod == dts.Tables[0].Rows[w].ItemArray[0].ToString())
                {
                    double cant2 = System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[3].ToString()) * System.Convert.ToDouble(cant) / System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[4].ToString());

                    double imp = System.Math.Round(System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[2].ToString()) * System.Math.Round(UMConverter(cant2, dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString()), 2), 2);
                    final = final + imp;
                }

                //  FichaCostoBase.Rows.Add("", "-", dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString() + "/" + dts.Tables[0].Rows[w].ItemArray[4].ToString(), System.Convert.ToString(cant2), dts.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp, 2)));
            }

            return final;


        }

        //private double Verifyer(String prod, String eprod, String cant)
        //{

        //  //  FichaCostoBase.Rows.Add(eprod, cant, "-", "-", "IPV", ipv.Replace("IPV ", ""), "-", "-");
        //    dts = dbc.SelectQuerryFixed("SELECT Producto.Nombre, EProducto.UM, Producto.PrecIn, Cantidad, PUM, Producto.DUM FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "' AND Producto.Nombre = '" + prod + "' ORDER BY Producto.Nombre");

        //    double final = 0;

        //    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
        //    {
        //        double cant2 = System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[3].ToString()) * System.Convert.ToDouble(cant) / System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[4].ToString());

        //        double imp = System.Math.Round(System.Convert.ToDouble(dts.Tables[0].Rows[w].ItemArray[2].ToString()) * System.Math.Round(UMConverter(cant2, dts.Tables[0].Rows[w].ItemArray[5].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString()), 2), 2);
        //        final = final + imp;
        //     //   FichaCostoBase.Rows.Add("", "-", dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString() + "/" + dts.Tables[0].Rows[w].ItemArray[4].ToString(), System.Convert.ToString(cant2), dts.Tables[0].Rows[w].ItemArray[2].ToString(), System.Convert.ToString(System.Math.Round(imp, 2)));
        //    }

        //    return final;

        //}
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl2.SelectedTab.Controls.Add(PanelOP);
            LoadComoBox();
            LoadRamal20();
            RefreshIPV();
            LoadSubMayor();
            LoadResIng();
            LoadFichaCosto();
            LoadValeSalida();
            if (tabControl1.SelectedIndex > 0 && tabControl1.SelectedIndex < tabControl1.TabCount)
            {
                LoadIPV(tabControl1.TabPages[tabControl1.SelectedIndex].Text.Replace("IPV ", ""));
            }
            CreateSubmenu();
            FillConcepts();
            if (IsCadena(tabControl2.SelectedTab.Text))
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }


        }
        private bool IsCadena(String unidad)
        {
            if (dbc.ExistQuerry("Select Id From Unidad Where Nombre = '" + unidad + "' And Categoria = 'Cadena'"))
                return true;
            return false;
        }
        private void FillConcepts()
        {
            WDF();

            colconcep.Items.Clear();
            dts = dbc.SelectQuerryFixed("Select Concepto From Concepto Where UName = '" + tabControl2.SelectedTab.Text + "'");

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                colconcep.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            }
        }

        private void WDF()
        {
            if (!dbc.ExistQuerry("Select Id From Concepto Where Concepto = 'Ingresos por Ventas'"))
            {
                dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Concepto]([Id],[Tipo],[Concepto],[UName] ,[Grupo])" +
            "VALUES  ('" + dbc.MaxQuerry("Concepto") + "','Entradas','Ingresos por Ventas','" + tabControl2.SelectedTab.Text +
            "','Ingresos por Ventas')");

            }
            if (!dbc.ExistQuerry("Select Id From Concepto Where Concepto = 'Compra de Mercancias'"))
            {
                dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Concepto]([Id],[Tipo],[Concepto],[UName] ,[Grupo])" +
            "VALUES  ('" + dbc.MaxQuerry("Concepto") + "','Salidas','Compra de Mercancias','" + tabControl2.SelectedTab.Text +
            "','Gastos Operacionales')");

            }
            if (!dbc.ExistQuerry("Select Id From Concepto Where Concepto = 'Costos de Traslados'"))
            {
                dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Concepto]([Id],[Tipo],[Concepto],[UName] ,[Grupo])" +
            "VALUES  ('" + dbc.MaxQuerry("Concepto") + "','Salidas','Costos de Traslados','" + tabControl2.SelectedTab.Text +
            "','Gastos Operacionales')");

            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadRamal20();
            LoadSubMayor();
            LoadResIng();

            LoadFichaCosto();
            LoadValeSalida();
            if (tabControl1.SelectedIndex > 0 && tabControl1.SelectedIndex < tabControl1.TabCount)
            {
                LoadIPV(tabControl1.TabPages[tabControl1.SelectedIndex].Text.Replace("IPV ", ""));
            }
            dateTimePicker2.Value = dateTimePicker1.Value;
            dateTimePicker3.Value = dateTimePicker2.Value;

            LoadFlujoCaja();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           // UMLoader();


            ConexionFix();

            if (!failed)
            {
          
            PanelOP_Resize(sender, e);
            // dbc = new MLB.DBControl();

            if (dbc.ExistQuerry("Select*from Sistema where SysView='True'"))
            {
                lightToolStripMenuItem.Checked = false;
                darkViewToolStripMenuItem.Checked = true;
                darkViewToolStripMenuItem_Click(sender, e);
                SetVissualState(darkViewToolStripMenuItem.Checked);
            }
             
            dts = dbc.SelectQuerryFixed("SELECT Nombre FROM Unidad");


            if (dts.Tables[0].Rows.Count > 0)
            {
                tabControl2.TabPages[0].Text = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                for (int w = 1; w < dts.Tables[0].Rows.Count; w++)
                {
                    tabControl2.TabPages.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
                    tabControl2.TabPages[tabControl2.TabPages.Count - 1].BackColor = Color.White;
                }
                tabControl2.SelectedTab = tabControl2.TabPages[0];


                LoadComoBox();

                RefreshIPV();

                LoadRamal20();
                LoadSubMayor();


                tabControl1.TabPages.RemoveAt(tabControl1.TabCount - 2);
                tabControl1.TabPages.RemoveAt(tabControl1.TabCount - 2);
               // divisaToolStripMenuItem.Checked = true;
                monedaNacionalToolStripMenuItem.Checked = true;
                monedaNacionalToolStripMenuItem_Click(sender, e);
                //divisaToolStripMenuItem_Click(sender, e);



                ramal20Top.Columns[0].Width = ramal20Base.Columns[0].Width + ramal20Base.Columns[1].Width + ramal20Base.Columns[2].Width;
          

                if (MayRecover())
                {
                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Quedó Información Temporal Guardada de la últma sesión.\nDesea recuperarla?....", "Información", buttons, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        LoadTemp();
                    }
                    else
                    {

                        if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
                        {
                            FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
                            //fi.Encrypt();
                            fi.Delete();
                            //  File.CreateText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");


                        }
                    }
                }
            }
            else
            {
                LoadRamal20();
               }


            UpdateState(true);
           


            //dts = dbc.SelectQuerryFixed("SELECT [NCuenta] FROM [MLB].[dbo].[Cuenta] ");

            //if (dts.Tables[0].Rows.Count > 0)
            //{
            //    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            //    {
            //        comboBox1.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            //    }
            // //  comboBox1.Text = dts.Tables[0].Rows[0].ItemArray[0].ToString();

            //}

            }


        }

        private void ConexionFix()
        {
            if (!failed)
            {
            
                try
                {
                    dbc.GetConnection().Open();
                    dbc.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    dbc.CloseConnection();
                    
                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Falló la conexión con el Servidor de Base de Datos...\nDesea configurarla nuevamente?", "Error de Conexion", buttons, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes && File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Configuration_Manager.exe"))
                    {
                        ///System.Windows.Forms
                        Process myProcess = new Process();
                        myProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Configuration_Manager.exe";//+"\\Register_Key.exe";
                        myProcess.StartInfo.Verb = "Open";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                        failed = true;
                    }
                    else
                    {
                        if (!File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Configuration_Manager.exe"))
                        {
                            System.Windows.Forms.MessageBox.Show("Falta el Módulo Configuration_Manager.exe. La reistalacion del sistema puede solucionar este problema.");
                        }
                        failed = true;
                        Close();
                    }


                }
            }
        }
        public bool MayRecover()
        {
            if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
            {
                FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");

                if (fi.Length > 1000)
                {
                    return true;
                }

               // FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
                //fi.Encrypt();
                fi.Delete();
                


            }

            return false;

        }

        //private void UMLoader()
        //{

        //    dts = dbc.SelectQuerryFixed("SELECT DISTINCT([UMSimb])FROM [MLB].[dbo].[UM]");
        //    UMRamal.Items.Clear();
        //    UMIPV.Items.Clear();
        //    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
        //    {
        //        UMRamal.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
        //        UMIPV.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
        //    }
        //}
        private void LoadComoBox()
        {
            if (!IsCadena(tabControl2.SelectedTab.Text))
                dts = dbc.SelectQuerryFixed("SELECT DISTINCT(UniCuenta.Cuenta) FROM Producto INNER JOIN UniCuenta ON Producto.Cuenta=UniCuenta.Cuenta WHERE Producto.Moneda ='" + GetMoneda() + "' AND Unidad = '" + tabControl2.SelectedTab.Text + "'");
            else
                dts = dbc.SelectQuerryFixed("SELECT DISTINCT(UniCuenta.Cuenta) FROM Producto INNER JOIN UniCuenta ON Producto.Cuenta=UniCuenta.Cuenta WHERE Producto.Moneda ='" + GetMoneda() + "'");

            comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                comboBox1.Items.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString());
            if (dts.Tables[0].Rows.Count > 0)
                comboBox1.SelectedIndex = 1;
            else
                comboBox1.Text = "";



        }
        private void RefreshIPV()
        {
            int tb = 2;

            while (tabControl1.TabPages[tb].Text != "SubMayor")
            {
                tabControl1.TabPages.RemoveAt(tb);
            }



            dts = dbc.SelectQuerryFixed("SELECT IPVName FROM UnidadIPV WHERE UName = '" + tabControl2.SelectedTab.Text + "'");

            if (dts.Tables.Count > 0 && dts.Tables[0].Rows.Count > 0)
            {
                tabControl1.TabPages[1].Text = "IPV " + dts.Tables[0].Rows[0].ItemArray[0].ToString();

                int itab = 2;

                for (int w = 1; w < dts.Tables[0].Rows.Count; w++)
                {
                    tabControl1.TabPages.Insert(itab, "IPV " + dts.Tables[0].Rows[w].ItemArray[0].ToString());
                    tabControl1.TabPages[itab].BackColor = Color.White;
                    tabControl1.TabPages[itab].Padding = tabControl1.TabPages[itab - 1].Padding;

                    itab++;
                }


            }

        }

        private void ClearRamal()
        {
            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'"))
            {
                dbc.SimplePlan("DELETE Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");


            }
            ReSetValues(RamalSaver);
            LoadRamal20();

        }
        private void SaveRamal()
        {
            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'"))
            {

                dbc.SimplePlan("DELETE FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                //    int w = 0;
                //    while (ramal20Base.Rows[w].Cells[0].Value != null)
                //    {

                //        dbc.SimplePlan("UPDATE [MLB].[dbo].[Ramal20] SET [Producto] = '" + GetData(ramal20Base.Rows[w].Cells[0].Value) + "'" +
                //",[UM] = '" + GetData(ramal20Base.Rows[w].Cells[1].Value) + "',[Precio] = '" + GetNumData(ramal20Base.Rows[w].Cells[2].Value) + "'" +
                //",[ICantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[3].Value) + "',[IImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[4].Value) + "'" +
                //",[ECantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[5].Value) + "',[EImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[6].Value) + "'" +
                //",[EICantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[7].Value) + "',[EIImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[8].Value) + "'" +
                //",[SCantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[9].Value) + "',[SImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[10].Value) + "'" +
                //",[SICantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[11].Value) + "',[SIImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[12].Value) + "'" +
                //",[TCantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[13].Value) + "',[TImporte] = '" + GetNumData(ramal20Base.Rows[w].Cells[14].Value) + "'" +
                //",[FPrecio] = '" + GetNumData(ramal20Base.Rows[w].Cells[15].Value) + "',[FCantidad] = '" + GetNumData(ramal20Base.Rows[w].Cells[16].Value) + "'" +
                //",[FImporte] = '"  + GetNumData(ramal20Base.Rows[w].Cells[17].Value)+ "' WHERE [Id] = '" + ramal20Base.Rows[w].Tag.ToString() + "'");

                //        w++;
                //    }
            }
            //else
            //{
            int w = 0;
            while (ramal20Base.Rows[w].Cells[0].Value != null)
            {
                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[Ramal20]([Id],[UName],[Cuenta],[Date],[Producto]" +
            ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Moneda])" +
            "VALUES ('" + dbc.MaxQuerry("Ramal20") + "','" + tabControl2.SelectedTab.Text + "','" + comboBox1.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "'" +
            ",'" + GetData(ramal20Base.Rows[w].Cells[0].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[1].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[2].Value) + "'" +
            ",'" + GetNumData(ramal20Base.Rows[w].Cells[3].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[4].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[5].Value) + "'" +
            ",'" + GetNumData(ramal20Base.Rows[w].Cells[6].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[7].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[8].Value) + "'" +
            ",'" + GetNumData(ramal20Base.Rows[w].Cells[9].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[10].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[11].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[12].Value) + "'" +
            ",'" + GetNumData(ramal20Base.Rows[w].Cells[13].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[14].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[15].Value) + "'" +
            ",'" + GetNumData(ramal20Base.Rows[w].Cells[16].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[17].Value).Replace("$", "") + "','" + GetMoneda() + "')");
                w++;
            }
            // }

        }
        private void SaveIPV()
        {

            if (tabControl1.SelectedIndex > 0 && tabControl1.SelectedIndex < tabControl1.TabCount - 2)
            {
                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Moneda = '" + GetMoneda() + "'"))
                {
                    dbc.SimplePlan("DELETE FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Moneda = '" + GetMoneda() + "'");
                    //int w = 0;
                    //while (GetData(IPVBase.Rows[w].Cells[0].Value) != " ")
                    //{
                    //    dbc.SimplePlan("UPDATE [MLB].[dbo].[IPV] SET [Producto] = '" + IPVBase.Rows[w].Cells[0].Value.ToString() + "'" +
                    //    ",[UM] = '" + IPVBase.Rows[w].Cells[1].Value.ToString() + "',[VPrice] = '" + IPVBase.Rows[w].Cells[2].Value.ToString() + "'" +
                    //    ",[ICantidad] = '" + IPVBase.Rows[w].Cells[3].Value.ToString() + "',[ICosto] = '" + GetNumData(IPVBase.Rows[w].Cells[4].Value) + "'" +
                    //    ",[ECantidad] = '" + IPVBase.Rows[w].Cells[5].Value.ToString() + "',[EImporte] = '" + GetNumData(IPVBase.Rows[w].Cells[6].Value)+ "'" +
                    //    ",[VCantidad] = '" + IPVBase.Rows[w].Cells[7].Value.ToString() + "',[VIngreso] = '" + GetNumData(IPVBase.Rows[w].Cells[8].Value)+ "'" +
                    //    ",[CUnitario] = '" + GetNumData(IPVBase.Rows[w].Cells[9].Value) + "',[CVendido] = '" + GetNumData(IPVBase.Rows[w].Cells[10].Value) + "'" +
                    //    ",[FCantidad] = '" + IPVBase.Rows[w].Cells[11].Value.ToString() + "',[FCosto] = '" + GetNumData(IPVBase.Rows[w].Cells[12].Value) + "'" +
                    //    " WHERE [Id] = '" + IPVBase.Rows[w].Tag.ToString() + "'");
                    //    w++;
                    //}

                    //int count = System.Convert.ToInt32((dbc.SelectQuerryFixed("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Moneda = '" + GetMoneda() + "'")).Tables[0].Rows[0].ItemArray[0].ToString());
                    //int real = Search4(IPVBase, "#$%");
                    //if (count<real)
                    //{
                    //    for ( w = count; w < real - count;w++ )
                    //    {
                    //        dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[IPV] ([Id],[UName],[IPVName],[Cuenta]" +
                    //",[Date],[Producto],[UM] ,[VPrice]  ,[ICantidad],[ICosto] ,[ECantidad],[EImporte]" +
                    //",[VCantidad],[VIngreso] ,[CUnitario],[CVendido],[FCantidad],[FCosto],[Moneda])" +

                    //"VALUES ('" + dbc.MaxQuerry("IPV") + "','" + tabControl2.SelectedTab.Text + "','" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "','" + comboBox1.Text + "'" +
                    //",'" + DateCultureConverter(dateTimePicker1.Value) + "','" + IPVBase.Rows[w].Cells[0].Value.ToString() + "','" + IPVBase.Rows[w].Cells[1].Value.ToString() + "'" +
                    //",'" + GetNumData(IPVBase.Rows[w].Cells[2].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[3].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[4].Value) + "'" +
                    //",'" + GetNumData(IPVBase.Rows[w].Cells[5].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[6].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[7].Value) + "'" +
                    //",'" + GetNumData(IPVBase.Rows[w].Cells[8].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[9].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[10].Value) + "'" +
                    //",'" + GetNumData(IPVBase.Rows[w].Cells[11].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[12].Value) + "','" + GetMoneda() + "')");

                    //    }
                    //}

                }
                //else
                //{

                int w = 0;
                while (GetData(IPVBase.Rows[w].Cells[0].Value) != " ")
                {
                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[IPV] ([Id],[UName],[IPVName],[Cuenta]" +
                ",[Date],[Producto],[UM] ,[VPrice]  ,[ICantidad],[ICosto] ,[ECantidad],[EImporte]" +
                ",[VCantidad],[VIngreso] ,[CUnitario],[CVendido],[FCantidad],[FCosto],[Moneda])" +

                "VALUES ('" + dbc.MaxQuerry("IPV") + "','" + tabControl2.SelectedTab.Text + "','" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "','" + comboBox1.Text + "'" +
                ",'" + DateCultureConverter(dateTimePicker1.Value) + "','" + IPVBase.Rows[w].Cells[0].Value.ToString() + "','" + IPVBase.Rows[w].Cells[1].Value.ToString() + "'" +
                ",'" + GetNumData(IPVBase.Rows[w].Cells[2].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[3].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[4].Value) + "'" +
                ",'" + GetNumData(IPVBase.Rows[w].Cells[5].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[6].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[7].Value) + "'" +
                ",'" + GetNumData(IPVBase.Rows[w].Cells[8].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[9].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[10].Value) + "'" +
                ",'" + GetNumData(IPVBase.Rows[w].Cells[11].Value) + "','" + GetNumData(IPVBase.Rows[w].Cells[12].Value) + "','" + GetMoneda() + "')");

                    w++;
                }
                //}


            }

        }
        private void ClearIPV()
        {

            if (tabControl1.SelectedIndex > 0 && tabControl1.SelectedIndex < tabControl1.TabCount - 2)
            {
                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Moneda = '" + GetMoneda() + "'"))

                    dbc.SimplePlan("DELETE IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Moneda = '" + GetMoneda() + "'");


            }
            ReSetValues(IPVSaver);
            LoadIPV(tabControl1.SelectedTab.Text);
        }
        private void ClearSubMayor()
        {


            if (dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day = '" + dateTimePicker1.Value.Day.ToString() + "'  AND Moneda = '" + GetMoneda() + "'"))
            {
                dbc.SimplePlan("DELETE SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day = '" + dateTimePicker1.Value.Day.ToString() + "'  AND Moneda = '" + GetMoneda() + "'");

            }
            ReSetValues(SMSaver);
            LoadSubMayor();


        }

        private void SaveSubMayor()
        {


            if (dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day = '" + dateTimePicker1.Value.Day.ToString() + "'  AND Moneda = '" + GetMoneda() + "'"))
            {

                //for (int w = SubMayorBase.Rows.Count - 1; w < SubMayorBase.Rows.Count; w++)
                //{
                dbc.SimplePlan("UPDATE [MLB].[dbo].[SubMayor] SET [SaldoInicial] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[2].Value) + "'" +
                    ",[Entrada] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[3].Value) + "',[EntInt] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[4].Value) + "'" +
                    ",[Salida] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[5].Value) + "',[SalInt] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[6].Value) + "'" +
                    ",[Traslado] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[7].Value) + "',[SaldoFinal] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[8].Value) + "',[CompRamal] = '" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[9].Value) +
                    "',[TTTEI] = '" + GetData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[4].ToolTipText) + "',[TTTSI] = '" + GetData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[6].ToolTipText) + "' WHERE [Id] = '" + SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Tag.ToString() + "'");
                //}
            }
            else
            {

                //for (int w = SubMayorBase.Rows.Count - 1; w < SubMayorBase.Rows.Count; w++)
                //{
                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[SubMayor]([Id],[UName],[Cuenta],[MDate],[Day]" +
              " ,[Month],[SaldoInicial],[Entrada],[EntInt],[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[RDate],[Moneda],[TTTEI],[TTTSI])" +

            "VALUES ('" + dbc.MaxQuerry("SubMayor") + "','" + tabControl2.SelectedTab.Text + "','" + comboBox1.Text + "'" +
            ",'" + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString() + "','" + SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[0].Value.ToString() + "','" + GetData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[1].Value) + "'" +
            ",'" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[2].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[3].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[4].Value) + "'" +
            ",'" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[5].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[6].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[7].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[8].Value) + "','" + GetNumData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[9].Value) + "'" +
            ",'" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + GetData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[4].ToolTipText) + "','" + GetData(SubMayorBase.Rows[SubMayorBase.Rows.Count - 1].Cells[6].ToolTipText) + "')");
                //}
            }


        }
        private void CleareResIng()
        {


            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "'"))
            {

                dbc.SimplePlan("DELETE [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "'");


                //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                //{
            }
            ReSetValues(RISaver);
            LoadResIng();
        }

        private void ClearFichaCosto()
        {


            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[FichaCosto] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "' And Cuenta = '" + comboBox1.Text + "'"))
            {

                dbc.SimplePlan("DELETE [MLB].[dbo].[FichaCosto] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "' And Cuenta = '" + comboBox1.Text + "'");

                //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                //{
            }
            ReSetValues(FCSaver);
            LoadFichaCosto();
        }
        private void ClearValeSalida()
        {


            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ValeSalida] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "'"))
            {

                dbc.SimplePlan("DELETE [MLB].[dbo].[ValeSalida] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "'");

                //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                //{
            }
            //ReSetValues(FCSaver);
            LoadValeSalida();
        }

        private void SaveResIng2()
        {


            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "'"))
            {

                //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                //{
                dbc.SimplePlan("UPDATE [MLB].[dbo].[ResIng] SET [InHoy] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[2].Value.ToString() + "'" +
                    ",[InHastaHoy] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[3].Value.ToString() + "',[CostHoy] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[4].Value.ToString() + "'" +
                    ",[CostHastaHoy] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[5].Value.ToString() + "',[IngAcum] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[6].Value.ToString() + "'" +
                    ",[CostAcum] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[7].Value.ToString() +
                    " WHERE [Id] = '" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Tag.ToString() + "'");
                //}
            }
            else
            {

                //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                //{
                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[ResIng]([Id],[UName],[Cuenta],[MDate],[Day]" +
              " ,[Month],[InHoy],[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[RDate])" +

            "VALUES ('" + dbc.MaxQuerry("ResIng") + "','" + tabControl2.SelectedTab.Text + "','" + comboBox1.Text + "'" +
            ",'" + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[0].Value.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[1].Value.ToString() + "'" +
            ",'" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[2].Value.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[3].Value.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[4].Value.ToString() + "'" +
            ",'" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[5].Value.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[6].Value.ToString() + "','" + ResIngBase.Rows[ResIngBase.Rows.Count - 1].Cells[7].Value.ToString() + "'" +
            ",'" + DateCultureConverter(dateTimePicker1.Value) + "')");
                //}
            }


        }

        private void SaveResIng()
        {


            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "'  AND Moneda = '" + GetMoneda() + "'"))
            {
                dbc.SimplePlan("DELETE FROM ResIng WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "' AND Moneda = '" + GetMoneda() + "'");


                // for (int w = 0; w < ResIngBase.Rows.Count-1; w++)
                //{
                //       dbc.SimplePlan("UPDATE [MLB].[dbo].[ResIng] SET [InHoy] = '" + GetNumData(ResIngBase.Rows[w].Cells[3].Value) + "'" +
                //    ",[InHastaHoy] = '" + GetNumData(ResIngBase.Rows[w].Cells[4].Value)+ "',[CostHoy] = '" + GetNumData(ResIngBase.Rows[w].Cells[5].Value) + "'" +
                //    ",[CostHastaHoy] = '" + GetNumData(ResIngBase.Rows[w].Cells[6].Value) + "',[IngAcum] = '" + GetNumData(ResIngBase.Rows[w].Cells[7].Value) + "'" +
                //    ",[CostAcum] = '" + GetNumData( ResIngBase.Rows[w].Cells[8].Value) +
                //    "' WHERE [Id] = '" + ResIngBase.Rows[w].Tag.ToString() + "'");
                //}
            }


            for (int w = 0; w < ResIngBase.Rows.Count - 1; w++)
            {
                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[ResIng]([Id],[UName],[Cuenta],[MDate],[Day]" +
              " ,[Month],[InHoy],[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[RDate],[Moneda])" +

            "VALUES ('" + dbc.MaxQuerry("ResIng") + "','" + tabControl2.SelectedTab.Text + "','" + ResIngBase.Rows[w].Cells[0].Value.ToString() + "'" +
            ",'" + dateTimePicker1.Value.Month.ToString() + dateTimePicker1.Value.Year.ToString() + "','" + ResIngBase.Rows[w].Cells[1].Value.ToString() + "','" + ResIngBase.Rows[w].Cells[2].Value.ToString() + "'" +
            ",'" + ResIngBase.Rows[w].Cells[3].Value.ToString() + "','" + ResIngBase.Rows[w].Cells[4].Value.ToString() + "','" + ResIngBase.Rows[w].Cells[5].Value.ToString() + "'" +
            ",'" + ResIngBase.Rows[w].Cells[6].Value.ToString() + "','" + ResIngBase.Rows[w].Cells[7].Value.ToString() + "','" + ResIngBase.Rows[w].Cells[8].Value.ToString() + "'" +
            ",'" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "')");
            }



        }


        private void PanelOP_Resize(object sender, EventArgs e)
        {
            dateTimePicker1.Width = PanelOP.Width - 5;
            comboBox1.Width = PanelOP.Width - 5;
            button3.Width = PanelOP.Width - 15;
            button2.Width = PanelOP.Width - 15;
            button1.Width = PanelOP.Width - 15;
        }

        private void ramal20Base_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                if (e.RowIndex >= 0 && GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) != " ")
                {
                    
                    while (ramal20Base.RowCount < 10)
                        ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);



                        if (e.ColumnIndex == 0 && ramal20Base.Rows[e.RowIndex].Cells[0].ReadOnly == false && ((ramal20Base.Rows[e.RowIndex+1].Cells[0].Value==null&&ramal20Base.Rows[e.RowIndex+1].Cells[0].ReadOnly)||(forzar.Enabled&&forzar.Checked)))
                    {
                        //  LoadRamal20();

                        dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecIn] FROM [MLB].[dbo].[Producto] WHERE [Cuenta] = '" + comboBox1.Text + "' AND Moneda = '" + GetMoneda() + "' AND Nombre LIKE '%" + GetData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) + "%' ORDER BY [Id]");
                        bool pass = false;
                        if (dts.Tables[0].Rows.Count == 1 && Search4(ramal20Base, dts.Tables[0].Rows[0].ItemArray[0].ToString()) >= e.RowIndex)
                        {
                            dbc.SimplePlan("INSERT INTO UndProd([Id],[UName],[Producto]) VALUES ('" + dbc.MaxQuerry("UndProd") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "')");

                            ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                            ramal20Base.Rows.Insert(e.RowIndex, dts.Tables[0].Rows[0].ItemArray[0].ToString(), dts.Tables[0].Rows[0].ItemArray[1].ToString(), dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00");
                            ramal20Base.Rows.RemoveAt(e.RowIndex + 1);
                            ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
                            ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                            ramal20Base.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = false;

                            // new code
                            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'"))
                            {
                                // dbc.SimplePlan("INSERT INTO UndProd([Id],[UName],[Producto]) VALUES ('" + dbc.MaxQuerry("UndProd") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "')");

                                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[Ramal20]([Id],[UName],[Cuenta],[Date],[Producto]" +
                      ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Moneda])" +
                      "VALUES ('" + dbc.MaxQuerry("Ramal20") + "','" + tabControl2.SelectedTab.Text + "','" + comboBox1.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "'" +
                      ",'" + GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) + "','" + GetData(ramal20Base.Rows[e.RowIndex].Cells[1].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[2].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[3].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[4].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[5].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[6].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[7].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[8].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[9].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[10].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[11].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[12].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[13].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[14].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[15].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[16].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[17].Value).Replace("$", "") + "','" + GetMoneda() + "')");

                                ramal20Base.Rows[e.RowIndex].Tag = System.Convert.ToInt32(dbc.MaxQuerry("Ramal20")) - 1;
                            }

                            pass = true;
                        }

                         dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecIn] FROM [MLB].[dbo].[Producto] WHERE [Cuenta] = '" + comboBox1.Text + "' AND Moneda = '" + GetMoneda() + "' AND Nombre = '" + GetData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) + "' ORDER BY [Id]");
                       
                        if (!pass&&dts.Tables[0].Rows.Count == 1 && Search4(ramal20Base, dts.Tables[0].Rows[0].ItemArray[0].ToString()) >= e.RowIndex)
                        {
                            dbc.SimplePlan("INSERT INTO UndProd([Id],[UName],[Producto]) VALUES ('" + dbc.MaxQuerry("UndProd") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "')");

                            ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                            ramal20Base.Rows.Insert(e.RowIndex, dts.Tables[0].Rows[0].ItemArray[0].ToString(), dts.Tables[0].Rows[0].ItemArray[1].ToString(), dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", "0", "0.00", dts.Tables[0].Rows[0].ItemArray[2].ToString(), "0", "0.00");
                            ramal20Base.Rows.RemoveAt(e.RowIndex + 1);
                            ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
                            ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                            ramal20Base.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = false;

                            // new code
                            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'"))
                            {
                                // dbc.SimplePlan("INSERT INTO UndProd([Id],[UName],[Producto]) VALUES ('" + dbc.MaxQuerry("UndProd") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "')");

                                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[Ramal20]([Id],[UName],[Cuenta],[Date],[Producto]" +
                      ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Moneda])" +
                      "VALUES ('" + dbc.MaxQuerry("Ramal20") + "','" + tabControl2.SelectedTab.Text + "','" + comboBox1.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "'" +
                      ",'" + GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) + "','" + GetData(ramal20Base.Rows[e.RowIndex].Cells[1].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[2].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[3].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[4].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[5].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[6].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[7].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[8].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[9].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[10].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[11].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[12].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[13].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[14].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[15].Value) + "'" +
                      ",'" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[16].Value) + "','" + GetNumData(ramal20Base.Rows[e.RowIndex].Cells[17].Value).Replace("$", "") + "','" + GetMoneda() + "')");

                                ramal20Base.Rows[e.RowIndex].Tag = System.Convert.ToInt32(dbc.MaxQuerry("Ramal20")) - 1;
                            }

                            pass = true;
                        }

                        if(!pass)
                        {
                            ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightCoral;
                            if (ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                                ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                            else{

                                if (hlpeve)
                                {
                                    hlpManager.hlpknd = HelpKind.Resolucion;
                                    hlpManager.topic = "Ramal20";
                                    hlpManager.tnumber = 1;
                                    hlpManager.errknd = ErrorKind.RM_Wront_Prod;
                                    
                                    hlpManager.atext.Add(comboBox1.Text);
                                    hlpManager.atext.Add(GetMoneda());

                                }
                            }


                        }
                        else{

                            if (hlpeve)
                            {
                                if (hlpManager.topic != "Ramal20")
                                    hlpManager.tnumber = 1;
                                hlpManager.hlpknd = HelpKind.Descripcion;
                                hlpManager.topic = "Ramal20";
                                hlpManager.atext.Clear();
                            }
                        }

                    }

                }
                else
                {
                    if (e.RowIndex >= 0)
                        ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                }

                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }




        }
        private String GetTotal(System.Windows.Forms.DataGridView dtgv, int cindx)
        {
            double total = 0;
            for (int w = 0; w < dtgv.Rows.Count - 1; w++)
            {
                if (dtgv.Rows[w].Cells[0].Value != null && w < dtgv.RowCount - 2)
                {
                    String aux = GetNumData(System.Math.Round(System.Convert.ToDouble(dtgv.Rows[w].Cells[cindx].Value), 2));
                    aux = aux.Replace("$", "");
                    aux = aux.Replace("(", "");
                    aux = aux.Replace(")", "");
                    if (aux == "")
                        aux = "0.00";
                    //if (aux.Contains(".") && aux.IndexOf(".") < aux.Length - 2)
                    //    aux = aux.Remove(aux.IndexOf(".") + 3);

                    total = total + System.Convert.ToDouble(aux);
                }
            }
            return System.Convert.ToString(total);
        }

        private String GetTotal2(System.Windows.Forms.DataGridView dtgv, int cindx)
        {
            double total = 0;
            for (int w = 0; w < dtgv.Rows.Count; w++)
            {
                if (dtgv.Rows[w].Cells[0].Value != null)
                {
                    String aux = GetNumData(System.Math.Round(System.Convert.ToDouble(dtgv.Rows[w].Cells[cindx].Value), 2));
                    aux = aux.Replace("$", "");
                    aux = aux.Replace("(", "");
                    aux = aux.Replace(")", "");
                    if (aux == "")
                        aux = "0.00";
                    //if (aux.Contains(".") && aux.IndexOf(".") < aux.Length - 2)
                    //    aux = aux.Remove(aux.IndexOf(".") + 3);

                    total = total + System.Convert.ToDouble(aux);
                }
            }
            return System.Convert.ToString(total);
        }

        private void ramal20Base_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0 && GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) != " ")
                {

                    if ((go && (e.ColumnIndex == 16 || e.ColumnIndex == 7 || e.ColumnIndex == 5 || e.ColumnIndex == 9 || e.ColumnIndex == 11 || e.ColumnIndex == 13 || e.ColumnIndex == 3) && e.RowIndex >= 0) || (forzar.Enabled && forzar.Checked && e.ColumnIndex != 17 && ( (e.ColumnIndex == 16 || e.ColumnIndex == 7 || e.ColumnIndex == 5 || e.ColumnIndex == 9 || e.ColumnIndex == 11 || e.ColumnIndex == 13 || e.ColumnIndex == 3) && e.RowIndex >= 0)))
                    {

                        ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[2].Value)), 2);
                        //ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].FormattedValue;

                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(ramal20Base, e.ColumnIndex + 1);
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                        ramal20Base.Rows[e.RowIndex].Cells[16].Value = System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[5].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[7].Value)) - System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[9].Value)) - System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[11].Value)) - System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[13].Value)), 2);
                        if (System.Convert.ToDouble(ramal20Base.Rows[e.RowIndex].Cells[16].Value) < 0.01 && System.Convert.ToDouble(ramal20Base.Rows[e.RowIndex].Cells[16].Value) > 0)
                            ramal20Base.Rows[e.RowIndex].Cells[16].Value = "0.00";
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value = GetTotal(ramal20Base, 17);
                        ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value = System.Math.Round(System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value), 2);

                        if (true)
                        {
                            //if (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[16].Value)) == 0)
                            //{
                            //    int y = 0;
                            //}
                            double final = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[17].Value));

                            double real = (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[4].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[6].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[8].Value))) - (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[10].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[12].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[14].Value)));


                            final = System.Math.Round(final, 2);
                            // real = real * -1;
                            real = System.Math.Round(real, 2);
                            if (real < 0)
                            {
                                real = real * -1;
                            }
                            if (final < 0)
                            {
                                final = final * -1;
                            }

                           
                            if ((System.Math.Round(final - real, 2) == 0.01) || (System.Math.Round(real - final, 2) == 0.01))
                            {

                                double sld = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[10].Value));
                                double ntrd = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[6].Value));
                                double trdo = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[14].Value));
                                double isld = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[12].Value));

                                if (sld != 0)
                                {
                                    // if (final!=0)
                                    ramal20Base.Rows[e.RowIndex].Cells[10].Value = System.Convert.ToString(System.Math.Round(sld - System.Math.Round(final - real, 2), 2));
                                    // else
                                    //  ramal20Base.Rows[e.RowIndex].Cells[10].Value = System.Convert.ToString(System.Math.Round(sld + System.Math.Round(final - real, 2), 2));
                                    ramal20Base.Rows[e.RowIndex].Cells[10].Style.ForeColor = Color.DarkBlue;
                                    ramal20Base.Rows[e.RowIndex].Cells[12].Style.ForeColor = Color.Black;
                                    ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[10].Value = GetTotal(ramal20Base, 10);
                                }
                                else if (isld != 0)
                                {
                                    // if (final != 0)
                                    ramal20Base.Rows[e.RowIndex].Cells[12].Value = System.Convert.ToString(System.Math.Round(isld - System.Math.Round(final - real, 2), 2));
                                    //  else
                                    //   ramal20Base.Rows[e.RowIndex].Cells[12].Value = System.Convert.ToString(System.Math.Round(isld + System.Math.Round(final - real, 2), 2));

                                    ramal20Base.Rows[e.RowIndex].Cells[12].Style.ForeColor = Color.DarkBlue;
                                    ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[12].Value = GetTotal(ramal20Base, 12);

                                    if (!EstaAjuste(GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value), GetNumData(ramal20Base.Rows[e.RowIndex].Cells[2].Value), e.RowIndex, 12))
                                        Ajustes.Add(new ValueSaver(tabControl1.SelectedTab.Text, GetNumData(ramal20Base.Rows[e.RowIndex].Cells[2].Value), e.RowIndex, 12, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                }
                                else if (trdo != 0)
                                {
                                    // if ((System.Math.Round(final - real, 2) == 0.01))
                                    ramal20Base.Rows[e.RowIndex].Cells[14].Value = System.Convert.ToString(System.Math.Round(trdo - System.Math.Round(final - real, 2), 2));
                                    // else
                                    //    ramal20Base.Rows[e.RowIndex].Cells[14].Value = System.Convert.ToString(System.Math.Round(trdo-System.Math.Round(final - real, 2), 2));
                                    ramal20Base.Rows[e.RowIndex].Cells[14].Style.ForeColor = Color.DarkBlue;
                                    ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[14].Value = GetTotal(ramal20Base, 14);
                                }
                                else if (ntrd != 0)
                                {
                                    // if ((System.Math.Round(final - real, 2) == 0.01))
                                    ramal20Base.Rows[e.RowIndex].Cells[6].Value = System.Convert.ToString(System.Math.Round(ntrd + System.Math.Round(final - real, 2), 2));
                                    //  else
                                    //      ramal20Base.Rows[e.RowIndex].Cells[6].Value = System.Convert.ToString(System.Math.Round(ntrd - 0.01, 2));

                                    ramal20Base.Rows[e.RowIndex].Cells[6].Style.ForeColor = Color.DarkBlue;
                                    ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[6].Value = GetTotal(ramal20Base, 6);
                                }
                                else
                                {

                                    ramal20Base.Rows[e.RowIndex].Cells[17].Value = System.Convert.ToString(System.Math.Round(real, 2));
                                    ramal20Base.Rows[e.RowIndex].Cells[17].Value = System.Math.Round(System.Convert.ToDouble(ramal20Base.Rows[e.RowIndex].Cells[17].Value), 2);
                                    ramal20Base.Rows[e.RowIndex].Cells[17].Style.ForeColor = Color.DarkRed;
                                    ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[17].Value = GetTotal(ramal20Base, 17);
                                   
                                    double nfinal = System.Convert.ToDouble(GetTotal(ramal20Base, 17));
                                   
                                }

                            }
                            else
                            {
                                if ((System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[16].Value)) * System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[15].Value)), 2)) == System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[17].Value)))
                                    ramal20Base.Rows[e.RowIndex].Cells[17].Style.ForeColor = Color.Black;
                            }

                        }

                        //  ramal20Base.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Style.Font.Bold = true;
                    }

                    if (e.RowIndex > -1 && ramal20Base.Rows[e.RowIndex].Cells[16].Value != null && System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[16].Value)) < 0)
                    {
                        ramal20Base.Rows[e.RowIndex].Cells[16].Style.ForeColor = Color.Red;
                    }
                    else if (e.RowIndex > -1 && ramal20Base.Rows[e.RowIndex].Cells[12].Value != null)
                        ramal20Base.Rows[e.RowIndex].Cells[16].Style.ForeColor = Color.Black;


                    if (godeep && !EstaRamalSaver(e.RowIndex, e.ColumnIndex) && e.RowIndex >= 0 && e.ColumnIndex != 16 && e.ColumnIndex != 17 && GetData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != " " && GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) != " " && GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value) != "Totales:" && (comboBox1.Text == "" || (e.ColumnIndex != 0 && e.ColumnIndex != 7 && e.ColumnIndex != 11)))
                    {
                        if (e.ColumnIndex<4||(e.ColumnIndex > 3 && (ramal20Base.Columns[e.ColumnIndex].HeaderText != "Importe" || System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value)) != 0 || System.Convert.ToDouble(GetNumData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) == 0)))//parche para la talla fantasmal
                        RamalSaver.Add(new ValueSaver(tabControl1.TabPages[0].Text, System.Convert.ToString(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(ramal20Base.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                    }

                    if (godeep && e.ColumnIndex == 13 && !EstaRamalSaver(ramal20Base.RowCount - 1, e.ColumnIndex + 1) && GetData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != " " && GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[e.ColumnIndex + 1].Value) != " " && GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[0].Value) == "Totales:" && comboBox1.Text != "")
                    {
                        RamalSaver.Add(new ValueSaver(tabControl1.TabPages[0].Text, System.Convert.ToString(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[e.ColumnIndex + 1].Value), ramal20Base.RowCount - 1, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[0].Value), GetMoneda()));
                    }
                    if (godeep && e.ColumnIndex == 5 && !EstaRamalSaver(ramal20Base.RowCount - 1, e.ColumnIndex + 1) && GetData(ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != " " && GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[e.ColumnIndex + 1].Value) != " " && GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[0].Value) == "Totales:" && comboBox1.Text != "")
                    {
                        RamalSaver.Add(new ValueSaver(tabControl1.TabPages[0].Text, System.Convert.ToString(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[e.ColumnIndex + 1].Value), ramal20Base.RowCount - 1, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[0].Value), GetMoneda()));
                    }


                }
                else
                {
                    if (e.RowIndex >= 0)
                        ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                }

                if ((forzar.Enabled && forzar.Checked && e.ColumnIndex != 17 && ((e.ColumnIndex == 15 || e.ColumnIndex == 2) && e.RowIndex >= 0)))
                {
                    if (e.ColumnIndex==15 &&(ramal20Base.Rows[e.RowIndex].Cells[2].Value.ToString() != ramal20Base.Rows[e.RowIndex].Cells[15].Value.ToString()))
                    {
                        ramal20Base.Rows[e.RowIndex].Cells[2].Value = ramal20Base.Rows[e.RowIndex].Cells[15].Value;
                       
                        String aux = ramal20Base.Rows[e.RowIndex].Cells[3].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[3].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[3].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[5].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[5].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[5].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[7].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[7].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[7].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[9].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[9].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[9].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[11].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[11].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[11].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[13].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[13].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[13].Value = aux;
                    }
                    if (e.ColumnIndex == 2 && (ramal20Base.Rows[e.RowIndex].Cells[2].Value.ToString() != ramal20Base.Rows[e.RowIndex].Cells[15].Value.ToString()))
                    {
                        ramal20Base.Rows[e.RowIndex].Cells[15].Value = ramal20Base.Rows[e.RowIndex].Cells[2].Value;
                       
                        String aux = ramal20Base.Rows[e.RowIndex].Cells[3].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[3].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[3].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[5].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[5].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[5].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[7].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[7].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[7].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[9].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[9].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[9].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[11].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[11].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[11].Value = aux;

                        aux = ramal20Base.Rows[e.RowIndex].Cells[13].Value.ToString();
                        ramal20Base.Rows[e.RowIndex].Cells[13].Value = "0.00";
                        ramal20Base.Rows[e.RowIndex].Cells[13].Value = aux; 

                        
                    }

                   

                   
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }



        }

        private bool EstaAjuste(String prod, String prec, int row, int col)
        {
            foreach (ValueSaver val in Ajustes)
            {
                if (val.producto == prod && val.cant == prec && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = System.Convert.ToString(ramal20Base.Rows[row].Cells[2].Value);
                    return true;
                }
            }
            return false;
        }

        private void CargarCuentaRamal(String prod, int row)
        {
            godeep = false;
            //   godeep2 = false;

            double pelab = 0;
            double prd = 0;

            foreach (ValueSaver val in RamalSaver)
            {
                if (val.col == 9 && val.producto == prod && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda() && comboBox1.Text != "")
                {
                    //usar tooltip  nueva extrcu para el eprod
                    pelab += System.Convert.ToDouble(val.cant);

                }
            }

            foreach (ValueSaver val in IPVSaver)
            {
                if (val.col == 5 && val.producto == prod && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    prd += System.Convert.ToDouble(val.cant);

                }
            }
            ramal20Base.Rows[row].Cells[7].Value = System.Convert.ToString(System.Math.Round(pelab + prd, 2));
            ramal20Base.Rows[row].Cells[9].Value = System.Convert.ToString(System.Math.Round(pelab + prd, 2));

            godeep = true;
            // godeep2 = true;
        }


        private void IPVBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    if (GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) == " " && System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) == 0)
                    {


                        //SalverFixer(e.RowIndex, RamalSaver);
                        SalverFixer(e.RowIndex, IPVSaver);
                        //IPVBase.Rows.RemoveAt(e.RowIndex);
                        //IPVBase.Rows.Insert(IPVBase.RowCount - 2);
                    }

                    if (GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) == " " && System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) != 0)
                    {
                        IPVBase.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.LightCoral;
                    }
                    else
                        IPVBase.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.White;



                    if (comboBox1.Text != "")
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex == 0)//&& GetMoneda() == "CUP"
                        {
                            // IPVBase.Rows.Insert(e.RowIndex, dts1.Tables[0].Rows[0].ItemArray[0].ToString(), "Rac", dts1.Tables[0].Rows[0].ItemArray[1].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", GetUnitPrice(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), "0.00", "0", "0.00");
                            // IPVBase.Rows.RemoveAt(e.RowIndex + 1);
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "Rac";

                            if (!EstaIPVSaver(e.RowIndex, e.ColumnIndex + 1) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                            {
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), e.RowIndex, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            }
                            IPVBase.Rows.Insert(IPVBase.RowCount - 2);
                            IPVBase.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                            IPVBase.Rows[e.RowIndex + 1].Cells[0].ReadOnly = false;


                            IPVBase.Rows[e.RowIndex].Cells[2].ReadOnly = false;
                            IPVBase.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                            IPVBase.Rows[e.RowIndex].Cells[4].ReadOnly = false;



                        }
                        if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 9)&&(!forzar.Enabled&&!forzar.Checked))//&& GetMoneda() == "CUP"
                        {



                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                            IPVBase.Rows[e.RowIndex].Cells[3].Value = "0";
                            IPVBase.Rows[e.RowIndex].Cells[5].Value = "0";
                            IPVBase.Rows[e.RowIndex].Cells[7].Value = "0";

                            if (e.ColumnIndex == 9)
                                IPVBase.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                            // IPVBase.Rows[e.RowIndex + 1].Cells[0].ReadOnly = false;



                        }

                        if (e.RowIndex >= 0 && (e.ColumnIndex == 4))//&& GetMoneda() == "CUP"
                        {

                            IPVBase.Rows[e.RowIndex].Cells[9].Value = System.Math.Round((System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) / System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value))), 6);

                            if (!EstaIPVSaver(e.RowIndex, 9) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                            {
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[9].Value), e.RowIndex, 9, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            }
                            IPVBase.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                            IPVBase.Rows[e.RowIndex].Cells[9].ReadOnly = true;
                            IPVBase.Rows[e.RowIndex].Cells[4].ReadOnly = true;

                            double aux = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[3].Value));

                            IPVBase.Rows[e.RowIndex].Cells[3].Value = System.Convert.ToString(aux + 1);
                            IPVBase.Rows[e.RowIndex].Cells[3].Value = System.Convert.ToString(aux);

                        }

                        if (!EstaIPVSaver(e.RowIndex, e.ColumnIndex) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                        }
                    }

                    if (comboBox1.Text==""&&e.ColumnIndex==5)
                    {
                        ValidarExistencias(e);
                    }

                }
                else
                {
                    if (e.RowIndex >= 0)
                        IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                }

                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }



        }



        private void PERamal20(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                //AmountCell(e.RowIndex, e.ColumnIndex);

                if (GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) != " " && !dbc.ExistQuerry("SELECT Id FROM EProducto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && dbc.ExistQuerry("SELECT Id FROM Producto WHERE Nombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'"))
                {
                    if (Esta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)) == -1)
                    {
                        bool aux = go;
                        go = true;
                        ramal20Base.Rows[Search4(ramal20Base, GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value))].Cells[9].Value = System.Math.Round(System.Convert.ToDouble(AmountCellNew(e.RowIndex, e.ColumnIndex)),2);
                        go = aux;
                    }

                }
                else if(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) != " " )
                {
                    //Norma
                    // 
                    if (ldipv)
                    {

                        CompleteRamal(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value));

                        System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM, UM, Cuenta FROM EProducto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'");

                        for (int w = 0; w < dts88.Tables[0].Rows.Count; w++)
                        {
                            double cant = 0;
                            String prod = "";

                            prod = dts88.Tables[0].Rows[w].ItemArray[0].ToString();

                            //     int f = 0;
                            System.Data.DataSet sdts;

                            ArrayList list = new ArrayList();

                            for (int x = 1; x < tabControl1.TabCount - 2; x++)
                            {
                                if (tabControl1.TabPages[x].Text.Contains("IPV "))
                                {
                                    String myipv = tabControl1.TabPages[x].Text;
                                    foreach (ValueSaver val in IPVSaver)//NoEsta2(list, val.cant, myipv)
                                    {
                                        if (val.col == 0 && val.IPVName == myipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                                        {
                                            sdts = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM, Producto.DUM, EProducto.UM FROM EProducto INNER JOIN Producto On EProducto.Producto = Producto.Nombre  WHERE NNombre = '" + val.cant + "' AND Producto ='" + prod + "'");
                                            for (int s = 0; s < sdts.Tables[0].Rows.Count; s++)
                                            {
                                                double aux = (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()) * AmountCell22(e.ColumnIndex, "", val.cant, myipv)) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());
                                                cant = cant + UMConverter(aux, sdts.Tables[0].Rows[s].ItemArray[3].ToString(), sdts.Tables[0].Rows[s].ItemArray[4].ToString());

                                            }

                                        }
                                    }




                                    //PutInRamal(prod, System.Convert.ToDouble(cant));//, dts.Tables[0].Rows[w].ItemArray[3].ToString());
                                }
                            }
                            // IntTransf2();
                            double ext = ExistenciaGeneral(prod, dts88.Tables[0].Rows[w].ItemArray[4].ToString());
                            double rango = GetRango(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), prod);
                            if ((ext - System.Convert.ToDouble(cant)) <= rango && (ext - System.Convert.ToDouble(cant)) > (rango * -1))
                            {
                                cant = ext;
                            }

                            PutInRamal(prod, System.Math.Round(System.Convert.ToDouble(cant),2));
                        }


                    }
                }
            }
        }
        private void ValidarExistencias(DataGridViewCellEventArgs e)
        {
              if(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) != " " && dbc.ExistQuerry("SELECT Id FROM EProducto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && !dbc.ExistQuerry("SELECT Id FROM Producto WHERE Nombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'"))
                {
                    //Norma
                    // 
                  String plist ="";
                  

                        System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM, UM, Cuenta FROM EProducto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'");

                        for (int w = 0; w < dts88.Tables[0].Rows.Count; w++)
                        {
                            double cant = 0;
                            String prod = "";

                            prod = dts88.Tables[0].Rows[w].ItemArray[0].ToString();

                            //     int f = 0;
                            System.Data.DataSet sdts;

                            ArrayList list = new ArrayList();

                            for (int x = 1; x < tabControl1.TabCount - 2; x++)
                            {
                                if (tabControl1.TabPages[x].Text.Contains("IPV "))
                                {
                                    String myipv = tabControl1.TabPages[x].Text;
                                    foreach (ValueSaver val in IPVSaver)//NoEsta2(list, val.cant, myipv)
                                    {
                                        if (val.col == 0 && val.IPVName == myipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == "" && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                                        {
                                            sdts = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM, Producto.DUM, EProducto.UM FROM EProducto INNER JOIN Producto On EProducto.Producto = Producto.Nombre  WHERE NNombre = '" + val.cant + "' AND Producto ='" + prod + "'");
                                            for (int s = 0; s < sdts.Tables[0].Rows.Count; s++)
                                            {
                                                double aux = (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()) * AmountCell22(e.ColumnIndex, "", val.cant, myipv)) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());
                                                cant = cant + UMConverter(aux, sdts.Tables[0].Rows[s].ItemArray[3].ToString(), sdts.Tables[0].Rows[s].ItemArray[4].ToString());

                                            }

                                        }
                                    }
                                }
                            }
                            // IntTransf2();
                            double mcant = 0;
                            sdts = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM, Producto.DUM, EProducto.UM FROM EProducto INNER JOIN Producto On EProducto.Producto = Producto.Nombre  WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "' AND Producto ='" + prod + "'");
                            for (int s = 0; s < sdts.Tables[0].Rows.Count; s++)
                            {
                                double aux = (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()) * AmountCell22(e.ColumnIndex, "", GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value),tabControl1.SelectedTab.Text)) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());
                                mcant = mcant + UMConverter(aux, sdts.Tables[0].Rows[s].ItemArray[3].ToString(), sdts.Tables[0].Rows[s].ItemArray[4].ToString());

                            }
                            double mext = 0;

                            double ext = ExistenciaGeneral(prod, dts88.Tables[0].Rows[w].ItemArray[4].ToString());
                            mext = ext - (cant - mcant);
                            if (mcant>mext&&cant>0)
                           {
                               plist = plist+prod + "\n";
                           }
                          
                        }


                  if (plist!="")
                  {
                      IPVBase.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.LightCoral;
                      System.Windows.Forms.MessageBox.Show(this,"Los siguentes productos para la elaboración de "+GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)+"\ntienen menos existencias de las requeridas:\n"+plist, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }
                  else{
                      if (IPVBase.Rows[e.RowIndex].Cells[5].Style.BackColor == Color.LightCoral)
                      {
                          IPVBase.Rows[e.RowIndex].Cells[5].Style.BackColor = IPVBase.Rows[e.RowIndex].Cells[3].Style.BackColor;
                      }
                  }

                }
        }
        private bool NoEsta(ArrayList l, String val, String uname, String cuenta, System.String date, String moneda)
        {
            foreach (ValueSaver vs in l)
            {
                if (vs.cant == val && vs.UName == uname && vs.Cuenta == cuenta && vs.Date == date && vs.moneda == moneda)
                {
                    return false;
                }
            }
            l.Add(new ValueSaver("", val, 0, 0, uname, cuenta, date, "", moneda));
            return true;
        }
        private bool NoEsta2(ArrayList l, String val, String ipv)
        {
            foreach (ValueSaver vs in l)
            {
                if (vs.cant == val && vs.IPVName == ipv)//&& vs.Date == dateTimePicker1.Value && vs.UName == tabControl2.SelectedTab.Text&& vs.moneda == GetMoneda()
                {
                    return false;
                }
            }
            l.Add(new ValueSaver(ipv, val, 0, 0, tabControl2.SelectedTab.Text, "", DateCultureConverter(dateTimePicker1.Value), "", GetMoneda()));
            return true;
        }

        private void PutInRamal(String prod, double cant)//, String um)
        {

            int w = 0;
            while (ramal20Base.Rows[w].Cells[0].Value != null && w < ramal20Base.RowCount - 2 && GetData(ramal20Base.Rows[w].Cells[0].Value) != prod)
                w++;
            // System.Data.DataSet sdts = dbc.SelectQuerryFixed("SELECT Cuenta FROM Producto Nombre = '"+prod+"'");
            //if (sdts.Tables[0].Rows.Count>0)
            //{

            // string kk = ramal20Base.Rows[w].Cells[0].Value.ToString();
            go = false;


            ramal20Base.Rows[w].Cells[9].Value = System.Convert.ToString(cant);//UMConverter(cant, GetData(ramal20Base.Rows[w].Cells[1].Value), um);
            // ramal20Base.Rows[w].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[9].Value))+cant);//UMConverter(cant, GetData(ramal20Base.Rows[w].Cells[1].Value), um);
            go = true;
            //ramal20Base.Rows[w].Cells[9].Style.Format = "N2";
            //ramal20Base.Rows[w].Cells[9].Value = ramal20Base.Rows[w].Cells[9].FormattedValue;
            ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);
            // }

        }
        //private double LeydelaK(int row, String prod, double cant)
        //{
        //    double fondo = ramal20Base.Rows[row].Cells[9].Value
        //}
        private bool ExExistencia(String prod, String cuenta, ref double existencia)
        {
            foreach (InBetween ib in Existences)
            {
                if (ib.Cuenta == cuenta && ib.producto == prod && ib.FromTo == tabControl2.SelectedTab.Text && ib.cant == DateCultureConverter(dateTimePicker1.Value))
                {

                    existencia = ib.Saldo;
                    counter++;
                    return true;

                }
            }
            return false;
        }
        private double Existencia(String prod, String cuenta)
        {

            double existencia = 0;
            double inicio = 0;
            double entrada = 0;
            double entint = 0;
            double salida = 0;
            double salint = 0;
            double traslado = 0;


            //if (!ExExistencia(prod,cuenta, ref existencia))
            //{




            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT [Producto],[ICantidad],[IImporte],[ECantidad],[EImporte],[TCantidad],[TImporte],[Id] FROM Ramal20 WHERE UName ='" +
                    tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "' Order by [Id]");
                inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());

                //ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);
                //inicio = (double)values[0];
                //entrada = (double)values[1];
                //traslado = (double)values[2];

                /*IntTransf2();*/
                ArrayList values = Transfer2Ramal2(prod, cuenta);
                entint = (double)values[0];
                salida = (double)values[1];
                salint = (double)values[2];


                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

            }
            else
            {
                bool poraki = false;
                if (dbc.ExistQuerry("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'"))
                {

                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'");
                    inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                    poraki = true;
                    //entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                    //traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());
                }

                ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);

                if (!poraki)
                    inicio = (double)values[0];
                entrada = (double)values[1];
                traslado = (double)values[2];

                /* IntTransf2();*/
                values = Transfer2Ramal2(prod, cuenta);
                entint = (double)values[0];
                salida = (double)values[1];
                salint = (double)values[2];

                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

                existencia = System.Math.Round(existencia, 2);

                //Existences.Add(new InBetween(cuenta, existencia, "", tabControl2.SelectedTab.Text, prod, DateCultureConverter(dateTimePicker1.Value), ""));

            }

            // }



            return existencia;


        }
        private double Existencia2(String prod, String cuenta)
        {

            double existencia = 0;
            double inicio = 0;
            double entrada = 0;
            double entint = 0;
            double salida = 0;
            double salint = 0;
            double traslado = 0;


            //if (!ExExistencia(prod,cuenta, ref existencia))
            //{




            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT [Producto],[ICantidad],[IImporte],[ECantidad],[EImporte],[TCantidad],[TImporte],[Id] FROM Ramal20 WHERE UName ='" +
                    tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "' Order by [Id]");
                inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());

                ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);
                inicio = (double)values[0];
                entrada = (double)values[1];
                traslado = (double)values[2];

                /*IntTransf2();*/
                values = Transfer2Ramal3(prod, cuenta);
                entint = (double)values[0];
                salida = (double)values[1];
                salint = (double)values[2];


                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

            }
            else
            {
                bool poraki = false;
                if (dbc.ExistQuerry("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'"))
                {

                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'");
                    inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                    poraki = true;
                    //entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                    //traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());
                }

                ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);

                if (!poraki)
                    inicio = (double)values[0];
                entrada = (double)values[1];
                traslado = (double)values[2];

                /* IntTransf2();*/
                values = Transfer2Ramal3(prod, cuenta);
                entint = (double)values[0];
                salida = (double)values[1];
                salint = (double)values[2];

                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

                existencia = System.Math.Round(existencia, 2);

                //Existences.Add(new InBetween(cuenta, existencia, "", tabControl2.SelectedTab.Text, prod, DateCultureConverter(dateTimePicker1.Value), ""));

            }

            // }



            return existencia;


        }
        private double ExistenciaGeneral(String prod, String cuenta)
        {

            double existencia = 0;
            double inicio = 0;
            double entrada = 0;
            double entint = 0;
            double salida = 0;
            double salint = 0;
            double traslado = 0;


            //if (!ExExistencia(prod,cuenta, ref existencia))
            //{




            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT [Producto],[ICantidad],[IImporte],[ECantidad],[EImporte],[TCantidad],[TImporte],[Id] FROM Ramal20 WHERE UName ='" +
                    tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' AND Producto = '" + prod + "' Order by [Id]");
                inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());

                ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);
                inicio = (double)values[0];
                entrada = (double)values[1];
                traslado = (double)values[2];

                /*IntTransf2();*/
                //values = Transfer2Ramal2(prod,cuenta);
                //entint = (double)values[0];
                //salida = (double)values[1] ;
                //salint = (double)values[2];


                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

            }
            else
            {
                bool poraki = false;
                if (dbc.ExistQuerry("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'"))
                {

                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[FCantidad], [FImporte] FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta AND Producto.Nombre = '" + prod + "'");
                    inicio = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[1].ToString());
                    poraki = true;
                    //entrada = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[3].ToString());
                    //traslado = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[5].ToString());
                }

                ArrayList values = GetPutIn2(RamalSaver, prod, cuenta);

                if (!poraki)
                    inicio = (double)values[0];
                entrada = (double)values[1];
                traslado = (double)values[2];

                /* IntTransf2();*/
                //values = Transfer2Ramal2(prod,cuenta);
                //entint = (double)values[0];
                //salida = (double)values[1];
                //salint = (double)values[2];

                existencia = (inicio + entrada + entint) - (salida + salint + traslado);

                existencia = System.Math.Round(existencia, 2);

                //Existences.Add(new InBetween(cuenta, existencia, "", tabControl2.SelectedTab.Text, prod, DateCultureConverter(dateTimePicker1.Value), ""));

            }

            // }



            return existencia;


        }


        private ArrayList GetPutIn2(ArrayList TbSaver, String prod, String cuenta)
        {
            // double total = 0;
            double inicio = 0;
            double entrada = 0;

            double traslado = 0;
            if (TbSaver != null)
            {
                // godeep = false;


                //  bool jet = false;
                int b = 0;
                for (int w = 0; w < TbSaver.Count; w++)
                {
                    ValueSaver val = (ValueSaver)TbSaver[w];
                    if (val.IPVName == "Ramal 20" && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    {
                        if (val.producto == prod && val.Cuenta == cuenta && (val.col == 3))
                        {
                            inicio = System.Convert.ToDouble(val.cant);
                            b++;
                        }
                        if (val.producto == prod && val.Cuenta == cuenta && (val.col == 5))
                        {
                            entrada = System.Convert.ToDouble(val.cant);
                            b++;
                        }
                        if (val.producto == prod && val.Cuenta == cuenta && (val.col == 13))
                        {
                            traslado = System.Convert.ToDouble(val.cant);
                            b++;
                        }
                    }
                    if (b > 2)
                    {
                        break;
                    }
                }



            }

            ArrayList l = new ArrayList();
            l.Add(inicio);
            l.Add(entrada);
            l.Add(traslado);

            return l;
        }
        private ArrayList Transfer2Ramal2(String prod, String cuenta)
        {
            //ramal20Base.Rows[k].Cells[9].Value = "0.00";
            ArrayList valores = new ArrayList();
            double entint = 0;
            double salida = 0;
            double salint = 0;

            valores.Add(entint);
            valores.Add(salida);
            valores.Add(salint);

            double slidasalver = 0;
            double sldintdasalver = 0;

            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == cuenta && ib.Tipo == "In")
                {


                    if (prod == ib.producto)
                    {
                        // ramal20Base.Rows[k].Cells[7].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[7].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        double val = System.Convert.ToDouble(valores[0].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(0);
                        valores.Insert(0, val);
                        val = System.Convert.ToDouble(valores[1].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(1);
                        valores.Insert(1, val);

                        //ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                    }

                }


                if (ib.Cuenta == cuenta && ib.Tipo == "Out")
                {
                    if (prod == ib.producto)
                    {
                        double aux = sldintdasalver;
                        sldintdasalver = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);


                        double val = System.Convert.ToDouble(valores[2].ToString()) + aux;
                        // double val = System.Convert.ToDouble(valores[2].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(2);
                        valores.Insert(2, val);

                        if (slidasalver != System.Convert.ToDouble(valores[1].ToString()))
                        {
                            valores.RemoveAt(1);
                            valores.Insert(1, slidasalver);
                        }
                    }

                }


                if (ib.Cuenta == cuenta && ib.Tipo == "Own")
                {
                    if (prod == ib.producto)
                    {
                        double aux = slidasalver;
                        slidasalver = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);


                        double val = System.Convert.ToDouble(valores[1].ToString()) + aux;
                        valores.RemoveAt(1);
                        valores.Insert(1, val);

                        if (sldintdasalver != System.Convert.ToDouble(valores[2].ToString()))
                        {
                            valores.RemoveAt(2);
                            valores.Insert(2, sldintdasalver);
                        }

                    }
                }
            }

            return valores;
        }
        private double GetYourCant(String eprod, String prod, String cuenta, int place)
        {


            double cantidad = 0;
            int plc = 1;
            foreach (InBetween ib in Transfers)
            {


                if (eprod == ib.eprod && prod == ib.producto && cuenta == ib.Cuenta && (ib.Tipo == "Own" || ib.Tipo == "Out") && ib.ipv == tabControl1.SelectedTab.Text)
                {
                    if (plc == place)
                    {
                        cantidad = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        return cantidad;
                    }
                    plc++;
                }



            }

            return cantidad;
        }
        private double GetYourCant2(String eprod, String prod, String cuenta, int place, String ipv)
        {


            double cantidad = 0;
            int plc = 1;
            foreach (InBetween ib in Transfers)
            {


                if (eprod == ib.eprod && prod == ib.producto && cuenta == ib.Cuenta && (ib.Tipo == "Own" || ib.Tipo == "Out") && ib.ipv == ipv)
                {
                    if (plc == place)
                    {
                        cantidad = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        return cantidad;
                    }
                    plc++;
                }



            }

            return cantidad;
        }
        private ArrayList Transfer2Ramal3(String prod, String cuenta)
        {
            //ramal20Base.Rows[k].Cells[9].Value = "0.00";
            ArrayList valores = new ArrayList();
            double entint = 0;
            double salida = 0;
            double salint = 0;

            valores.Add(entint);
            valores.Add(salida);
            valores.Add(salint);

            //  double slidasalver = 0;

            foreach (InBetween ib in Transfers)
            {
                if (ib.Cuenta == cuenta && ib.Tipo == "In")
                {


                    if (prod == ib.producto)
                    {
                        // ramal20Base.Rows[k].Cells[7].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[7].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                        double val = System.Convert.ToDouble(valores[0].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(0);
                        valores.Insert(0, val);
                        val = System.Convert.ToDouble(valores[1].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(1);
                        valores.Insert(1, val);

                        //ramal20Base.Rows[k].Cells[9].Value = System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2));
                    }

                }


                if (ib.Cuenta == cuenta && ib.Tipo == "Out")
                {
                    if (prod == ib.producto)
                    {

                        double val = System.Convert.ToDouble(valores[2].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(2);
                        valores.Insert(2, val);
                    }

                }


                if (ib.Cuenta == cuenta && ib.Tipo == "Own")
                {
                    if (prod == ib.producto)
                    {
                        //double aux = slidasalver;
                        //slidasalver = System.Math.Round(System.Convert.ToDouble(ib.cant), 2);


                        double val = System.Convert.ToDouble(valores[1].ToString()) + System.Math.Round(System.Convert.ToDouble(ib.cant), 2);
                        valores.RemoveAt(1);
                        valores.Insert(1, val);
                    }
                }
            }

            return valores;
        }
        private double UMConverter(double cant, String um1, String um2)
        {

            if (um1 == um2)
            {
                return System.Math.Round(cant, 2);
            }
            //DBControl dbc = new DBControl();
            System.Data.DataSet sdts = dbc.SelectQuerryFixed("Select Razon From Conversion Where DE ='" + um2 + "' And Para = '" + um1 + "'");
            if (sdts.Tables[0].Rows.Count > 0)
            {

                return cant * System.Convert.ToDouble(sdts.Tables[0].Rows[0].ItemArray[0].ToString());
            }

            return 0;
        }


        private double AmountCellNew(int row, int col)
        {
            double amount = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    amount = amount + System.Convert.ToDouble(val.cant);//= System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    //esta = true;
                }
            }
            return amount;
        }
        private double AmountCell(int row, int col)
        {
            double amount = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    amount = amount + System.Convert.ToDouble(val.cant);//= System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    //esta = true;
                }
            }
            return amount;
        }
        private void UpdateState(bool state)
        {
            dbc.SimplePlan("Update Sistema Set Easier = '" + System.Convert.ToString(state) + "'");
        }
        private double AmountCell2(int col, String cuenta, String prod)
        {
            double amount = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.col == col && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.producto == prod && val.moneda == GetMoneda())
                {
                    amount = amount + System.Convert.ToDouble(val.cant);//= System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    //esta = true;
                }
            }
            return amount;
        }

        private double AmountCell22(int col, String cuenta, String prod, String ipv)
        {
            double amount = 0;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.col == col && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.producto == prod && val.moneda == GetMoneda())
                {
                    amount = amount + System.Convert.ToDouble(val.cant);//= System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    //esta = true;
                }
            }
            return amount;
        }

        private bool EstaSMSaver(int row, int col)
        {

            bool esta = false;
            if (SMSaver != null)
            {


                foreach (ValueSaver val in SMSaver)
                {
                    if (val.row == row && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    {
                        val.cant = System.Convert.ToString(SubMayorBase.Rows[row].Cells[col].Value);
                        esta = true;
                    }
                }
            }
            return esta;
        }
        private bool EstaRISaver(int row, int col)
        {

            bool esta = false;
            if (SMSaver != null)
            {


                foreach (ValueSaver val in RISaver)
                {
                    if (val.row == row && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    {
                        val.cant = System.Convert.ToString(ResIngBase.Rows[row].Cells[col].Value);
                        esta = true;
                    }
                }
            }
            return esta;
        }

        private bool EstaRamalSaver(int row, int col)
        {

            // bool esta = false;
            if (RamalSaver != null)
            {


                foreach (ValueSaver val in RamalSaver)
                {
                   
                    if (val.row == row && val.col == col && val.IPVName == tabControl1.TabPages[0].Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    {
                        val.cant = System.Convert.ToString(ramal20Base.Rows[row].Cells[col].Value);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool HayRamalSaver()
        {

            // bool esta = false;
            if (RamalSaver != null)
            {


                foreach (ValueSaver val in RamalSaver)
                {
                    if (val.IPVName == tabControl1.TabPages[0].Text && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    {
                        //val.cant = System.Convert.ToString(ramal20Base.Rows[row].Cells[col].Value);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool EstaSomeoneSaver(ArrayList al, DataGridView dtgv, int row, int col)
        {

            // bool esta = false;



            foreach (ValueSaver val in al)
            {
                if (val.row == row && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = System.Convert.ToString(dtgv.Rows[row].Cells[7].Value);
                    return true;
                }
            }

            return false;
        }


        private bool EstaIPVSaver(int row, int col)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    return true;
                }
            }
            return false;
        }
        private void GetTheFuckOut(int row, int col)
        {
            // bool esta = false;
            for (int w = 0; w < IPVSaver.Count; w++)
            {
                ValueSaver val = (ValueSaver)IPVSaver[w];
                if (val.producto == "Totales:" && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    IPVSaver.RemoveAt(w);
                    break;
                }
            }

        }

        private bool EstaIPVSaver2(int row, int col, String cuenta)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = System.Convert.ToString(IPVBase.Rows[row].Cells[col].Value);
                    return true;
                }
            }
            return false;
        }
        private bool EstaIPVSaver3(int row, int col, String cuenta, int rrow)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = System.Convert.ToString(IPVBase.Rows[rrow].Cells[col].Value);
                    return true;
                }
            }
            return false;
        }
        private bool EstaIPVSaver4(int row, int col, String cuenta, String value, String ipv)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.row == row && val.col == col && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    val.cant = value;
                    return true;
                }
            }
            return false;
        }
        private int GetMyRow(String prod, String cuenta)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.producto == prod && val.col == 0 && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    return val.row;

                }
            }
            return 0;
        }
        private bool EstaIPVSaver4(String data)
        {
            // bool esta = false;
            foreach (ValueSaver val in IPVSaver)
            {
                if (val.producto == "Totales:" && val.col == IPVBase.ColumnCount - 1 && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    //val.cant = System.Convert.ToString(IPVBase.Rows[varow].Cells[col].Value);
                    val.cant = data;
                    return true;
                }
            }
            return false;
        }
        
        private void IPVBase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                if (e.RowIndex >= 0 && GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) != " ")
                {

                    if (forzar.Enabled&&forzar.Checked)
                    {
                        if (e.RowIndex >= 0 && (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 7 || e.ColumnIndex == 11) && e.RowIndex < IPVBase.RowCount - 1)
                        {
                        
                                if (e.ColumnIndex == 7)
                                {
                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[2].Value));
                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), 2);

                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value));
                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value), 2);

                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(IPVBase, e.ColumnIndex + 1);
                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = GetTotal(IPVBase, e.ColumnIndex + 3);
                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value), 2);

                                }
                                else
                                {
                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[9].Value));
                                    IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), 2);

                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(IPVBase, e.ColumnIndex + 1);
                                    IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                                }
                                // IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Style.Font.Bold = true;

                                IPVBase.Rows[e.RowIndex].Cells[11].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) - System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[7].Value));
                                IPVBase.Rows[e.RowIndex].Cells[11].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[11].Value), 2);


                                IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[11].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[9].Value));
                                IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[12].Value), 2);

                                IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value = GetTotal(IPVBase, 12);
                                IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value), 2);
                        }
                        if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 9) && e.RowIndex < IPVBase.RowCount - 1)
                        {
                            if (e.ColumnIndex==2)
                            {
                                String aux = IPVBase.Rows[e.RowIndex].Cells[7].Value.ToString();
                                IPVBase.Rows[e.RowIndex].Cells[7].Value = "0.00";
                                IPVBase.Rows[e.RowIndex].Cells[7].Value = aux;
                            }
                            if (e.ColumnIndex == 9)
                            {
                                String aux = IPVBase.Rows[e.RowIndex].Cells[3].Value.ToString();
                                IPVBase.Rows[e.RowIndex].Cells[3].Value = "0.00";
                                IPVBase.Rows[e.RowIndex].Cells[3].Value = aux;

                                aux = IPVBase.Rows[e.RowIndex].Cells[5].Value.ToString();
                                IPVBase.Rows[e.RowIndex].Cells[5].Value = "0.00";
                                IPVBase.Rows[e.RowIndex].Cells[5].Value = aux;

                                aux = IPVBase.Rows[e.RowIndex].Cells[7].Value.ToString();
                                IPVBase.Rows[e.RowIndex].Cells[7].Value = "0.00";
                                IPVBase.Rows[e.RowIndex].Cells[7].Value = aux;
                            }


                        }

                        if (e.ColumnIndex == 5)
                        {
                            //AmountCell(e.RowIndex, e.ColumnIndex);

                            if (GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) != " " && !dbc.ExistQuerry("SELECT Id FROM EProducto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && dbc.ExistQuerry("SELECT Id FROM Producto WHERE Nombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && forzar.Enabled && forzar.Checked)
                            {
                                if (Esta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)) == -1)
                                {
                                    bool yp = false;
                                    bool aux = go;
                                    go = true;
                                    if (!yp && !EstaIPVSaver(e.RowIndex, e.ColumnIndex) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                                    {
                                        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                    }
                                    ramal20Base.Rows[Search4(ramal20Base, GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value))].Cells[9].Value = System.Math.Round(System.Convert.ToDouble(AmountCellNew(e.RowIndex, e.ColumnIndex)), 2);
                                    go = aux;
                                }

                            }
                        }
                    }
                    else{
                    
                    if (go2 && e.RowIndex >= 0 && (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 7 || e.ColumnIndex == 11) && e.RowIndex < IPVBase.RowCount - 1)
                    {
                        go2 = false;
                        bool yp = false;

                        if (e.ColumnIndex == 5 && dbc.ExistQuerry("SELECT Id FROM EProducto WHERE NNombre ='" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) > 0)
                        {
                            if (!EstaIPVSaver(e.RowIndex, e.ColumnIndex) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            if (comboBox1.Text == "")
                            {

                                int irow = GetMaxIPV2(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)));

                                if (ldipv && !EstaIPVSaver3(irow, e.ColumnIndex, GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)), e.RowIndex))
                                {
                                    //put in the real ipv
                                    IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), irow, e.ColumnIndex, tabControl2.SelectedTab.Text, GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                }
                            }


                            IPVBase.Rows[e.RowIndex].Cells[9].Value = GetUnitPrice2(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), System.Convert.ToDouble((IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)));

                            if (!EstaIPVSaver(e.RowIndex, 9))
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[9].Value), e.RowIndex, 9, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            yp = true;

                        }

                        if (e.ColumnIndex == 7)
                        {
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[2].Value));
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), 2);

                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value));
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value), 2);

                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(IPVBase, e.ColumnIndex + 1);
                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = GetTotal(IPVBase, e.ColumnIndex + 3);
                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value), 2);

                        }
                        else
                        {
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[9].Value));
                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), 2);

                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(IPVBase, e.ColumnIndex + 1);
                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                        }
                        // IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Style.Font.Bold = true;

                        IPVBase.Rows[e.RowIndex].Cells[11].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) - System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[7].Value));
                        IPVBase.Rows[e.RowIndex].Cells[11].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[11].Value), 2);


                        IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[11].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[9].Value));
                        IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[12].Value), 2);

                        IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value = GetTotal(IPVBase, 12);
                        IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[12].Value), 2);


                        if (!yp && !EstaIPVSaver(e.RowIndex, e.ColumnIndex) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            if (!EstaIPVSaver(e.RowIndex, 9))
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[9].Value), e.RowIndex, 9, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                            if (!EstaIPVSaver(e.RowIndex, 2))
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[2].Value), e.RowIndex, 2, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));


                        }


                        if (!yp && comboBox1.Text == "")
                        {

                            int irow = GetMaxIPV2(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)));

                            if (ldipv && !EstaIPVSaver3(irow, e.ColumnIndex, GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)), e.RowIndex))
                            {
                                //put in the real ipv


                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), irow, e.ColumnIndex, tabControl2.SelectedTab.Text, GetCuenta(GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                            }
                        }

                        PERamal20(e);


                        if (e.ColumnIndex == 5 && dbc.ExistQuerry("SELECT Id FROM Producto WHERE Nombre ='" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "'") && System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[5].Value)) > 0)
                        {


                            double valant = System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value);


                            IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = IPV_Ramal_Ajustment(tabControl1.SelectedTab.Text, System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[9].Value), Search4(ramal20Base, GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value)), e.RowIndex);

                            if (valant != System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value))
                            {
                                IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.ForeColor = Color.DarkBlue;
                            }
                            else
                                IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.ForeColor = Color.Black;

                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = GetTotal(IPVBase, e.ColumnIndex + 1);
                            IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 1].Value), 2);

                        }

                        if (System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[11].Value)) != 0 && e.ColumnIndex != 7 && e.ColumnIndex != 11) // nuevo ajuste
                        {

                            double final = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[12].Value));

                            double real = (System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[4].Value)) + System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[6].Value))) - (System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[10].Value)));


                            final = System.Math.Round(final, 2);
                            // real = real * -1;
                            real = System.Math.Round(real, 2);
                            if (real < 0)
                            {
                                real = real * -1;
                            }
                            if (final < 0)
                            {
                                final = final * -1;
                            }


                            if ((System.Math.Round(final - real, 2) == 0.01) || (System.Math.Round(real - final, 2) == 0.01))
                            {
                                IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Convert.ToString(System.Math.Round(real, 2));
                                IPVBase.Rows[e.RowIndex].Cells[12].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[e.RowIndex].Cells[12].Value), 2);
                                IPVBase.Rows[e.RowIndex].Cells[12].Style.ForeColor = Color.DarkBlue;
                                IPVBase.Rows[IPVBase.RowCount - 1].Cells[12].Value = GetTotal(IPVBase, 12);
                            }
                            else
                            {
                                if ((System.Math.Round(System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[11].Value)) * System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[9].Value)), 2)) == System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[12].Value)))
                                    IPVBase.Rows[e.RowIndex].Cells[12].Style.ForeColor = Color.Black;
                            }

                        }




                        if (e.ColumnIndex == 7) // el ajuste lokol
                        {
                            double cdv = System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value));
                            double mynum = System.Convert.ToDouble(System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[12].Value)) - (System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[4].Value)) + System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[6].Value))));

                            cdv = System.Math.Round(cdv, 2);
                            mynum = mynum * -1;
                            mynum = System.Math.Round(mynum, 2);

                            if ((System.Math.Round(cdv - mynum, 2) > 0.00) || (System.Math.Round(mynum - cdv, 2) > 0.00))
                            {
                                IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = System.Convert.ToString(mynum);
                                IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Style.ForeColor = Color.DarkBlue;

                                IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = GetTotal(IPVBase, e.ColumnIndex + 3);
                                IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value = System.Math.Round(System.Convert.ToDouble(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[e.ColumnIndex + 3].Value), 2);
                            }
                            else
                                IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Style.ForeColor = Color.Black;

                            GetTheFuckOut(IPVBase.RowCount - 1, 10);
                            IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[IPVBase.RowCount - 1].Cells[10].Value), IPVBase.RowCount - 1, 10, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[IPVBase.RowCount - 1].Cells[0].Value), GetMoneda()));

                            GetTheFuckOut(IPVBase.RowCount - 1, 8);
                            IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[IPVBase.RowCount - 1].Cells[8].Value), IPVBase.RowCount - 1, 8, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[IPVBase.RowCount - 1].Cells[0].Value), GetMoneda()));


                        }




                        EstaIPVSaver(e.RowIndex, 11);

                        if (!EstaIPVSaver4(GetNumData(IPVBase.Rows[IPVBase.RowCount - 1].Cells[IPVBase.ColumnCount - 1].Value)) && IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[IPVBase.ColumnCount - 1].Value), IPVBase.Rows.Count - 1, IPVBase.ColumnCount - 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[IPVBase.Rows.Count - 1].Cells[0].Value), GetMoneda()));
                        }



                        if (e.RowIndex > -1 && IPVBase.Rows[e.RowIndex].Cells[11].Value != null && System.Convert.ToDouble(GetNumData(IPVBase.Rows[e.RowIndex].Cells[11].Value)) < 0)
                        {
                            IPVBase.Rows[e.RowIndex].Cells[11].Style.ForeColor = Color.Red;
                        }
                        else if (e.RowIndex > -1 && IPVBase.Rows[e.RowIndex].Cells[11].Value != null)
                            IPVBase.Rows[e.RowIndex].Cells[11].Style.ForeColor = Color.Black;



                        go2 = true;
                    }

                    if (e.RowIndex >= 0 && e.ColumnIndex == 0 && comboBox1.Text == "")//&& GetMoneda() == "CUP"
                    {
                        //if (IPVBase.Rows[e.RowIndex].Cells[0].Style.BackColor == Color.LightYellow)
                        //{
                        bool pass = false;
                        System.Data.DataSet dts1 = dbc.SelectQuerryFixed("SELECT distinct(NNombre),Precio FROM EProducto INNER JOIN Producto ON Producto.Nombre = EProducto.Producto WHERE NNombre LIKE '%" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "%' AND Moneda = '" + GetMoneda() + "'");
                        if (dts1.Tables[0].Rows.Count == 1)
                        {
                            go2 = false;
                            //  IPVBase.Rows[e.RowIndex].Cells[0].Value = dts1.Tables[0].Rows[0].ItemArray[0].ToString();


                            IPVBase.Rows.Insert(e.RowIndex, dts1.Tables[0].Rows[0].ItemArray[0].ToString(), "Rac", dts1.Tables[0].Rows[0].ItemArray[1].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", GetUnitPrice(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), "0.00", "0", "0.00");
                            IPVBase.Rows.RemoveAt(e.RowIndex + 1);
                            IPVBase.Rows.Insert(IPVBase.RowCount - 2);
                            IPVBase.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                            IPVBase.Rows[e.RowIndex + 1].Cells[0].ReadOnly = false;




                            // IPVBase.Rows[e.RowIndex].Cells[10].Value = "0";


                            IPVBase.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.White;
                            CompleteRamal(dts1.Tables[0].Rows[0].ItemArray[0].ToString());

                            if (!EstaIPVSaver(e.RowIndex, e.ColumnIndex))
                            {
                                // IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex));
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), e.RowIndex, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));


                            }



                            //  int max = System.Convert.ToInt32(dbc.SelectQuerryFixed("select count(UndProd.Id) from UndProd inner join Producto on UndProd.Producto = Producto.Nombre where UndProd.UName = '" + tabControl2.SelectedTab.Text + "' and Producto.PrecOut>0 and Producto.Cuenta='" + GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()) + "'").Tables[0].Rows[0].ItemArray[0].ToString());
                            int irow = GetMaxIPV3(tabControl1.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()));

                            if (ldipv && !EstaIPVSaver2(irow, e.ColumnIndex, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString())))
                            {
                                //put in the real ipv
                                //int irow = GetMaxIPV3(tabControl1.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()));

                                //int f;
                                //if(irow > 0)
                                //     f = 99;
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), irow, e.ColumnIndex, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), irow, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                //precios
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[2].Value), irow, 2, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                                IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[9].Value), irow, 9, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                            }
                            pass = true;
                        }
                       
                        
                         //dts1 = dbc.SelectQuerryFixed("SELECT distinct(NNombre),Precio FROM EProducto INNER JOIN Producto ON Producto.Nombre = EProducto.Producto WHERE NNombre = '" + GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value) + "' AND Moneda = '" + GetMoneda() + "'");
                         //if (dts1.Tables[0].Rows.Count == 1)
                         //{
                         //    go2 = false;
                         //    //  IPVBase.Rows[e.RowIndex].Cells[0].Value = dts1.Tables[0].Rows[0].ItemArray[0].ToString();


                         //    IPVBase.Rows.Insert(e.RowIndex, dts1.Tables[0].Rows[0].ItemArray[0].ToString(), "Rac", dts1.Tables[0].Rows[0].ItemArray[1].ToString(), "0", "0.00", "0", "0.00", "0", "0.00", GetUnitPrice(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), "0.00", "0", "0.00");
                         //    IPVBase.Rows.RemoveAt(e.RowIndex + 1);
                         //    IPVBase.Rows.Insert(IPVBase.RowCount - 2);
                         //    IPVBase.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                         //    IPVBase.Rows[e.RowIndex + 1].Cells[0].ReadOnly = false;




                         //    // IPVBase.Rows[e.RowIndex].Cells[10].Value = "0";


                         //    IPVBase.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.White;
                         //    CompleteRamal(dts1.Tables[0].Rows[0].ItemArray[0].ToString());

                         //    if (!EstaIPVSaver(e.RowIndex, e.ColumnIndex))
                         //    {
                         //        // IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex));
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), e.RowIndex, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));


                         //    }



                         //    //  int max = System.Convert.ToInt32(dbc.SelectQuerryFixed("select count(UndProd.Id) from UndProd inner join Producto on UndProd.Producto = Producto.Nombre where UndProd.UName = '" + tabControl2.SelectedTab.Text + "' and Producto.PrecOut>0 and Producto.Cuenta='" + GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()) + "'").Tables[0].Rows[0].ItemArray[0].ToString());
                         //    int irow = GetMaxIPV3(tabControl1.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()));

                         //    if (ldipv && !EstaIPVSaver2(irow, e.ColumnIndex, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString())))
                         //    {
                         //        //put in the real ipv
                         //        //int irow = GetMaxIPV3(tabControl1.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()));

                         //        //int f;
                         //        //if(irow > 0)
                         //        //     f = 99;
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), irow, e.ColumnIndex, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value), irow, e.ColumnIndex + 1, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                         //        //precios
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[2].Value), irow, 2, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                         //        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[e.RowIndex].Cells[9].Value), irow, 9, tabControl2.SelectedTab.Text, GetCuenta(dts1.Tables[0].Rows[0].ItemArray[0].ToString()), DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));

                         //    }
                         //    pass = true;
                         //}

                        if(!pass)
                            if (e.RowIndex != IPVBase.RowCount - 1)
                            {
                                IPVBase.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightCoral;
                                if (hlpeve)
                                {
                                    hlpManager.hlpknd = HelpKind.Resolucion;
                                    hlpManager.topic = "IPV";
                                    hlpManager.tnumber = 1;
                                    hlpManager.errknd = ErrorKind.IPV_Wront_Prod;
                                    hlpManager.atext.Clear();

                                }
                            }
                        else{

                                    if (hlpeve)
                                    {
                                        hlpManager.hlpknd = HelpKind.Descripcion;
                                        hlpManager.topic = "IPV";
                                        hlpManager.tnumber = 1;
                                        // hlpManager.errknd = ErrorKind.IPV_Wront_Prod;
                                        hlpManager.atext.Clear();
                                    }
                            }


                        go2 = true;//}

                    }

                    if (e.ColumnIndex == 5)
                    {
                        GetTheFuckOut(IPVBase.RowCount - 1, 6);
                        IPVSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(IPVBase.Rows[IPVBase.RowCount - 1].Cells[6].Value), IPVBase.RowCount - 1, 6, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(IPVBase.Rows[IPVBase.RowCount - 1].Cells[0].Value), GetMoneda()));

                    }
                }
                }
                else
                {
                    if (e.RowIndex >= 0 && (e.ColumnIndex != 1 && GetData(IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != "Rac"))
                        IPVBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                {
                    dbc.CloseConnection();
                }
                
            }



        }
        private int GetMaxIPV(String ipv, String cuenta)
        {
            int max = 0;

            dts = dbc.SelectQuerryFixed("select count(UndProd.Id) from UndProd inner join Producto on UndProd.Producto = Producto.Nombre where UndProd.UName = '" + tabControl2.SelectedTab.Text + "' and Producto.PrecOut>0 and Producto.Cuenta='" + cuenta + "'");
            max = System.Convert.ToInt32(dts.Tables[0].Rows[0].ItemArray[0].ToString());


            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    if (val.row >= max)
                        max = val.row + 1;

                }
            }

            return max;
        }

        private int GetMaxIPV4(String ipv, String cuenta, int max)
        {



            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName == ipv && val.producto != "Totales:" && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    if (val.row >= max)
                        max = val.row + 1;

                }
            }

            return max;
        }

        private int GetMaxIPV3(String ipv, String cuenta)
        {
            int max = 0;

            // ldipv = false;
            ipv = ipv.Replace("IPV ", "");
            //  IPVBase.Rows.Clear();
            //  System.Data.DataSet dts;

            if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + ipv + "'  AND Moneda = '" + GetMoneda() + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[VPrice],[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad],[VIngreso],[CUnitario],[CVendido]" +
                ",[FCantidad],[FCosto],[Id] FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + ipv + "' AND Moneda = '" + GetMoneda() + "'");

                // godeep = false;
                //  int chck = 0;
                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    max = w;




            }
            else
            {

                dts = dbc.SelectQuerryFixed("SELECT [Nombre],[DUM],[PrecOut],[PrecIn] FROM [MLB].[dbo].[Producto] INNER JOIN [MLB].[dbo].[UndProd] ON Producto.Nombre = UndProd.Producto WHERE [Cuenta] = '" + cuenta + "' AND Moneda = '" + GetMoneda() + "' AND UName = '" + tabControl2.SelectedTab.Text + "' AND [PrecOut] != '0' ORDER BY UndProd.Id");

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    max++;




                dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],Producto.PrecOut,[FCantidad],Producto.PrecIn FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + cuenta + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "' AND IPV.FCantidad != 0 AND Producto.Cuenta = IPV.Cuenta");

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    max = w + 1;

                dts = dbc.SelectQuerryFixed("SELECT distinct(IPV.Producto),IPV.UM,[VPrice],[FCantidad],[CUnitario] FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + cuenta + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.FCantidad != 0 AND IPV.Moneda = '" + GetMoneda() + "'");

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    max++;

                //dts = dbc.SelectQuerryFixed("SELECT distinct(IPV.Producto),IPV.UM,[VPrice],[FCantidad],[CUnitario] FROM [MLB].[dbo].[IPV] WHERE UName='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + cuenta + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.FCantidad != 0 AND IPV.Moneda = '" + GetMoneda() + "'");

                //for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                //    max++;


            }

            max = GetMaxIPV4("IPV " + ipv, cuenta, max);

            return max;
        }

        private int GetMaxIPV2(String prod, String cuenta)
        {
            int max = 0;



            foreach (ValueSaver val in IPVSaver)
            {
                if (val.producto == prod && val.IPVName == tabControl1.SelectedTab.Text && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    if (val.row >= max)
                        max = val.row;

                }
            }

            return max;
        }
        private String GetUnitPrice(String eprod)
        {


            System.Data.DataSet dts1 = dbc.SelectQuerryFixed("SELECT Cantidad, Producto.PrecIn, PUM, Producto.DUM, Eproducto.UM  FROM EProducto INNER JOIN Producto ON EProducto.Producto = Producto.Nombre  WHERE EProducto.NNombre = '" + eprod + "'");
            double uprice = 0;
            for (int w = 0; w < dts1.Tables[0].Rows.Count; w++)
            {
                double c = System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString());
                c = UMConverter(System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString()), dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
                c = System.Math.Round(c, 2);
                uprice += System.Math.Round(c * System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString()), 2) / System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[2].ToString());
            }

            return System.Convert.ToString(uprice);
        }

        private String GetUnitPrice2(String eprod, double cant)//reajustar a la cantidad
        {
            if (ldipv)
                IntTransf2();
            System.Data.DataSet dts1 = dbc.SelectQuerryFixed("SELECT Cantidad, Producto.PrecIn, PUM, Producto.DUM, Eproducto.UM, Eproducto.Producto,  Eproducto.Cuenta FROM EProducto INNER JOIN Producto ON EProducto.Producto = Producto.Nombre  WHERE EProducto.NNombre = '" + eprod + "' and EProducto.Producto in (SELECT Eproducto.Producto FROM EProducto INNER JOIN Producto ON EProducto.Producto = Producto.Nombre  WHERE EProducto.NNombre = '" + eprod + "'  GROUP BY Eproducto.Producto Having count (Eproducto.Producto)=1)");
            double uprice = 0;
            ArrayList pxa = new ArrayList();
            for (int w = 0; w < dts1.Tables[0].Rows.Count; w++)
            {
                double c = System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString());
                c = (c * cant) / System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[2].ToString());

                //c = UMConverter(System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString()), dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
                c = UMConverter(c, dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
                c = System.Math.Round(c, 2);
                //uprice += System.Math.Round(c * System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString()), 2) / System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[2].ToString());

                double rcant = GetYourCant(eprod, dts1.Tables[0].Rows[w].ItemArray[5].ToString(), dts1.Tables[0].Rows[w].ItemArray[6].ToString(), HowMany(pxa, dts1.Tables[0].Rows[w].ItemArray[5].ToString(), dts1.Tables[0].Rows[w].ItemArray[6].ToString()));
                double real = 0;
                //bool pinter = false;
                if (c != rcant)
                {
                    real = c;
                    c = rcant;
                    //  pinter = true;
                }



                //double ext = Existencia(dts1.Tables[0].Rows[w].ItemArray[5].ToString(), dts1.Tables[0].Rows[w].ItemArray[6].ToString());
                //double rango = GetRango(eprod, dts1.Tables[0].Rows[w].ItemArray[5].ToString());



                //if ((ext - System.Convert.ToDouble(c)) <= rango && (ext - System.Convert.ToDouble(c)) > (rango * -1))
                //{
                //    c = ext;
                //}
                uprice = uprice + System.Math.Round(c * System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString()), 2);
                //uprice = System.Math.Round(uprice, 2);
            }

            dts1 = dbc.SelectQuerryFixed("SELECT Eproducto.Producto, Producto.PrecIn, Producto.DUM, Eproducto.UM FROM EProducto INNER JOIN Producto ON EProducto.Producto = Producto.Nombre  WHERE EProducto.NNombre = '" + eprod + "'  GROUP BY Eproducto.Producto, Producto.PrecIn, Producto.DUM, Eproducto.UM Having count (Eproducto.Producto)>1 Order by Eproducto.Producto");
            System.Data.DataSet sdts = new System.Data.DataSet();
            double cc = 0;
            for (int w = 0; w < dts1.Tables[0].Rows.Count; w++)
            {
                //double c = System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString());
                sdts = dbc.SelectQuerryFixed("SELECT Producto, Cantidad, PUM FROM EProducto WHERE NNombre = '" + eprod + "' AND Producto ='" + dts1.Tables[0].Rows[w].ItemArray[0].ToString() + "'");
                for (int s = 0; s < sdts.Tables[0].Rows.Count; s++)
                {
                    double variable = (cant * (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()))) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());// (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()) * AmountCell2(e.ColumnIndex, "", val.cant)) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());(cant * (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()))) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString());// (System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[1].ToString()) * AmountCell2(e.ColumnIndex, "", val.cant)) / System.Convert.ToDouble(sdts.Tables[0].Rows[s].ItemArray[2].ToString())
                    cc = cc + UMConverter(variable, dts1.Tables[0].Rows[w].ItemArray[2].ToString(), dts1.Tables[0].Rows[w].ItemArray[3].ToString()); ;
                }
                // cc = 
                uprice = uprice + System.Math.Round(cc * System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString()), 2);
                cc = 0;
            }

            if (!DeleteUPrice(tabControl1.SelectedTab.Text, eprod, System.Convert.ToInt32(cant)))
                uprice = ExistsUPrice(tabControl1.SelectedTab.Text, eprod, System.Convert.ToInt32(cant), uprice);
            // ArrayList cantsave = new ArrayList();
            //double c2 =0;
            //String pname = "-";
            //for (int w = 0; w < dts1.Tables[0].Rows.Count; w++)
            //{

            //    if (pname == "-")
            //    {


            //        double c = System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString());
            //        c = (c * cant) / System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[2].ToString());

            //        //c = UMConverter(System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString()), dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
            //      //  c = UMConverter(c, dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
            //      //  c = System.Math.Round(c, 2);
            //        pname = dts1.Tables[0].Rows[w].ItemArray[5].ToString();

            //        c2 = c;

            //    }
            //    else{

            //        if (dts1.Tables[0].Rows[w].ItemArray[5].ToString() == dts1.Tables[0].Rows[w-1].ItemArray[5].ToString())
            //        {
            //                 double c = System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString());
            //            c = (c * cant) / System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[2].ToString());

            //            //c = UMConverter(System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[0].ToString()), dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
            //            c = UMConverter(c, dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
            //          //  c = System.Math.Round(c, 2);
            //            pname = dts1.Tables[0].Rows[w].ItemArray[5].ToString();
            //            c2 += c;

            //            if (w == dts1.Tables[0].Rows.Count-1||dts1.Tables[0].Rows[w].ItemArray[5].ToString() != dts1.Tables[0].Rows[w + 1].ItemArray[5].ToString())
            //            {
            //                c2 = UMConverter(c2, dts1.Tables[0].Rows[w].ItemArray[3].ToString(), dts1.Tables[0].Rows[w].ItemArray[4].ToString());
            //                uprice = uprice + System.Math.Round(c2 * System.Convert.ToDouble(dts1.Tables[0].Rows[w].ItemArray[1].ToString()), 2);
            //                pname = "-";
            //            }

            //        }
            //        //else{

            //        //    pname = "-";
            //        //}

            //    }


            //}

            return System.Convert.ToString(System.Math.Round(uprice / cant, 6));
        }
        //private String GetUnitPrice3(String eprod, double cant)//reajustar a la cantidad
        //{
        //    double uprice = 1;
        //    foreach (ValueSaver val in ImpEntSaver)
        //   {
        //       //if (System.Convert.ToDouble(GetNumData(IPVBase.Rows[val.row].Cells[5].Value)) == System.Convert.ToDouble(GetNumData(IPVBase.Rows[val.row].Cells[11].Value)))
        //       //{
        //       if (val.col == 6 && val.producto == eprod && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
        //       {
        //           return System.Convert.ToString(System.Math.Round((val.cant / ), 6));
        //       }



        //   }
        //    if (cant == 0)
        //    {
        //        cant = 1;
        //    }
        //    return System.Convert.ToString(System.Math.Round(uprice / cant, 6));
        //}
        private int Esta(String prod)
        {
            int w = 0;
            while (ramal20Base.Rows[w].Cells[0].Value != null && ramal20Base.RowCount > w + 1)
            {
                if (ramal20Base.Rows[w].Cells[0].Value.ToString() == prod)
                {
                    w = -1;
                    break;
                }
                w++;
            }
            return w;

        }
        private int Esta489(String prod)
        {
            int w = 0;
            while (ramal20Base.Rows[w].Cells[0].Value != null && ramal20Base.RowCount > w + 1)
            {
                if (ramal20Base.Rows[w].Cells[0].Value.ToString() == prod)
                {
                    return w;
                    // break;
                }
                w++;
            }
            return w;


        }

        private void CompleteRamal(String PElaborado)
        {

            dts = dbc.SelectQuerryFixed("SELECT Producto.Nombre, Producto.DUM, Producto.PrecIn FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + PElaborado + "'");

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                int k = Esta(dts.Tables[0].Rows[w].ItemArray[0].ToString());

                if (k != -1)
                {


                    for (int j = 2; j < ramal20Base.ColumnCount; j++)
                    {
                        if (ramal20Base.Columns[j].DefaultCellStyle.Format == "C2")
                            ramal20Base.Rows[k].Cells[j].Value = "0.00";
                        else
                            ramal20Base.Rows[k].Cells[j].Value = "0";

                    }



                    ramal20Base.Rows[k].Cells[0].Value = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                    ramal20Base.Rows[k].Cells[1].Value = dts.Tables[0].Rows[w].ItemArray[1].ToString();
                    ramal20Base.Rows[k].Cells[2].Value = dts.Tables[0].Rows[w].ItemArray[2].ToString();
                    ramal20Base.Rows[k].Cells[15].Value = dts.Tables[0].Rows[w].ItemArray[2].ToString();

                    ramal20Base.Rows.Insert(ramal20Base.RowCount - 2);


                }

            }


        }

        private void UnFillRamal(String PElaborado)
        {

            dts = dbc.SelectQuerryFixed("SELECT Producto.Nombre, Producto.DUM, Producto.PrecIn FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + PElaborado + "'");

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                int k = Esta489(dts.Tables[0].Rows[w].ItemArray[0].ToString());

                if (k != -1)
                {





                    if (comboBox1.Text == "")
                    {
                        if (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[k].Cells[9].Value)) == 0)
                        {
                            SalverFixer2(k, RamalSaver, tabControl1.TabPages[0].Text);
                            ramal20Base.Rows.RemoveAt(k);

                        }

                    }



                }

            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            forzar.Enabled = false;
           // forzar.Checked = false;
            if (tabControl1.SelectedTab.Text == "Ramal 20")
            {
                LoadRamal20();
                if (hlpeve)
                {

                    if (hlpManager.topic != "Ramal20")
                        hlpManager.tnumber = 1;

                    hlpManager.topic = "Ramal20";
                }
            }
            if (tabControl1.SelectedTab.Text.Contains("IPV "))
            {
                IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;
                LoadIPV(tabControl1.SelectedTab.Text);
                tabControl1.SelectedTab.Controls.Add(TLIPV);
                IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;

                if (hlpeve)
                {

                    if (hlpManager.topic != "IPV")
                        hlpManager.tnumber = 1;

                    hlpManager.topic = "IPV";
                }
               // hlptopic = "IPV";
            }
            if (tabControl1.SelectedTab.Text == "SubMayor")
            {
                forzar.Checked = false;
                LoadSubMayor();

                if (hlpeve)
                {

                    if (hlpManager.topic != "SubMayor")
                        hlpManager.tnumber = 1;

                hlpManager.topic = "SubMayor";
                }
            }
            if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
            {
                forzar.Checked = false;
                LoadResIng();
                if (hlpeve)
                {

                    if (hlpManager.topic != "Resumen de Ingresos")
                        hlpManager.tnumber = 1;

                    hlpManager.topic = "Resumen de Ingresos";
                }
            }

            if (tabControl1.SelectedTab.Text == "Ficha de Costo")
            {
                forzar.Checked = false;
                if (GetMoneda() == "CUP")
                {
                    LoadFichaCosto();
                    FCostoTop.Columns[0].Width = FichaCostoBase.Columns[0].Width + FichaCostoBase.Columns[1].Width;
                    if (hlpeve)
                    {

                        if (hlpManager.topic != "Ficha de Costo")
                            hlpManager.tnumber = 1;

                        hlpManager.topic = "Ficha de Costo";
                    }
                }

            }

            if (tabControl1.SelectedTab.Text == "Vale de Salida")
            {
                // if (GetMoneda() == "CUP")
                forzar.Checked = false;
                LoadValeSalida();
                if (hlpeve)
                {

                    if (hlpManager.topic != "Vale de Salida")
                        hlpManager.tnumber = 1;

                    hlpManager.topic = "Vale de Salida";
                }
            }

            if (tabControl1.SelectedTab.Text == "Flujo de Caja")
            {
                // if (GetMoneda() == "CUP")
                forzar.Checked = false;
                LoadFlujoCaja();
                if (hlpeve)
                {

                    if (hlpManager.topic != "Flujo de Caja")
                        hlpManager.tnumber = 1;

                    hlpManager.topic = "Flujo de Caja";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            buttons = MessageBoxButtons.YesNo;
            //System.Windows.Forms.DialogResult result;


            // quit logo 
            //loaded=true;
            // Displays the MessageBox.
            //connected=false;
            result = MessageBox.Show(this, "Está seguro de que desea Guardar los datos del " + tabControl1.SelectedTab.Text + " para la Unidad: " + tabControl2.SelectedTab.Text + ", Cuenta: " + comboBox1.Text + ", del dia " + DateCultureConverter(dateTimePicker1.Value) + "?..", "Advertencia", buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (tabControl1.SelectedTab.Text.Contains("IPV "))
                {
                    if (CValidar(IPVBase))
                    {
                        SaveIPV();
                        LoadIPV(tabControl1.SelectedTab.Text.Replace("IPV ", ""));
                        ReSetValues(IPVSaver);
                    }

                }
                if (tabControl1.SelectedIndex == 0)
                {
                    if (CValidar(ramal20Base))
                    {
                        SaveRamal();
                        LoadRamal20();
                        ReSetValues(RamalSaver);
                    }
                }
                if (tabControl1.SelectedTab.Text == "SubMayor" && comboBox1.Text != "")
                {
                    SaveSubMayor();
                    LoadSubMayor();
                    ReSetValues(SMSaver);
                }
                if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                {
                    SaveResIng();
                    LoadResIng();
                    ReSetValues(RISaver);
                }
                if (tabControl1.SelectedTab.Text == "Ficha de Costo")
                {
                    SaveFichCost();
                    LoadFichaCosto();
                    ReSetValues(FCSaver);
                    if (FichaCostoBase.RowCount > 0)
                    {
                        tabControl1.SelectedTab.BackColor = Color.LightGray;
                    }
                }

                if (tabControl1.SelectedTab.Text == "Vale de Salida")
                {
                    SaveValeSalida();
                    LoadValeSalida();
                    //if (ValeSalidaBase.RowCount > 0)
                    //{
                    //    tabControl1.SelectedTab.BackColor = Color.LightGray;
                    //}
                }

                if (tabControl1.SelectedTab.Text == "Flujo de Caja")
                {
                    SaveFlujoCaja();
                    LoadFlujoCaja();
                    if (FlujoCajaBase.RowCount > 1)
                    {
                        tabControl1.SelectedTab.BackColor = Color.LightGray;
                    }
                    else
                    {
                        tabControl1.SelectedTab.BackColor = Color.Transparent;

                    }
                }
            }
        }
        private String ConcepSplit2(String concepto)
        {
            String cpt = "";
            if (concepto.Contains("Ingresos por Ventas (Cuenta: "))
            {
                cpt = concepto.Replace("Ingresos por Ventas ", "");
            }
            if (concepto.Contains("Compra de Mercancias (Cuenta: "))
            {
                cpt = concepto.Replace("Compra de Mercancias ", "");
            }
            if (concepto.Contains("Costos de Traslados (Cuenta: "))
            {
                cpt = concepto.Replace("Costos de Traslados ", "");
            }

            return cpt;
        }
        private String ConcepSplit(String concepto)
        {
            String cpt = concepto;
            if (concepto.Contains("Ingresos por Ventas (Cuenta: "))
            {
                cpt = "Ingresos por Ventas";
            }
            if (concepto.Contains("Compra de Mercancias (Cuenta: "))
            {
                cpt = "Compra de Mercancias";
            }
            if (concepto.Contains("Costos de Traslados (Cuenta: "))
            {
                cpt = "Costos de Traslados";
            }

            return cpt;
        }
        private void SaveFlujoCaja()
        {
            if (dateTimePicker1.Value.ToShortDateString() == dateTimePicker2.Value.ToShortDateString() && dateTimePicker2.Value.ToShortDateString() == dateTimePicker3.Value.ToShortDateString())
            {

                dbc.SimplePlan("DELETE FROM Flujo WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "'");
                for (int w = 0; w < FlujoCajaBase.RowCount - 1; w++)
                {

                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[Flujo]([Id],[Date],[Tipo],[Concepto],[Importe],[UName],[Moneda],[IInicial],[SubConcep])" +

                  "VALUES ('" + dbc.MaxQuerry("Flujo") + "','" + GetData(FlujoCajaBase.Rows[w].Cells[0].Value) + "','" + GetData(FlujoCajaBase.Rows[w].Cells[1].Value) + "','" + GetData(FlujoCajaBase.Rows[w].Cells[2].Value) + "'" +
                   ",'" + GetData(FlujoCajaBase.Rows[w].Cells[3].Value) + "','" + tabControl2.SelectedTab.Text + "','" + GetMoneda() + "','" + textBox1.Text + "','" + GetData(FlujoCajaBase.Rows[w].Cells[2].ToolTipText) + "')");

                }
            }
            else
                System.Windows.Forms.MessageBox.Show(this, "Solo puede Guardar Asientos de un solo dia...\nRevise el rango de Tiempo en el Fujo de Caja, y la Fecha");
        }

        private void ResIngBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 6 && e.RowIndex >= 0)
                {


                    // double ingHoy = AmountIPV2();
                    //double costHoy = AmountIPV();

                    double ingHoy = System.Convert.ToDouble(GetNumData(ResIngBase.Rows[e.RowIndex].Cells[3].Value));
                    double costHoy = System.Convert.ToDouble(GetNumData(ResIngBase.Rows[e.RowIndex].Cells[5].Value));

                    double ingHH = System.Convert.ToDouble(GetNumData(ResIngBase.Rows[e.RowIndex].Cells[7].Value)) + ingHoy;

                    double costHH = System.Convert.ToDouble(GetNumData(ResIngBase.Rows[e.RowIndex].Cells[8].Value)) + costHoy;

                    //ResIngBase.Rows[e.RowIndex].Cells[2].Value = System.Convert.ToDouble(ingHoy);

                    ResIngBase.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToDouble(ingHH);
                    // ResIngBase.Rows[e.RowIndex].Cells[4].Value = System.Convert.ToDouble(costHoy);
                    ResIngBase.Rows[e.RowIndex].Cells[6].Value = System.Convert.ToDouble(costHH);

                    //Totalizar;
                    //  ResIngBase.Rows.Add("Totales: ", dateTimePicker1.Value.Day.ToString(), MonthConverter(dateTimePicker1.Value.Month), GetTotal(ResIngBase, 3), GetTotal(ResIngBase, 4), GetTotal(ResIngBase, 5), GetTotal(ResIngBase, 6), GetTotal(ResIngBase, 7), GetTotal(ResIngBase, 8));
                    ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[4].Value = GetTotal(ResIngBase, 4);
                    ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[6].Value = GetTotal(ResIngBase, 6);
                    ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[7].Value = GetTotal(ResIngBase, 7);
                    ResIngBase.Rows[ResIngBase.RowCount - 1].Cells[8].Value = GetTotal(ResIngBase, 8);

                }

                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (init.Visible&&secs < 10)
            {
                this.Opacity = 100;
                timer1.Enabled = false;
                init.Visible = false;

            }

            secs++;
            
            if (hlpeve)
            {
               // timer1.Interval = 1000;
                if (hlptimer>5)
                {
                    String htext = ea.HelpText(hlpManager);
                    if (htext != "La Ostia tío...!")
                    {
                        helpcomm.Text = htext;
                        toolTip1.SetToolTip(helpcomm, htext);


                        if (darkViewToolStripMenuItem.Checked)
                        {
                            if (hlpManager.hlpknd == HelpKind.Resolucion)
                                helpcomm.ForeColor = Color.Orange;
                            if (hlpManager.hlpknd == HelpKind.Descripcion)
                                helpcomm.ForeColor = Color.White;

                        }
                        else
                        {
                            if (hlpManager.hlpknd == HelpKind.Resolucion)
                                helpcomm.ForeColor = Color.DarkRed;
                            if (hlpManager.hlpknd == HelpKind.Descripcion)
                                helpcomm.ForeColor = Color.Black;
                        }

                    }
                   

                    //if (hlpManager.topic == "Saludo" && hlpManager.tnumber == 1)
                    //{
                    //    pictureBox2.Image = Image.FromFile("Media\\Vissuals\\eve anime start.gif");
                    //}
                    //else 
                    if (hlpManager.tnumber < 4)
                        pictureBox2.Image = Image.FromFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Media\\Vissuals\\eve anime middle.gif");
                    else
                        pictureBox2.Image = Image.FromFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Media\\Vissuals\\eve anime end.gif");



                    
                   // helpcomm.Visible = false;
                    hlptimer = -1;

                    if (hlpManager.tnumber < 6)
                        hlpManager.tnumber++;
                    else
                    {
                        hlpManager.tnumber = 0;
                        hlpManager.hlpknd = HelpKind.Descripcion;
                    }
                }
                hlptimer++;
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {

            //MLB.Imprimir imp = new MLB.Imprimir("Ramal20", "WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'");
            MLB.Imprimir imp = new MLB.Imprimir();
            if (tabControl1.SelectedIndex == 0)
            {
                if (coToolStripMenuItem.Checked)
                {
                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Desea Generar el Reporte de Exitencias en Ramal20?...", "Confirmación", buttons, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        PreparerRamal3();
                        imp.table = "FullRamal";
                    }
                    else{
                        PreparerRamal();
                        imp.table = "Ramal20";
                    }
                    
                }
                else{
                    PreparerRamal();
                    imp.table = "Ramal20";
                }
               
            }
            else if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
            {
                PreparerResIng();
                imp.table = "ResIng";
            }
            else if (tabControl1.SelectedTab.Text == "SubMayor")
            {
                PreparerSubMayor();
                imp.table = "SubMayor";
            }
            else if (tabControl1.SelectedTab.Text == "Ficha de Costo")
            {
                PreparerFichCost();
                imp.table = "FichaCosto";
            }
            else if (tabControl1.SelectedTab.Text == "Vale de Salida")
            {
                PreparerValeSalida();
                imp.table = "ValeSalida";
            }
            else if (tabControl1.SelectedTab.Text == "Flujo de Caja")
            {
                PreparerFlujo();
                imp.table = "Flujo";
            }
            else
            {
                PreparerIPV();
                imp.table = "IPV";
            }


            imp.ShowDialog();
        }
        private void PreparerRamal()
        {
            dbc.SimplePlan("DELETE FROM PRamal WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            String var = "som@e!";
            for (int w = 0; w < ramal20Base.RowCount; w++)
            {
                if (ramal20Base.Rows[w].Cells[0].Value != null && var != GetData(ramal20Base.Rows[w].Cells[0].Value))
                {
                    var = GetData(ramal20Base.Rows[w].Cells[0].Value);
                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PRamal]([Id],[Producto]" +
                   ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
                   "VALUES ('" + dbc.MaxQuerry("PRamal") + "','" + GetData(ramal20Base.Rows[w].Cells[0].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[1].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[2].Value) + "'" +
                   ",'" + GetData(ramal20Base.Rows[w].Cells[3].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[4].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[5].Value) + "'" +
                   ",'" + GetNumData(ramal20Base.Rows[w].Cells[6].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[7].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[8].Value) + "'" +
                   ",'" + GetData(ramal20Base.Rows[w].Cells[9].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[10].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[11].Value) + "'" +
                   ",'" + GetNumData(ramal20Base.Rows[w].Cells[12].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[13].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[14].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[15].Value) + "','" + GetData(ramal20Base.Rows[w].Cells[16].Value) + "','" + GetNumData(ramal20Base.Rows[w].Cells[17].Value) + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "')");

                }

            }
        }
        private void PreparerRamal2()
        {
            dbc.SimplePlan("DELETE FROM PRamal WHERE [MachineName]= '" + System.Environment.MachineName + "'");
           // if (!IsCadena(tabControl2.SelectedTab.Text))
           // {
                dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte]" +
                       ",[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Id] FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' Order by [Id]");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                      
                            dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PRamal]([Id],[Producto]" +
                           ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
                           "VALUES ('" + dbc.MaxQuerry("PRamal") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
                           ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "'" +
                           ",'" + dts.Tables[0].Rows[w].ItemArray[6].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[7].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[8].ToString() + "'" +
                           ",'" + dts.Tables[0].Rows[w].ItemArray[9].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[10].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[11].ToString() + "'" +
                           ",'" + dts.Tables[0].Rows[w].ItemArray[12].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[13].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[14].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[15].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[16].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[17].ToString() + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "')");

                        

                    }
            //}
        }
        private void PreparerRamal3()
        {
            dbc.SimplePlan("DELETE FROM PRamal WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            // if (!IsCadena(tabControl2.SelectedTab.Text))
            // {
            dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte]" +
                   ",[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Cuenta] FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "' And [EICantidad] = '0'  Order by [Id]");


            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PRamal]([Id],[Producto]" +
               ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad],[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
               "VALUES ('" + dbc.MaxQuerry("PRamal") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[6].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[7].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[8].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[9].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[10].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[11].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[12].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[13].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[14].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[15].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[16].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[17].ToString() + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + dts.Tables[0].Rows[w].ItemArray[18].ToString() + "','" + GetMoneda() + "')");



            }
            //}
        }
        private void PreparerIPV()
        {
            dbc.SimplePlan("DELETE FROM PIPV WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < IPVBase.RowCount; w++)
            {
                if (IPVBase.Rows[w].Cells[0].Value == null)
                    break;

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PIPV]([Id],[Producto],[UM],[VPrice]" +
             ",[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad],[VIngreso],[CUnitario],[CVendido]" +
             ",[FCantidad],[FCosto],[MachineName],[UName],[Date],[Cuenta],[Moneda],[IPVName])" +
               "VALUES ('" + dbc.MaxQuerry("PIPV") + "','" + GetData(IPVBase.Rows[w].Cells[0].Value) + "','" + GetData(IPVBase.Rows[w].Cells[1].Value) + "','" + GetData(IPVBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(IPVBase.Rows[w].Cells[3].Value) + "','" + GetData(IPVBase.Rows[w].Cells[4].Value) + "','" + GetData(IPVBase.Rows[w].Cells[5].Value) + "'" +
               ",'" + GetData(IPVBase.Rows[w].Cells[6].Value) + "','" + GetData(IPVBase.Rows[w].Cells[7].Value) + "','" + GetData(IPVBase.Rows[w].Cells[8].Value) + "'" +
               ",'" + GetData(IPVBase.Rows[w].Cells[9].Value) + "','" + GetData(IPVBase.Rows[w].Cells[10].Value) + "','" + GetData(IPVBase.Rows[w].Cells[11].Value) + "'" +
               ",'" + GetData(IPVBase.Rows[w].Cells[12].Value) + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "','" + tabControl1.SelectedTab.Text + "')");

            }
        }
        private void PreparerIPV2()
        {
            dbc.SimplePlan("DELETE FROM PIPV WHERE [MachineName]= '" + System.Environment.MachineName + "'");

             dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[VPrice],[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad],[VIngreso],[CUnitario],[CVendido]" +
                    ",[FCantidad],[FCosto],[IPVName] FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "' ORDER BY Id");

                    //godeep2 = false;
                    
            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PIPV]([Id],[Producto],[UM],[VPrice]" +
             ",[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad],[VIngreso],[CUnitario],[CVendido]" +
             ",[FCantidad],[FCosto],[MachineName],[UName],[Date],[Cuenta],[Moneda],[IPVName])" +
               "VALUES ('" + dbc.MaxQuerry("PIPV") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[6].ToString()+ "','" + dts.Tables[0].Rows[w].ItemArray[7].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[8].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[9].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[10].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[11].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[12].ToString() + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "','" +dts.Tables[0].Rows[w].ItemArray[13].ToString() + "')");

            }
        }
        private void PreparerFichCost()
        {
            dbc.SimplePlan("DELETE FROM PFichaCosto WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < FichaCostoBase.RowCount; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PFichaCosto]([Id],[EProducto],[Raciones],[Producto]" +
             ",[UM],[Norma],[Cantidad],[Precio],[Importe],[MachineName],[UName],[Date],[Moneda],[Cuenta])" +
             "VALUES ('" + dbc.MaxQuerry("PFichaCosto") + "','" + GetData(FichaCostoBase.Rows[w].Cells[0].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[1].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(FichaCostoBase.Rows[w].Cells[3].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[4].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[5].Value) + "'" +
               ",'" + GetData(FichaCostoBase.Rows[w].Cells[6].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[7].Value) + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");

            }
        }
        private void PreparerFichCost2()
        {
            dbc.SimplePlan("DELETE FROM PFichaCosto WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dts = dbc.SelectQuerryFixed("Select [EProducto],[Raciones],[Producto],[UM],[Norma],[Cantidad],[Precio],[Importe] FROM FichaCosto WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "' ORDER BY Id");
            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {
                    
                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PFichaCosto]([Id],[EProducto],[Raciones],[Producto]" +
             ",[UM],[Norma],[Cantidad],[Precio],[Importe],[MachineName],[UName],[Date],[Moneda],[Cuenta])" +
             "VALUES ('" + dbc.MaxQuerry("PFichaCosto") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + GetData(FichaCostoBase.Rows[w].Cells[5].Value) + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[6].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[7].ToString() + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");

            }
        }
        private void SaveFichCost()
        {
            dbc.SimplePlan("DELETE FROM FichaCosto WHERE [UName]= '" + tabControl2.SelectedTab.Text + "' AND [Date]='" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND [Cuenta] = '" + comboBox1.Text + "'");

            for (int w = 0; w < FichaCostoBase.RowCount; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[FichaCosto]([Id],[EProducto],[Raciones],[Producto]" +
             ",[UM],[Norma],[Cantidad],[Precio],[Importe],[UName],[Date],[Moneda],[Cuenta])" +
             "VALUES ('" + dbc.MaxQuerry("FichaCosto") + "','" + GetData(FichaCostoBase.Rows[w].Cells[0].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[1].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(FichaCostoBase.Rows[w].Cells[3].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[4].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[5].Value) + "'" +
               ",'" + GetData(FichaCostoBase.Rows[w].Cells[6].Value) + "','" + GetData(FichaCostoBase.Rows[w].Cells[7].Value) + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");

            }
        }
        private void PreparerValeSalida()
        {
            dbc.SimplePlan("DELETE FROM PValeSalida WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < ValeSalidaBase.RowCount; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PValeSalida]([Id],[Producto],[UM],[Cantidad],[Precio],[Importe],[MachineName],[UName],[Date],[Moneda],[Cuenta])" +
              "VALUES ('" + dbc.MaxQuerry("PValeSalida") + "','" + GetData(ValeSalidaBase.Rows[w].Cells[0].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[1].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(ValeSalidaBase.Rows[w].Cells[3].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[4].Value) + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");

            }
        }
        private void PreparerValeSalida2()
        {
            dbc.SimplePlan("DELETE FROM PValeSalida WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dts = dbc.SelectQuerryFixed("Select [Producto],[UM],[Cantidad],[Precio],[Importe] FROM ValeSalida WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Text + "' ORDER BY Id");
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                       dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PValeSalida]([Id],[Producto],[UM],[Cantidad],[Precio],[Importe],[MachineName],[UName],[Date],[Moneda],[Cuenta])" +
                         "VALUES ('" + dbc.MaxQuerry("PValeSalida") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString()+ "'" +
                           ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");
    
                    }
              
            
        }
        private void PreparerFlujo()
        {
            dbc.SimplePlan("DELETE FROM PFlujo WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < FlujoCajaBase.RowCount - 1; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PFlujo]([Id],[IDate],[Tipo],[Concepto],[Importe],[IInicial],[IFinal] ,[Balance],[UName],[Moneda] ,[MachineName],[FechaI],[FechaF])" +
              "VALUES ('" + dbc.MaxQuerry("PFlujo") + "','" + GetData(FlujoCajaBase.Rows[w].Cells[0].Value) + "','" + GetData(FlujoCajaBase.Rows[w].Cells[1].Value) + "','" + GetData(FlujoCajaBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(FlujoCajaBase.Rows[w].Cells[3].Value) + "','" + textBox1.Text + "','" + textBox2.Text + "','" + balance.Text + "','" + tabControl2.SelectedTab.Text + "','" + GetMoneda() + "','" + System.Environment.MachineName + "','"+dateTimePicker2.Value.ToShortDateString()+"','"+dateTimePicker3.Value.ToShortDateString()+"')");

            }
        }
        private void SaveValeSalida()
        {
            dbc.SimplePlan("DELETE FROM ValeSalida WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND [Cuenta] = '" + comboBox1.Text + "'");

            if (ValeSalidaBase.RowCount > 1)
            {

                for (int w = 0; w < ValeSalidaBase.RowCount; w++)
                {

                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[ValeSalida]([Id],[Producto],[UM],[Cantidad],[Precio],[Importe],[UName],[Date],[Moneda],[Cuenta])" +
                  "VALUES ('" + dbc.MaxQuerry("ValeSalida") + "','" + GetData(ValeSalidaBase.Rows[w].Cells[0].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[1].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[2].Value) + "'" +
                   ",'" + GetData(ValeSalidaBase.Rows[w].Cells[3].Value) + "','" + GetData(ValeSalidaBase.Rows[w].Cells[4].Value) + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetMoneda() + "','" + comboBox1.Text + "')");

                }
            }

        }
        private void PreparerSubMayor()
        {
            dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < SubMayorBase.RowCount; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PSubMayor]([Id],[Day],[Month],[SaldoInicial],[Entrada],[EntInt]" +
             ",[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
               "VALUES ('" + dbc.MaxQuerry("PSubMayor") + "','" + GetData(SubMayorBase.Rows[w].Cells[0].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[1].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[2].Value) + "'" +
               ",'" + GetData(SubMayorBase.Rows[w].Cells[3].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[4].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[5].Value) + "'" +
               ",'" + GetData(SubMayorBase.Rows[w].Cells[6].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[7].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[8].Value) + "','" + GetData(SubMayorBase.Rows[w].Cells[9].Value) +
               "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "')");

            }
        }
        private void PreparerSubMayor2()
        {
            dbc.SimplePlan("DELETE FROM PSubMayor WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            dts = dbc.SelectQuerryFixed("SELECT [Day],[Month],[SaldoInicial],[Entrada],[EntInt],[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[Id]" +
                   " FROM [MLB].[dbo].[SubMayor] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' ORDER BY [Day]");

            for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PSubMayor]([Id],[Day],[Month],[SaldoInicial],[Entrada],[EntInt]" +
             ",[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
               "VALUES ('" + dbc.MaxQuerry("PSubMayor") + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
             ",'" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "'" +
               ",'" + dts.Tables[0].Rows[w].ItemArray[6].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[7].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[8].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[9].ToString() +
               "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + comboBox1.Text + "','" + GetMoneda() + "')");

            }
        }

        private void PreparerResIng()
        {
            dbc.SimplePlan("DELETE FROM PResIng WHERE [MachineName]= '" + System.Environment.MachineName + "'");
            for (int w = 0; w < ResIngBase.RowCount; w++)
            {

                dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[PResIng]([Id],[Day],[Month],[InHoy]" +
             ",[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[MachineName],[UName],[Date],[Cuenta],[Moneda])" +
               "VALUES ('" + dbc.MaxQuerry("PResIng") + "','" + GetData(ResIngBase.Rows[w].Cells[1].Value) + "','" + GetData(ResIngBase.Rows[w].Cells[2].Value) + "','" + GetData(ResIngBase.Rows[w].Cells[3].Value) + "'" +
               ",'" + GetData(ResIngBase.Rows[w].Cells[4].Value) + "','" + GetData(ResIngBase.Rows[w].Cells[5].Value) + "','" + GetData(ResIngBase.Rows[w].Cells[6].Value) + "'" +
               ",'" + GetData(ResIngBase.Rows[w].Cells[7].Value) + "','" + GetData(ResIngBase.Rows[w].Cells[8].Value) + "','" + System.Environment.MachineName + "','" + tabControl2.SelectedTab.Text + "','" + DateCultureConverter(dateTimePicker1.Value) + "','" + GetData(ResIngBase.Rows[w].Cells[0].Value) + "','" + GetMoneda() + "')");

            }
        }

        private String GetData(Object val)
        {
            if (val != null)
            {
                return val.ToString();
            }
            return " ";

        }
        private String GetNumData(Object val)
        {
            try{

           
            if (val != null && val.ToString() != "")
            {
                //String aux = val.ToString().Replace("$", "");
                //aux = aux.Replace("(", "");
                //aux = aux.Replace(")", "");

                String aux = System.Convert.ToString(System.Convert.ToDouble(val));
                // aux = aux.Replace(",", "");
               if (aux == "")
                   aux = "0";
                if (Isnumeric(aux))
                  return aux;

            }
            return "0.00";

                 }
            catch(Exception e)
            {
                String aux = val.ToString().Replace("$", "");
                aux = aux.Replace("(", "");
                aux = aux.Replace(")", "");
                bool crap = Isnumeric(aux);
                return "0.00";
            }
        }
        private bool Isnumeric(String num)
        {
            double shit = 0;
            //num = "15.78";
            return System.Double.TryParse(num, out shit);
            
        }
        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //C:\WINDOWS\system32\calc.exe

            try
            {
                Process myProcess = new Process();

                myProcess.StartInfo.FileName = System.Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\calc.exe";//+"\\Register_Key.exe";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Brother donde tu metes la calculadora de Windows?...", "Easier " + ex.Source);
            }
        }

        private void editarVariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               

                SetVissualState(darkViewToolStripMenuItem.Checked);
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\DataFiller.exe";//+"\\Register_Key.exe";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se encuentra el módulo de Edicion de Variables. La reinstalación de la aplicación pudiera resolver este problema.", "Easier " + ex.Source);
            }
        }

        private void acesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MLB.Authorization aut = new MLB.Authorization(this);
            aut.ShowDialog(this);
            //editarVariablesToolStripMenuItem.Enabled = true;

        }

        private void monedaNacionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (monedaNacionalToolStripMenuItem.Checked)
            {
                if (tabControl1.TabPages[tabControl1.TabCount - 2].Text != "Vale de Salida")
                {
                    tabControl1.TabPages.Insert(tabControl1.TabCount - 1, "Ficha de Costo");
                    tabControl1.TabPages[tabControl1.TabCount - 2].Controls.Add(TLFichaCosto);
                    tabControl1.TabPages.Insert(tabControl1.TabCount - 1, "Vale de Salida");
                    tabControl1.TabPages[tabControl1.TabCount - 2].Controls.Add(TLValeSalida);


                }
                //tabControl1.TabPages[tabControl1.TabCount - 3].Visible = true;
                //tabControl1.TabPages[tabControl1.TabCount - 2].Visible = true;
            }
            divisaToolStripMenuItem.Checked = false;
            monedaNacionalToolStripMenuItem.Checked = true;
            LoadComoBox();
            LoadResIng();
            LoadFlujoCaja();

        }

        private void divisaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (divisaToolStripMenuItem.Checked)
            {
                if (tabControl1.TabPages[tabControl1.TabCount - 2].Text != "Resumen de Ingresos")
                {

                   // tabControl1.TabPages.RemoveAt(tabControl1.TabCount - 2);
                   // tabControl1.TabPages.RemoveAt(tabControl1.TabCount - 2);

                }

                //tabControl1.TabPages[tabControl1.TabCount-3].Visible = false;
                //tabControl1.TabPages[tabControl1.TabCount - 2].Visible = false;

            }
            monedaNacionalToolStripMenuItem.Checked = false;
            divisaToolStripMenuItem.Checked = true;
            LoadComoBox();

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                IPVBase.Columns[0].ReadOnly = false;
            }
            else
                IPVBase.Columns[0].ReadOnly = true;

        }

        private void SubMayorBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    SubMayorBase.Rows[e.RowIndex].Cells[8].Value = (System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[4].Value)) +
                        System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))) - (System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[5].Value)) + System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[6].Value)) +
                        System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[7].Value)));
                    SubMayorBase.Rows[e.RowIndex].Cells[8].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[8].Value), 2);

                    double aux = 0;
                    if (ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value != null)
                    {
                        aux = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value.ToString());
                    }

                    SubMayorBase.Rows[e.RowIndex].Cells[9].Value = System.Convert.ToDouble(System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[8].Value)) - aux - AmountIPV4());

                    if (System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[9].Value) < 0.01)
                    {
                        SubMayorBase.Rows[e.RowIndex].Cells[9].Value = "0.00";
                    }
                    SubMayorBase.Rows[e.RowIndex].Cells[9].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[9].Value), 2);
                    if (!EstaSMSaver(e.RowIndex, e.ColumnIndex))
                    {
                        SMSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(SubMayorBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(SubMayorBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                    }

                }

                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }

            //  doubfle saldofinal = (entrada + diaAnt) - (salida + traslado);
        }

        private void SubMayorBase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    SubMayorBase.Rows[e.RowIndex].Cells[8].Value = (System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[4].Value)) +
                        System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))) - (System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[5].Value)) + System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[6].Value)) +
                        System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[7].Value)));
                    SubMayorBase.Rows[e.RowIndex].Cells[8].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[8].Value), 2);

                    double aux = 0;
                    if (ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value != null)
                    {
                        aux = System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.Rows.Count - 1].Cells[17].Value.ToString());
                    }

                    SubMayorBase.Rows[e.RowIndex].Cells[9].Value = System.Convert.ToDouble(System.Convert.ToDouble(GetNumData(SubMayorBase.Rows[e.RowIndex].Cells[8].Value)) - aux - AmountIPV4());

                    if (System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[9].Value) < 0.01)
                    {
                        SubMayorBase.Rows[e.RowIndex].Cells[9].Value = "0.00";
                    }
                    SubMayorBase.Rows[e.RowIndex].Cells[9].Value = System.Math.Round(System.Convert.ToDouble(SubMayorBase.Rows[e.RowIndex].Cells[9].Value), 2);

                }

                if (!EstaSMSaver(e.RowIndex, e.ColumnIndex) && e.RowIndex >= 0 && go)
                {
                    SMSaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(SubMayorBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(SubMayorBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }

        }

        private void ValeSalidaBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void ResIngBase_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ramal20Base_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Delete && ramal20Base.SelectedCells.Count > 0 && ramal20Base.SelectedCells[0].RowIndex < ramal20Base.RowCount - 1)
                {
                    if (ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[0].Value != null && ((!forzar.Enabled) || (forzar.Enabled && forzar.Checked && dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))))
                    {
                        dbc.SimplePlan("DELETE [UndProd] WHERE [Producto] = '" + GetData(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[0].Value) + "' AND [UName] = '" + tabControl2.SelectedTab.Text + "'");
                        ramal20Base.Rows.RemoveAt(ramal20Base.SelectedCells[0].RowIndex);
                        SalverFixer(ramal20Base.SelectedCells[0].RowIndex, RamalSaver);
                        SalverFixer(ramal20Base.SelectedCells[0].RowIndex, IPVSaver);

                    }

                }

                if (e.KeyCode.ToString().Length == 1 && ramal20Base.SelectedCells.Count > 0 && ramal20Base.SelectedCells[0].RowIndex < ramal20Base.RowCount - 1 && ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[0].Value != null && ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[0].ReadOnly)
                {
                    int w = ramal20Base.SelectedCells[0].RowIndex+1;
                    bool findit = false;

                    while (w < ramal20Base.RowCount && w!=ramal20Base.SelectedCells[0].RowIndex&& !findit)
                    {

                        if (ramal20Base.Rows[w].Cells[0].Value==null)
                        {
                            w = 0;
                        }
                        if (e.KeyCode.ToString() == GetData(ramal20Base.Rows[w].Cells[0].Value)[0].ToString() || e.KeyCode.ToString().ToLower(cinf) == GetData(ramal20Base.Rows[w].Cells[0].Value)[0].ToString())
                        {
                            findit = true;
                           // ramal20Base.SelectedCells.Clear();
                            int k = 0; 
                            while (k < ramal20Base.RowCount && ramal20Base.Rows[k].Cells[0].Value!= null )
                            {
                                ramal20Base.Rows[k].Cells[0].Selected = false;
                                //ramal20Base.SelectedCells[k].Selected = false;
                                k++;
                            }
                            ramal20Base.Rows[w].Cells[0].Selected = true;
                            
                                
                        }
                        w++;
                    }
                     
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }


        }

        private void ResIngBase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 6 && e.RowIndex >= 0 && e.RowIndex < ResIngBase.RowCount - 1)
                {


                    if (!EstaRISaver(e.RowIndex, e.ColumnIndex))
                    {
                        RISaver.Add(new ValueSaver(tabControl1.SelectedTab.Text, System.Convert.ToString(ResIngBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(ResIngBase.Rows[e.RowIndex].Cells[0].Value), GetMoneda()));
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (ex.Message.Contains("onnection"))
                dbc.CloseConnection();
            }



        }

        private void ResIngTop_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadResIng();
            }
        }

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (tableLayoutPanel4.ColumnStyles[0].Width == 20F)
                tableLayoutPanel4.ColumnStyles[0].Width = 5F;
            else
                tableLayoutPanel4.ColumnStyles[0].Width = 20F;


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           

            // quit logo 
            //loaded=true;
            // Displays the MessageBox.
            //connected=false;
           // buttons = MessageBoxButtons.YesNo;
           // result = MessageBox.Show(this, "Esta seguro de que ha Guardado todos los cambios antes Cerrar?...", "Confirmación", buttons, MessageBoxIcon.Question);
           // if (result == DialogResult.No)
          //  {
             //   e.Cancel = true;
           // }
            if (!failed)
            {
                if (salir == false)
                {
                    MLB.Salir s = new MLB.Salir(OnSalir(), this);
                    s.ShowDialog(this);
                    e.Cancel = true;
                }

                if (!e.Cancel)
                {
                    if (HayInfo())
                       SaveTemp();
                    else
                    {
                        if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
                        {
                            FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
                            //fi.Encrypt();
                            fi.Delete();
                            //  File.CreateText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");


                        }
                    }

                    
                        
                    if (dbc.ExistQuerry("Select Automatic From Salva Where Automatic = 'True'"))
                    {
                        try
                        {
                           // toolStripStatusLabel2.Text = "Guardando Base de Datos ...";
                            dts = dbc.SelectQuerryFixed("Select Destino From Salva");
                            if(dts.Tables[0].Rows.Count>0)
                            dbc.SimplePlan("BACKUP DATABASE [MLB] TO  DISK = N'" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "\\Salva_BD_" + System.DateTime.Now.ToShortDateString().Replace("/", "-") + ".bak' WITH NOFORMAT, NOINIT,  NAME = N'MLB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10");
                           // toolStripStatusLabel2.Text = "Base de Datos Guardada Correctamente.";
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            if (ex.Message.Contains("onnection"))
                            dbc.CloseConnection();
                            // toolStripStatusLabel2.Text = "Ocurrió un error." + ex.Message.ToString() + " Intente nuevamente.";
                        }
                    }
                    UpdateState(false);
                   }
            }




        }
        private bool  HayInfo()
        {
            if (RamalSaver.Count > 0 || IPVSaver.Count > 0 || RISaver.Count > 0 || FCSaver.Count > 0)
            {
                return true;
            }
            return false;
            
        }
        public void OutYa()
        {
            salir = true;
            Close();
        }
        private void guardarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int aux = tabControl1.SelectedIndex;

            for (int w = tabControl1.TabCount-1; w >= 0; w--)
            {
                tabControl1.SelectedIndex = w;
                if (tabControl1.SelectedTab.Text != "Resumen de Ingresos" && tabControl1.SelectedTab.Text != "Flujo de Caja")
                {
                    //tabControl1.SelectedIndex = w;
                    button2_Click(sender, e);
                }
                

            }

            tabControl1.SelectedIndex = aux;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (forzar.Enabled&&forzar.Checked)
            {
                    buttons = MessageBoxButtons.YesNo;
                  
                    result = MessageBox.Show(this, "Esta seguro de que desea borrar los datos Superficiales del " + tabControl1.SelectedTab.Text + " para la Unidad: " + tabControl2.SelectedTab.Text + ", Cuenta: " + comboBox1.Text + ", del dia " + DateCultureConverter(dateTimePicker1.Value) + "?..", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (tabControl1.SelectedTab.Text.Contains("IPV "))
                        {
                            ReSetValues(IPVSaver);
                            LoadIPV(tabControl1.SelectedTab.Text);
                        }
                        if (tabControl1.SelectedIndex == 0)
                        {
                            ReSetValues(RamalSaver);
                            LoadRamal20();
                        }
                    }

            }
            else{

                    buttons = MessageBoxButtons.YesNo;
                  
                    result = MessageBox.Show(this, "Esta seguro de que desea borrar los datos del " + tabControl1.SelectedTab.Text + " para la Unidad: " + tabControl2.SelectedTab.Text + ", Cuenta: " + comboBox1.Text + ", del dia " + DateCultureConverter(dateTimePicker1.Value) + "?..", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (tabControl1.SelectedTab.Text.Contains("IPV "))
                        {
                            ClearIPV();
                        }
                        if (tabControl1.SelectedIndex == 0)
                        {
                            ClearRamal();
                        }
                        if (tabControl1.SelectedTab.Text == "SubMayor" && comboBox1.Text != "")
                        {
                            ClearSubMayor();
                        }
                        if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                        {
                            CleareResIng();
                        }
                        if (tabControl1.SelectedTab.Text == "Vale de Salida")
                        {
                            ClearValeSalida();
                        }
                        if (tabControl1.SelectedTab.Text == "Ficha de Costo")
                        {
                            ClearFichaCosto();
                        }
                        if (tabControl1.SelectedTab.Text == "Flujo de Caja")
                        {
                            ClearFlujo();
                        }

                    }
            }


        }
        private void ClearFlujo()
        {

            if (dateTimePicker1.Value.ToShortDateString() == dateTimePicker2.Value.ToShortDateString() && dateTimePicker2.Value.ToShortDateString() == dateTimePicker3.Value.ToShortDateString())
            {

                if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[Flujo] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "'"))
                {

                    dbc.SimplePlan("DELETE [MLB].[dbo].[Flujo] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda ='" + GetMoneda() + "'");

                    //for (int w = ResIngBase.Rows.Count - 1; w < ResIngBase.Rows.Count; w++)
                    //{
                }
                FlujoCajaBase.Rows.Clear();
                ReSetValues(FlujoSaver);
                LoadFlujoCaja();
            }
            else
                System.Windows.Forms.MessageBox.Show(this, "Solo puede Borrar Asientos de un solo dia...\nRevise el rango de Tiempo en el Fujo de Caja, y la Fecha");

        }

        private void ramal20Top_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadRamal20();
            }
        }

        private void IPVBase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadResIng();
            }
        }

        private void SubMayorTop_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadSubMayor();
            }
        }

        private void FCostoTop_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                LoadFichaCosto();
            }

        }

        private void ValeSalidaTop_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                LoadValeSalida();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Must be 64 bits, 8 bytes.
            MLB.Imprimir imp = new MLB.Imprimir();

            PreparerRamal2();
            PreparerIPV2();
            PreparerSubMayor2();
            PreparerFichCost2();
            PreparerValeSalida2();
            imp.table = "ALL";
            imp.Show(this);
            
           
        }

        private void tabPage1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void activarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MLB.Encripter enc = new MLB.Encripter();
            //System.Windows.Forms.MessageBox.Show("De la "+System.Environment.MachineName+"\n La clave es: "+enc.EncryptText(System.Environment.MachineName,"WILDWEST"));
            //comboBox1.Text = enc.EncryptText(System.Environment.MachineName, "WILDWEST");

            //MLB.CheckPack enc = new MLB.CheckPack();
            //String clave = enc.GetEncryption(System.Environment.MachineName);
            //System.Windows.Forms.MessageBox.Show("De la " + System.Environment.MachineName + "\n La clave es: " + clave +
            //                                                                                 "\n y desenc es: " + enc.GetBoth(System.Environment.MachineName));
            //comboBox1.Text = clave;

            MLB.ActReq ar = new MLB.ActReq();
            ar.Show(this);

        }

        private void IPVTop_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LoadIPV(tabControl1.SelectedTab.Text.Replace("IPV ", ""));
        }

        private void ramal20Base_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ramal20Base.RowCount > 10)
            {
                System.Drawing.Font fnt = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Regular);

                ramal20Base.Rows[rower].Cells[0].Style.Font = fnt;

                System.Drawing.Font fnt2 = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Bold);

                ramal20Base.Rows[e.RowIndex].Cells[0].Style.Font = fnt2;

                rower = e.RowIndex;
            }


            //ramal20Base.Rows[ramalrow].Cells[0].Style.BackColor = Color.White;
            //ramal20Base.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.WhiteSmoke;
            //ramalrow = e.RowIndex;
            // ramal20Base.Rows[e.RowIndex].DividerHeight = 1;


        }

        private void IPVBase_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (IPVBase.RowCount > 10 && rower < IPVBase.RowCount)
            {
                System.Drawing.Font fnt = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Regular);

                IPVBase.Rows[rower].Cells[0].Style.Font = fnt;

                System.Drawing.Font fnt2 = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Bold);

                IPVBase.Rows[e.RowIndex].Cells[0].Style.Font = fnt2;

                rower = e.RowIndex;
            }
        }

        private void ResIngBase_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (ResIngBase.RowCount > 1)
            {
                System.Drawing.Font fnt = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Regular);

                ResIngBase.Rows[rower].Cells[0].Style.Font = fnt;

                System.Drawing.Font fnt2 = new System.Drawing.Font(ramal20Base.DefaultCellStyle.Font.FontFamily.Name, ramal20Base.DefaultCellStyle.Font.Size, FontStyle.Bold);

                ResIngBase.Rows[e.RowIndex].Cells[0].Style.Font = fnt2;

                rower = e.RowIndex;
            }
        }

        private void FichaCostoBase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == FichaCostoBase.ColumnCount - 1)
            //{
            //    for (int w = 0; w < FichaCostoBase.RowCount; w++)
            //    {
            //        FichaCostoBase.Rows[w].Cells[e.ColumnIndex].ReadOnly = true;

            //    }
            //    if (GetData(FichaCostoBase.Rows[e.RowIndex].Cells[0].Value) == "")
            //        FichaCostoBase.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
            //}
        }
        private int Finder(System.Windows.Forms.DataGridView dtgv, String val, int col, int row, bool que)
        {
            int w = row;


            while (w > -1 && w < dtgv.RowCount && GetData(dtgv.Rows[w].Cells[col].Value.ToString().Replace(" ","")) != val)
            {
                if (que)
                    w++;
                else
                    w--;

            }

            if (que)
                return w - 1;
            else
                return w + 1;

        }
        private String ReCall(System.Windows.Forms.DataGridView dtgv, int col, int desde, int hasta)
        {
            int w = desde;
            double import = 0;
            while (w <= hasta)
            {
                import += System.Convert.ToDouble(GetNumData(dtgv.Rows[w].Cells[col].Value));
                w++;

            }
            return System.Convert.ToString(import);
        }
        private void FichaCostoBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FichaCostoBase.ColumnCount-1)
            {
           
                    int princ = Finder(FichaCostoBase, "-", 3, e.RowIndex, false);
                    int final = Finder(FichaCostoBase, "-", 3, e.RowIndex, true);

                    if (princ > 0 && princ < FichaCostoBase.RowCount - 1 && final > 0 && final < FichaCostoBase.RowCount - 1 && System.Convert.ToInt32(GetNumData(FichaCostoBase.Rows[princ - 1].Cells[1].Value)) > 0)
                    {

                        FichaCostoBase.Rows[final + 1].Cells[FichaCostoBase.ColumnCount - 1].Value = ReCall(FichaCostoBase, FichaCostoBase.ColumnCount - 1, princ, final);

                        if (!EstaSomeoneSaver(FCSaver, FichaCostoBase, e.RowIndex, e.ColumnIndex))
                        {
                            FCSaver.Add(new ValueSaver(System.Convert.ToString(FichaCostoBase.Rows[princ - 1].Cells[4].Value) + " " + System.Convert.ToString(FichaCostoBase.Rows[princ - 1].Cells[5].Value), System.Convert.ToString(FichaCostoBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), e.RowIndex, e.ColumnIndex, tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(FichaCostoBase.Rows[princ - 1].Cells[0].Value) + " (s)", GetMoneda()));
                        }

                        if (!EstaSomeoneSaver(FCSaver, FichaCostoBase, final + 1, System.Convert.ToInt32(GetData(FichaCostoBase.Rows[princ - 1].Cells[1].Value))))
                        {
                            FCSaver.Add(new ValueSaver(System.Convert.ToString(FichaCostoBase.Rows[princ - 1].Cells[4].Value) + " " + System.Convert.ToString(FichaCostoBase.Rows[princ - 1].Cells[5].Value), System.Convert.ToString(FichaCostoBase.Rows[final + 1].Cells[FichaCostoBase.ColumnCount - 1].Value), final + 1, System.Convert.ToInt32(GetData(FichaCostoBase.Rows[princ - 1].Cells[1].Value)), tabControl2.SelectedTab.Text, comboBox1.Text, DateCultureConverter(dateTimePicker1.Value), GetData(FichaCostoBase.Rows[princ - 1].Cells[0].Value), GetMoneda()));
                        }


                       
                    }
            }

            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                FichaCostoBase.Rows[e.RowIndex].Cells[FichaCostoBase.ColumnCount - 1].Value = System.Convert.ToString(System.Math.Round(System.Convert.ToDouble(GetNumData(FichaCostoBase.Rows[e.RowIndex].Cells[5].Value))*System.Convert.ToDouble(GetNumData(FichaCostoBase.Rows[e.RowIndex].Cells[6].Value)),2));
                   
                    int princ = Finder(FichaCostoBase, "-", 3, e.RowIndex, false);
                    int final = Finder(FichaCostoBase, "-", 3, e.RowIndex, true);

                    if (princ > 0 && princ < FichaCostoBase.RowCount - 1 && final > 0 && final < FichaCostoBase.RowCount - 1 && System.Convert.ToInt32(GetNumData(FichaCostoBase.Rows[princ - 1].Cells[1].Value)) > 0)
                    {
                        FichaCostoBase.Rows[final + 1].Cells[FichaCostoBase.ColumnCount - 1].Value = ReCall(FichaCostoBase, FichaCostoBase.ColumnCount - 1, princ, final);
                    }

            }
            if (!timer2.Enabled)
            {
                timer2.Enabled = true;
            }

        }
        private bool EsteTEsta(String prod, String prec, int row)
        {
            foreach (ValueSaver val in AjustesInRamal)
            {
                if (val.producto == prod && val.cant == prec && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    double value = System.Convert.ToDouble(GetNumData(FichaCostoBase.Rows[row].Cells[7].Value));
                    double cant = System.Convert.ToDouble(GetNumData(FichaCostoBase.Rows[row].Cells[5].Value));
                    double precio = System.Convert.ToDouble(GetNumData(FichaCostoBase.Rows[row].Cells[6].Value));
                    //
                    val.col = System.Convert.ToInt32(System.Math.Round((value - (cant * precio)), 2) * 100);
                    return true;
                }
            }
            return false;
        }
        private bool EsteEsta(String prod, String prec)
        {
            foreach (ValueSaver val in Ajustes)
            {
                if (val.producto == prod && val.cant == prec && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {

                    return true;
                }
            }
            return false;
        }
        private void Finisher(String prod, double prec, double cant, double value)
        {

            int w = 0;
            while (GetData(ramal20Base.Rows[w].Cells[0].Value) != " " && GetData(ramal20Base.Rows[w].Cells[0].Value) != "Totales:" && w < ramal20Base.RowCount - 1)
            {
                if (GetData(ramal20Base.Rows[w].Cells[0].Value) == prod && System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[2].Value)) == prec && ramal20Base.Rows[w].Cells[3].ReadOnly)
                {


                    ramal20Base.Rows[w].Cells[8].Value = System.Math.Round(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[8].Value)) + System.Math.Round((value - (cant * prec)), 2), 2);
                    ramal20Base.Rows[w].Cells[10].Value = ramal20Base.Rows[w].Cells[8].Value;
                    break;
                }
                w++;
            }

        }
        private int WhereInIPV(String ipv, String eprod, String cuenta)
        {

            foreach (ValueSaver val in IPVSaver)
            {
                if (val.IPVName == ipv)
                {
                    //&& IPVBase.Rows[val.row].Cells[val.col].Value != null 
                    if (val.cant == eprod && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == cuenta && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                        return val.row;
                }
            }
            return 0;

        }
        private void rePutinFCosto()
        {
            foreach (ValueSaver val in FCSaver)
            {

                if (val.row < FichaCostoBase.RowCount && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    FichaCostoBase.Rows[val.row].Cells[7].Value = System.Convert.ToString(val.cant);


            }
        }
      

        private double ExistsUPrice(String ipv, String eprod, int cant, double uprice)
        {
            foreach (ValueSaver val in FCSaver)
            {
                if (val.producto == eprod && val.col == cant && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {

                    return System.Convert.ToDouble(val.cant);
                }
            }
            return uprice;
        }
        private bool DeleteUPrice(String ipv, String eprod, int cant)
        {
            for (int w = 0; w < FCSaver.Count; w++)
            {
                ValueSaver val = (ValueSaver)FCSaver[w];
                if ((val.producto == eprod) && val.col != cant && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                {
                    FCSaver.RemoveAt(w);
                    DeleteTrail(ipv, eprod);
                    return true;
                }
            }
            return false;
        }

        private void DeleteTrail(String ipv, String eprod)
        {
            for (int w = 0; w < FCSaver.Count; w++)
            {
                ValueSaver val = (ValueSaver)FCSaver[w];
                if ((val.producto == eprod + " (s)") && val.IPVName == ipv && val.UName == tabControl2.SelectedTab.Text && val.Cuenta == comboBox1.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                    FCSaver.RemoveAt(w);
            }
        }

        private void FichaCostoBase_SelectionChanged(object sender, EventArgs e)
        {

        }
        private double GetRango(String eprod, String prod)
        {

            System.Data.DataSet dts88 = dbc.SelectQuerryFixed("SELECT SUM( EProducto.Cantidad), EProducto.PUM, Producto.DUM,EProducto.UM, EProducto.Producto FROM Producto INNER JOIN EProducto ON Nombre = EProducto.Producto WHERE EProducto.NNombre = '" + eprod + "'AND Producto.Nombre = '" + prod + "' GROUP BY EProducto.Producto, EProducto.PUM, Producto.DUM,EProducto.UM");
            if (dts88.Tables[0].Rows.Count > 0)
            {
                double kant = System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[0].ToString()) / System.Convert.ToDouble(dts88.Tables[0].Rows[0].ItemArray[1].ToString());

                kant = UMConverter(kant, dts88.Tables[0].Rows[0].ItemArray[2].ToString(), dts88.Tables[0].Rows[0].ItemArray[3].ToString());
                // IntTransf2();
                return kant;
            }
            return 0;


        }


        private void IPVBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete && IPVBase.SelectedCells.Count > 0 && IPVBase.SelectedCells[0].RowIndex < IPVBase.RowCount - 1)
            {
                IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[3].Value = 0;
                IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[5].Value = 0;
                IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[7].Value = 0;

                if (IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value != null && (System.Convert.ToDouble(GetNumData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[3].Value)) == 0 && System.Convert.ToDouble(GetNumData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[5].Value)) == 0 && System.Convert.ToDouble(GetNumData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[7].Value)) == 0) && (comboBox1.Text == "" || (IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[3].Value != null)))
                {
                    if (!dbc.ExistQuerry("Select Producto From  [UndProd] WHERE [Producto] = '" + GetData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value) + "' AND [UName] = '" + tabControl2.SelectedTab.Text + "'") && (!forzar.Enabled||(forzar.Enabled&&forzar.Checked)))
                    {
                        UnFillRamal(GetData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value));
                        SalverFixer(IPVBase.SelectedCells[0].RowIndex, IPVSaver);
                        if (comboBox1.Text == "")
                        {
                            SalverFixerOnTop(GetData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value), IPVSaver, GetCuenta(GetData(IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value)));
                        }

                        IPVBase.Rows.RemoveAt(IPVBase.SelectedCells[0].RowIndex);
                        //SalverFixer(ramal20Base.SelectedCells[0].RowIndex, RamalSaver);



                    }

                }

            }

            if (e.KeyCode.ToString().Length == 1 && IPVBase.SelectedCells.Count > 0 && IPVBase.SelectedCells[0].RowIndex < IPVBase.RowCount - 1 && IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].Value != null && IPVBase.Rows[IPVBase.SelectedCells[0].RowIndex].Cells[0].ReadOnly)
            {
                int w = IPVBase.SelectedCells[0].RowIndex + 1;
                bool findit = false;

                while (w < IPVBase.RowCount && w != IPVBase.SelectedCells[0].RowIndex && !findit)
                {

                    if (IPVBase.Rows[w].Cells[0].Value == null)
                    {
                        w = 0;
                    }
                    if (e.KeyCode.ToString() == GetData(IPVBase.Rows[w].Cells[0].Value)[0].ToString() || e.KeyCode.ToString().ToLower(cinf) == GetData(IPVBase.Rows[w].Cells[0].Value)[0].ToString())
                    {
                        findit = true;
                        // ramal20Base.SelectedCells.Clear();
                        int k = 0;
                        while (k < IPVBase.RowCount && IPVBase.Rows[k].Cells[0].Value != null)
                        {
                            IPVBase.Rows[k].Cells[0].Selected = false;
                            //ramal20Base.SelectedCells[k].Selected = false;
                            k++;
                        }
                        IPVBase.Rows[w].Cells[0].Selected = true;
                    }
                    w++;
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            bool estuve = false;
            while (failed&&!estuve) // jeje verificando la conexion..
            {
                dbc = new MLB.DBControl();
                failed = false;
                ConexionFix();
                estuve = true;
            }
            if (estuve)
            {
                dbc = new MLB.DBControl();
                LoadRamal20();
            }

            if (!CheckActivation())
            {
                tableLayoutPanel4.Enabled = false;
                vistaToolStripMenuItem.Enabled = false;
                herramientasToolStripMenuItem.Enabled = false;
                editarToolStripMenuItem.Enabled = false;
                acesoToolStripMenuItem.Enabled = false;
                guardarTodoToolStripMenuItem.Enabled = false;
                datosTemporalesToolStripMenuItem.Enabled = false;
                vistaToolStripMenuItem.Enabled = false;
                vistaToolStripMenuItem1.Enabled = false;
                imprimirToolStripMenuItem.Enabled = false;
                asistenteDelEasierToolStripMenuItem.Enabled = false;
                label3.Text = "Sistema No Activado. Por favor Reactívelo...";
            }
            else
            {
                tableLayoutPanel4.Enabled = true;
                vistaToolStripMenuItem.Enabled = true;
                vistaToolStripMenuItem1.Enabled = true;
                herramientasToolStripMenuItem.Enabled = true;
                editarToolStripMenuItem.Enabled = true;
                acesoToolStripMenuItem.Enabled = true;
                guardarTodoToolStripMenuItem.Enabled = true;
                imprimirToolStripMenuItem.Enabled = true;
                datosTemporalesToolStripMenuItem.Enabled = true;
                vistaToolStripMenuItem.Enabled = true;
                asistenteDelEasierToolStripMenuItem.Enabled = true;
                label3.Text = "";
            }
            CreateModuleMenu(sender, e);

        }
        private void CreateModuleMenu(object sender, EventArgs e)
        {

            try
            {
                System.Collections.IEnumerator modules = ModuleSearch();
                int w = 0;
                this.modulosToolStripMenuItem.DropDownItems.Clear();
                while (modules.MoveNext())
                {
                    FileInfo fi = (FileInfo)modules.Current;
                    if (!fi.Name.Contains("sqlexpr32.exe"))
                    {
                        this.modulosToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(fi.Name.Replace(".exe", "")));
                        this.modulosToolStripMenuItem.DropDownItems[w].Click += new System.EventHandler(modulosToolStripMenuItem_Click);
                    }
                    w++;
                }

            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        private System.Collections.IEnumerator ModuleSearch()
        {
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Modules");

            System.Collections.IEnumerator myEnum = di.GetFiles("*.exe").GetEnumerator(); ;

            return myEnum;
        }
        bool CheckActivation()
        {
            int secs = -1;
            System.DateTime expdate = System.DateTime.Today;
            if (System.IO.File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key"))
            {
                MLB.Encripter enc = new MLB.Encripter();
                String fulltextdata = enc.DecryptFile2(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.key", System.Environment.MachineName);

                String date = fulltextdata.Substring(fulltextdata.LastIndexOf(" (w) ") + 5, (fulltextdata.Length) - (fulltextdata.LastIndexOf(" (w) ") + 5));

                expdate = System.Convert.ToDateTime(date);

                secs = System.DateTime.Compare(expdate, System.DateTime.Today);
            }

            if (secs < 0)
            {
                return false;
            }
            return true;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void acercaDeEasierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MLB.AboutEasier ae = new MLB.AboutEasier();
            ae.Show();
            //System.Windows.Forms.MessageBox.Show("Easier 1.0.0.0 Suite El Abuso\n" + "WildWest Company Copyright ® 2013, Killer Productions Inc.\nPinar del Río, Cuba");
        }

        private int FindInt(String Name)
        {
            int place = 0;

            while (place < tabControl1.TabCount && tabControl1.TabPages[place].Text != Name)
                place++;
            return place;
        }

        private bool IstheLast(String IPVName, int row)
        {
            int este = FindInt(IPVName);

            foreach (ValueSaver val in IPVSaver)
            {
                if (val.col == 5 && val.row == row && System.Convert.ToDouble(val.cant) != 0)
                {
                    if (FindInt(val.IPVName) > este)
                    {
                        return false;
                    }
                }
            }

            return true;

        }
        private double IPV_Ramal_Ajustment(String IPVName, double myrslt, double price, int rowr, int rowi)
        {
            //int top = FindInt(IPVName);

            double inramal = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[rowr].Cells[10].Value)); //Importe en ramal20

            if (IstheLast(IPVName, rowi))
            {

                double rsum = 0;
                foreach (ValueSaver val in IPVSaver)
                {
                    if (val.col == 5 && val.row == rowi && System.Convert.ToDouble(val.cant) != 0 && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda() && val.Cuenta == comboBox1.Text)
                    {
                        rsum += System.Math.Round(System.Convert.ToDouble(val.cant) * price, 2);
                    }
                }
                rsum = System.Math.Round(rsum, 2);
                myrslt = System.Math.Round(myrslt + System.Math.Round((inramal - rsum), 2), 2);


                return myrslt;
            }

            return myrslt;

        }

        private void TLRamal20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ramal20Base_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 && ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == true && e.Button == System.Windows.Forms.MouseButtons.Right && ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Totales:" && e.ColumnIndex == 0)
            {

                Control c = sender as Control;


                ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = contextMenuStrip1;

                ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip.Show(c, mouse);


                ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip.ResumeLayout(true);

                ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = null;

                //                this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                //                this.iPVCabaretToolStripMenuItem});
                //                 this.iPVCabaretToolStripMenuItem.Name = "iPVCabaretToolStripMenuItem";
                //                 this.iPVCabaretToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
                //                 this.iPVCabaretToolStripMenuItem.Text = "IPV Cabaret";
                //                      System.Windows.Forms.ToolStripMenuItem enviar
                //                  private System.Windows.Forms.ToolStripMenuItem iPVCabaretToolStripMenuItem;
                //                  this.iPVCabaretToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                // 
                //                  this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
                // 
                //                // ramal20Base.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                //contextMenuStrip1.Visible = true;
                // contextMenuStrip1.Location = contextMenuStrip1.;
                // contextMenuStrip1.Update();
            }
        }

        private void ramal20Base_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
        }

        private void CreateSubmenu()
        {
            // ref System.Windows.Forms.ContextMenuStrip contextMenu  contextMenu.dropdow
            dts = dbc.SelectQuerryFixed("Select Distinct(IPVName) From UnidadIPV Where UName = '" + tabControl2.SelectedTab.Text + "'");


            if (dts.Tables[0].Rows.Count > 0)
            {
                //System.Windows.Forms.ToolStripItem[] ipvsmenu = new System.Windows.Forms.ToolStripItem[] { };
                enviara.DropDownItems.Clear();
                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {

                    enviara.DropDownItems.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), null, EnviaraItem_Click);
                    // System.Windows.Forms.ToolStripMenuItem newitem = new System.Windows.Forms.ToolStripMenuItem(dts.Tables[0].Rows[w].ItemArray[0].ToString(),null,EnviaraItem_Click);
                    //  ipvsmenu.Aggregate(newitem);
                }


            }
        }

        private void EnviaraItem_Click(object sender, EventArgs e)
        {

        }

        private void Send2IPV(String uname, String ipvname, String prod)
        {
            if (prod.Contains("veza"))
            {
                int y = 45;
                y++;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[3].Value.ToString()) > 0 || System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[5].Value.ToString()) > 0) && !dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
            {
                MLB.Descomponer desc = new MLB.Descomponer(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[1].Value.ToString(), ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[2].Value.ToString(), System.Convert.ToString(System.Convert.ToDouble(GetNumData(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[3].Value)) + System.Convert.ToDouble(GetNumData(ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[5].Value))), ramal20Base.Rows[ramal20Base.SelectedCells[0].RowIndex].Cells[4].Value.ToString(), comboBox1.Text, GetMoneda(), tabControl2.SelectedTab.Text, this, dateTimePicker1.Value.ToShortDateString());
                desc.Show(this);
            }

        }
        private double GetFInicio(System.DateTime fecha)
        {
            System.Data.DataSet mdts = dbc.SelectQuerryFixed("Select IInicial From Flujo Where Date = '" + fecha.AddDays(-1).ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'");

            double inicio = 0;
            double ingresos = 0;
            double gastos = 0;
            if (mdts.Tables[0].Rows.Count > 0)
            {
                if (mdts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                    inicio = System.Convert.ToDouble(mdts.Tables[0].Rows[0].ItemArray[0].ToString());
                mdts = dbc.SelectQuerryFixed("Select Sum(Importe)IInicial From Flujo Where Date = '" + fecha.AddDays(-1).ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Tipo = 'Entradas'");
                if (mdts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                    ingresos = System.Convert.ToDouble(mdts.Tables[0].Rows[0].ItemArray[0].ToString());
                mdts = dbc.SelectQuerryFixed("Select Sum(Importe)IInicial From Flujo Where Date = '" + fecha.AddDays(-1).ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Tipo = 'Salidas'");
                if (mdts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                    gastos = System.Convert.ToDouble(mdts.Tables[0].Rows[0].ItemArray[0].ToString());

                return inicio + (ingresos - gastos);
            }

            return 0;

        }
        //Select Concepto.Tipo, Grupo, Concepto.Concepto, Sum(Flujo.Importe) from Flujo Inner Join Concepto on Concepto.Concepto = Flujo.Concepto GROUP BY Concepto.Tipo, Grupo, Concepto.Concepto Order By Tipo
        private void FCEstandar()
        {
             FlujoCajaBase.Rows.Clear();

            if (dbc.ExistQuerry("Select Id From Flujo Where Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'"))
            {

                dts = dbc.SelectQuerryFixed("Select Concepto.Tipo, Grupo, Concepto.Concepto, Sum(Flujo.Importe) From Flujo Inner Join Concepto on Concepto.Concepto = Flujo.Concepto Where Concepto.UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' GROUP BY Concepto.Tipo, Grupo, Concepto.Concepto Order By Tipo");


                double fimp = GetFInicio(dateTimePicker2.Value);

                String fech = dateTimePicker2.Value.ToShortDateString();
                bool gray = false;
                if (dts.Tables[0].Rows.Count > 0)
                {
                    fech = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                textBox1.Text = System.Convert.ToString(fimp);
                double suma = 0;

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {

                    Lookin(dts.Tables[0].Rows[w].ItemArray[2].ToString());

                    if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && !gray)
                    {
                        gray = true;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();

                    }
                    else if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && gray)
                    {
                        gray = false;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                    }



                    //if (dts.Tables[0].Rows[w].ItemArray[6].ToString()!= "")
                    //{
                    //    FlujoCajaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString() + " " + GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString()), dts.Tables[0].Rows[w].ItemArray[3].ToString());
                    //}
                    //else
                    FlujoCajaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString());

                    //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = dts.Tables[0].Rows[w].ItemArray[4].ToString();

//                     if (dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Ingresos por Ventas" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Compra de Mercancias" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Costos de Traslados")
//                     {
//                         FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
//                         FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
//                         FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;
//                     }


//                   /  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
//                     FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
//                     FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    //  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString());

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Value.ToString() == "Entradas")
                    {
                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Value.ToString() == "Salidas")
                    {
                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }


                    if (gray)
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;
                    }

                }

                double final = fimp + suma;
                //FlujoCajaBase.Rows.Add("");
                FlujoCajaBase.Rows.Add("Balance: ", "", "", System.Convert.ToString(System.Math.Round(final - fimp, 2)));

           
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.Moccasin;
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Style.BackColor = Color.Moccasin;
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Style.BackColor = Color.Moccasin;
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].Style.BackColor = Color.Moccasin;
               
                FlujoCajaBase.Rows[0].Cells[0].Selected = false;
                
                balance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                newbalance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                textBox2.Text = System.Convert.ToString(System.Math.Round(final, 2));

                //   FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;


            }
        }
        private void LoadFlujoCaja()
        {
            FlujoCajaBase.Rows.Clear();

            if (dbc.ExistQuerry("Select Id From Flujo Where Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'"))
            {

                dts = dbc.SelectQuerryFixed("Select Date, Tipo, Concepto, Importe, Id, IInicial, SubConcep From Flujo Where UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "'");


                double fimp = GetFInicio(dateTimePicker2.Value);

                String fech = dateTimePicker2.Value.ToShortDateString();
                bool gray = false;
                if (dts.Tables[0].Rows.Count > 0)
                {
                    fech = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                textBox1.Text = System.Convert.ToString(fimp);
                double suma = 0;

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {

                    Lookin(dts.Tables[0].Rows[w].ItemArray[2].ToString());

                    if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && !gray)
                    {
                        gray = true;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();

                    }
                    else if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && gray)
                    {
                        gray = false;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                    }



                    //if (dts.Tables[0].Rows[w].ItemArray[6].ToString()!= "")
                    //{
                    //    FlujoCajaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString() + " " + GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString()), dts.Tables[0].Rows[w].ItemArray[3].ToString());
                    //}
                    //else
                    FlujoCajaBase.Rows.Add(System.Convert.ToDateTime(dts.Tables[0].Rows[w].ItemArray[0].ToString()).ToShortDateString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString());

                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = dts.Tables[0].Rows[w].ItemArray[4].ToString();

                    if (dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Ingresos por Ventas" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Compra de Mercancias" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Costos de Traslados")
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;
                    }


                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    //  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString());

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Entradas")
                    {
                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Salidas")
                    {
                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }


                    if (gray)
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;
                    }

                }
                FlujoCajaBase.Rows[0].Cells[0].Selected = false;
                double final = fimp + suma;
                balance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                newbalance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                textBox2.Text = System.Convert.ToString(System.Math.Round(final, 2));

                //   FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;


            }
            else
            {

                //  double ingHoy = AmountIPV2(cuenta);
                //   double costHoy = AmountIPV(cuenta);

                double fimp = GetFInicio(dateTimePicker2.Value);

                textBox1.Text = System.Convert.ToString(fimp);

                double suma = 0;


                for (int k = 1; k < comboBox1.Items.Count; k++)
                {
                    double cing = AmountIPV2(comboBox1.Items[k].ToString());
                    if (cing > 0)
                    {


                        String cncpt = "Ingresos por Ventas";
                        // String ttt = "Cuenta: " + comboBox1.Items[k].ToString();
                        Lookin(cncpt);
                        cing = System.Math.Round(cing, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Entradas", cncpt, cing);

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        // FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }




                    double cent = ImpEntradas(comboBox1.Items[k].ToString());
                    if (cent > 0)
                    {

                        String cpt = "Compra de Mercancias";
                        Lookin(cpt);
                        cent = System.Math.Round(cent, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Salidas", cpt, cent);

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        //  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());

                    }

                    double itras = ImpTraslados(comboBox1.Items[k].ToString());
                    if (itras > 0)
                    {

                        String cpt = "Costos de Traslados";
                        Lookin(cpt);
                        itras = System.Math.Round(itras, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Salidas", cpt, itras);
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());

                    }



                }

                int w = 0;
                foreach (ValueSaver vs in FlujoSaver)
                {

                    if (vs.UName == tabControl2.SelectedTab.Text && vs.Date == dateTimePicker2.Value.ToShortDateString() && vs.moneda == GetMoneda())
                    {
                        FlujoCajaBase.Rows.Add(vs.Date, vs.cant, vs.Cuenta, vs.producto);
                        vs.IPVName = System.Convert.ToString(FlujoCajaBase.RowCount - 2);
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = System.Convert.ToString(FlujoCajaBase.RowCount - 2);

                        if (vs.cant == "Entradas")
                        {
                            suma += System.Convert.ToDouble(vs.producto);
                        }
                        if (vs.cant == "Salidas")
                        {
                            suma -= System.Convert.ToDouble(vs.producto);
                        }
                    }
                    w++;
                }
                //                  if (FlujoCajaBase.RowCount>1)
                //                  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.White;
                double final = fimp + suma;
                balance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                newbalance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                textBox2.Text = System.Convert.ToString(System.Math.Round(final, 2));
                // LoadResIng();
            }
            FlujoCajaBase.Tag = "Released";
        }
        private void GroupFlujoCaja()
        {
            FlujoCajaBase.Rows.Clear();

            if (dbc.ExistQuerry("Select Id From Flujo Where Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'"))
            {

                dts = dbc.SelectQuerryFixed("Select Distinct(Concepto), SUM(Importe),Tipo From Flujo Where UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' Group by Concepto,Tipo");


                double fimp = GetFInicio(dateTimePicker2.Value);

                String fech = dateTimePicker2.Value.ToShortDateString();
                bool gray = false;
                //if (dts.Tables[0].Rows.Count > 0)
                //{
                //    fech = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                //}

                textBox1.Text = System.Convert.ToString(fimp);
                double suma = 0;

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {

                    Lookin(dts.Tables[0].Rows[w].ItemArray[0].ToString());

                    //if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && !gray)
                    //{
                    //    gray = true;
                    //    fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();

                    //}
                    //else if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && gray)
                    //{
                    //    gray = false;
                    //    fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                    //}



                    //if (dts.Tables[0].Rows[w].ItemArray[6].ToString()!= "")
                    //{
                    //    FlujoCajaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString() + " " + GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString()), dts.Tables[0].Rows[w].ItemArray[3].ToString());
                    //}
                    //else
                    FlujoCajaBase.Rows.Add(dateTimePicker2.Value.ToShortDateString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[0].ToString(),dts.Tables[0].Rows[w].ItemArray[1].ToString());

                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = "0";

                    if (dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Ingresos por Ventas" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Compra de Mercancias" || dts.Tables[0].Rows[w].ItemArray[2].ToString() == "Costos de Traslados")
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;
                    }


                    //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString().Replace("(", "")).Replace(")", "");
                    ////  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].ToolTipText = GetData(dts.Tables[0].Rows[w].ItemArray[6].ToString());

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Entradas")
                    {
                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Salidas")
                    {
                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }


                    if (gray)
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;
                    }

                }
                FlujoCajaBase.Rows[0].Cells[0].Selected = false;
                double final = fimp + suma;
                balance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                newbalance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                textBox2.Text = System.Convert.ToString(System.Math.Round(final, 2));

                //   FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;


            }
            else
            {

                //  double ingHoy = AmountIPV2(cuenta);
                //   double costHoy = AmountIPV(cuenta);

                double fimp = GetFInicio(dateTimePicker2.Value);

                textBox1.Text = System.Convert.ToString(fimp);

                double suma = 0;

                double cing = 0;
                double cent = 0;
                double itras = 0;

                for (int k = 1; k < comboBox1.Items.Count; k++)
                {
                    cing = cing + AmountIPV2(comboBox1.Items[k].ToString());
                    cent = cent + ImpEntradas(comboBox1.Items[k].ToString());
                    itras = itras + ImpTraslados(comboBox1.Items[k].ToString());
                }
                    if (cing > 0)
                    {


                        String cncpt = "Ingresos por Ventas";
                        // String ttt = "Cuenta: " + comboBox1.Items[k].ToString();
                        Lookin(cncpt);
                        cing = System.Math.Round(cing, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Entradas", cncpt, cing);

                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        // FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }


                    if (cent > 0)
                    {

                        String cpt = "Compra de Mercancias";
                        Lookin(cpt);
                        cent = System.Math.Round(cent, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Salidas", cpt, cent);

                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        //  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());

                    }

                   
                    if (itras > 0)
                    {

                        String cpt = "Costos de Traslados";
                        Lookin(cpt);
                        itras = System.Math.Round(itras, 2);
                        FlujoCajaBase.Rows.Add(dateTimePicker1.Value.ToShortDateString(), "Salidas", cpt, itras);

                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();
                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[2].ReadOnly = true;
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].ReadOnly = true;

                        //FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[4].ToolTipText = "Cuenta: " + comboBox1.Items[k].ToString();

                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());

                    }



                

                int w = 0;
                foreach (ValueSaver vs in FlujoSaver)
                {

                    if (vs.UName == tabControl2.SelectedTab.Text && vs.Date == dateTimePicker2.Value.ToShortDateString() && vs.moneda == GetMoneda())
                    {
                        FlujoCajaBase.Rows.Add(vs.Date, vs.cant, vs.Cuenta, vs.producto);
                        vs.IPVName = System.Convert.ToString(FlujoCajaBase.RowCount - 2);
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = System.Convert.ToString(FlujoCajaBase.RowCount - 2);

                        if (vs.cant == "Entradas")
                        {
                            suma += System.Convert.ToDouble(vs.producto);
                        }
                        if (vs.cant == "Salidas")
                        {
                            suma -= System.Convert.ToDouble(vs.producto);
                        }
                    }
                    w++;
                }
                //                  if (FlujoCajaBase.RowCount>1)
                //                  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.White;
                double final = fimp + suma;
                balance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                newbalance.Text = "Balance: " + System.Convert.ToString(System.Math.Round(final - fimp, 2));
                textBox2.Text = System.Convert.ToString(System.Math.Round(final, 2));
                // LoadResIng();
            }
        }
        private void Lookin(String cpt)
        {
            bool est = false;
            for (int w = 0; w < colconcep.Items.Count; w++)
            {
                if (colconcep.Items[w].ToString() == cpt)
                {
                    est = true;
                }
            }

            if (!est)
            {
                colconcep.Items.Add(cpt);
            }
        }
        private bool Filled(int row, DataGridView dtgv)
        {
            bool rslt = true;

            for (int w = 0; w < dtgv.ColumnCount; w++)
                if (GetData(dtgv.Rows[row].Cells[w].Value) == " ")
                    rslt = false;

            return rslt;
        }

        private void FlujoCajaBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == FlujoCajaBase.Rows.Count - 2 && Filled(e.RowIndex, FlujoCajaBase) && FlujoCajaBase.Rows[e.RowIndex].Tag == null)
            {
                //  toolStripStatusLabel2.Text = "Insertando Datos...";

                int w = e.RowIndex;//System.Convert.ToInt32(dbc.SelectQuerryFixed("SELECT COUNT([ID]) FROM Producto").Tables[0].Rows[0].ItemArray[0].ToString());
                //for (int w = cant; w < MainProducto.RowCount - 1; w++)
                //{
                FlujoSaver.Add(new ValueSaver("", FlujoCajaBase.Rows[e.RowIndex].Cells[1].Value.ToString(), 0, 0, tabControl2.SelectedTab.Text, FlujoCajaBase.Rows[e.RowIndex].Cells[2].Value.ToString(), FlujoCajaBase.Rows[e.RowIndex].Cells[0].Value.ToString(), FlujoCajaBase.Rows[e.RowIndex].Cells[3].Value.ToString(), GetMoneda()));
                //}

                //   MessageBox.Show("Insertados " + System.Convert.ToString((MainProducto.RowCount - 1) - cant) + " productos.");

                //  FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 1].Cells[0].Selected = true;
                FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = System.Convert.ToString(FlujoCajaBase.RowCount - 2);

                // toolStripStatusLabel2.Text = System.Convert.ToString(1) + " Datos Insertados...";
            }

            if (e.RowIndex == FlujoCajaBase.Rows.Count - 2 && !Filled(e.RowIndex, FlujoCajaBase) && FlujoCajaBase.Rows[e.RowIndex].Tag == null)
            {
                //  toolStripStatusLabel2.Text = "Insertando Datos...";

                FlujoCajaBase.Rows[e.RowIndex].Cells[0].Value = dateTimePicker1.Value.ToShortDateString();

                // toolStripStatusLabel2.Text = System.Convert.ToString(1) + " Datos Insertados...";
            }
            if (e.ColumnIndex == 2)
            {
                dts = dbc.SelectQuerryFixed("Select Tipo From Concepto Where UName = '" + tabControl2.SelectedTab.Text + "' And Concepto = '" + GetData(FlujoCajaBase.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) + "'");
                if (dts.Tables[0].Rows.Count > 0)
                {
                    FlujoCajaBase.Rows[e.RowIndex].Cells[1].Value = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                }
            }
            if (e.ColumnIndex == 3)
            {
                double sume = 0;

                for (int w = 0; w < FlujoCajaBase.RowCount - 1; w++)
                {
                    if (FlujoCajaBase.Rows[w].Cells[1].Value != null)
                    {
                        if (FlujoCajaBase.Rows[w].Cells[1].Value.ToString() == "Entradas")
                        {
                            sume += System.Convert.ToDouble(FlujoCajaBase.Rows[w].Cells[3].Value);
                        }
                        if (FlujoCajaBase.Rows[w].Cells[1].Value.ToString() == "Salidas")
                        {
                            sume -= System.Convert.ToDouble(FlujoCajaBase.Rows[w].Cells[3].Value);
                        }

                    }

                }

                textBox2.Text = System.Convert.ToString(System.Convert.ToDouble(textBox1.Text) + sume);

                balance.Text = System.Convert.ToString(sume);
                newbalance.Text = balance.Text;

                if (!timer2.Enabled)
                {
                    timer2.Enabled = true;
                }

            }

        }

        private void FlujoCajaBase_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount -2].Cells[3].Value == null)
            //{
            //    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value = "0.00";
            //}
            //   FlujoSaver.Add(new ValueSaver();
        }

        private void FlujoCajaBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete && FlujoCajaBase.SelectedCells.Count > 0 && FlujoCajaBase.SelectedCells[0].RowIndex < FlujoCajaBase.RowCount - 1)
            {
                // toolStripStatusLabel2.Text = " Eliminando...";
                if (FlujoCajaBase.Rows[FlujoCajaBase.SelectedCells[0].RowIndex].Tag != null)
                {
                    int w = 0;
                    foreach (ValueSaver vs in FlujoSaver)
                    {

                        if (vs.IPVName == System.Convert.ToString(FlujoCajaBase.SelectedCells[0].RowIndex) && vs.UName == tabControl2.SelectedTab.Text && vs.Date == dateTimePicker2.Value.ToShortDateString() && vs.moneda == GetMoneda())
                        {
                            break;
                        }
                        w++;
                    }
                    FlujoSaver.RemoveAt(w);
                    FlujoCajaBase.Rows.RemoveAt(FlujoCajaBase.SelectedCells[0].RowIndex);

                }
                // toolStripStatusLabel2.Text = " Eliminado...";
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.CompareTo(dateTimePicker3.Value) > 0)
            {
                dateTimePicker3.Value = dateTimePicker2.Value;
            }
            else
            {
                if (dateTimePicker2.Value.ToShortDateString() != dateTimePicker1.Value.ToShortDateString())
                    LoadFlujoCaja();
            }
        }
        private void RangoFlujo()
        {
            FlujoCajaBase.Rows.Clear();

            if (dbc.ExistQuerry("Select Id From Flujo Where Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'"))
            {

                dts = dbc.SelectQuerryFixed("Select Date, Tipo, Concepto, Importe, Id, IInicial From Flujo Where UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "' And Date Between '" + dateTimePicker2.Value.ToShortDateString() + "' And '" + dateTimePicker3.Value.ToShortDateString() + "' Order by Date ");


                double fimp = GetFInicio(dateTimePicker2.Value);

                textBox1.Text = System.Convert.ToString(fimp);
                double suma = 0;
                String fech = dateTimePicker2.Value.ToShortDateString();
                bool gray = false;
                if (dts.Tables[0].Rows.Count > 0)
                {
                    fech = dts.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {

                    Lookin(dts.Tables[0].Rows[w].ItemArray[2].ToString());
                    if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && !gray)
                    {
                        gray = true;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();

                    }
                    else if (fech != dts.Tables[0].Rows[w].ItemArray[0].ToString() && gray)
                    {
                        gray = false;
                        fech = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                    }

                    FlujoCajaBase.Rows.Add(dts.Tables[0].Rows[w].ItemArray[0].ToString(), dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[2].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString());
                    FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Tag = dts.Tables[0].Rows[w].ItemArray[4].ToString();

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Entradas")
                    {
                        suma += System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }

                    if (FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[1].Value.ToString() == "Salidas")
                    {
                        suma -= System.Convert.ToDouble(FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[3].Value.ToString());
                    }

                    if (gray)
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        FlujoCajaBase.Rows[FlujoCajaBase.RowCount - 2].Cells[0].Style.BackColor = Color.White;
                    }


                }

                double final = fimp + suma;
                balance.Text = "Balance: " + System.Convert.ToString(final - fimp);
                newbalance.Text = "Balance: " + System.Convert.ToString(final - fimp);
                textBox2.Text = System.Convert.ToString(final);


            }
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value.CompareTo(dateTimePicker2.Value) < 0)
            {
                dateTimePicker2.Value = dateTimePicker3.Value;
            }
            else
            {
                if (dateTimePicker3.Value.ToShortDateString() != dateTimePicker2.Value.ToShortDateString())
                    LoadFlujoCaja();
            }
        }
        private double ImpTraslados(String cuenta)
        {
            double itras = 0;

            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT SUM([TImporte]) FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                if (dts.Tables[0].Rows.Count > 0)
                {
                    itras = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                }
            }
            else
            {
                if (RamalSaver != null)
                {

                    foreach (ValueSaver val in RamalSaver)
                    {
                        if (val.col == 14 && val.producto == "Totales:" && val.Cuenta == cuenta && val.IPVName == tabControl1.TabPages[0].Text && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                        {

                            itras += System.Convert.ToDouble(val.cant);
                            break;

                        }
                    }
                }
            }

            return itras;
        }

        private double ImpEntradas(String cuenta)
        {
            double ient = 0;

            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
            {
                //Fill
                dts = dbc.SelectQuerryFixed("SELECT SUM([EImporte]) FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "'  AND Moneda = '" + GetMoneda() + "'");

                if (dts.Tables[0].Rows.Count > 0)
                {
                    ient = System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());
                }
            }
            else
            {



                if (RamalSaver != null)
                {

                    foreach (ValueSaver val in RamalSaver)
                    {
                        if (val.col == 6 && val.producto == "Totales:" && val.Cuenta == cuenta && val.IPVName == tabControl1.TabPages[0].Text && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda())
                        {

                            ient += System.Convert.ToDouble(val.cant);
                            break;

                        }
                    }
                }
            }
            return ient;
        }
        public ArrayList GetEntradas(ArrayList ipal, String cta)
        {
            ArrayList opal = new ArrayList();
            if (RamalSaver != null)
            {

                foreach (ValueSaver val in RamalSaver)
                {
                    if (val.col == 5 && val.Cuenta == cta && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value) && val.moneda == GetMoneda() && IsIn(ipal, val.producto))
                    {

                        opal.Add(val.producto);
                        opal.Add(val.cant);

                    }
                }
            }
            return opal;
        }
        private bool IsIn(ArrayList al, String prod)
        {
            foreach (String val in al)
            {
                if (val == prod)
                {
                    return true;
                }
            }

            return false;
        }
        private void auditarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gráficasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MLB.Estadisticas est = new MLB.Estadisticas(tabControl2.SelectedTab.Text, dateTimePicker1.Value, GetMoneda());
            est.Show(this);
        }

        private void tablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MLB.FCReport fcr = new MLB.FCReport(tabControl2.SelectedTab.Text, dateTimePicker1.Value, GetMoneda());
            fcr.Show(this);
        }



        private void componerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (CheckList() && !dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
            {
                MLB.Componer comp = new MLB.Componer(GetPList(), comboBox1.Text, GetMoneda(), tabControl2.SelectedTab.Text, this, dateTimePicker1.Value.ToShortDateString(),RamalSaver);
                comp.Show(this);
            }

        }
        private ArrayList GetPList()
        {

            ArrayList al = new ArrayList();
            for (int w = 0; w < ramal20Base.SelectedCells.Count; w++)
            {
                if (System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[3].Value.ToString()) > 0 || System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[5].Value.ToString()) > 0 || System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[7].Value.ToString()) > 0)
                    al.Add(new ValueSaver("", "", 0, 0, System.Convert.ToString(System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[3].Value.ToString()) + System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[5].Value.ToString()) + System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[7].Value.ToString())), ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[1].Value.ToString(), ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[2].Value.ToString(), ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[0].Value.ToString(), System.Convert.ToString(System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[4].Value.ToString()) + System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[6].Value.ToString()) + System.Convert.ToDouble(System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[8].Value.ToString())))));
            }
            return al;
        }
        private bool CheckList()
        {
            bool var = true;
            for (int w = 0; w < ramal20Base.SelectedCells.Count; w++)
            {
                if (System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[3].Value.ToString()) <= 0 && System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[5].Value.ToString()) <= 0 && System.Convert.ToDouble(ramal20Base.Rows[ramal20Base.SelectedCells[w].RowIndex].Cells[7].Value.ToString()) <= 0)
                    var = false;
            }
            if (ramal20Base.SelectedCells.Count < 1)
                var = false;

            return var;
        }

        private void contenidoDeAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void validarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (tabControl1.SelectedTab.Text.Contains("IPV "))
            {
                Validar(IPVBase);
            }
            else
                Validar(ramal20Base);
            
            
        }
        private void Validar(DataGridView dtgv)
        {
            dts = dbc.SelectQuerryFixed("SELECT  Producto,UName FROM [MLB].[dbo].[UndProd] group by UName,Producto Having count(UName)>1 ");
            String rslt = "";
            while (dbc.SelectQuerryFixed("SELECT  Producto,UName FROM [MLB].[dbo].[UndProd] group by UName,Producto Having count(UName)>1 ").Tables[0].Rows.Count > 0)
            {


                for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                {
                    System.Data.DataSet dts2 = dbc.SelectQuerryFixed("SELECT TOP(1) Id FROM [MLB].[dbo].[UndProd] Where Producto = '" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "' And UName='" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "' Order by Id Asc");
                    if (dts.Tables[0].Rows.Count > 0)
                    {

                        dbc.SimplePlan("DELETE  FROM [MLB].[dbo].[UndProd] Where  Id = '" + dts2.Tables[0].Rows[0].ItemArray[0].ToString() + "'");
                        rslt = dts.Tables[0].Rows[w].ItemArray[0].ToString() + " - " + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "\n";
                    }
                }
            }

            if (rslt != "")
            {
                System.Windows.Forms.MessageBox.Show(this, "Se Rectificaron los Siguientes Productos: \n" + rslt, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
              


             rslt = "";
            for (int w = 0; w < dtgv.RowCount - 1; w++)
            {
                if (dtgv.Rows[w].Cells[0].Value == null)
                {
                    break;
                }

                if (dtgv.Rows[w].Cells[dtgv.ColumnCount - 2].Value != null&& System.Convert.ToDouble(GetNumData(dtgv.Rows[w].Cells[dtgv.ColumnCount - 2].Value)) < 0)
                {
                    rslt = rslt + GetData(dtgv.Rows[w].Cells[0].Value) + "\n";
                }

            }
            if (rslt != "")
            {
                //MessageBoxButtons buttons = MessageBoxButtons.Yes;
                System.Windows.Forms.MessageBox.Show(this, "Los Siguientes productos tienen una cantidad final menor que 0:\n" + rslt + "Se recomienda revisar estas incoherencias antes de Guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }

            if (rslt == "")
            System.Windows.Forms.MessageBox.Show(this, "Validación realizada. 0 errores encontrados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private bool CValidar(DataGridView dtgv)
        {
            String rslt = "";
                 for (int w=0;w<dtgv.RowCount-1;w++)
                 {
                     if (dtgv.Rows[w].Cells[0].Value == null || dtgv.Rows[w].Cells[dtgv.ColumnCount-2].Value==null)
                     {
                         break;
                     }

                     if (System.Convert.ToDouble(GetNumData(dtgv.Rows[w].Cells[dtgv.ColumnCount - 2].Value))<0)
                     {
                         rslt = rslt + GetData(dtgv.Rows[w].Cells[0].Value) + "\n";
                     }

                 }
                 if (rslt != "")
                 {
                     buttons = MessageBoxButtons.YesNo;
                     //System.Windows.Forms.DialogResult result;


                     // quit logo 
                     //loaded=true;
                     // Displays the MessageBox.
                     //connected=false;
                     result = MessageBox.Show(this, "Los Siguientes productos tienen una cantidad final menor que 0:\n" + rslt + "Se recomienda revisar estas incoherencias antes de Guardar.\n Desea continuar de todas formas?", "Advertencia", buttons, MessageBoxIcon.Warning);
                     if (result == DialogResult.Yes)
                     {
                         return true;

                     }
                     return false;
                 }
                return true;
           
        }
       
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // buttons = MessageBoxButtons.YesNo;
            //System.Windows.Forms.DialogResult result;


            // quit logo 
            //loaded=true;
            // Displays the MessageBox.
            //connected=false;
           // result = MessageBox.Show(this, "Esta seguro de que ha Guardado todos los cambios antes Cerrar?...", "Confirmación", buttons, MessageBoxIcon.Question);
           // if (result == DialogResult.Yes)
           // {
           //     Close();
           // }

            
            if (salir == false)
            {
                MLB.Salir s = new MLB.Salir(OnSalir(), this);
                s.ShowDialog(this);
               // Close();
            }

        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (coToolStripMenuItem.Checked)
            {
                coToolStripMenuItem.Checked = false;
                coToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else
            {
                coToolStripMenuItem.Checked = true;
                coToolStripMenuItem.CheckState = CheckState.Checked;
            }

        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (coToolStripMenuItem.Checked)
                {
                    CopyPaste.Clear();
                    CopyPaste.Add(new ValueSaver("All", "", 0, 0, tabControl2.SelectedTab.Text, comboBox1.Text, dateTimePicker1.Value.ToShortDateString(), "", GetMoneda()));
                    
                }
                else
                {
                    CopyPaste.Clear();
                    CopyPaste.Add(new ValueSaver(tabControl1.SelectedTab.Text, "", 0, 0, tabControl2.SelectedTab.Text, comboBox1.Text, dateTimePicker1.Value.ToShortDateString(), "", GetMoneda()));
                    
                }

            }

        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (CopyPaste.Count>0)
                {
                    ValueSaver d2c = (CopyPaste[0]) as ValueSaver;
                    //d2c.IPVName = "All";
                    if (d2c.IPVName == "All" &&d2c.UName == tabControl2.SelectedTab.Text && d2c.Cuenta == comboBox1.Text && d2c.moneda == GetMoneda())
                    {
                       System.Data.DataSet dtsw = dbc.SelectQuerryFixed("SELECT IPVName FROM UnidadIPV WHERE UName = '" + tabControl2.SelectedTab.Text + "'");

                        for (int w = 1; w < comboBox1.Items.Count; w++)
                        {

                            PasteRamal20(d2c, "Ramal20",comboBox1.Items[w].ToString());

                            if (dtsw.Tables.Count > 0 && dtsw.Tables[0].Rows.Count > 0)
                            {
                                for (int k = 0; k < dtsw.Tables[0].Rows.Count; k++)
                                {
                                    // d2c.IPVName = dts.Tables[0].Rows[w].ItemArray[0].ToString();
                                    PasteIPV(d2c, dtsw.Tables[0].Rows[k].ItemArray[0].ToString(), comboBox1.Items[w].ToString());
                                }
                            }

                            // d2c.IPVName = "All";

                            PasteSubMayor(d2c, "SubMayor", comboBox1.Items[w].ToString());
                        }
                         PasteResIng(d2c,"Resumen de Ingresos");
                      //  PasteResIng(d2c);
                    } 
                    else
                    {
                        if (d2c.IPVName == tabControl1.SelectedTab.Text && d2c.UName == tabControl2.SelectedTab.Text && d2c.Cuenta == comboBox1.Text && d2c.moneda == GetMoneda())
                        {
                            if (tabControl1.SelectedTab.Text.Contains("IPV "))
                            {
                                PasteIPV(d2c, tabControl1.SelectedTab.Text.Replace("IPV ", ""), comboBox1.Text);
                               
                            }
                            if (tabControl1.SelectedIndex==0)
                            {
                                PasteRamal20(d2c,"Ramal20",comboBox1.Text);

                            }

                            if (tabControl1.SelectedTab.Text == "SubMayor")
                            {

                                PasteSubMayor(d2c, tabControl1.SelectedTab.Text,comboBox1.Text);

               
                            }
                            if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                            {
                                PasteResIng(d2c, tabControl1.SelectedTab.Text);

                            }
                         }
                    }
                    if (tabControl1.SelectedTab.Text.Contains("IPV "))
                    {
                        
                        LoadIPV(tabControl1.SelectedTab.Text.Replace("IPV ", ""));
                        
                    }
                    if (tabControl1.SelectedIndex == 0)
                    {
                       
                        LoadRamal20();
                        
                    }
                    if (tabControl1.SelectedTab.Text == "SubMayor" && comboBox1.Text != "")
                    {
                       
                        LoadSubMayor();
                       
                    }
                    if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                    {
                        
                        LoadResIng();
                        
                    }
                    if (tabControl1.SelectedTab.Text == "Ficha de Costo")
                    {
                  
                        LoadFichaCosto();
                        
                        if (FichaCostoBase.RowCount > 0)
                        {
                            tabControl1.SelectedTab.BackColor = Color.LightGray;
                        }
                    }

                    if (tabControl1.SelectedTab.Text == "Vale de Salida")
                    {
                       
                        LoadValeSalida();
                        //if (ValeSalidaBase.RowCount > 0)
                        //{
                        //    tabControl1.SelectedTab.BackColor = Color.LightGray;
                        //}
                    }

                    if (tabControl1.SelectedTab.Text == "Flujo de Caja")
                    {
                        LoadFlujoCaja();
                        if (FlujoCajaBase.RowCount > 1)
                        {
                            tabControl1.SelectedTab.BackColor = Color.LightGray;
                        }
                        else
                        {
                            tabControl1.SelectedTab.BackColor = Color.Transparent;

                        }
                    }
                }
               

            }
        }

        private void PasteRamal20(ValueSaver d2c, String tabName, String cuenta)
        {
            if (dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + d2c.Date + "' AND  Moneda = '" + GetMoneda() + "'"))
            {

                if (!dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "'"))
                {

                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[FCantidad],[FImporte],[Moneda] FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "' Order by [Id]");

                    godeep = false;
                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Ramal20]([Id],[UName],[Cuenta],[Date],[Producto]" +
                        ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad]" +
                        ",[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Moneda])" +
                        " VALUES ('" + dbc.MaxQuerry("Ramal20") + "','" + tabControl2.SelectedTab.Text + "','" + cuenta + "','" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "','" +
                         dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                         "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() +
                         "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "')");
                    }
                   // LoadRamal20();
                }
                else
                {

                    buttons = MessageBoxButtons.YesNo;

                    result = MessageBox.Show(this, "Esta Accion Sobreescribira los datos del " + tabName + " existentes del dia anterior.\n Esta seguro de que desea Continuar?...", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dbc.SimplePlan("Delete Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + System.Convert.ToDateTime(d2c.Date).AddDays(-1).ToShortDateString() + "' AND Moneda = '" + GetMoneda() + "'");

                        dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[Precio],[FCantidad],[FImporte],[Moneda] FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "' Order by [Id]");

                        godeep = false;
                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            dbc.SimplePlan("INSERT INTO [MLB].[dbo].[Ramal20]([Id],[UName],[Cuenta],[Date],[Producto]" +
                            ",[UM],[Precio],[ICantidad],[IImporte],[ECantidad],[EImporte],[EICantidad],[EIImporte],[SCantidad]" +
                            ",[SImporte],[SICantidad],[SIImporte],[TCantidad],[TImporte],[FPrecio],[FCantidad],[FImporte],[Moneda])" +
                            " VALUES ('" + dbc.MaxQuerry("Ramal20") + "','" + tabControl2.SelectedTab.Text + "','" + cuenta + "','" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "','" +
                             dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                             "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() +
                             "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[3].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[4].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "')");
                        }
                   //     LoadRamal20();
                    }
                }
            }
        }
        private void PasteSubMayor(ValueSaver d2c, String tabName, String cuenta)
        {
            if (dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "'"))
            {
                if (!dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND RDate='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT [SaldoFinal],[CompRamal] FROM [MLB].[dbo].[SubMayor] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "'");


                    dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[SubMayor]([Id],[UName],[Cuenta],[MDate],[Day]" +
                 " ,[Month],[SaldoInicial],[Entrada],[EntInt],[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[RDate],[Moneda])" +
                 "VALUES ('" + dbc.MaxQuerry("SubMayor") + "','" + tabControl2.SelectedTab.Text + "','" + cuenta + "'" +
                   ",'" + dateTimePicker1.Value.AddDays(-1).Month.ToString() + dateTimePicker1.Value.AddDays(-1).Year.ToString() + "','" + dateTimePicker1.Value.AddDays(-1).Day.ToString() + "','" + MonthConverter(dateTimePicker1.Value.AddDays(-1).Month) + "'" +
                   ",'" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                   "0.00" + "','" + "0.00" + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[0].ItemArray[1].ToString() + "'" +
                   ",'" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "','" + GetMoneda() + "')");

                    //LoadSubMayor();
                }
                else
                {
                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Esta Accion Sobreescribira los datos del " + tabName + " existentes del dia anterior.\n Esta seguro de que desea Continuar?...", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dbc.SimplePlan("DElETE SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND RDate='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "'  AND Moneda = '" + GetMoneda() + "'");

                        dts = dbc.SelectQuerryFixed("SELECT [SaldoFinal],[CompRamal] FROM [MLB].[dbo].[SubMayor] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "' AND Moneda = '" + GetMoneda() + "'");


                        dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[SubMayor]([Id],[UName],[Cuenta],[MDate],[Day]" +
                     " ,[Month],[SaldoInicial],[Entrada],[EntInt],[Salida],[SalInt],[Traslado],[SaldoFinal],[CompRamal],[RDate],[Moneda])" +
                     "VALUES ('" + dbc.MaxQuerry("SubMayor") + "','" + tabControl2.SelectedTab.Text + "','" + cuenta + "'" +
                       ",'" + dateTimePicker1.Value.AddDays(-1).Month.ToString() + dateTimePicker1.Value.AddDays(-1).Year.ToString() + "','" + dateTimePicker1.Value.AddDays(-1).Day.ToString() + "','" + MonthConverter(dateTimePicker1.Value.AddDays(-1).Month) + "'" +
                       ",'" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                       "0.00" + "','" + "0.00" + "','" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[0].ItemArray[1].ToString() + "'" +
                       ",'" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "','" + GetMoneda() + "')");
                       // LoadSubMayor();
                    }
                }
            }
        }
        private void PasteResIng(ValueSaver d2c,String tabName)
        {
            if (dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "' AND Moneda ='" + GetMoneda() + "'"))
            {
                if (!dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND RDate='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "' AND Moneda ='" + GetMoneda() + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT [Cuenta],[InHastaHoy],[CostHastaHoy] FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "' AND Moneda ='" + GetMoneda() + "'");

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[ResIng]([Id],[UName],[Cuenta],[MDate],[Day]" +
                       " ,[Month],[InHoy],[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[RDate],[Moneda])" +

                     "VALUES ('" + dbc.MaxQuerry("ResIng") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "'" +
                     ",'" + dateTimePicker1.Value.AddDays(-1).Month.ToString() + dateTimePicker1.Value.AddDays(-1).Year.ToString() + "','" + dateTimePicker1.Value.AddDays(-1).Day.ToString() + "','" + MonthConverter(dateTimePicker1.Value.AddDays(-1).Month) + "'" +
                     ",'" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
                     ",'" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "','" + GetMoneda() + "')");



                    }
                    //LoadResIng();

                }
                else
                {

                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Esta Accion Sobreescribira los datos del " + tabName + " existentes del dia anterior.\n Esta seguro de que desea Continuar?...", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dbc.SimplePlan("Delete ResIng WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND RDate='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "' AND Moneda ='" + GetMoneda() + "'");

                        dts = dbc.SelectQuerryFixed("SELECT [Cuenta],[IngAcum],[CostAcum] FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND RDate='" + System.Convert.ToDateTime(d2c.Date).ToShortDateString() + "' AND Moneda ='" + GetMoneda() + "'");

                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            dbc.SimplePlan(" INSERT INTO [MLB].[dbo].[ResIng]([Id],[UName],[Cuenta],[MDate],[Day]" +
                           " ,[Month],[InHoy],[InHastaHoy],[CostHoy],[CostHastaHoy],[IngAcum],[CostAcum],[RDate],[Moneda])" +

                         "VALUES ('" + dbc.MaxQuerry("ResIng") + "','" + tabControl2.SelectedTab.Text + "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "'" +
                         ",'" + dateTimePicker1.Value.AddDays(-1).Month.ToString() + dateTimePicker1.Value.AddDays(-1).Year.ToString() + "','" + dateTimePicker1.Value.AddDays(-1).Day.ToString() + "','" + MonthConverter(dateTimePicker1.Value.AddDays(-1).Month) + "'" +
                         ",'" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "'" +
                         ",'" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "','" + GetMoneda() + "')");



                        }
                       // LoadResIng();
                    }
                }

            }
        }
        private void PasteIPV(ValueSaver d2c, String tabName, String cuenta)
        {
            if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + d2c.Date + "' AND IPVName = '" + tabName + "'  AND Moneda = '" + GetMoneda() + "'"))
            {

                if (!dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "' AND IPVName = '" + d2c.IPVName.Replace("IPV ", "") + "'  AND Moneda = '" + GetMoneda() + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[VPrice],[CUnitario]" +
                    ",[FCantidad],[FCosto],[Moneda] FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + d2c.Date + "' AND IPVName = '" + tabName + "' AND Moneda = '" + GetMoneda() + "'");

                    //godeep2 = false;

                    for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                    {
                        dbc.SimplePlan("INSERT INTO [MLB].[dbo].[IPV] ([Id],[UName],[IPVName],[Cuenta]" +
                        ",[Date],[Producto],[UM],[VPrice],[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad]" +
                       ",[VIngreso],[CUnitario],[CVendido],[FCantidad],[FCosto],[Moneda])VALUES " +
                       "('" + dbc.MaxQuerry("IPV") + "','" + tabControl2.SelectedTab.Text + "','" + tabName.Replace("IPV ", "") + "','" + cuenta + "','" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() +
                       "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                           "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + "0.00" + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[6].ToString() + "')");


                    }
                   // LoadIPV(d2c.IPVName);
                }
                else
                {

                    buttons = MessageBoxButtons.YesNo;
                    result = MessageBox.Show(this, "Esta Accion Sobreescribira los datos del " + tabName + " existentes del dia anterior.\n Esta seguro de que desea Continuar?...", "Advertencia", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dbc.SimplePlan("Delete IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + System.Convert.ToDateTime(d2c.Date).AddDays(-1).ToShortDateString() + "'  AND IPVName = '" + tabName + "' AND Moneda = '" + GetMoneda() + "'");

                        dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],[VPrice],[CUnitario]" +
                    ",[FCantidad],[FCosto],[Moneda] FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + d2c.Date + "' AND IPVName = '" + tabName + "' AND Moneda = '" + GetMoneda() + "'");

                    
                        //godeep2 = false;


                        for (int w = 0; w < dts.Tables[0].Rows.Count; w++)
                        {
                            dbc.SimplePlan("INSERT INTO [MLB].[dbo].[IPV] ([Id],[UName],[IPVName],[Cuenta]" +
                            ",[Date],[Producto],[UM],[VPrice],[ICantidad],[ICosto],[ECantidad],[EImporte],[VCantidad]" +
                           ",[VIngreso],[CUnitario],[CVendido],[FCantidad],[FCosto],[Moneda])VALUES " +
                           "('" + dbc.MaxQuerry("IPV") + "','" + tabControl2.SelectedTab.Text + "','" + tabName.Replace("IPV ", "") + "','" + cuenta + "','" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() +
                           "','" + dts.Tables[0].Rows[w].ItemArray[0].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[1].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[2].ToString() + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" +
                               "0.00" + "','" + dts.Tables[0].Rows[w].ItemArray[3].ToString() + "','" + "0.00" + "','" + CantFixer3(dts.Tables[0].Rows[w].ItemArray[1].ToString(), dts.Tables[0].Rows[w].ItemArray[4].ToString()) + "','" + dts.Tables[0].Rows[w].ItemArray[5].ToString() + "','" + dts.Tables[0].Rows[w].ItemArray[13].ToString() + "')");


                        }
                        //LoadIPV(d2c.IPVName);
                    }
                }
            }
        }

        private void ramal20Top_Resize(object sender, EventArgs e)
        {
            
        }

        private void IPVTop_Resize(object sender, EventArgs e)
        {
          //  IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;
        
        }

        private void FCostoTop_Resize(object sender, EventArgs e)
        {
           // FCostoTop.Columns[0].Width = FichaCostoBase.Columns[0].Width + FichaCostoBase.Columns[1].Width + FichaCostoBase.Columns[2].Width;
        
        }

        private void ramal20Base_Resize(object sender, EventArgs e)
        {
            ramal20Top.Columns[0].Width = ramal20Base.Columns[0].Width + ramal20Base.Columns[1].Width + ramal20Base.Columns[2].Width;
        }

        private void IPVBase_Resize(object sender, EventArgs e)
        {
            IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;
        
        }

        private void FichaCostoBase_Resize(object sender, EventArgs e)
        {

         FCostoTop.Columns[0].Width = FichaCostoBase.Columns[0].Width + FichaCostoBase.Columns[1].Width;
        
        }

        private String OnSalir()
        {
            String rslt = "";

            for (int w = 1; w < comboBox1.Items.Count; w++)
            {
                if (!dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Items[w].ToString() + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'"))
                {
                    rslt = rslt + "Cuenta: " + comboBox1.Items[w].ToString() + " - Ramal20\n";
                }

                dts = dbc.SelectQuerryFixed("SELECT IPVName FROM UnidadIPV WHERE UName = '" + tabControl2.SelectedTab.Text + "'");

                if (dts.Tables.Count > 0 && dts.Tables[0].Rows.Count > 0)
                {
                    // tabControl1.TabPages[1].Text = "IPV " + dts.Tables[0].Rows[0].ItemArray[0].ToString();

                    //  int itab = 2;

                    for (int k = 0; k < dts.Tables[0].Rows.Count; k++)
                    {
                        if (!dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Items[w].ToString() + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + dts.Tables[0].Rows[k].ItemArray[0].ToString() + "'  AND Moneda = '" + GetMoneda() + "'"))
                        {
                            rslt = rslt + "Cuenta: " + comboBox1.Items[w].ToString() + " - IPV " + dts.Tables[0].Rows[k].ItemArray[0].ToString() + "\n";
                        }

                    }
                }

                if (!dbc.ExistQuerry("SELECT Id FROM SubMayor WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Items[w].ToString() + "' AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND [Day] = '"+dateTimePicker1.Value.Day.ToString()+"' AND Moneda = '" + GetMoneda() + "'"))
                {
                    rslt = rslt + "Cuenta: " + comboBox1.Items[w].ToString() + " - SubMayor\n";
                }

                

                 if (!dbc.ExistQuerry("Select[Id]FROM FichaCosto WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Items[w].ToString() + "'"))
                 {
                     rslt = rslt + "Cuenta: " + comboBox1.Items[w].ToString() + " - Ficha de Costo\n";
                 }
                 if (!dbc.ExistQuerry("Select[Id]FROM ValeSalida WHERE [UName]='" + tabControl2.SelectedTab.Text + "' AND [Date]= '" + DateCultureConverter(dateTimePicker1.Value) + "' AND [Moneda]='" + GetMoneda() + "' AND Cuenta = '" + comboBox1.Items[w].ToString() + "'"))
                 {
                     rslt = rslt + "Cuenta: " + comboBox1.Items[w].ToString() + " - Vale de Salida\n";
                 }

               
            }
            if (!dbc.ExistQuerry("SELECT Id FROM [MLB].[dbo].[ResIng] WHERE UName ='" + tabControl2.SelectedTab.Text + "'  AND MDate='" + DateConverter(dateTimePicker1.Value) + "' AND Day ='" + dateTimePicker1.Value.Day.ToString() + "' AND Moneda ='" + GetMoneda() + "'"))
            {
                rslt = rslt + "- Resumen de Ingresos\n";
            }
            if (!dbc.ExistQuerry("Select Id From Flujo Where Date = '" + dateTimePicker1.Value.ToShortDateString() + "' And UName = '" + tabControl2.SelectedTab.Text + "' And Moneda = '" + GetMoneda() + "'") && FlujoCajaBase.RowCount > 1)
            {
                rslt = rslt + "- Flujo de Caja\n";
            }
            return rslt;
        }

        private void TLRamal20_Scroll(object sender, ScrollEventArgs e)
        {
            //ramal20Top.Width = ramal20Base.Width;
            //ramal20Top.Columns[0].Width = ramal20Base.Columns[0].Width + ramal20Base.Columns[1].Width + ramal20Base.Columns[2].Width;

            //ramal20Top.Columns[ramal20Top.ColumnCount-1].Width = ramal20Base.Columns[15].Width + ramal20Base.Columns[16].Width + ramal20Base.Columns[17].Width -10;
           
            //if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll && (e.OldValue < e.NewValue))
            //{
            //    ramal20Base.Columns[ramal20Base.ColumnCount - 1].Width = 70;
            //}
            //else
            //{
            //    ramal20Base.Columns[ramal20Base.ColumnCount - 1].Width = 60;
            //}
        }

        private void TLIPV_Scroll(object sender, ScrollEventArgs e)
        {
            IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;
        
        }
        private void BigValidarRamal()
        {
             //dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],Producto.[PrecIn] ,[FCantidad],[FImporte]FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Not Like '%(Defensa)%' ORDER BY Ramal20.Id");

             System.Data.DataSet mydts = dbc.SelectQuerryFixed("SELECT Sum([FImporte])FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "'");
             System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT Sum([FImporte])FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "'");

            if (mydts.Tables[0].Rows.Count>0&&myotherdts.Tables[0].Rows.Count>0)
            {
                double ayer = System.Convert.ToDouble(GetNumData(myotherdts.Tables[0].Rows[0].ItemArray[0].ToString()));
                double hoy = System.Convert.ToDouble(GetNumData(mydts.Tables[0].Rows[0].ItemArray[0].ToString()));
                if (ayer!= hoy)
                {
                    
                    mydts = dbc.SelectQuerryFixed("SELECT Producto FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' And Ramal20.Id Not In (SELECT Ramal20.Id FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "')");

                    String p = "";
                    for (int w = 0; w < mydts.Tables[0].Rows.Count;w++ )
                    {
                        p = p + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + "\n";
                    }
                    if (p != "")
                        System.Windows.Forms.MessageBox.Show(this,"Los Siguientes Productos fueron Borrados de las Bases de Datos del Sistema: \n" + p + "Estas incoherencias descuaran el sistema. Corríja este Error, antes de contabizar el dia.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

        }

        private String BigValidarRamal2()
        {
            //dts = dbc.SelectQuerryFixed("SELECT [Producto],[UM],Producto.[PrecIn] ,[FCantidad],[FImporte]FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' AND Producto.Cuenta = Ramal20.Cuenta  AND Ramal20.Producto Not Like '%(Defensa)%' ORDER BY Ramal20.Id");

            System.Data.DataSet mydts = dbc.SelectQuerryFixed("SELECT Sum([FImporte])FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre INNER JOIN UndProd ON UndProd.Producto = Producto.Nombre WHERE Ramal20.UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "'");
            System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT Sum([FImporte])FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "'");

            if (mydts.Tables[0].Rows.Count > 0 && myotherdts.Tables[0].Rows.Count > 0)
            {
                double ayer = System.Convert.ToDouble(GetNumData(myotherdts.Tables[0].Rows[0].ItemArray[0].ToString()));
                double hoy = System.Convert.ToDouble(GetNumData(mydts.Tables[0].Rows[0].ItemArray[0].ToString()));
                if (ayer != hoy)
                {

                    mydts = dbc.SelectQuerryFixed("SELECT Producto FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "' And Ramal20.Id Not In (SELECT Ramal20.Id FROM Ramal20 INNER JOIN Producto ON Ramal20.Producto = Producto.Nombre INNER JOIN UndProd ON UndProd.Producto = Producto.Nombre WHERE Ramal20.UName ='" + tabControl2.SelectedTab.Text + "' AND  Ramal20.Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Ramal20.Moneda = '" + GetMoneda() + "')");

                    String p = "";
                    for (int w = 0; w < mydts.Tables[0].Rows.Count; w++)
                    {
                        p = p + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + "\n";
                    }
                    if (p != "")
                        return p;//System.Windows.Forms.MessageBox.Show(this, "Los Siguientes Productos fueron Borrados de las Bases de Datos del Sistema: \n" + p + "Estas incoherencias descuaran el sistema. Corríja este Error, antes de contabizar el dia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return "";
        }


        private void BigValidarIPV()
        {

            System.Data.DataSet mydts = dbc.SelectQuerryFixed("SELECT Sum([FCosto])FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 AND Producto.Cuenta = IPV.Cuenta");
            System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT Sum([FCosto])FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 ");
            String p = "";
            if (mydts.Tables[0].Rows.Count > 0 && myotherdts.Tables[0].Rows.Count > 0)
            {
                double ayer = System.Convert.ToDouble(GetNumData(myotherdts.Tables[0].Rows[0].ItemArray[0].ToString()));
                double hoy = System.Convert.ToDouble(GetNumData(mydts.Tables[0].Rows[0].ItemArray[0].ToString()));
                if (ayer != hoy)
                {

                    mydts = dbc.SelectQuerryFixed("SELECT Producto FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0  And IPV.Id Not In (Select IPV.Id FROM [MLB].[dbo].[IPV]INNER JOIN Producto ON IPV.Producto = Producto.Nombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 AND Producto.Cuenta = IPV.Cuenta)");

                 
                    for (int w = 0; w < mydts.Tables[0].Rows.Count; w++)
                    {
                        p = p + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + "\n";
                    }
                    
                }
            }
            mydts = dbc.SelectQuerryFixed("SELECT Sum([FCosto])FROM [MLB].[dbo].[IPV] INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 ");
            myotherdts = dbc.SelectQuerryFixed("SELECT Sum([FCosto])FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 ");
           
            if (mydts.Tables[0].Rows.Count > 0 && myotherdts.Tables[0].Rows.Count > 0)
            {
                double ayer = System.Convert.ToDouble(GetNumData(myotherdts.Tables[0].Rows[0].ItemArray[0].ToString()));
                double hoy = System.Convert.ToDouble(GetNumData(mydts.Tables[0].Rows[0].ItemArray[0].ToString()));
                if (ayer != hoy)
                {

                    mydts = dbc.SelectQuerryFixed("SELECT Producto FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0  And IPV.Id Not In (Select IPV.Id FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 )");


                    for (int w = 0; w < mydts.Tables[0].Rows.Count; w++)
                    {
                        p = p + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + "\n";
                    }
                    
                }
            }

            if (p != "")
                System.Windows.Forms.MessageBox.Show(this, "Los Siguientes fueron Borrados de las Bases de Datos del Sistema: \n" + p + "Estas incoherencias descuaran el sistema. Corríja este Error, antes de contabizar el dia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
          //  System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT distinct(IPV.Producto),IPV.UM,EProducto.Precio,[FCantidad],[CUnitario],[FCosto] FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "'");

        }
        private String BigValidarIPV2(String ipv)
        {

            System.Data.DataSet mydts = dbc.SelectQuerryFixed("SELECT Distinct (EProducto.NNombre), [FCosto] FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 group by EProducto.NNombre, [FCosto]");
            System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT Sum([FCosto])FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" +ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0 ");
            String p = "";
            if (mydts.Tables[0].Rows.Count > 0 && myotherdts.Tables[0].Rows.Count > 0)
            {
                double ayer = System.Convert.ToDouble(GetNumData(myotherdts.Tables[0].Rows[0].ItemArray[0].ToString()));
                double hoy = 0;
                for (int w = 0; w < mydts.Tables[0].Rows.Count; w++ )
                {
                    hoy = hoy + System.Convert.ToDouble(GetNumData(mydts.Tables[0].Rows[w].ItemArray[1].ToString()));
                }
                hoy = System.Math.Round(hoy, 2);
                if (ayer != hoy)
                {

                    mydts = dbc.SelectQuerryFixed("SELECT Producto FROM [MLB].[dbo].[IPV] WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0  And IPV.Id Not In (Select IPV.Id FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + ipv + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND IPV.Moneda = '" + GetMoneda() + "'  AND IPV.FCantidad !=0)");


                    for (int w = 0; w < mydts.Tables[0].Rows.Count; w++)
                    {
                        p = p + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + "\n";
                    }

                }
            }
          

            if (p != "")
                return p;
               // System.Windows.Forms.MessageBox.Show(this, "Los Siguientes fueron Borrados de las Bases de Datos del Sistema: \n" + p + "Estas incoherencias descuaran el sistema. Corríja este Error, antes de contabizar el dia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //  System.Data.DataSet myotherdts = dbc.SelectQuerryFixed("SELECT distinct(IPV.Producto),IPV.UM,EProducto.Precio,[FCantidad],[CUnitario],[FCosto] FROM [MLB].[dbo].[IPV]INNER JOIN EProducto ON IPV.Producto = EProducto.NNombre WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  IPV.Cuenta='" + comboBox1.Text + "'  AND  IPVName='" + tabControl1.SelectedTab.Text.Replace("IPV ", "") + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPV.FCantidad !=0 AND IPV.Moneda = '" + GetMoneda() + "'");
            return "";
        }
        private void temasDeAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName)+ "\\Help\\index.html";//+"\\Register_Key.exe";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se encuentra el archivo de Ayuda. La reinstalación de la aplicación pudiera resolver este problema.", "Easier " + ex.Source);
            }
        }
        
        // Ayuda con Eve
        private void asistenteDelEasierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!hlpeve)
            {
                buttons = MessageBoxButtons.YesNo;
                result = MessageBox.Show(this, "Esta a punto de Lanzar el Asistente del Easier...\nEstá seguro de que desea continuar?", "Confirmación", buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    asistenteDelEasierToolStripMenuItem.Checked = true;
                    hlpeve = true;
                    timer1.Interval = 1000;
                    timer1.Enabled = true;
                    hlptimer = 5;
                    hlpManager.tnumber = 1;
                    hlpManager.topic = "Saludo";
                    hlpManager.atext = new ArrayList();
                    ea = new EAHelp();
                    helpcomm.Text = "Hola... Soy Eve y voy ayudarte a utilizar el Easier.";
                    pictureBox2.Image = Image.FromFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName)+"\\Media\\Vissuals\\eve anime start.gif");
                }
            }
            else
            {
                buttons = MessageBoxButtons.YesNo;
                result = MessageBox.Show(this, "Esta a punto de Cerrar el Asistente del Easier...\nEstá seguro de que desea continuar?", "Confirmación", buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    asistenteDelEasierToolStripMenuItem.Checked = false;
                    hlpeve = false;
                    
                    timer1.Enabled = false;
                }
            }
               
        }

        private void asistenteDelEasierToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (asistenteDelEasierToolStripMenuItem.Checked)
            {
                pictureBox2.Visible = true;
                helpcomm.Visible = true;
            }
            if (!asistenteDelEasierToolStripMenuItem.Checked)
            {
                pictureBox2.Visible = false;
                helpcomm.Visible = false;
            }
        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = System.Environment.CurrentDirectory + "\\Help\\index.html";//+"\\Register_Key.exe";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se encuentra el archivo de Ayuda. La reinstalación de la aplicación pudiera resolver este problema.", "Easier " + ex.Source);
            }
        }


        private void CheckSubMayor4Help(double dif, double diaant)
        {
            double ramals = 0;
            double ipvs = 0;
            String p="";
            if (System.Math.Round(dif,2) != 0)
            {
                hlpManager.hlpknd = HelpKind.Resolucion;
                hlpManager.topic = "SubMayor";
                hlpManager.tnumber = 1;
                if (diaant != GetInicios())
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_Inicio1;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(dif.ToString());
                }
                else if ((ipvs = AmountIPV5(comboBox1.Text)) != (ramals = System.Convert.ToDouble(GetNumData(ramal20Base.Rows[ramal20Base.RowCount - 1].Cells[10].Value))) && DidCheckIPV())
                {
                    

                    hlpManager.errknd = ErrorKind.SM_Desc_RIPV;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    hlpManager.atext.Add(System.Math.Round((ramals - ipvs), 2).ToString());
                }
                else if ((p = BigValidarRamal2()) != "")
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_Inicio2;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    hlpManager.atext.Add(p);
                }
                else if ((p = HugeValidarIPV()) != "")
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_Inicio3;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    hlpManager.atext.Add(p);
                }
                else if ((p = RamalSubZero())!= "")
                {
                    hlpManager.errknd = ErrorKind.SM_Ramal_SubZero;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    hlpManager.atext.Add(p);
                }
                else if (!DidCheckIPV())
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_ChkIPV;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    
                }
                else if ((p = DifPrec()) != "")
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_Prec;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    hlpManager.atext.Add(p);
                }
                else
                {
                    hlpManager.errknd = ErrorKind.SM_Desc_UnKnown;
                    hlpManager.atext.Clear();
                    hlpManager.atext.Add(System.Math.Round(dif, 2).ToString());
                    //hlpManager.atext.Add(p);
                }
                 
            }
            
            else{
                if (tabControl1.SelectedTab.Text == "SubMayor")
                    hlpManager.topic = "SubMayor";
                    
                    hlpManager.atext.Clear();
                    hlpManager.tnumber = 1;
                   
                    hlpManager.hlpknd = HelpKind.Descripcion;
               
                
            }
        }
        private String RamalSubZero()
        {
            String prod = "";
            for (int w = 0; w < ramal20Base.RowCount; w++ )
            {
                if (ramal20Base.Rows[w].Cells[0].Value == null)
                    break;
                if (System.Convert.ToDouble(GetNumData(ramal20Base.Rows[w].Cells[ramal20Base.ColumnCount-2].Value))<0)
                {
                    prod = prod + GetData(ramal20Base.Rows[w].Cells[0].Value) + "\n";
                }
            }
            return prod;
        }
        private String DifPrec()
        {
            System.Data.DataSet mydts = dbc.SelectQuerryFixed("Select Producto, Precio, PrecIn from Ramal20 Inner Join Producto On Ramal20.Precio <> Producto.PrecIn Where Ramal20.Producto = Producto.Nombre and Ramal20.Cuenta = '" + comboBox1.Text + "' and UName = '" + tabControl2.SelectedTab.Text + "' and Producto.Cuenta = '" + comboBox1.Text + "' and Date = '" + dateTimePicker1.Value.AddDays(-1).ToShortDateString() + "'");
            String rst = "";
                for (int w = 0; w < mydts.Tables[0].Rows.Count; w++)
                {
                    rst = rst + mydts.Tables[0].Rows[w].ItemArray[0].ToString() + ": " + mydts.Tables[0].Rows[w].ItemArray[1].ToString() + " (Ayer), " + mydts.Tables[0].Rows[w].ItemArray[2].ToString() + " (Hoy).\n";
                }
            return rst;
            //if (rst!="")
            //{
            //    return true;
            //}

            //return false;
        }
        private bool DidCheckIPV()
        {
       
                System.Data.DataSet mydts = dbc.SelectQuerryFixed("Select Count(IPVName) From UnidadIPV Where UName='" + tabControl2.SelectedTab.Text + "'");
                if (dts.Tables[0].Rows.Count > 0)
                {
                    int kp= System.Convert.ToInt32(mydts.Tables[0].Rows[0].ItemArray[0].ToString());
                    int w = 0;
                    foreach (ValueSaver val in IPVSaver)
                    {
                        if (val.col == 12 && val.producto == "Totales:" && val.Cuenta == comboBox1.Text && val.moneda == GetMoneda() && val.UName == tabControl2.SelectedTab.Text && val.Date == DateCultureConverter(dateTimePicker1.Value))
                        {
                            w++;
                        }


                        if (w >= kp)
                            return true;
                    }
                }

            return false;
          
     
        }
        private String HugeValidarIPV()
        {
            String p = "";
               
            for (int w = 1; w < tabControl1.TabCount - 2; w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");
                p = p + BigValidarIPV2(IPVName);
               if (!tabControl1.TabPages[w + 1].Text.Contains("IPV "))
                {
                    break;
                }
           }

            return p;
        }
        private double GetInicios()
        {
          
            System.Data.DataSet ndts = dbc.SelectQuerryFixed("Select SUM([FImporte]) FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "'  AND Moneda = '" + GetMoneda() + "'");
            double ramal = 0;

            if (ndts.Tables[0].Rows.Count > 0 && ndts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
            {
                ramal = System.Convert.ToDouble(GetNumData(ndts.Tables[0].Rows[0].ItemArray[0].ToString()));
            }
            String cuenta = comboBox1.Text;
            double amount = 0;
            for (int w = 1; w < tabControl1.TabCount - 2; w++)
            {
                System.String IPVName = tabControl1.TabPages[w].Text.Replace("IPV ", "");

                if (dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPVName = '" + IPVName + "'"))
                {
                    dts = dbc.SelectQuerryFixed("SELECT SUM([FCosto]) FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + cuenta + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value.AddDays(-1)) + "' AND IPVName = '" + IPVName + "'");

                    if (dts.Tables[0].Rows[0].ItemArray[0].ToString() != "")
                        amount += System.Convert.ToDouble(dts.Tables[0].Rows[0].ItemArray[0].ToString());

                 //   ipv.Add("IPV " + IPVName);


                }

                if (!tabControl1.TabPages[w + 1].Text.Contains("IPV "))
                {
                    break;
                }
            }

            return System.Math.Round(ramal + amount, 2);
        }

        private void forzar_CheckedChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0 )
            {
                if (forzar.Checked)
                {
                    LockRamal(false);
                }
                else{
                    LockRamal(true);

                }
                
            }
            if (tabControl1.SelectedTab.Text.Contains("IPV "))
            {
                if (forzar.Checked)
                {
                    LockIPV(false);
                } 
                else
                {
                    LockIPV(true);

                }

            }
            if (tabControl1.SelectedTab.Text=="Ficha de Costo")
            {
                if (forzar.Checked)
                {
                    LockFichaCosto(false);
                }
                else
                {
                    LockFichaCosto(true);

                }

            }
        }

        private void forzar_EnabledChanged(object sender, EventArgs e)
        {
            if (!forzar.Enabled)//&&tabControl1.SelectedIndex!=0&&!tabControl1.SelectedTab.Text.Contains("IPV ")
            {
                forzar.Checked = false;

                dataGridViewTextBoxColumn7.ReadOnly = true;
                dataGridViewTextBoxColumn9.ReadOnly = true;
                dataGridViewTextBoxColumn10.ReadOnly = true;
                dataGridViewTextBoxColumn19.ReadOnly = true;

                dataGridViewTextBoxColumn81.ReadOnly = true;
                dataGridViewTextBoxColumn88.ReadOnly = true;
                Cantidad.ReadOnly = true;
                Precio.ReadOnly = true;
                int place = Search4(ramal20Base, "@#$%**$#");
                if (place<ramal20Base.RowCount)
                {
                     ramal20Base.Rows[place].Cells[0].ReadOnly = false;
                }
               

                

                
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ramal20Top.Columns[0].Width = ramal20Base.Columns[0].Width + ramal20Base.Columns[1].Width + ramal20Base.Columns[2].Width;
            FCostoTop.Columns[0].Width = FichaCostoBase.Columns[0].Width + FichaCostoBase.Columns[1].Width ;
            IPVTop.Columns[0].Width = IPVBase.Columns[0].Width + IPVBase.Columns[1].Width + IPVBase.Columns[2].Width;
        }

        private void guardarDatosTemporalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void SaveTemp()
        {
            MLB.XMLSolver solver = new MLB.XMLSolver();
            try
            {
                ArrayList al = new ArrayList();
                al.Add(RamalSaver);
                al.Add(IPVSaver);
                al.Add(SMSaver);
                al.Add(RISaver);
               // al.Add(FCSaver);
                al.Add(FCSaver);
                al.Add(Ajustes);
                al.Add(AjustesInRamal);

                solver.SaveTemData(al);
                //al.Add()
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                //solver.CloseAll();
                if (ex.Message.Contains("onnection"))
                    dbc.CloseConnection();
            }
        }
        private void cargarDatosTemporalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void LoadTemp()
        {
            MLB.XMLSolver solver = new MLB.XMLSolver();
            try
            {
                //solver = new MLB.XMLSolver();

                ArrayList al = solver.LoadTemData();

                if (al.Count > 0)
                    RamalSaver = (ArrayList)al[0];

                if (al.Count > 1)
                    IPVSaver = (ArrayList)al[1];

                if (al.Count > 2)
                    SMSaver = (ArrayList)al[2];

                if (al.Count > 3)
                    RISaver = (ArrayList)al[3];

                if (al.Count > 4)
                    FCSaver = (ArrayList)al[4];

                if (al.Count > 5)
                    Ajustes = (ArrayList)al[5];

                if (al.Count > 6)
                    AjustesInRamal = (ArrayList)al[6];

             

                if (tabControl1.SelectedIndex == 0)
                {
                    LoadRamal20();
                }

                if (tabControl1.SelectedTab.Text.Contains("IPV "))
                {
                    LoadIPV(tabControl1.SelectedTab.Text);
                }

                if (tabControl1.SelectedTab.Text == "SubMayor")
                {
                    LoadSubMayor();
                }

                if (tabControl1.SelectedTab.Text == "Ficha de Costo")
                {
                    LoadFichaCosto();
                }

                if (tabControl1.SelectedTab.Text == "Resumen de Ingresos")
                {
                    LoadResIng();
                }

                if (tabControl1.SelectedTab.Text == "Flujo de Caja")
                {
                    LoadFlujoCaja();
                }
                // al.Add(RamalSaver);
                // al.Add(IPVSaver);
                //  MLB.XMLSolver solver = new MLB.XMLSolver();
                //  solver.SaveTemData(al);
                //al.Add()

            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Easier " + ex.Source, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

               // solver.CloseAll();

                if (ex.Message.Contains("onnection"))
                    dbc.CloseConnection();
            }
        }
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTemp();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaverByPBar();

           

        }

        private void SaverByPBar()
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            timer3.Enabled = true;

            SaveTemp();
            
          //  progressBar1.Visible = false;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            SaverByPBar();

        }

        private void calentarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ReBindValues();
            //ReBindValues2();
            //ReBindValuesRamal();
            //ReBindValuesSaved();
        }

        private void datosTemporalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value<=90)
            {
            
            progressBar1.Value = progressBar1.Value + 10;
            if (progressBar1.Value>=100)
            {
                //progressBar1.Value = 0;
                progressBar1.Visible = false;
                timer3.Enabled = false;
            }
            }
        }

        private void FlujoCajaBase_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
            
                if (FlujoCajaBase.Tag.ToString()=="Released")
                {
                    GroupFlujoCaja();
                    FlujoCajaBase.Columns[0].HeaderText = "Fecha";
                    FlujoCajaBase.Columns[1].HeaderText = "Tipo";
                    FlujoCajaBase.Tag = "Colapsed";
                }
                else if (FlujoCajaBase.Tag.ToString() == "Colapsed")
                {
                    FCEstandar();
                    FlujoCajaBase.Columns[0].HeaderText = "Tipo";
                    FlujoCajaBase.Columns[1].HeaderText = "Grupo";
                    FlujoCajaBase.Tag = "Stadarizaed";
                }
                else
                {
                    LoadFlujoCaja();
                    FlujoCajaBase.Columns[0].HeaderText = "Fecha";
                    FlujoCajaBase.Columns[1].HeaderText = "Tipo";
                    FlujoCajaBase.Tag = "Released";
                }
            }

           
            
        }

      
        public void Forccer()
        {
            if ((tabControl1.SelectedIndex==0&&dbc.ExistQuerry("SELECT Id FROM Ramal20 WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND Moneda = '" + GetMoneda() + "'") )|| (tabControl1.SelectedTab.Text.Contains("IPV ")&&dbc.ExistQuerry("SELECT Id FROM IPV WHERE UName ='" + tabControl2.SelectedTab.Text + "' AND  Cuenta='" + comboBox1.Text + "' AND Date='" + DateCultureConverter(dateTimePicker1.Value) + "' AND IPVName = '" + tabControl1.SelectedTab.Text.Replace("IPV ","") + "'  AND Moneda = '" + GetMoneda() + "'")))
            {
                
                forzar.Enabled = true;
                LockRamal(true);
                if (tabControl1.SelectedTab.Text.Contains("IPV "))
                LockIPV(true);
            }
        }

        private void darkViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightToolStripMenuItem.Checked = false;
            darkViewToolStripMenuItem.Checked = true;

       
            //ramal20
            ramal20Base.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            UM.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            ramal20Base.BackgroundColor = System.Drawing.SystemColors.Info;
            ramal20Base.EnableHeadersVisualStyles = false;
          //  ramal20Base.ColumnHeadersDefaultCellStyle.Font.Bold = true;
           // ramal20Top.EnableHeadersVisualStyles = false;
           // ramal20Base.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            //IPV
            IPVBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;            
            UMIPV.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            IPVBase.BackgroundColor = System.Drawing.SystemColors.Info;
            IPVBase.EnableHeadersVisualStyles = false;
         //   IPVBase.ColumnHeadersDefaultCellStyle.Font.Bold = true;
           // IPVTop.EnableHeadersVisualStyles = false;
            //Submayor
            SubMayorBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            SubMayorBase.BackgroundColor = System.Drawing.SystemColors.Info;
            SubMayorBase.EnableHeadersVisualStyles = false;
          // SubMayorTop.EnableHeadersVisualStyles = false;
            //resumen de ingeso
            ResIngBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            ResIngBase.BackgroundColor = System.Drawing.SystemColors.Info;  
            ResIngBase.EnableHeadersVisualStyles = false;
           // ResIngTop.EnableHeadersVisualStyles = false;
            //ficha de costo
            FichaCostoBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            FichaCostoBase.BackgroundColor = System.Drawing.SystemColors.Info;
            FichaCostoBase.EnableHeadersVisualStyles = false;
          //  FCostoTop.EnableHeadersVisualStyles = false;
            //vale salida
            ValeSalidaBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            ValeSalidaBase.BackgroundColor = System.Drawing.SystemColors.Info;
            ValeSalidaBase.EnableHeadersVisualStyles = false;
         //   ValeSalidaTop.EnableHeadersVisualStyles = false;
            //flujo caja
            FlujoCajaBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            Tipo.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            colconcep.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Info;
            FlujoCajaBase.BackgroundColor = System.Drawing.SystemColors.Info;

            //Form1
            tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            tableLayoutPanel2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;

            tabControl2.SelectedTab.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            menuStrip1.ForeColor = System.Drawing.SystemColors.Window;
            vistaToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Window;
            ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            herramientasToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            vistaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            arToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;

            TLFlujo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            groupBox1.ForeColor = System.Drawing.SystemColors.Window;
            groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            groupBox2.ForeColor = System.Drawing.SystemColors.Window;
            groupBox3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            groupBox3.ForeColor = System.Drawing.SystemColors.Window;
            panel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            TLRamal20.ForeColor = System.Drawing.SystemColors.ControlText;
            TLRamal20.BackColor = System.Drawing.SystemColors.Info;
            TLIPV.ForeColor = System.Drawing.SystemColors.ControlText;
            TLIPV.BackColor = System.Drawing.SystemColors.Info;
            TLSubMayor.ForeColor = System.Drawing.SystemColors.ControlText;
            TLSubMayor.BackColor = System.Drawing.SystemColors.Info;
            TLResIng.ForeColor = System.Drawing.SystemColors.ControlText;
            TLResIng.BackColor = System.Drawing.SystemColors.Info;
            TLFichaCosto.BackColor = System.Drawing.SystemColors.Info;
            TLFichaCosto.ForeColor = System.Drawing.SystemColors.ControlText;
            TLValeSalida.ForeColor = System.Drawing.SystemColors.ControlText;
            TLValeSalida.BackColor = System.Drawing.SystemColors.Info;
            TLFlujo.ForeColor = System.Drawing.SystemColors.ControlText;
            TLFlujo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;

            label1.ForeColor = System.Drawing.SystemColors.Window;
            label2.ForeColor = System.Drawing.SystemColors.Window;
            label3.ForeColor = System.Drawing.SystemColors.Window;
            label3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;

            forzar.ForeColor = System.Drawing.SystemColors.Window;

            helpcomm.ForeColor = Color.White;

            for (int w = 0; w < tabControl1.TabCount; w++)
            {
                if(tabControl1.TabPages[w].Text=="Flujo de Caja")
                    tabControl1.TabPages[w].BackColor = System.Drawing.SystemColors.InactiveCaptionText;
                else
                tabControl1.TabPages[w].BackColor = System.Drawing.SystemColors.Info;
            }

            SetVissualState(true);
        }

       

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkViewToolStripMenuItem.Checked = false;
            lightToolStripMenuItem.Checked = true;

           
            //ramal20
            ramal20Base.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            UM.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            ramal20Base.BackgroundColor = System.Drawing.SystemColors.Window;
            ramal20Base.EnableHeadersVisualStyles = true;
           // ramal20Base.ColumnHeadersDefaultCellStyle.Font.Bold = false;
          //  ramal20Top.EnableHeadersVisualStyles = true;
            //IPV
            IPVBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;            
            UMIPV.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            IPVBase.BackgroundColor = System.Drawing.SystemColors.Window;
            IPVBase.EnableHeadersVisualStyles = true;
            //IPVBase.ColumnHeadersDefaultCellStyle.Font.Bold = false;
           // IPVTop.EnableHeadersVisualStyles = true;
            //Submayor
            SubMayorBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            SubMayorBase.BackgroundColor = System.Drawing.SystemColors.Window;
            SubMayorBase.EnableHeadersVisualStyles = true;
          //  SubMayorTop.EnableHeadersVisualStyles = true;
            //resumen de ingeso
            ResIngBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            ResIngBase.BackgroundColor = System.Drawing.SystemColors.Window;
            ResIngBase.EnableHeadersVisualStyles = true;
           // ResIngTop.EnableHeadersVisualStyles = true;
            //ficha de costo
            FichaCostoBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            FichaCostoBase.BackgroundColor = System.Drawing.SystemColors.Window;
            FichaCostoBase.EnableHeadersVisualStyles = true;
           // FCostoTop.EnableHeadersVisualStyles = true;
            //vale salida
            ValeSalidaBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            ValeSalidaBase.BackgroundColor = System.Drawing.SystemColors.Window;
            ValeSalidaBase.EnableHeadersVisualStyles = true;
         //   ValeSalidaTop.EnableHeadersVisualStyles = true;
            //flujo caja
            FlujoCajaBase.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            Tipo.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            colconcep.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            FlujoCajaBase.BackgroundColor = System.Drawing.SystemColors.Window;
            //Form1
            tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;

            tabControl2.SelectedTab.BackColor = System.Drawing.SystemColors.Window;
            menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            menuStrip1.ForeColor = System.Drawing.SystemColors.ControlText;
            vistaToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            herramientasToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            vistaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            arToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;

            TLRamal20.BackColor = System.Drawing.SystemColors.Window;
            TLIPV.BackColor = System.Drawing.SystemColors.Window;
            TLSubMayor.BackColor = System.Drawing.SystemColors.Window;
            TLResIng.BackColor = System.Drawing.SystemColors.Window;
            TLFichaCosto.BackColor = System.Drawing.SystemColors.Window;
            TLValeSalida.BackColor = System.Drawing.SystemColors.Window;
            TLFichaCosto.BackColor = System.Drawing.SystemColors.Window;           
            TLFlujo.BackColor = System.Drawing.SystemColors.Window;
            groupBox1.BackColor = System.Drawing.SystemColors.Window;
            groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            groupBox2.BackColor = System.Drawing.SystemColors.Window;
            groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            groupBox3.BackColor = System.Drawing.SystemColors.Window;
            groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            panel1.BackColor = System.Drawing.SystemColors.Window;
            TLRamal20.ForeColor = System.Drawing.SystemColors.ControlText;
            TLFlujo.ForeColor = System.Drawing.SystemColors.ControlText;
            label1.ForeColor = System.Drawing.SystemColors.ControlText;
            label2.ForeColor = System.Drawing.SystemColors.ControlText;
            label3.ForeColor = System.Drawing.SystemColors.ControlText;
            label3.BackColor = System.Drawing.SystemColors.Window;

            forzar.ForeColor = System.Drawing.SystemColors.ControlText;

            helpcomm.ForeColor = Color.Black;

            for (int w = 0; w < tabControl1.TabCount; w++)
            {
                tabControl1.TabPages[w].BackColor = System.Drawing.SystemColors.Window;
            }
            SetVissualState(false);
          //  InitializeComponent();
        }
        private void SetVissualState(bool state)
        {
            dbc.SimplePlan("Update Sistema Set SysView = '" + state + "'");
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ayudaToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if(darkViewToolStripMenuItem.Checked)
            ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;

        }

        private void ayudaToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if(darkViewToolStripMenuItem.Checked)
            ayudaToolStripMenuItem.ForeColor = ayudaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void herramientasToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                herramientasToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void herramientasToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                herramientasToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void vistaToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                vistaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void vistaToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                vistaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void vistaToolStripMenuItem1_DropDownOpened(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                vistaToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void vistaToolStripMenuItem1_DropDownClosed(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                vistaToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void editarToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void editarToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                editarToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void arToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                arToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void arToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (darkViewToolStripMenuItem.Checked)
                arToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
        }

        private void modulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Text != "Módulos")
            {
                try
                {

                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Modules\\" + ((ToolStripMenuItem)sender).Text + ".exe";//+"\\Register_Key.exe";
                    myProcess.StartInfo.Verb = "Open";
                    myProcess.StartInfo.CreateNoWindow = true;
                    //myProcess.StartInfo.
                    myProcess.Start();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("No se encuentra el módulo de especificado. La reinstalación de la aplicación pudiera resolver este problema.", "Easier " + ex.Source);
                }
            }
        }
        
        
    }
}
