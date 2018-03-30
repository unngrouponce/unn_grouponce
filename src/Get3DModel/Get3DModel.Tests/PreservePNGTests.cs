using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Preserse;

namespace Get3DModel.Tests
{
    //Тестирование блока, отвечающего за преобразование изображения высокой резкости и сохранения его в png файл.

    [TestClass]
    public class PreservePNGTests
    {
        [TestMethod]
        public void savePNG_()
        {
            //arrange
            Solution s=new Solution();
            s.createdBeginSolution(10,10);    //Изображение с белыми пикселями
            PreservePNG presPNG=new PreservePNG();
            string path="D:\\";
            //act
            presPNG.savePNG(s, path);
            //assert
            //???
        }
    }
}
