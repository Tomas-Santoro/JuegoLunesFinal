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
        protected int life;


        protected TransformData transform;

        protected float speed;
        public string texturePath;

        protected Animation idle;
        protected Animation walk;

        protected Animation currentAnimation;

        protected Renderer render;

        public Character(string p_texturePath, Vector2 startposition)// = "Textures/Enemy/Idle/0.png")    ====>  //Textures/Enemy/
        {
            texturePath = p_texturePath;

            isAlive = true;

            //Idle animation

            List<Texture> idleTexture = new List<Texture>();
            for (int i = 0; i < 6; i++)
            {
                idleTexture.Add(new Texture(p_texturePath + "/Idle/" + i + ".png"));
            }
            idle = new Animation(p_texturePath + "/Idle/", idleTexture, 0.5f, true);

            //Walk Animation
            List<Texture> walkXTexture = new List<Texture>();
            for (int i = 0; i < 6; i++)
            {
                walkXTexture.Add(new Texture(p_texturePath + "/Walk/" + i + ".png"));
                Engine.Debug($"cargando la textura de la caminacion: {i}   {p_texturePath}/Walk/{i} .png ");
            }

            walk = new Animation(p_texturePath + "/Walk/", walkXTexture, 0.1f, true);

            currentAnimation = idle;
            transform = new TransformData(startposition, new Vector2(1.0f, 1.0f),0.0f);
            //Position = startposition;
            //Rotation = 0f;
            //Scale = (1.0f, 1.0f);
            render = new Renderer();
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

        protected void Die()
        {

            isAlive = false;

            //Puesto aca para testear, sería el contador de enemigos***************************************************
            EnemyManager.Instance.quantity--;
            //*********************************************************************************************************

        }

        public void Draw()
        {
            if (!isAlive) return;


            //Nueva version usando el componente Render
            render.Texture = currentAnimation.CurrentFrame;
            render.Render(transform);

        }

        public Vector2 GetPosition()
        {
            return transform.Position;
        }

        public Vector2 GetSize()
        {
           
                
            return new Vector2(currentAnimation.CurrentFrame.Width * transform.Scale.X,currentAnimation.CurrentFrame.Height * transform.Scale.Y);

            //return transform.Scale;
        }

        protected Animation CreateAnimation(string route, int frames, float speed, bool loop)
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

        public float Speed { get => speed; set => speed = value; }

    }
}
