using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using CalculatedBlock;
using ParsingInputData;


namespace Get3DModel.UnitTests
{
    [TestClass]
    public class MathematicialSearchPoint1Test
    {
        /*private MathematicialSearchPoint core;
        private double[,] Blue;                  //Матрица изображения
            
        [TestInitialize]
        public void TestInitialize()
        {
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("CalculatedBlock\\sample_10.png"); //Картинка
            for (int i = 0; i < img.Height; i++)
                for (int j = 0; j < img.Width; j++)
                {
                    Color c = ((Bitmap)img.Clone()).GetPixel(i, j);
                    Blue[i, j] = 0.3 * c.R + 0.59 * c.G + 0.11 * c.B;         //Заполнение монохромного изображения
                }

            MathematicialSearchPoint core=new MathematicialSearchPoint1();
        }

       [TestCleanup]
        public void TestCleanup()
        {

        }*/

        [TestMethod]
        public void MathematicialSearchPoint_1Test()          //Проверка ядер свертки
        {
            //arrange
            MathematicialSearchPoint core=new MathematicialSearchPoint1();
            
            //act
            double[,] exepectedGx = new double[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            double[,] exepectedGy = new double[,] { { 1, 0, 1 }, { 0, -4, 0 }, { 1, 0, 1 } };
            
            double[,] actualGx=core.XMatrix;
            double[,] actualGy=core.YMatrix;

            //assert
            Debug.WriteLine("Проверка ядер  свертки Gx и Gy");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(exepectedGx[i, j], actualGx[i, j]);
                    Assert.AreEqual(exepectedGy[i, j], actualGy[i, j]);
                }
        }

        [TestMethod]
        public void gradientAtPoint1_CornerPoint_Test()              //Проверка градиента  угловой точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");
            
            //Blue[i, j] = 0.3 * img.GetPixel(i, j).R + 0.59 * img.GetPixel(i, j).G + 0.11 * img.GetPixel(i, j).B;    //Заполнение монохромного изображения
              
            MathematicialSearchPoint core = new MathematicialSearchPoint1();
            core.setImage(img);
            double[,] X = new double[3,3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    X[i,j]=0;
            for (int i = 1; i < 3; i++)
                for (int j = 1; j < 3; j++)
                    X[i, j] = img.GetPixel(i-1, j-1).B;               //Матрица окружения точки (0,0)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        gradX+=X[i,j]*core.XMatrix[j,i];
                        gradY+=X[i,j]*core.YMatrix[j,i];
                    }
            //act
            double exepected = (gradX+gradY)/2;
            double actual = core.gradientAtPoint(0, 0);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint1_BoundaryPoint_Test()              //Проверка градиента  граничной точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint1();
            core.setImage(img);

            double[,] X = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    X[i, j] = 0;
            for (int i = 798; i < 800; i++)
                for (int j = 2; j < 5; j++)
                    X[i - 798, j - 2] = img.GetPixel(i, j).B;             //Матрица окружения точки (799,3)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = (gradX + gradY) / 2;
            double actual = core.gradientAtPoint(799, 3);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint1_IntPoint_Test()              //Проверка градиента  внутренней точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            MathematicialSearchPoint core = new MathematicialSearchPoint1();
            core.setImage(img);

            double[,] X = new double[3, 3];
            for (int i = 600; i < 603; i++)
                for (int j = 600; j < 603; j++)
                    X[i - 600, j - 600] = img.GetPixel(i, j).B;   //Матрица окружения точки (601,601)
            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = (gradX + gradY) / 2;
            double actual = core.gradientAtPoint(601, 601);
            //assert
            Assert.AreEqual(exepected, actual);
        }
    }
}
