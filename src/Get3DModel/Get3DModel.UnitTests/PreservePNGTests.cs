using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Preserse;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class PreservePNGTests
    {
        [TestMethod]
        public void savePNGTest()      //Проверка сохранения OBJ файла
        {
            //arrange
            string path = "Solution";
            Solution sol = new Solution();
            sol.createdBeginSolution(100, 100);
            PreservePNG PresPNG = new PreservePNG();
            //act
            PresPNG.savePNG(sol, path);
            //assert
            // Assert.AreEqual(exepected, rez);
        }
    }
}
