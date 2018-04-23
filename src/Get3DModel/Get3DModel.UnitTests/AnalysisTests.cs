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
    public class AnalysisTests
    {
        [TestMethod]
        public void getDispersionTest()          //Проверка подсчета дисперсии
        {
            //arrange
            List<double> l = new List<double>() { 0, 1, 5.5, 1};
            List<Data.Point> listPoint=new List<Data.Point>();
            Analysis an = new Analysis(listPoint);
            
            //act
            double p=1/l.Count;              //вероятность
            double M=0;                      //мат ожидание
             for (int i=0;i<l.Count;i++)
                M=M+(l[i]*p);
            double D=0;                      //диспесия
            for (int i=0;i<l.Count;i++)
                D=D+Math.Pow((l[i]-M),2)*p;

            double exepected = D;
            double actual = an.getDispersion(l);

            //assert
            Assert.AreEqual(exepected, actual);
        }

        [TestMethod]
        public void getCore()          //Проверка подсчета дисперсии
        {
            //arrange
            List<double> l = new List<double>() { 0, 1, 5.5, 1 };
            List<Data.Point> listPoint = new List<Data.Point>();
            Analysis an = new Analysis(listPoint);

            //act
            double p = 1 / l.Count;              //вероятность
            double M = 0;                      //мат ожидание
            for (int i = 0; i < l.Count; i++)
                M = M + (l[i] * p);
            double D = 0;                      //диспесия
            for (int i = 0; i < l.Count; i++)
                D = D + Math.Pow((l[i] - M), 2) * p;

            double exepected = D;
            double actual = an.getDispersion(l);

            //assert
            Assert.AreEqual(exepected, actual);
        }
    }
}
