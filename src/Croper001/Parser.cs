using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
namespace Croper001
{
    /// <summary>
    /// Читает конфигурационный файл в указанной директории, сохраняет параметры и изображения.
    /// </summary>
    public class Parser
    {
        private double _FDISTANCE;
        private double _FWIDTH;
        private double _HCOEFF;  //TODO
        private string _TEMPLATE;
        private Bitmap[] _Images;

        /// <summary>
        /// Фокусное расстояние
        /// </summary>
        public double FDISTANCE { get { return _FDISTANCE; } }

        /// <summary>
        ///Наблюдаемая ширина в фокусе
        /// </summary>
        public double FWIDTH { get { return _FWIDTH; } }

        /// <summary>
        ///Коэффициент для вычисления абсолютной высоты фокуса
        /// </summary>
        public double HCOEFF { get { return _HCOEFF; } }

        /// <summary>
        ///шаблон имени файла
        /// </summary>
        public string TEMPLATE { get { return _TEMPLATE; } }

        /// <summary>
        ///массив изображений
        /// </summary>
        public Bitmap[] Images { get { return _Images; } }


        /// <param name="folderName">имя папки</param>
        public Parser(string folderName)
        {
            List<string> filesname = new List<string>();
            try { filesname = Directory.GetFiles(folderName).ToList(); }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("the configuration file is not found");
                Environment.Exit(-1);
            }
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
