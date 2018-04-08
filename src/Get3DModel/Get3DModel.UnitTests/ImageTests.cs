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
        public void widthTest()          //Проверка получения ширины изображения в пикселях
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
        public void heightTest()       //Проверка получения высоты изображения в пикселях
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
        public void thisXYTest_InImage()                        //Проверка цвета  пикселя в обертке исходного изображения
        {
            //arrange
            string path = "Data\\r50g100b150.png";
            IParser parser = new Parser();
            Bitmap bitmap = parser.readPNG(path);
            double tall=0;

            Data.Image img = new Data.Image(bitmap, tall, true);
            //act
            Color rez=img[0,0];
            Color exepected = Color.FromArgb(255, 50, 100, 150);
            //assert
            Debug.WriteLine("Проверка цвета");
            Assert.AreEqual(rez, exepected);
        }

        [TestMethod]
        public void thisXYTest_OutSideImage()                        //Проверка цвета пикселя за границами обертки исходного изображения
        {
            //arrange
            string path = "Data\\r50g100b150.png";
            IParser parser = new Parser();
            Bitmap bitmap = parser.readPNG(path);
            double tall = 0;

            Data.Image img = new Data.Image(bitmap, tall, true) { DefaultColor = Color.Silver };
            //act
            Color rez = img[img.width()+1, img.height()+1];
            Color exepected = Color.Silver;                     //Цвет по умолчанию
            //assert
            Debug.WriteLine("Проверка цвета за пределами диапазона");
            Assert.AreEqual(exepected, rez);
        }


    }
}
