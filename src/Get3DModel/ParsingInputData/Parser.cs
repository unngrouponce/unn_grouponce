using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;


namespace ParsingInputData
{
   public class Parser:IParser
    {
        public Dictionary<string, double> readConfig(string path)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();
            try
            {
                string conf = new StreamReader(path).ReadToEnd();
                var _FDISTANCE = Convert.ToDouble(new Regex(@"f=(\d+)").Match(conf).Groups[1].ToString());
                var _FWIDTH = Convert.ToDouble(new Regex(@"w=(\d+)").Match(conf).Groups[1].ToString());
                res["FDISTANCE"] = _FDISTANCE;
                res["FWIDTH"] = _FWIDTH;
                res["HCOEFF"] = 1;
            }
            catch
            {
                Console.WriteLine("the configuration data is incorrect");
                Environment.Exit(-1);
            }
            return res;
        }

        public System.Drawing.Bitmap readPNG(string path)
        {
            Console.WriteLine(string.Format("processing of the {0} image is started...", path));
            return (Bitmap)Image.FromFile(path);
        }
    }
}
