using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyManager
    {
        private static EnemyManager instance;
        //public Player player;

        //public static LevelType currentState = LevelType.Game;
        public static EnemyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyManager();
                }
                return instance;
            }
        }

        private int enemyQuantity;
        private List<Enemy> enemies;
        private ObjectPool<Enemy> enemyPool;

        public delegate void Defeat();
        private event Defeat OnDefeat;
        public event Defeat OnEnemyDefeated
        {
            add => OnDefeat += value;
            remove => OnDefeat -= value;
        }

        private EnemyManager() {
            enemyQuantity = 5;
            enemies = new List<Enemy>();
            enemyPool = new ObjectPool<Enemy>(5);
        }

        public int quantity { get => enemyQuantity; set => enemyQuantity = value; }
        //Version original, la modificada no recibe parámetros de entrada
        //public Enemy CreateRandomEnemy(Vector2 position)
        public Enemy CreateRandomEnemy()
        {
            // Generar un número aleatorio entre 1 y 3
            Random random = new Random();
            int enemyType = random.Next(1, 4); // 1, 2, o 3

            Enemy enemyTemplate;
            if (enemyType == 1)
            {
                enemyTemplate = new EnemyN();//("Textures/Animations/Enemy", position);
            }
            else if (enemyType == 2)
            {
                    enemyTemplate = new EnemigoR();//(position);
            }
            else if (enemyType == 3)
            {
                enemyTemplate = new EnemyL();
            }
            else
            {
                throw new ArgumentException("Tipo de enemigo desconocido.");
            }

            enemies.Add(enemyTemplate);
            //enemyTemplate.SetPosition(position);
            return enemyTemplate;
        }

        // Método para liberar un enemigo de vuelta a la pool
        public void ReleaseEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);


            OnDefeat.Invoke();
            enemyPool.ReleaseObject(enemy);
        }

        public void Update()
        {
            //foreach (var enemy in GetEnemies())
            //{
            //    if (!enemy.isAlive)
            //    {
            //        ReleaseEnemy(enemy);
            //        quantity--;
            //    }
            //    else
            //    {
            //        enemy.Update();
            //    }
            //}

            
            for (int i = enemies.Count() - 1; i >= 0; i--) // Iterar de atrás hacia adelante
            {
                Enemy enemy = enemies[i];
                if (!enemy.isAlive)
                {
                    ReleaseEnemy(enemy);
                    Engine.Debug($"El enemigo que tiene que morir es el numero: {i}");
                    //enemies.RemoveAt(i); // Elimina el enemigo

                    Engine.Debug($"Cantidad de enemigos restantes a derrotar es: {quantity}");  
                    quantity--;
                }
                else
                {
                    enemy.Update();
                }
            }

            if (quantity > 0 && GetEnemies().Count() <= 2) {
                CreateRandomEnemy();
            }
        }

        // Método para obtener la lista de enemigos
        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }

}
