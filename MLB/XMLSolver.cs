using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MLB
{
    class XMLSolver
    {
        private String path { get; set; }
        private StreamReader sr;
        private StreamWriter sw;
        public XMLSolver(/*String path*/)
        {
            
//            else{

////                 sr = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
////                 sw = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
//            }

            
            //this.path = path;

        }
        private void Initialize()
        {
            sw.Write("<B-Fecha:" + System.DateTime.Now.ToShortDateString() + ">\n<E-Fecha>");
        }
        public void CloseAll()
        {
            sr.Close();
            sw.Close();
        }
        public void SaveTemData(ArrayList metadata)
        {
            if (!File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
            {
                sw = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp",true);
                //sw.Close();


            }
            else
            sw = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
            for (int k = 0; k < metadata.Count; k ++ )
            {
                ArrayList basedata = (ArrayList)metadata[k];
                sw.WriteLine("<BeginArray"+k.ToString()+">");
                for (int p = 0; p < basedata.Count; p ++ )
                {
                    ValueSaver vs = (ValueSaver) basedata[p];
                    sw.WriteLine("<"+vs.producto+"-;-"+vs.Cuenta+"-;-"+vs.UName+"-;-"+vs.moneda+"-;-"+vs.Date+"-;-"+vs.IPVName+"-;-"+vs.cant+"-;-"+vs.row.ToString()+"-;-"+vs.col.ToString()+">");
                }
                sw.WriteLine("<EndArray" + k.ToString() + ">");

            }
            sw.Close();
        }
        public ArrayList LoadTemData()
        {
           
//                File.CreateText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");

            ArrayList al = new ArrayList();
            if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
            {
                sr = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");

                int num = 0;

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    if (line == "<BeginArray" + num + ">")
                    {
                        ArrayList buffer = new ArrayList();

                        while (line != "<EndArray" + num + ">")
                        {
                            line = sr.ReadLine();
                            if (line != "<EndArray" + num + ">")
                            {

                                line = line.Replace("<", "");
                                line = line.Replace(">", "");
                                string[] stringSeparators = new string[] { "-;-" };
                                String[] content = line.Split(stringSeparators, StringSplitOptions.None);
                                //  = line.Split(new string[] { "-;-" });

                                if (content.Length == 9)
                                {
                                    ValueSaver vs = new ValueSaver(content[5], content[6], System.Convert.ToInt32(content[7]), System.Convert.ToInt32(content[8]), content[2], content[1], content[4], content[0], content[3]);
                                    buffer.Add(vs);
                                }
                            }

                        }
                        al.Add(buffer);
                    }

                    num++;
                }

                sr.Close();
            }
            return al;
           
        }
        public void DeleteTemFile()
        {
            if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
            {
                FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
                //fi.Encrypt();
                fi.Delete();
              //  File.CreateText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");


            }

        }
        public bool MayRecover()
        {
            if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp"))
            {
                FileInfo fi = new FileInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Easier\\easier.tmp");
                
                if (fi.Length> 1000)
                {
                    return true;
                }
               
            }
            return false;

        }


    }
}
