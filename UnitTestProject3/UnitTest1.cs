using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Media;


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
    }
        //[TestMethod]
        //public void TestMethod2()
        //{

        //    Enemy enemigoL = new EnemyL();
        //    //var speed = 1.0f;
        //    //var result = (speed == enemigoL.Speed);
        //    //Assert.IsFalse(result);
        //    var speed = enemigoL.Speed;
        //    Assert.AreEqual(speed, enemigoL.Speed);
        //}

        //    [TestMethod]
        //    public void TestEnemySpeed()
        //    {
        //        // Arrange
        //        Enemy enemigoL = new EnemyL();

        //        // Act
        //        var speed = enemigoL.Speed;

        //        // Assert
        //        var expectedSpeed = 2.0f; // Reemplaza 2.0f con el valor esperado real
        //        Assert.AreEqual(expectedSpeed, speed, "EnemyL's speed did not match the expected value.");
        //    }

        //[TestClass]
        //public class EnemyTests
        //{
        //    [TestMethod]
        //    public void TestEnemyMovement()
        //    {
        //        // Arrange
        //        Enemy enemy = new EnemyL();
        //        float initialX = enemy.Position.X;
        //        float expectedX = initialX - enemy.Speed * Time.DeltaTime;

        //        // Act
        //        enemy.Movement();

        //        // Assert
        //        Assert.AreEqual(expectedX, enemy.Position.X, 0.01f, "El movimiento del enemigo no es correcto.");
        //    }
        //}


        [TestClass]
        public class TextureManagerTests
        {
            [TestMethod]
            public void GetTexture_ShouldReturnExistingTexture_WhenPathExists()
            {
                // Arrange
                string path = "path/to/texture.png";
                TextureManager.GetTexture(path); // Asegurarse de que la textura está en el diccionario

                // Act
                var texture = TextureManager.GetTexture(path);

                // Assert
                var resultadoEsperado = path;
                Assert.AreEqual(resultadoEsperado, texture.Path);
            }
        
            [TestMethod]
            public void GetTexture_ShouldAddNewTexture_WhenPathDoesNotExist()
            {
                // Arrange
                string path = "path/to/new_texture.png";

                // Act
                var texture = TextureManager.GetTexture(path);

                // Assert
                var resultadoEsperado = path;
                Assert.AreEqual(resultadoEsperado, texture.Path);
            }
        }
    //NO BORRAR SON PARTE DEL TEST
        public static class TextureManager
        {
            private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

            public static Texture GetTexture(string path)
            {
                if (textures.ContainsKey(path))
                    return textures[path];
                else
                {
                    var tex = new Texture(path);
                    textures.Add(path, tex);
                    return tex;
                }
            }
        }

        public class Texture
        {
            public string Path { get; private set; }

            public Texture(string path)
            {
                Path = path;
            }
        }

    //***************************************************************


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
    public class EnemyTests
    {
        [TestMethod]
        public void Enemy_FollowsPlayer_WhenFar()
        {
            // Arrange
            var enemy = new Enemy();
            enemy.SetPosition(new Vector2(0, 0));
            GameLevel.player = new Player();
            GameLevel.player.Position = new Vector2(100, 0);

            // Act
            enemy.Update();

            // Assert
            Assert.IsTrue(enemy.Position.X > 0); // Se mueve hacia el jugador
        }
    }








}



