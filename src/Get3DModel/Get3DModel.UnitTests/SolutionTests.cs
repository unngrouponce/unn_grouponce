using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;


namespace Get3DModel.UnitTests
{
    [TestClass]
    public class SolutionTests
    {
        [TestMethod]
        public void createdBeginSolution_image10x50_SizeBitmap10x50()
        {

            //Начальное решение представляет Bitmap 10x50

            //arrange
            Solution s = new Solution();
            int w = 10;
            int h = 50;

            Color exepected_color = Color.White;

            //act
            s.createdBeginSolution(w, h);

            //assert
            Assert.AreEqual(s.sharpImage.Width, w);
            Assert.AreEqual(s.sharpImage.Height, h);
        }

        [TestMethod]
        public void createdBeginSolution_image10x20_WhitePix()
        {

            //Начальное решение представляет Bitmap из белых пикселей

            //arrange
            Solution s = new Solution();
            int w = 10;
            int h = 20;

            Color exepected_color = Color.White;

            //act
            s.createdBeginSolution(w, h);
           
            //assert
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    Assert.AreEqual(s.sharpImage.GetPixel(i, j).ToArgb(), exepected_color.ToArgb());
        }

        [TestMethod]
        public void createdBeginSolution_image10x20_minus1()
        {

            //Начальное решение представляет Bitmap 10 на 20 с матрицей высот =-1

            //arrange
            Solution s = new Solution();
            int w = 10;
            int h = 20;

            double exepected_mapHeights = -1;

            //act
            s.createdBeginSolution(w, h);

            //assert
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    Assert.AreEqual(s.getValue(i, j), exepected_mapHeights);
        }
    }
}
