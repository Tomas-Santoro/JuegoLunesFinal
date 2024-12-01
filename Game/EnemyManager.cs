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
            //enemyQuantity = 5;
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
            Engine.Debug($"Cantidad de enemigos restantes a derrotar es: {quantity}");
            quantity--;
            enemies.Remove(enemy);
            OnDefeat.Invoke();
            enemyPool.ReleaseObject(enemy);
        }

        public void Update()
        {
            if (GameManager.Instance.CurrentLevelType == LevelType.Game) {
                //Engine.Debug($"------- Enemy Count = {enemies.Count()}");
                for (int i = enemies.Count() - 1; i >= 0; i--) // Iterar de atrás hacia adelante
                {
                    Enemy enemy = enemies[i];
                    if (!enemy.isAlive)
                    {
                        ReleaseEnemy(enemy);
                        Engine.Debug($"El enemigo que tiene que morir es el numero: {i}");
                        //enemies.RemoveAt(i); // Elimina el enemigo



                    }
                    else
                    {
                        enemy.Update();
                    }
                }


                //quantity = 5
                //Count = actuales
                // cuando count es 3, quantity sigue siendo 5
                // tenemos 5 manzanas para repartir entre 
                // quantity - count > 0
                if (quantity > 0 && GetEnemies().Count() <= 2 && (quantity - GetEnemies().Count() > 0)) {
                    CreateRandomEnemy();
                }
            } else {
                //Engine.Debug("-------------------------- Se supone que estamos en el nivel 2");
                if (quantity > 0 && GetEnemies().Count() <= 1 && (quantity - GetEnemies().Count() > 0))
                {
                    CreateRandomEnemy();
                }
            }
        }

        //public void KillAll() {
        //    foreach (Enemy enemy in enemies) {
        //        if (enemy.isAlive) { 
        //            enemy.isAlive = false;
        //        }
        //    }
        //}

        // Método para obtener la lista de enemigos
        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }

}
