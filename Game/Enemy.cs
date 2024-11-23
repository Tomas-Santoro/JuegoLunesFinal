using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class Enemy:Character, IDamageable
    {

        //Implementacion de Interfaz de Daño
        private int hitPoints;
        public int HitPoints => hitPoints;

        public bool IsDestroyed { get; set; }
        public event Action<IDamageable> OnDestroyed;

        //public bool isAlive = true;
        //private int life = 3;

        private Animation idle;
        private Animation walk;

        //private float x;
        //private float y;
        private float speed = 3.0f;
        //private float width = 1.0f;  // Ancho del enemigo
        //private float height = 1.0f; // Alto del enemigo

        public bool isMoving = false;

        //public string texturePath = "Textures/Enemy";

        //public Enemy(string p_texturePath, Vector2 startposition) : base(p_texturePath, startposition)
        //{
        //    texturePath = "Textures/Enemy";
        //    //texturePath = "Textures/Animations/Enemy";
        //    startposition = new Vector2(800.0f, 400.0f);
        //}

        public Enemy() : base("Textures/Animations/Enemy", new Vector2(800.0f, 400.0f))
        {
            //Idle animation
            List<Texture> idleTexture = new List<Texture>();
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/0.png"));
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/1.png"));
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/2.png"));
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/3.png"));
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/4.png"));
            idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/5.png"));

            idle = new Animation("Textures/Animations/Enemy/Idle/", idleTexture, 0.5f, true);

            //Walk Animation
            List<Texture> walkXTexture = new List<Texture>();
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/0.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/1.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/2.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/3.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/4.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/5.png"));


            walk = new Animation("Textures/Animations/Enemy/Walk/", walkXTexture, 0.1f, true);
        }


        //private Animation idle;
        //private Animation walk;

        //private Animation currentAnimation;

        //public Enemy(string p_texturePath = "Textures/Enemy/Idle/0.png")
        //{
        //.    texturePath = p_texturePath;

        //    // Inicializo posición
        //    x = 800;
        //    y = 400;

        //    //Idle animation
        //    List<Texture> idleTexture = new List<Texture>();
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/0.png"));
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/1.png"));
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/2.png"));
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/3.png"));
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/4.png"));
        //    idleTexture.Add(new Texture("Textures/Animations/Enemy/Idle/5.png"));

        //    idle = new Animation("Textures/Animations/Enemy/Idle/", idleTexture, 0.5f, true);

        //    //Walk Animation
        //    List<Texture> walkXTexture = new List<Texture>();
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/0.png"));
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/1.png"));
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/2.png"));
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/3.png"));
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/4.png"));
        //    walkXTexture.Add(new Texture("Textures/Animations/Enemy/Walk/5.png"));


        //    walk = new Animation("Textures/Animations/Enemy/Walk/", walkXTexture, 0.1f, true);

        //    currentAnimation = idle;
        //}


        public void Update()
        {
            if (!isAlive)
            {
               
                GameManager.Instance.ChangeLevel(LevelType.Victory);

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

        //public void TakeDamage()
        //{
        //    life--;
        //    if (life <= 0)
        //    {
        //        Die();
        //    }
        //}

        //private void Die()
        //{

        //    isAlive = false;


        //}

        public bool IsAlive()
        {
            return isAlive;
        }

        private void FollowPlayer()
        {
            isMoving = true;

            float playerX = GameLevel.player.Position.X;
            float playerY = GameLevel.player.Position.Y;



            float directionX = playerX - Position.X;
            float directionY = playerY - Position.Y;

            // Normalizar el vector de dirección para que tenga longitud 1
            float magnitude = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

            // Solo se mueve si el enemigo no está ya en la misma posición que el jugador
            if (magnitude > 0) //si la distancia es mayor a cero se mueve. 
            {
                directionX /= magnitude;
                directionY /= magnitude;

                //Position.X += directionX * speed;
                //Position.Y += directionY * speed;

                Position += new Vector2(directionX * speed, directionY * speed);
            }
        }

        //public void Draw()
        //{
        //    if (!isAlive) return;

        //    var texture = currentAnimation.CurrentFrame;
        //    Engine.Draw(texture, (int)x, (int)y, 1, 1, 0, 0, 0); // Asegurate de dibujar la textura correcta

        //}
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
        //Implementacion de interfaz de daño
        public void GetDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
        //public Vector2 GetPosition()
        //{
        //    return new Vector2(x, y);
        //}

        //public Vector2 GetSize()
        //{
        //    return new Vector2(width, height);
        //}



        //private Animation CreateAnimation(string route, int frames, float speed, bool loop)
        //{
        //    var textures = new List<Texture>();

        //    for (int i = 1; i <= frames; i++)
        //    {
        //        var tt = route + i + ".png";
        //        textures.Add(new Texture(tt));
        //    }

        //    return new Animation(route, textures, speed, loop);
        //}
    }

}
