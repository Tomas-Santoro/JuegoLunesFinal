using System;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject4._7._2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 5;
            int y = 6;

            var vector = new Vector2(x, y);
            var resultado = vector.ToString();

            var resultadoEsperado = $"{x}, {y}";

            Assert.AreEqual(resultado, resultadoEsperado);
        }
    }
}
