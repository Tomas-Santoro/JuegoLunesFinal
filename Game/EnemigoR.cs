using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class EnemigoR : Enemy
    {
            private float speed = 6.0f;
          private Animation idle;
         private Animation walk;
        public bool isMoving = false;
        public EnemigoR(Vector2 vec) : base("Textures/Animations/EnemigoR",new Vector2(400.0f, 400.0f))
        {
            // Cambiar la velocidad del enemigo
            Engine.Debug("se creo enemigo rapido");
            // Idle animation
            List<Texture> idleTexture = new List<Texture>
        {
            new Texture("Textures/Animations/EnemigoR/Idle/0.png"),
            new Texture("Textures/Animations/EnemigoR/Idle/1.png"),
            new Texture("Textures/Animations/EnemigoR/Idle/2.png"),
            new Texture("Textures/Animations/EnemigoR/Idle/3.png"),
            new Texture("Textures/Animations/EnemigoR/Idle/4.png"),
            new Texture("Textures/Animations/EnemigoR/Idle/5.png")
        };

            idle = new Animation("Textures/Animations/EnemigoR/Idle/", idleTexture, 0.5f, true);

            // Walk Animation
            List<Texture> walkXTexture = new List<Texture>
        {
            new Texture("Textures/Animations/EnemigoR/Walk/0.png"),
            new Texture("Textures/Animations/EnemigoR/Walk/1.png"),
            new Texture("Textures/Animations/EnemigoR/Walk/2.png"),
            new Texture("Textures/Animations/EnemigoR/Walk/3.png"),
            new Texture("Textures/Animations/EnemigoR/Walk/4.png"),
            new Texture("Textures/Animations/EnemigoR/Walk/5.png")
        };

            walk = new Animation("Textures/Animations/EnemigoR/Walk/", walkXTexture, 0.1f, true);
        }
    }






}

