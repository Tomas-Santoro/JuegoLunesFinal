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

        private EnemyManager() {
            enemyQuantity = 10;
        }


        public int quantity { get => enemyQuantity; set => enemyQuantity = value; }
    }
}
