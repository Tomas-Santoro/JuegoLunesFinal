using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class Enemy:Character, IDamageable, IPoolable
    {

        //Implementacion de Interfaz de Daño
        protected int hitPoints;
        public int HitPoints => hitPoints;

        public bool IsDestroyed { get; set; }

        public bool isMoving = false;

        protected Animation dead;

        public Enemy() : base("Textures/Animations/EnemigoR", new Vector2(800.0f, 400.0f))
        {
            //Idle animation
            List<Texture> idleTexture = new List<Texture>();
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/0.png"));
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/1.png"));
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/2.png"));
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/3.png"));
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/4.png"));
            idleTexture.Add(new Texture("Textures/Animations/EnemigoR/Idle/5.png"));

            idle = new Animation("Textures/Animations/EnemigoR/Idle/", idleTexture, 0.5f, true);

            //Walk Animation
            List<Texture> walkXTexture = new List<Texture>();
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/0.png"));
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/1.png"));
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/2.png"));
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/3.png"));
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/4.png"));
            walkXTexture.Add(new Texture("Textures/Animations/EnemigoR/Walk/5.png"));

            walk = new Animation("Textures/Animations/EnemigoR/Walk/", walkXTexture, 0.1f, true);

            List<Texture> deadTexture = new List<Texture>();
            deadTexture.Add(new Texture("Textures/Animations/Dead/0.png"));
            deadTexture.Add(new Texture("Textures/Animations/Dead/1.png"));
            deadTexture.Add(new Texture("Textures/Animations/Dead/2.png"));
            deadTexture.Add(new Texture("Textures/Animations/Dead/3.png"));
            deadTexture.Add(new Texture("Textures/Animations/Dead/4.png"));

            dead = new Animation("Textures/Animations/Dead/", deadTexture, 0.7f, true);
        }

        public Enemy(string p_texturePath, Vector2 startposition) : base(p_texturePath, startposition)
        {
        }


        public void Update()
        {
            if (!isAlive)
            {
                //COmentado para testear la pool de enemigos
                //GameManager.Instance.ChangeLevel(LevelType.Victory);

                return;
            }
            FollowPlayer();


            if (isMoving)
            {
                Animation = walk; // Asegúrate de que 'walk' esté definida como tu animación de caminar
            }
            else
            {
                Animation = idle; // Cambiar a animación de inactividad si no se está moviendo
            }
            Animation.Update();
        }


        public bool IsAlive()
        {
            return isAlive;
        }

        protected void FollowPlayer()
        {
            isMoving = true;

            float playerX = GameLevel.player.Position.X;
            float playerY = GameLevel.player.Position.Y;



            float directionX = playerX - Position.X;
            float directionY = playerY - Position.Y;

            // Normalizar el vector de dirección para que tenga longitud 1
            float magnitude = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

            // Solo se mueve si el enemigo no está ya en la misma posición que el jugador
            if (magnitude > 30) //si la distancia es mayor a cero se mueve. 
            {
                directionX /= magnitude;
                directionY /= magnitude;

                Position += new Vector2(directionX * speed, directionY * speed);
            }
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
        //Implementacion de interfaz de daño
        public void GetDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints < 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            isAlive = false;
        }
        public void Reset() //funcion de interfaz Ipool
        {
            IsDestroyed = false;
            hitPoints = 1;
        }

    }

}
