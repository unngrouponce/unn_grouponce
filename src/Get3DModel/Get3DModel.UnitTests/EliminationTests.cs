using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using CalculatedBlock;
using Data;
using ParsingInputData;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class EliminationTests
    {
        [TestMethod]
        public void calculateGradientImageTest()   //Проверка подсчета градиентов на картинке
        {
            //arrange
            IParser parser=new Parser();
            Bitmap bitmap = parser.readPNG("Data\\sample_10.png");
            IMathematical strat = new MathematicialSearchPoint1();   //Подсчет градиента первой стратегией
            strat.setImage(bitmap);

            Elimination elemenation = new Elimination(strat);
            Data.Image img = new Data.Image("Data\\sample_10.png");

            //act
            elemenation.calculateGradientImage(img);
            double[,] exepected=new double[img.width(),img.height()];
            double[,] actual = elemenation.swingSharpness;
            
            //assert
            for (int i = 0; i < img.width(); i++)
                for (int j = 0; j < img.height(); j++)
                    Assert.AreEqual(strat.gradientAtPoint(i, j), actual[i,j]);
            
        }

        [TestMethod]
        public void getSolutionTest()          //Проверка отсева (получения хороших точек)
        {
            //Хорошая точка-значение градиента больше порогового значения
            //arrange
            IMathematical strat = new MathematicialSearchPoint1();   
            Elimination elemenation = new Elimination(strat);
            Data.Image img = new Data.Image("Data\\sample_10.png");
            elemenation.calculateGradientImage(img);      //Вычисление градиента
            //act
            double maxGradient=elemenation.swingSharpness[0,0];
            double minGradient=elemenation.swingSharpness[0,0];
            for (int i = 0; i < img.width(); i++)       //Найти макс и мин
                for (int j = 0; j < img.height(); j++)
                {
                    maxGradient = Math.Max(maxGradient, elemenation.swingSharpness[i, j]);
                    minGradient = Math.Min(minGradient, elemenation.swingSharpness[i, j]);
                }
            double threshold = minGradient + ((maxGradient - minGradient) * 0.1); //порог
            List<Data.Point> exepected=new List<Data.Point>();
            for (int i = 0; i < img.width(); i++)
                for (int j = 0; j < img.height(); j++)
                    if (elemenation.swingSharpness[i, j] >= threshold)
                        exepected.Add(new Data.Point(i, j));
         
            List<Data.Point> actual = new List<Data.Point>();
            actual = elemenation.getSolution();
            //assert
            Debug.WriteLine("Проверка количества точек");
            Assert.AreEqual(actual.Count, exepected.Count);
            Debug.WriteLine("Проверка самих точек");
            int t = 0;
            foreach (Data.Point p1 in exepected)
            {
                Assert.AreEqual(p1.x, actual[t].x);
                Assert.AreEqual(p1.y, actual[t].y);
                t++;
            }
        }
        
    }
}
