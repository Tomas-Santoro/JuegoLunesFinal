using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class GameLevel : Level
    {

        //public static string p_texturePathe ="";
        public static Player player = new Player();  //REVISAR ESTO PARA UML agregacion
        
        //Pool de enemigos creada aca
        //decidimos no ponerla aca y dejarla en el enemy manager
        //ObjectPool<Enemy> enemyPool = new ObjectPool<Enemy>(5);

        //public static Vector2 startpos = new Vector2 (0,0);

        //public static Enemy enemy = new Enemy(p_texturePathe, startpos);


        //Enemigo original comentado para testear
        //public static Enemy enemy = new EnemyL();
        
        //  public static EnemigoR enemyR = new EnemigoR(new Vector2());
       // public EnemigoR enemy = new EnemigoR(new Vector2(185f, 185f));
        public static Food food = new Food(new Vector2(200f,500));

        //public EnemyManager = new EnemyManager();

        //public static Enemy enemy = new Enemy("p_texturePathe", new Vector2(0, 0));  //REVISAR ESTO PARA UML agregacion

        private const int width = 20;//27;
        private const int height = 10;// 16;
        private const int listlength = 17;
        private static int[,] tilemap = new int[width, height];
        private string[] tilelist = new string[listlength];

        public GameLevel(Texture background, LevelType p_levelType) : base(background, p_levelType)
        {

            Engine.Debug($"El primer mapa se generó satisfactoriamente");
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

            //tilelist[0] = null;
            for (int i = 0; i < listlength; i++)
            {

                tilelist[i] = "Textures/Terrain/Grass/" + i + ".png";
            }

            player.OnChangedLife += OnLifeChangedHandler;
            player.OnDeathAction += GameManager.Instance.OnDeathHandler;
            Engine.Debug($"Enemigos totales a ser vencidos: {EnemyManager.Instance.quantity}");
            EnemyManager.Instance.OnEnemyDefeated += GameManager.Instance.OnEnemyDefeatedHandler;
         

        }

        private void OnLifeChangedHandler(int life)
        {
            Engine.Debug($"vida del jugador: {life}");
        }

        //public void OnDeathHandler()
        //{
        //    Engine.Debug("aaaaaaaaa");
        //}



        public override void Update()
        {
            player.Update();
            //enemy.Update();
            food.Update();
            EnemyManager.Instance.Update();

            if (CollisionsUtilities.IsBoxColliding(player.GetPosition(), player.GetSize(), food.GetPosition(), food.GetSize()))
            {
                Engine.Debug("healed");
            }


            //for (int i = 0; i < EnemyManager.Instance.GetEnemies().Count(); i++)
            foreach (var enemy in EnemyManager.Instance.GetEnemies())
            {
                // Verifica colisión entre jugador y enemigo
                if (CollisionsUtilities.IsBoxColliding(player.GetPosition(), player.GetSize(),
                    enemy.GetPosition(), enemy.GetSize()))
                {
                    // Si el jugador está atacando y el enemigo está vivo
                    if (player.IsAttacking() && enemy.IsAlive())
                    {
                        enemy.GetDamage(1); // El enemigo recibe daño y puede morir


                        //Hardcodeado para testear********************************************************************************
                        Engine.Debug("El enemigo ha muerto");
                        //enemy = new Enemy();************************************************************************************

                    }
                    else if (enemy.IsAlive())
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
