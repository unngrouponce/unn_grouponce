using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
namespace ParsingInputData
{
    public class Parser
    {
        private double _FDISTANCE;
        private double _FWIDTH;
        private double _HCOEFF;  //TODO
        private string _TEMPLATE;
        private Bitmap[] _Images;

        public double FDISTANCE { get { return _FDISTANCE; } }
        public double FWIDTH { get { return _FWIDTH; } }
        public double HCOEFF { get { return _HCOEFF; } }
        public string TEMPLATE { get { return _TEMPLATE; } }
        public Bitmap[] Images { get { return _Images; } }

        public Parser(string folderName)
        {
            List<string> filesname = Directory.GetFiles(folderName).ToList<string>();
            _TEMPLATE = (filesname.First(x => x.EndsWith(".camera"))).Split('.')[0];
            string conf = new StreamReader(_TEMPLATE + ".camera").ReadToEnd();
            _FDISTANCE = Convert.ToDouble(new Regex(@"f=(\d+)").Match(conf).Groups[1].ToString());
            _FWIDTH = Convert.ToDouble(new Regex(@"w=(\d+)").Match(conf).Groups[1].ToString());

            var im = filesname.FindAll(x => x.StartsWith(_TEMPLATE) && x.EndsWith(".png"));
            _Images = new Bitmap[im.Count];
            for (int i = 0; i < im.Count; i++)
            {
                _Images[i] = (Bitmap)Image.FromFile(im.ElementAt(i));
            }
        }
    }
}
