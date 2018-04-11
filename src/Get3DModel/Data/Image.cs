using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using ParsingInputData;
using System.Drawing.Imaging;

namespace Data
{
    /// <summary>
    /// Класс для преобразованной картинки PNG
    /// </summary>
    public class Image : IDisposable, IEnumerable<System.Drawing.Point>
    {
        private Bitmap _image;
        private double _tall;
        /// <summary>
        /// Цвет по-умолачнию (используется при выходе координат за пределы изображения)
        /// </summary>
        public Color DefaultColor { get; set; }
        private byte[] data;//буфер исходного изображения
        private int stride;
        private BitmapData bmpData;




        /// <param name="pathImage">Путь к изображению</param>
        public Image(string pathImage)
        {
            IParser parser = new Parser();
            _image = parser.readPNG(pathImage);

            FileInfo infoImage = new FileInfo(pathImage);
            string name = infoImage.Name.Replace(".png", "");
            string[] nameSplit = name.Split('_');
            _tall = Convert.ToDouble(nameSplit[1]);
            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            stride = bmpData.Stride;
            data = new byte[stride * image.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);
            DefaultColor = Color.Silver;
        }
        /// <summary>
        /// Создание обертки поверх bitmap.
        /// </summary>
        public Image(Bitmap image)
        {

            this._image = image;
            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            stride = bmpData.Stride;
            data = new byte[stride * image.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);
            DefaultColor = Color.Silver;

        }
        public Image(Image image):this(image.image){}

        /// <summary>
        /// Ширина изображения в пикселях
        /// </summary>
        public int width() { return _image.Width; }
        /// <summary>
        /// Высота изображения в пикселях
        /// </summary>
        public int height() { return _image.Height; }
        /// <summary>
        /// Получает цвет указанного пикселя в этом изображении
        /// </summary>
        public Color GetPixel(int x, int y) { return _image.GetPixel(x, y); }
        /// <summary>
        /// Возвращает пиксел из исходнго изображения.
        /// Либо заносит пиксел в выходной буфер.
        /// </summary>
        public Color this[int x, int y]
        {
            get
            {
                var i = GetIndex(x, y);
                return i < 0 ? DefaultColor : Color.FromArgb(data[i + 3], data[i + 2], data[i + 1], data[i]);
            }

            set
            {
                var i = GetIndex(x, y);
                if (i >= 0)
                {
                    data[i] = value.B;
                    data[i + 1] = value.G;
                    data[i + 2] = value.R;
                    data[i + 3] = value.A;
                }
            }
        }
        /// <summary>
        /// Возвращает пиксел из исходнго изображения.
        /// Либо заносит пиксел в выходной буфер.
        /// </summary>
        public Color this[System.Drawing.Point p]
        {
            get { return this[p.X, p.Y]; }
            set { this[p.X, p.Y] = value; }
        }
        /// <summary>
        /// Заносит в выходной буфер значение цвета, заданные в double.
        /// Допускает выход double за пределы 0-255.
        /// </summary>
        public void SetPixel(System.Drawing.Point p, double r, double g, double b)
        {
            if (r < 0) r = 0;
            if (r >= 256) r = 255;
            if (g < 0) g = 0;
            if (g >= 256) g = 255;
            if (b < 0) b = 0;
            if (b >= 256) b = 255;

            this[p.X, p.Y] = Color.FromArgb((int)r, (int)g, (int)b);
        }

        int GetIndex(int x, int y)
        {
            return (x < 0 || x >= width() || y < 0 || y >= height()) ? -1 : x * 4 + y * stride;
        }


        public void Dispose()
        {
            System.Runtime.InteropServices.Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            _image.UnlockBits(bmpData);
        }

        /// <summary>
        /// Перечисление всех точек изображения
        /// </summary>
        public IEnumerator<System.Drawing.Point> GetEnumerator()
        {
            for (int y = 0; y < _image.Height; y++)
                for (int x = 0; x < _image.Width; x++)
                    yield return new System.Drawing.Point(x, y);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Относительная высота на котором сделано изображение
        /// </summary>
        public double tall { get { return _tall; } }
        public Bitmap image { get {
                return _image;
            } }
        public Bitmap Convolution(double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);

            using (var wr = new Image((Bitmap)_image.Clone()) { DefaultColor = Color.Silver })
            {
                foreach (var p in wr)
                {
                    double r = 0d, g = 0d, b = 0d;

                    for (int i = 0; i < w; i++)
                        for (int j = 0; j < h; j++)
                        {
                            var pixel = wr[p.X + i - 1, p.Y + j - 1];
                            r += matrix[j, i] * pixel.R;
                            g += matrix[j, i] * pixel.G;
                            b += matrix[j, i] * pixel.B;
                        }
                    wr.SetPixel(p, Math.Abs(r), g, b);
                }
                _image = wr.image;
            }
            return _image;
        }
        public double ConvolutionAtPoint(int x, int y, double[,] matrix)
        {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);
            double r = 0d;
            using (var wr = new Image((Bitmap)_image.Clone()))
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        var pixel = wr[x + i - 1, y + j - 1];
                        r += matrix[j, i] * pixel.R;
                    }
            }
            return Math.Abs(r);
        }
        public static double[,] GaussianKernel(int lenght, double weight)
        {
            double[,] kernel = new double[lenght, lenght];
            double kernelSum = 0;
            int foff = (lenght - 1) / 2;
            double distance = 0;
            double constant = 1d / (2 * Math.PI * weight * weight);
            for (int y = -foff; y <= foff; y++)
            {
                for (int x = -foff; x <= foff; x++)
                {
                    distance = ((y * y) + (x * x)) / (2 * weight * weight);
                    kernel[y + foff, x + foff] = constant * Math.Exp(-distance);
                    kernelSum += kernel[y + foff, x + foff];
                }
            }
            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < lenght; x++)
                {
                    kernel[y, x] = kernel[y, x] * 1d / kernelSum;
                }
            }
            return kernel;
        }
    }
}
