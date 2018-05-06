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
    public class MathematicialSearchPoint11Test
    {
        [TestMethod]
        public void MathematicialSearchPoint_11Test()          //Проверка ядер свертки
        {
            //arrange
            MathematicialSearchPoint core = new MathematicialSearchPoint11();

            //act
            double[,] exepectedGx = new double[,]  { { 0, 0, 0, 1, 0, 0, 0 }, { 0, 1, 1, 2, 1, 1, 0 }, { 0, 1, 2, 3, 2, 1, 0 },
                { 1, 2, 3, -44, 3, 2, 1 }, { 0, 0, 0, 1, 0, 0, 0 }, { 0, 1, 1, 2, 1, 1, 0 }, { 0, 1, 2, 3, 2, 1, 0 } };
            double[,] exepectedGy = new double[,]  { { 1, 0, 0, 0, 0, 0, 1 }, { 0, 2, 1, 1, 1, 2, 0 }, { 0, 1, 3, 2, 3, 1, 0 },
                { 0, 1, 2, -44, 2, 1, 0 }, { 1, 0, 0, 0, 0, 0, 1 }, { 0, 2, 1, 1, 1, 2, 0 }, { 0, 1, 3, 2, 3, 1, 0 } };
            double[,] actualGx = core.XMatrix;
            double[,] actualGy = core.YMatrix;

            //assert
            Debug.WriteLine("Проверка ядер  свертки Gx и Gy");
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    Assert.AreEqual(exepectedGx[i, j], actualGx[i, j]);
                    Assert.AreEqual(exepectedGy[i, j], actualGy[i, j]);
                }
        }

        [TestMethod]
        public void gradientAtPoint11_CornerPoint_Test()              //Проверка градиента  угловой точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            //Blue[i, j] = 0.3 * img.GetPixel(i, j).R + 0.59 * img.GetPixel(i, j).G + 0.11 * img.GetPixel(i, j).B;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint11();
            core.setImage(img);
            double[,] X = new double[7, 7];
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    X[i, j] = 0;
            for (int i = 3; i < 7; i++)
                for (int j = 3; j < 7; j++)
                    X[i, j] = img.GetPixel(i - 3, j - 3).B;               //Матрица окружения точки (0,0)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = (gradX + gradY) / 2;
            double actual = core.gradientAtPoint(0, 0);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint11_BoundaryPoint_Test()              //Проверка градиента  граничной точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png"); ;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint11();
            core.setImage(img);

            double[,] X = new double[7, 7];
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    X[i, j] = 0;
            for (int i = 796; i < 800; i++)
                for (int j = 0; j < 7; j++)
                    X[i - 796, j] = img.GetPixel(i, j).B;             //Матрица окружения точки (799,3)

            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
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
        public void gradientAtPoint11_IntPoint_Test()              //Проверка градиента  внутренней точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            MathematicialSearchPoint core = new MathematicialSearchPoint11();
            core.setImage(img);

            double[,] X = new double[7, 7];
            for (int i = 599; i < 606; i++)
                for (int j = 599; j < 606; j++)
                    X[i - 599, j - 599] = img.GetPixel(i, j).B;   //Матрица окружения точки (602,602)
            double gradX = 0;
            double gradY = 0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    gradX += X[i, j] * core.XMatrix[j, i];
                    gradY += X[i, j] * core.YMatrix[j, i];
                }
            //act
            double exepected = (gradX + gradY) / 2;
            double actual = core.gradientAtPoint(602, 602);
            //assert
            Assert.AreEqual(exepected, actual);
        }
    }
}
