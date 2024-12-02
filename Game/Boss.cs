using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Boss : Enemy
    {
        public Boss() : base("Textures/Animations/Boss", new Vector2(800.0f, 300.0f))
        {
            speed = 4.0f;
        }
    }
}
