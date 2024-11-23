using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Game
{
    public class Food : IGameObject
    {
        private TransformData transform;

        public float x;
        public float y;

        private float width = 1.0f;  
        private float height = 1.0f; 

        float swapAnim = 0f;
        public bool isActive = true;

        public string texturePath;
        private Animation idle;

        private Animation currentAnimation;
        public Food(Vector2 startposition, string p_texturePath = "Textures/Animations/Resources/Food/Idle/4.png") 
        { 
            texturePath = p_texturePath;

            List<Texture> idleTexture = new List<Texture>();
            idleTexture.Add(new Texture("Textures/Animations/Resources/Food/Idle/4.png"));
            idleTexture.Add(new Texture("Textures/Animations/Resources/Food/Idle/5.png"));
            idleTexture.Add(new Texture("Textures/Animations/Resources/Food/Idle/6.png"));


            idle = new Animation("Textures/Animations/Player/Idle/", idleTexture, 0.25f, true);

            currentAnimation = idle;

            transform = new TransformData(startposition, new Vector2(1.0f, 1.0f), 0.0f);
            x = 185;
            y = 150;
        }

        public void Draw()
        {
            if (!isActive)
            {
                return;
            }
            var texture = currentAnimation.CurrentFrame;
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Rotation, 0, 0);
        }

        public void SetPosition(Vector2 position)
        {
            x = transform.Position.X;
            y = transform.Position.Y;
        }
        public Vector2 GetPosition()
        {
            return transform.Position;
        }

        public Vector2 GetSize()
        {
            return transform.Scale;
        }
        public void Update()
        {
            if (!isActive)
            {
                return;
            }
            currentAnimation.Update();
        }

        public void Destroy()
        {
            isActive = false;
        }

    }
       
}
