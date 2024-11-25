using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemigoR : Enemy
    {
        public EnemigoR() : base("Textures/Animations/EnemigoR", new Vector2(800.0f, 200.0f))
        {
            // Cambiar la velocidad del enemigo
            Engine.Debug("se creo enemigo rapido");
            speed = 6.0f;
        }
    }
}

