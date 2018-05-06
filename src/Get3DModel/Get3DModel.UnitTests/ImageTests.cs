using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

using Data;
using ParsingInputData;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void widthTest()            //Проверка получения ширины изображения в пикселях
        {
            //arrange
            Data.Image img = new Data.Image("Data\\sample_01.png");

            //act
            double widh = img.width();
            //assert
            Debug.WriteLine("Проверка ширины");
            Assert.AreEqual(widh, 20);
        }

        [TestMethod]
        public void heightTest()         //Проверка получения высоты изображения в пикселях
        {
            //arrange
            Data.Image img = new Data.Image("Data\\sample_01.png");

            //act
            double height = img.height();
            //assert
            Debug.WriteLine("Проверка ширины");
            Assert.AreEqual(height, 10);
        }

        [TestMethod]
        public void GetPixelTest()        //Проверка получения цвета пикселя изображения
        {
            //arrange
            Data.Image img = new Data.Image("Data\\sample_01.png");

            //act

            //assert
            Debug.WriteLine("Проверка цвета пикселя");
            for (int i = 0; i < img.width(); i++)        //По всем пикселям
                for (int j = 0; j < img.height(); j++)
                    Assert.AreEqual(img.GetPixel(i, j), Color.FromArgb(0, 0, 0));
        }

         [TestMethod]
        public void tallTest()                        //Проверка считывания высоты изображения при создании объекта конструктором Image(path)
        {
            //arrange
            string path = "Data\\sample_01.png";
            Data.Image img = new Data.Image(path);
            //act
            double rez = img.tall;
            double exepected = 1;
            //assert
            Assert.AreEqual(exepected, rez);
        }

        /*[TestMethod]
        public void thisXYTest_GetInImage()                        //Проверка цвета  пикселя в обертке исходного изображения
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall=0;

           // Data.Image img = new Data.Image(bitmap, true);
           // //act
           // Color rez=img[0,0];
           // Color exepected = Color.FromArgb(255, 50, 100, 150);
           // Assert.AreEqual(rez, exepected);
        }

        [TestMethod]
        public void thisXYTest_GetOutSideImage()                        //Проверка цвета пикселя за границами обертки исходного изображения
        {
            ////arrange
            //string path = "Data\\r50g100b150.png";
            //IParser parser = new Parser();
            //Bitmap bitmap = parser.readPNG(path);
            ////double tall = 0;

            //Data.Image img = new Data.Image(bitmap,  true);
            ////act
            //Color rez = img[img.width()+1, img.height()+1];
            //Color exepected = img.DefaultColor;                     //Цвет по умолчанию
            ////assert
            //Assert.AreEqual(exepected, rez);
        }

        [TestMethod]
        public void thisXYTest_SetInImage()                        //Проверка записи цвета пикселя в изображении
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap, true);
           // //act
           // Color rez = img[0, 0] = Color.Black;
           // Color exepected = Color.Black;
           // //assert
           // Assert.AreEqual(exepected, rez);
        }

        [TestMethod]
        public void thisXYTest_SetOutSideImage()                        //Проверка записи цвета пикселя за границы изображения
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap, true);
           // //act
           // Color rez_false = img[img.width()+50, img.height()+50]=Color.Black;
           // Color rez_true = img[img.width() + 50, img.height() + 50];
           // Color exepected = img.DefaultColor;                    
           // //assert
           // Assert.AreEqual(exepected,rez_true);
        }

        [TestMethod]
        public void thisPointTest_GetInImage()                        //Проверка цвета  точки в обертке исходного изображения
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap,  true);
           // System.Drawing.Point point = new System.Drawing.Point(0, 0);
           // //act
           // Color rez = img[point];
           // Color exepected = Color.FromArgb(255, 50, 100, 150);
           // //assert
           // Assert.AreEqual(rez, exepected);
        }

        [TestMethod]
        public void thisPointTest_GetOutSideImage()                        //Проверка цвета точки за границами обертки исходного изображения
        {
            ////arrange
            //string path = "Data\\r50g100b150.png";
            //IParser parser = new Parser();
            //Bitmap bitmap = parser.readPNG(path);
            ////double tall = 0;

            //Data.Image img = new Data.Image(bitmap,  true);
            //System.Drawing.Point point = new System.Drawing.Point(img.width() + 1, img.height() + 1);
            ////act
            //Color rez = img[point];
            //Color exepected = img.DefaultColor;                     //Цвет по умолчанию
            ////assert
            //Assert.AreEqual(exepected, rez);
        }

        [TestMethod]
        public void thisPointTest_SetInImage()                        //Проверка записи цвета точки в изображении
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap,  true);
           // System.Drawing.Point point = new System.Drawing.Point(0, 0);
           // //act
           // Color rez = img[point] = Color.Black;
           // Color exepected = Color.Black;
           // //assert
           // Assert.AreEqual(exepected, rez);
        }

        [TestMethod]
        public void thisPointTest_SetOutSideImage()          //Проверка записи цвета точки за границами изображения
        {
          //  //arrange
          //  string path = "Data\\r50g100b150.png";
          //  IParser parser = new Parser();
          //  Bitmap bitmap = parser.readPNG(path);
          ////  double tall = 0;

          //  Data.Image img = new Data.Image(bitmap,  true);
          //  System.Drawing.Point point = new System.Drawing.Point(img.width() + 1, img.height() + 1);
          //  //act
          //  Color rez_false = img[point] = Color.Black;
          //  Color rez_true = img[point];
          //  Color exepected = img.DefaultColor;
          //  //assert
          //  Assert.AreEqual(exepected, rez_true);
        }

        [TestMethod]
        public void SetPixelTest_SetInImage()                        //Проверка записи цвета  пикселя изображения в выходной буфер
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap);
           // System.Drawing.Point point = new System.Drawing.Point(0, 0);
           // //act
           // img.SetPixel(point,255, 255, 255);
           // Color rez = img[0, 0];
           // Color exepected = Color.White;
           // //assert
           // Assert.AreEqual(exepected.ToArgb(), rez.ToArgb());
        }

        [TestMethod]
        public void SetPixelTest_SetOutSideImage()                     //Проверка записи цвета за границами цветовых значений RGB (0-255)
        {
           // //arrange
           // string path = "Data\\r50g100b150.png";
           // IParser parser = new Parser();
           // Bitmap bitmap = parser.readPNG(path);
           //// double tall = 0;

           // Data.Image img = new Data.Image(bitmap);
           // System.Drawing.Point point = new System.Drawing.Point(0, 0);
           // //act
           // img.SetPixel(point, 255, 0, 255);
           // Color rez = img[0, 0];
           // Color exepected = Color.FromArgb(255,0,255);
           // //assert
           // Assert.AreEqual(exepected, rez);
        }

        [TestMethod]
        public void ConvolutionTest()                        //Проверка считывания высоты изображения при создании объекта конструктором Image(path)
        {
            //arrange
           /* string path = "Data\\sample_01.png";
            Data.Image img = new Data.Image(path);

            double[,] matrix=new double[3,3];
            int size=3;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i, j] = 0;
            matrix[0, 1] = 1;
            matrix[1, 0] = 1;
            matrix[1, 2] = 1;
            matrix[2, 1] = 1;
            //act
            IParser parser = new Parser();
            Bitmap exepected = parser.readPNG(path);
            Bitmap rez = img.Convolution(matrix);
            //assert
            Assert.AreEqual(exepected, rez);
        }*/
    }
}
