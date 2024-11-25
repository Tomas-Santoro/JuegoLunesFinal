using System;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
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

    [TestClass]
    public class EnemyTests
    {
        [TestMethod]
        public void Enemy_FollowsPlayer_WhenFar()
        {
           
            var enemy = new EnemigoR();
            enemy.SetPosition(new Vector2(0, 0));
            GameLevel.player = new Player();
            GameLevel.player.Position = new Vector2(100, 0);

            
            enemy.Update();
           

           
            Assert.IsTrue(enemy.Position.X > 0); 
        }
    }

}
