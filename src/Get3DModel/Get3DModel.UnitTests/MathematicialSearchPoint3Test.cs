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
    public class MathematicialSearchPoint3Test
    {
        [TestMethod]
        public void MathematicialSearchPoint_3Test()          //Проверка ядра свертки
        {
            //arrange
            MathematicialSearchPoint core = new MathematicialSearchPoint3();

            //act
            double[,] exepectedG = new double[,] { { 1, 1, 1 }, { 1,-8,1 }, { 1,1,1 } };
            double[,] actualG = core.XMatrix;
          

            //assert
            Debug.WriteLine("Проверка ядер  свертки Gx и Gy");
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.AreEqual(exepectedG[i, j], actualG[i, j]);
        }

        [TestMethod]
        public void gradientAtPoint3_CornerPoint_Test()              //Проверка градиента  угловой точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            MathematicialSearchPoint core = new MathematicialSearchPoint3();
            core.setImage(img);
            double[,] X = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    X[i, j] = 0;
            for (int i = 1; i < 3; i++)
                for (int j = 1; j < 3; j++)
                    X[i, j] = img.GetPixel(i - 1, j - 1).B;               //Матрица окружения точки (0,0)

            double grad = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grad += X[i, j] * core.XMatrix[j, i];
            //act
            double exepected = grad;
            double actual = core.gradientAtPoint(0, 0);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint3_BoundaryPoint_Test()              //Проверка градиента  граничной точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png"); ;    //Заполнение монохромного изображения

            MathematicialSearchPoint core = new MathematicialSearchPoint3();
            core.setImage(img);

            double[,] X = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    X[i, j] = 0;
            for (int i = 798; i < 800; i++)
                for (int j = 2; j < 5; j++)
                    X[i - 798, j - 2] = img.GetPixel(i, j).B;             //Матрица окружения точки (799,3)

            double grad = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grad += X[i, j] * core.XMatrix[j, i];
            //act
            double exepected = grad;
            double actual = core.gradientAtPoint(799, 3);
            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void gradientAtPoint3_IntPoint_Test()              //Проверка градиента  внутренней точки
        {
            //arrange
            IParser parser = new Parser();
            Bitmap img = parser.readPNG("Data\\sample_10.png");

            MathematicialSearchPoint core = new MathematicialSearchPoint3();
            core.setImage(img);

            double[,] X = new double[3, 3];
            for (int i = 600; i < 603; i++)
                for (int j = 600; j < 603; j++)
                    X[i - 600, j - 600] = img.GetPixel(i, j).B;   //Матрица окружения точки (601,601)
            double grad = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grad += X[i, j] * core.XMatrix[j, i];
                    
            //act
            double exepected = grad;
            double actual = core.gradientAtPoint(601, 601);
            //assert
            Assert.AreEqual(exepected, actual);
        }
    }
}
