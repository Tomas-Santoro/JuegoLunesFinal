using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game
{
    public delegate void TestDel(int a);
    #region Level
    public enum LevelType
    {
        Menu,
        Game,
        Defeat,
        Victory
    }
    

    #endregion


    public class Program
    {
        public const int SCREEN_HEIGHT = 720;
        public const int SCREEN_WIDTH = 1280;


        private static void Main(string[] args)
        {
            Initialization();


            while (true)
            {
                Time.CalculateDeltaTime();
                Update();
                Render();

            }
        }

            private static void Initialization()
            {
                Time.Initialize();
                Engine.Initialize("Paradigmas de programación", SCREEN_WIDTH, SCREEN_HEIGHT);
            }

            private static void Update()
            {
         
                GameManager.Instance.Update();
            }

            private static void Render()
            {
                Engine.Clear(); // Borra la pantalla
                //Textura Terreno
                GameManager.Instance.Render();

                Engine.Show(); // Muestra las imagenes dibujadas

            }
        }

}
