using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Character
    {
        public bool isAlive = false;
        private int life;


        private TransformData transform;
        //private float x;
        //private float y;
        //private float width = 1.0f;  // Ancho del enemigo
        //private float height = 1.0f; // Alto del enemigo

        // private Vector2 size; //tamano de la imagen, no sabemos si se usa o no en algun lado

        private float speed;
        public string texturePath;

        public static Animation idle;
        public static Animation walk;

        private Animation currentAnimation;

        public Character(string p_texturePath, Vector2 startposition)// = "Textures/Enemy/Idle/0.png")    ====>  //Textures/Enemy/
        {
            texturePath = p_texturePath;

            isAlive = true;

            // Inicializo posición
            //x = 800;
            //y = 400;

            //p_texturePath = "Textures/Animations/Enemy";

            //Idle animation

            //List<Texture> idleTexture = new List<Texture>();
            //for (int i = 0; i < 6; i++)
            //{
            //    idleTexture.Add(new Texture(p_texturePath + "/Idle/" + i + ".png"));
            //}
            //idle = new Animation(p_texturePath + "/Idle/", idleTexture, 0.5f, true);

            ////Walk Animation
            //List<Texture> walkXTexture = new List<Texture>();
            //for (int i = 0; i < 6; i++)
            //{
            //    walkXTexture.Add(new Texture(p_texturePath + "/Walk/" + i + ".png"));
            //    Engine.Debug($"cargando la textura de la caminacion: {i}   {p_texturePath}/Walk/{i} .png ");
            //}

            //walk = new Animation(p_texturePath + "/Walk/", walkXTexture, 0.1f, true);

            currentAnimation = idle;
            transform = new TransformData(startposition, new Vector2(1.0f, 1.0f),0.0f);
            //transform.Position = startposition;
            //transform.Rotation = 0f;
            //transform.Scale = new Vector2(1.0f, 1.0f);
        }

        public void TakeDamage()
        {
            life--;
            Engine.Debug($"Golpe recibido");
            if (life <= 0)
            {
                Die();
            }
        }

        private void Die()
        {

            isAlive = false;

        }

        public void Draw()
        {
            if (!isAlive) return;

            var texture = currentAnimation.CurrentFrame;
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Rotation, 0, 0);
            //Engine.Draw(texture, 400, 800, 1, 1, 0, 0, 0);
            //Engine.Draw(texture, (int)x, (int)y, 1, 1, 0, 0, 0); // Asegurate de dibujar la textura correcta

        }

        public Vector2 GetPosition()
        {
            return transform.Position;
        }

        public Vector2 GetSize()
        {
            return transform.Scale;
        }

        private Animation CreateAnimation(string route, int frames, float speed, bool loop)
        {
            var textures = new List<Texture>();

            for (int i = 1; i <= frames; i++)
            {
                var tt = route + i + ".png";
                textures.Add(new Texture(tt));
            }

            return new Animation(route, textures, speed, loop);
        }

        public Animation Animation { get { return currentAnimation; } set => currentAnimation = value; }

        public Vector2 Position { get => transform.Position; set => transform.Position = value; }

        public TransformData Transform { get => transform; }

        public Animation CurrentAnimation { get => currentAnimation; set => currentAnimation = value; }

        //public int Life { get => life; set => life = value; }
    }
}
