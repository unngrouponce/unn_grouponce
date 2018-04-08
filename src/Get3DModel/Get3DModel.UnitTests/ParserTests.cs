using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;


using ParsingInputData;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void readConfigTest_СorrectFile()              //Считывания корректного файла настроек по указанному пути
        {

            //arrange
            IParser parser = new Parser();
            string path = "Parser\\sample.camera";
            Dictionary<string, double> res = new Dictionary<string, double>();        //Результат
            Dictionary<string, double> exepected = new Dictionary<string, double>();  //Ожидаемый результат
            exepected.Add("FDISTANCE", 300);
            exepected.Add("FWIDTH", 75);
            exepected.Add("HCOEFF", 1);

            //act
            res = parser.readConfig(path);

            //assert
            Debug.WriteLine("Проверка размеров словарей!");
            Assert.AreEqual(res.Count, exepected.Count);
            Debug.WriteLine("Проверка считывания FDISTANCE");
            Assert.AreEqual(res["FDISTANCE"], exepected["FDISTANCE"]);
            Debug.WriteLine("Проверка считывания FWIDTH");
            Assert.AreEqual(res["FWIDTH"], exepected["FWIDTH"]);
            Debug.WriteLine("Проверка считывания HCOEFF");
            Assert.AreEqual(res["HCOEFF"], exepected["HCOEFF"]);
        }

        [TestMethod]
        public void readConfigTest_InсorrectBigFile()    //Cчитывание некорректного файла настроек - количество строк больше 2-ух
        {
            //arrange
            IParser parser = new Parser();
            string path = "Parser\\sample2.camera";
            Dictionary<string, double> res = new Dictionary<string, double>();        //Результат

            //act
            res = parser.readConfig(path);

            //assert
            //Проверить системный код ошибки
        }

        [TestMethod]
        public void readConfigTest_InсorrectSmallFile() //Cчитывание некорректного файла настроек - количество строк меньше 2-ух
        {

        }

        [TestMethod]
        public void readConfigTest_InсorrectWayFile() //Cчитывание несуществующего файла
        {
        }

        [TestMethod]
        public void readPNGTest_BlackImage()       //Cчитывание изображения 
        {
            //arrange

            IParser parser = new Parser();
            string path = "Parser\\sample_01.png";
            //Bitmap exepected = new Bitmap(path);        //Результат

            //act
            Bitmap rez = parser.readPNG(path);

            //assert
            Debug.WriteLine("Проверка ширины");
            Assert.AreEqual(rez.Width, 20);
            Debug.WriteLine("Проверка высоты");
            Assert.AreEqual(rez.Height, 10);
            Debug.WriteLine("Проверка цвета");
            for (int i = 0; i < rez.Width; i++)        //По всем пикселям
                for (int j = 0; j < rez.Height; j++)
                    Assert.AreEqual(rez.GetPixel(i, j), Color.FromArgb(0, 0, 0));
        }
    }
}