﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class GameLevel : Level
    {


        public static Player player;// = new Player(); 
        
        public static Food food = new Food(new Vector2(200f,500));



        private const int width = 20;//27;
        private const int height = 10;// 16;
        private const int listlength = 17;
        private static int[,] tilemap = new int[width, height];
        private string[] tilelist = new string[listlength];

        public GameLevel(Texture background, LevelType p_levelType) : base(background, p_levelType)
        {

            Engine.Debug($"El mapa se generó satisfactoriamente");
            //Creacion mapa de tiles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //este codigo pinta todo con bordes, pero pinta una linea feita
                    //creo que es un error de recorte

                    if (i == 0)
                        if (j == 0)
                            tilemap[i, j] = 0;
                        else if (j == height - 1)
                            tilemap[i, j] = 9;
                        else tilemap[i, j] = 5;
                    else if (i == width - 1)
                        if (j == 0)
                            tilemap[i, j] = 2;
                        else if (j == height - 1)
                            tilemap[i, j] = 11;
                        else tilemap[i, j] = 7;
                    else if (j == 0)
                        tilemap[i, j] = 1;
                    else if (j == height - 1)
                        tilemap[i, j] = 10;
                    else tilemap[i, j] = 6;


                }
            }

            player = new Player(); 

            //tilelist[0] = null;
            for (int i = 0; i < listlength; i++)
            {
                if (p_levelType == LevelType.Game)
                {
                    tilelist[i] = "Textures/Terrain/Grass/" + i + ".png";
                }
                else {
                    tilelist[i] = "Textures/Terrain/Farm/" + i + ".png";
                }
            }



            if (p_levelType == LevelType.Game)
            {
                player.OnChangedLife += OnLifeChangedHandler;
                player.OnDeathAction += GameManager.Instance.OnDeathHandler;
                Engine.Debug($"Enemigos totales a ser vencidos: {EnemyManager.Instance.quantity}");
                EnemyManager.Instance.OnEnemyDefeated += GameManager.Instance.OnEnemyDefeatedHandler;
                EnemyManager.Instance.quantity = 5;
            }
            else if (p_levelType == LevelType.GameB) {
                EnemyManager.Instance.quantity = 15;
                EnemyManager.Instance.SetBossLevel();
                Engine.Debug("*************************El nivel 2 se cargó satisfactoriamente");
            }
         
            

        }

        private void OnLifeChangedHandler(int life)
        {
            Engine.Debug($"vida del jugador: {life}");
        }

        public override void Update()
        {
            player.Update();
            //enemy.Update();
            food.Update(player);
            EnemyManager.Instance.Update();

            foreach (var enemy in EnemyManager.Instance.GetEnemies())
            {

                //Nueva colisión entre Jugador y Enemigo
                // Si el jugador está atacando y el enemigo está vivo
                if (player.IsAttacking() && enemy.IsAlive())
                {
                    if (CollisionsUtilities.IsCircleColliding(player.GetPosition() + (player.GetSize() / 2), 45, enemy.GetPosition() + (enemy.GetSize() / 2), 30))
                    //if (CollisionsUtilities.IsBoxColliding(player.GetPosition(), player.GetSize(), enemy.GetPosition(), enemy.GetSize()))
                    {
                        Engine.Debug("Chequeo de colisiones con los enemigos");
                        Engine.Debug($"Jugador {player.GetPosition() + (player.GetSize() / 2)}");
                        Engine.Debug($"Enemigo {enemy.GetPosition() + (enemy.GetSize() / 2)}");
                        enemy.GetDamage(1); // El enemigo recibe daño y puede morir
                        Engine.Debug("El enemigo ha muerto");
                    }
                }
                else if (enemy.IsAlive()) {
                    //CollisionsUtilities.IsCircleColliding(player.GetPosition() + (player.GetSize()/2), 30,food.GetPosition() + (food.GetSize()/2),30)
                    if (CollisionsUtilities.IsCircleColliding(player.GetPosition() + (player.GetSize() / 2), 20, enemy.GetPosition() + (enemy.GetSize() / 2), 20))
                    {
                        player.TakeDamage(); // El jugador recibe daño si no está atacando
                    }
                }

            }

            //Puesto solo para testear********************************************************************************
            if (Engine.GetKey(Keys.H))
            {
                Engine.Debug($"Cantidad de enemigos a derrotar: {EnemyManager.Instance.quantity}");
            }
            //*********************************************************************************************************
        }
        public override void Render()
        {

            Engine.Draw(background);

            if(EnemyManager.Instance.quantity >= 0)
            {
                DrawMap();

                player.Draw();


            }

            food.Draw();

            foreach (var enemy in EnemyManager.Instance.GetEnemies())
            {
                enemy.Draw();
            }

            
        }

        private void DrawMap()
        {

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tilemap[i, j] >= 0 && tilemap[i, j] != null)
                    {
                        Engine.Draw(tilelist[tilemap[i, j]], i * 64, j * 64, 1, 1, 0, 0, 0); // Usar 'x' e 'y' para la posición
                    }
                }
            }
        }

        public static bool CheckCollisions(float futureX, float futureY, float widthCharacter, float heightCharacter)
        {
            bool result = false;

            int tileX = (int)futureX / 64; //se redondea para abajo la division
            int tileY = (int)futureY / 64; //se redondea para abajo la division

            if (tileX < 0 || tileY < 0 || tileX >= width - 2 || tileY >= height - 2) 
            {
                return true; //si está fuera del mapa, se considera colisión
            }

            //aca se va a aplicar una logica mejor diferenciando los tipos de bloques en un futuro
            if (tilemap[tileX, tileY] == -1)
                result = true;
            else
                result = false;

            return result;
        }
    }
}
