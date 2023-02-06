using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// int Suma(int s1,int s2);  

namespace UMConvert
{

    public class Utility
    {
        static void Main(string[] args)
        {
           // System.Console.WriteLine("Calling methods from MathLibrary.DLL:");

            if (args.Length != 3)
            {
                System.Console.WriteLine("Usage: TestCode <num1> <num2>");
                return;
            }
            double cant = double.Parse(args[0]);
            //String num2 = String.pa.Parse(args[1]);




        }
        public static double UMConverter(double cant, String um1, String um2)
        {




            if (um1 == "Kg" && um2 == "Lb") // de lb a kg
            {
                return System.Math.Round(cant / 2.174, 2);
            }

            if (um1 == "Lb" && um2 == "Kg")
            {
                return System.Math.Round(cant * 2.174, 2);
            }
            if (um1 == "Lt" && um2 == "Lb")
            {
                return System.Math.Round(cant / 2.174, 2);
            }
            if (um1 == "Lt" && um2 == "Lb")
            {
                return System.Math.Round(cant * 2.174, 2);
            }
            if (um1 == "Kg" && um2 == "g")
            {
                return System.Math.Round(cant / 1000, 2);
            }
            if (um1 == "g" && um2 == "Kg")
            {
                return System.Math.Round(cant * 1000, 2);
            }

            if (um1 == "Lb" && um2 == "g")
            {
                return System.Math.Round(cant / 460, 2);
            }
            if (um1 == "g" && um2 == "Lb")
            {
                return System.Math.Round(cant * 460, 2);
            }

            return 0;
        }
    }

    
}
