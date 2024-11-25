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

        float swapAnim = 0f;
        public bool isActive = true;

        public string texturePath;
        private Animation idle;

        private Animation currentAnimation;

        int healPoints = 1;
        public int Heal => healPoints;

        public bool IsDestroyed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            transform.Scale = new Vector2(1.0f, 1.0f);  
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

        public Vector2 Position { get => transform.Position; set => transform.Position = value; }

        public Vector2 GetPosition()
        {
            return transform.Position;
        }

        public Vector2 GetSize()
        {

            return new Vector2(currentAnimation.CurrentFrame.Width * transform.Scale.X, currentAnimation.CurrentFrame.Height * transform.Scale.Y); ;

            //return transform.Scale;
        }
        public void Update(Player player)
        {
            if (!isActive)
            {
                return;
            }
            currentAnimation.Update();
            Collision(player);
            
        }

        public void Destroy()
        {
            isActive = false;
        }
        
        public void Collision(Player player)
        {
            if (CollisionsUtilities.IsCircleColliding(player.GetPosition() + (player.GetSize() / 2), 30, GetPosition() + (GetSize() / 2), 30))
            {
                GetHeal(1, player);



            }
               // Engine.Debug($"NO se CURA         {GetPosition()} pos jugador  {Player.Instance.Position}" );
        }

        public void GetHeal(int heal,Player player)
        {
            player.Life += heal;
            Engine.Debug($"Food healed by {heal}, total heal points: {healPoints}");
            Destroy();

        }
    }
       
}
