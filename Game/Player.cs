﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player : Character
    {
        private SoundPlayer attackSound = new SoundPlayer("Audio/swing.wav");
        private event TestDel OnChangeLifeEvent;
        public event TestDel OnChangedLife
        {
            add => OnChangeLifeEvent += value;
            remove => OnChangeLifeEvent -= value;
        }

        //private int life = 7;
        public int Life
        {
            get => life;
            set
            {
                life = value;
                OnChangeLifeEvent?.Invoke(life);
            }
        }

        public delegate void Death();
        private event Death OnDeath;
        public event Death OnDeathAction
        {
            add => OnDeath += value;
            remove => OnDeath -= value;
        }


        //agregada esta instanciacion de player para poder usar los eventos, revisar
        private static Player instance;

        //public static Player Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Player();
        //        }
        //        return instance;
        //    }
        //}

        private float damageCooldown = 3.0f; // En segundos
        private float damageCooldownTimer = 0f; // Temporizador para enfriamiento de daño

        private bool isAttacking = false;
        private float attackDuration = 0.5f;
        private float attackTimer = 0f;

        private bool isHit = false;
        private float hitDuration = 0.5f; // Duración de la animación de golpe
        private float hitTimer = 0f; // Temporizador para controlar la duración del golpe

        private int direcFlip = 1;


        //private Animation idle;
        //private Animation walk;
        private Animation attack;
        private Animation hit;

        //private float speed = 5.0f;

        public Player() : base("Textures/Animations/Player", new Vector2(150.0f, 150.0f))
        {

            string p_texturePath = "Textures/Animations/Player";

            //Attack Animation
            //List<Texture> attackTexture = new List<Texture>();
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/0.png"));
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/1.png"));
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/2.png"));
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/3.png"));
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/4.png"));
            ////attackTexture.Add(new Texture("Textures/Animations/Player/Attack/5.png"));


            ////attack = new Animation("Textures/Animations/Player/Attack/", attackTexture, 0.1f, true);

            //for (int i = 0; i < 6; i++)
            //{
            //    attackTexture.Add(new Texture("Textures/Animations/Player/Attack/" + i + ".png"));
            //}
            //attack = new Animation("Textures/Animations/Player/Attack/", attackTexture, 0.1f, true);

            attack = CreateAnimation(p_texturePath + "/Attack/", 5, 0.1f, true);

            ////Hit Animation
            //List<Texture> hitTexture = new List<Texture>();
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/0.png"));
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/1.png"));
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/2.png"));
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/3.png"));
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/4.png"));
            ////hitTexture.Add(new Texture("Textures/Animations/Player/Hit/5.png"));

            //for (int i = 0; i < 6; i++)
            //{
            //    hitTexture.Add(new Texture("Textures/Animations/Player/Hit/" + i + ".png"));
            //}

            //hit = new Animation("Textures/Animations/Player/Hit/", hitTexture, 0.5f, true);

            hit = CreateAnimation(p_texturePath + "/Hit/", 5, 0.5f, true);



            Engine.Debug($"cargando las texturas del jugador");

            life = 7;
            speed = 5.0f;
        }



        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        private void StartAttack()
        {
            isAttacking = true;
            attackTimer = 0f;
            CurrentAnimation = attack;
            //attackSound.Play(); Otro Audio por si las dudas, no andan 2 a la vez

        }

        private void EndAttack()
        {
            isAttacking = false;
            CurrentAnimation = idle;

        }

        private void StartHit()
        {
            isHit = true;
            CurrentAnimation = hit;

        }

        private void EndHit()
        {
            isHit = false;
            CurrentAnimation = idle;

        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        private void Die()
        {   

            isAlive = false;
            Engine.Debug("Estoy Muerto");
            OnDeath?.Invoke();
            Engine.Debug("Se invoco al evento de muerte");

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
                    CurrentAnimation.Update();
                    return;
                }
            }

            bool isMoving = false;
            direcFlip = 1;

            if (Engine.GetKey(Keys.J) && !isAttacking)
            {
                StartAttack();
                //Engine.Debug($" La cantidad de enemigos a derrotar es: {EnemyManager.Instance.quantity}");
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
                    if (!GameLevel.CheckCollisions(Position.X, Position.Y - speed, Transform.Scale.X, Transform.Scale.Y))
                    {
                        SetPosition(Position - new Vector2(0.0f, speed));
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.S)) // S
                {
                    if (!GameLevel.CheckCollisions(Position.X, Position.Y + speed + Transform.Scale.Y, Transform.Scale.X, Transform.Scale.Y))
                    {
                        SetPosition(Position + new Vector2(0.0f, speed));
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.A)) // A
                {
                    if (!GameLevel.CheckCollisions(Position.X - speed, Position.Y, Transform.Scale.X, Transform.Scale.Y))
                    {
                        SetPosition(Position - new Vector2(speed, 0.0f));
                        //x -= speed;
                        isMoving = true; 
                        direcFlip = 1;
                    }
                }
                if (Engine.GetKey(Keys.D)) // D
                {
                    if (!GameLevel.CheckCollisions(Position.X + speed + Transform.Scale.X, Position.Y, Transform.Scale.X, Transform.Scale.Y))
                    {
                        SetPosition(Position + new Vector2(speed, 0.0f));
                        isMoving = true;
                    }
                }

                // Cambiar a animación de caminar si se está moviendo
                if (isMoving)
                {
                    CurrentAnimation = walk; // Asegúrate de que 'walk' esté definida como tu animación de caminar
                }
                else
                {
                    CurrentAnimation = idle; // Cambiar a animación de inactividad si no se está moviendo
                }
            }
            CurrentAnimation.Update();

            Debug.Print(life.ToString());
            Debug.Print(CurrentAnimation.ToString());
        }

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
