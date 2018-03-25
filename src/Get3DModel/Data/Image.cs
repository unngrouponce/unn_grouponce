<<<<<<< HEAD
﻿using System;
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
        private byte[] outData;//выходной буфер
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

        }
        /// <summary>
        /// Создание обертки поверх bitmap.
        /// </summary>
        /// <param name="copySourceToOutput">Копирует исходное изображение в выходной буфер</param>
        public Image(Bitmap image, double tall = 0, bool copySourceToOutput = false)
        {

            this._image = image;

            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            stride = bmpData.Stride;

            data = new byte[stride * image.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

            outData = copySourceToOutput ? (byte[])data.Clone() : new byte[stride * image.Height];

        }

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
                    outData[i] = value.B;
                    outData[i + 1] = value.G;
                    outData[i + 2] = value.R;
                    outData[i + 3] = value.A;
                };
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
            System.Runtime.InteropServices.Marshal.Copy(outData, 0, bmpData.Scan0, outData.Length);
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
        /// Меняет местами входной и выходной буферы
        /// </summary>
        public void SwapBuffers()
        {
            var temp = data;
            data = outData;
            outData = temp;
        }


        /// <summary>
        /// Относительная высота на котором сделано изображение
        /// </summary>
        public double tall { get { return _tall; } }
        public Bitmap image { get { return _image; } }
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
                    wr.SetPixel(p, r, g, b);
                }
                _image = wr.image;
            }
            return _image;
        }
    }
}
=======
﻿using System;
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
        private byte[] outData;//выходной буфер
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

        }
        /// <summary>
        /// Создание обертки поверх bitmap.
        /// </summary>
        /// <param name="copySourceToOutput">Копирует исходное изображение в выходной буфер</param>
        public Image(Bitmap image, double tall = 0, bool copySourceToOutput = false)
        {

            this._image = image;

            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            stride = bmpData.Stride;

            data = new byte[stride * image.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

            outData = copySourceToOutput ? (byte[])data.Clone() : new byte[stride * image.Height];

        }

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
                    outData[i] = value.B;
                    outData[i + 1] = value.G;
                    outData[i + 2] = value.R;
                    outData[i + 3] = value.A;
                };
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
            System.Runtime.InteropServices.Marshal.Copy(outData, 0, bmpData.Scan0, outData.Length);
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
        /// Меняет местами входной и выходной буферы
        /// </summary>
        public void SwapBuffers()
        {
            var temp = data;
            data = outData;
            outData = temp;
        }


        /// <summary>
        /// Относительная высота на котором сделано изображение
        /// </summary>
        public double tall { get { return _tall; } }
        public Bitmap image { get { return _image; } }
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
                    wr.SetPixel(p, r, g, b);
                }
                _image = wr.image;
            }
            return _image;
        }
    }
}
>>>>>>> 1eee81c8bed866d30752674d5ebff54a77cca92b
