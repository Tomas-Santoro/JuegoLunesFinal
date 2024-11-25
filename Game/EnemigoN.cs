using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemyN : Enemy
    {
        public EnemyN() : base("Textures/Animations/Enemy", new Vector2(800.0f, 400.0f))
        {
            speed = 3.0f;
        }
    }
}
