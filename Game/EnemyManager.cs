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

        private EnemyManager() {
            enemyQuantity = 10;
            enemies = new List<Enemy>();
            enemyPool = new ObjectPool<Enemy>();
        }

        public int quantity { get => enemyQuantity; set => enemyQuantity = value; }
    public Enemy CreateRandomEnemy(Vector2 position)
    {
        // Generar un número aleatorio entre 1 y 3
        Random random = new Random();
        int enemyType = random.Next(1, 4); // 1, 2, o 3

        Enemy enemyTemplate;
        if (enemyType == 1)
        {
            enemyTemplate = new Enemy("Textures/Animations/Enemy", position);
        }
        else if (enemyType == 2)
        {
            enemyTemplate = new EnemigoR(position);
        }
        //else if (enemyType == 3)
        //{
        //    enemyTemplate = new EnemyL(position);
        //}
        else
        {
            throw new ArgumentException("Tipo de enemigo desconocido.");
        }

        enemies.Add(enemyTemplate);
        return enemyTemplate;
    }

    // Método para obtener la lista de enemigos
    public List<Enemy> GetEnemies()
    {
        return enemies;
    }
    }







}
