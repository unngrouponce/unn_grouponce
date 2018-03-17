using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using ParsingInputData;

namespace Data
{
    /// <summary>
    /// Класс для преобразованной картинки PNG
    /// </summary>
    public class Image
    {
        Bitmap image;
        double height;

        public Image(string pathImage)
        {
            IParser parser = new Parser();
            image = parser.readPNG(pathImage);

            string[] path = pathImage.Split('/');
            string name = path[path.Length - 1].Replace(".png", "");
            height = Convert.ToDouble(name);
        }
    }
}
