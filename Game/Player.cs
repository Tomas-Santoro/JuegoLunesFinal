using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player
    {
        private event TestDel OnChangeLifeEvent;
        public event TestDel OnChangedLife
        {
            add => OnChangeLifeEvent += value;
            remove => OnChangeLifeEvent -= value;
        }

        private int life = 3;
        public int Life
        {
            get => life;
            set
            {
                life = value;
                OnChangeLifeEvent?.Invoke(life);
            }
        }

        public bool isAlive = true;

        private float damageCooldown = 3.0f; // En segundos
        private float damageCooldownTimer = 0f; // Temporizador para enfriamiento de daño

        private bool isAttacking = false;
        private float attackDuration = 0.5f;
        private float attackTimer = 0f;

        private bool isHit = false;
        private float hitDuration = 0.5f; // Duración de la animación de golpe
        private float hitTimer = 0f; // Temporizador para controlar la duración del golpe

        public float x;
        public float y;

        private float width = 1.0f;  // Ancho del jugador
        private float height = 1.0f; // Alto del jugador

        private int direcFlip = 1;


        public string texturePath;

        private Animation idle;
        private Animation walkX;
        private Animation attack;
        private Animation hit;

        private Animation currentAnimation;

        private float speed = 5.0f;

        public Player(string p_texturePath = "Textures/Knight/Idle/0.png")
        {
            texturePath = p_texturePath;

            //Idle animation
            List<Texture> idleTexture = new List<Texture>();
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/0.png"));
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/1.png"));
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/2.png"));
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/3.png"));
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/4.png"));
            idleTexture.Add(new Texture("Textures/Animations/Player/Idle/5.png"));

            idle = new Animation("Textures/Animations/Player/Idle/", idleTexture, 0.5f, true);

            //Walk Animation
            List<Texture> walkXTexture = new List<Texture>();
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/0.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/1.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/2.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/3.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/4.png"));
            walkXTexture.Add(new Texture("Textures/Animations/Player/Walk/5.png"));


            walkX = new Animation("Textures/Animations/Player/Walk/", walkXTexture, 0.1f, true);

            //Attack Animation
            List<Texture> attackTexture = new List<Texture>();
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/0.png"));
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/1.png"));
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/2.png"));
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/3.png"));
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/4.png"));
            attackTexture.Add(new Texture("Textures/Animations/Player/Attack/5.png"));


            attack = new Animation("Textures/Animations/Player/Attack/", attackTexture, 0.1f, true);

            //Hit Animation
            List<Texture> hitTexture = new List<Texture>();
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/0.png"));
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/1.png"));
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/2.png"));
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/3.png"));
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/4.png"));
            hitTexture.Add(new Texture("Textures/Animations/Player/Hit/5.png"));

            hit = new Animation("Textures/Animations/Player/Hit/", hitTexture, 0.5f, true);

            currentAnimation = idle;

            //Posicion inicial (no encontre donde lo seteaban)
            x = 150;
            y = 150;
        }

        public void SetPosition(Vector2 position)
        {
            x = position.X;
            y = position.Y;
        }

        private void StartAttack()
        {
            isAttacking = true;
            attackTimer = 0f;
            currentAnimation = attack;

        }

        private void EndAttack()
        {
            isAttacking = false;
            currentAnimation = idle;

        }

        private void StartHit()
        {
            isHit = true;
            currentAnimation = hit;

        }

        private void EndHit()
        {
            isHit = false;
            currentAnimation = idle;

        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        private void Die()
        {
            isAlive = false;
            Engine.Debug("Estoy Muerto");
            GameManager.Instance.ChangeLevel(LevelType.Defeat);
        }
        public bool CanTakeDamage()
        {
            return damageCooldownTimer >= damageCooldown;
        }
        public void TakeDamage()
        {

            if (CanTakeDamage()) //INVOKE
            {
                Life--;
                StartHit();
                damageCooldownTimer = 0f;
            }
            if (Life <= 0)
            {
                Die();
            }
        }

        public void Update()
        {
            if (!isAlive)
            {
                return;
            }

            damageCooldownTimer += Time.DeltaTime;

            if (isHit)
            {
                hitTimer += Time.DeltaTime;
                if (hitTimer >= hitDuration)
                {
                    EndHit();
                }
                else
                {
                    currentAnimation.Update();
                    return;
                }
            }

            bool isMoving = false;
            direcFlip = 1;

            if (Engine.GetKey(Keys.J) && !isAttacking)
            {
                StartAttack();
            }

            if (isAttacking)
            {
                attackTimer += Time.DeltaTime;
                if (attackTimer >= attackDuration)
                {
                    EndAttack();
                }
            }
            else
            {
                // Lógica de movimiento
                if (Engine.GetKey(Keys.W)) // W
                {
                    if (!GameLevel.CheckCollisions(x, y - speed, width, height))
                    {
                        y -= speed;
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.S)) // S
                {
                    if (!GameLevel.CheckCollisions(x, y + speed + height, width, height))
                    {
                        y += speed;
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.A)) // A
                {
                    if (!GameLevel.CheckCollisions(x - speed, y, width, height))
                    {
                        x -= speed;
                        isMoving = true;
                        direcFlip = 1;
                    }
                }
                if (Engine.GetKey(Keys.D)) // D
                {
                    if (!GameLevel.CheckCollisions(x + speed + width, y, width, height))
                    {
                        x += speed;
                        isMoving = true;
                    }
                }

                // Cambiar a animación de caminar si se está moviendo
                if (isMoving)
                {
                    currentAnimation = walkX; // Asegúrate de que 'walk' esté definida como tu animación de caminar
                }
                else
                {
                    currentAnimation = idle; // Cambiar a animación de inactividad si no se está moviendo
                }
            }
            currentAnimation.Update();

            Debug.Print(life.ToString());
            Debug.Print(currentAnimation.ToString());
        }

        public void Draw()
        {
            if (!isAlive)
            {
                return;
            }
            var texture = currentAnimation.CurrentFrame;
            Engine.Draw(texture, (int)x, (int)y, direcFlip, 1, 0, 0, 0); // Asegúrate de dibujar la textura correcta
        }

        public Vector2 GetPosition()
        {
            return new Vector2(x, y);
        }

        public Vector2 GetSize()
        {
            return new Vector2(width, height);
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
    }

}
