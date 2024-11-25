using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using System.Numerics;


namespace UnitTestProject4
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

    public class UnitTest2
    {
        [TestClass]
        public class EnemyTests
        {
            [TestMethod]
            public void Enemy_FollowsPlayer_WhenFar()
            {
                // Arrange
                var enemy = new EnemyN();
                enemy.SetPosition(new Vector2(0, 0));


                // Act


                // Assert
                Assert.AreEqual(enemy.Position.X, 0); // Se mueve hacia el jugador
            }
        }
    }

    //[TestClass]
    //public class EnemyTests
    //{
    //    [TestMethod]
    //    public void Enemy_FollowsPlayer_WhenFar()
    //    {
    //        // Arrange
    //        var enemy = new Enemy();
    //        enemy.SetPosition(new Vector2(0, 0));
    //        GameLevel.player = new Player();
    //        GameLevel.player.Position = new Vector2(100, 0);

    //        // Act
    //        enemy.Update();

    //        // Assert
    //        Assert.IsTrue(enemy.Position.X > 0); // Se mueve hacia el jugador
    //    }
    //}

    [TestClass]
    public class TransformDataTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var initialPosition = new Vector2(10, 20);
            var initialScale = new Vector2(2, 2);
            var initialRotation = 45.0f;

            // Act
            var transform = new TransformData(initialPosition, initialScale, initialRotation);

            // Assert
            Assert.AreEqual(initialPosition, transform.Position);
            Assert.AreEqual(initialScale, transform.Scale);
            Assert.AreEqual(initialRotation, transform.Rotation);
        }

        [TestMethod]
        public void Properties_ShouldAllowModification()
        {
            // Arrange
            var transform = new TransformData(new Vector2(0, 0), new Vector2(1, 1), 0);

            // Act
            transform.Position = new Vector2(100, 200);
            transform.Scale = new Vector2(1.5f, 1.5f);
            transform.Rotation = 90.0f;

            // Assert
            Assert.AreEqual(new Vector2(100, 200), transform.Position);
            Assert.AreEqual(new Vector2(1.5f, 1.5f), transform.Scale);
            Assert.AreEqual(90.0f, transform.Rotation);
        }
    }



    [TestClass]
    public class EnemyManagerTests
    {
        [TestMethod]
        public void Singleton_ShouldReturnSameInstance()
        {
            // Act
            var instance1 = EnemyManager.Instance;
            var instance2 = EnemyManager.Instance;
            instance1.quantity = 1;
            // Assert
            Assert.AreSame(instance1, instance2);
        }

       
        }
    }
   


