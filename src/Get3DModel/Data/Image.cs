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
        double _tall;

        /// <param name="pathImage">Путь к изображению</param>
        public Image(string pathImage)
        {
            IParser parser = new Parser();
            image = parser.readPNG(pathImage);

            string[] path = pathImage.Split('/');
            string name = path[path.Length - 1].Replace(".png", "");
            _tall = Convert.ToDouble(name);
        }

        /// <summary>
        /// Ширина изображения в пикселях
        /// </summary>
        public int width() { return image.Width;}
        /// <summary>
        /// Высота изображения в пикселях
        /// </summary>
        public int height() { return image.Height;}
        /// <summary>
        /// Получает цвет указанного пикселя в этом изображении
        /// </summary>
        public Color GetPixel(int x, int y){return image.GetPixel(x, y);}
        /// <summary>
        /// Относительная высота на котором сделано изображение
        /// </summary>
        public double tall { get { return _tall; } }
    }
}
