using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyL : Enemy
    {
        public EnemyL() : base("Textures/Animations/Enemy3", new Vector2(800.0f, 400.0f))
        {
            speed = 1.0f;
        }
    }
}
