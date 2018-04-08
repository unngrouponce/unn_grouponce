using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Preserse;

namespace Get3DModel.UnitTests
{
    [TestClass]
    public class PreserveOBJTests
    {
        [TestMethod]
        public void saveOBJTest()      //Проверка сохранения файла
        {
            //arrange
            string path = "Solution";
            Solution sol = new Solution();
            sol.createdBeginSolution(100, 100);
            string patchconfig="Parser\\sample.camera";
            Setting setting=new Setting(patchconfig);
            IPreserveOBJ PresOBJ = new PreserveOBJ();
            //act
            PresOBJ.saveOBJ(sol, setting, path);
            //assert
            // Assert.AreEqual(exepected, rez);
        }
    }
}
