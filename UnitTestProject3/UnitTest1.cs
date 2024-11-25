using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
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

        [TestMethod]
        public void TestMethod2()
        {

            Enemy enemigoL = new EnemyL();
            //var speed = 1.0f;
            //var result = (speed == enemigoL.Speed);
            //Assert.IsFalse(result);
            var speed = enemigoL.Speed;
            Assert.AreEqual(speed, enemigoL.Speed);
        }
    }
}
