using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Boss : Enemy
    {
        private Animation hit;

        //private bool isHit = false;
        //private float hitDuration = 0.5f; // Duración de la animación de golpe
        //private float hitTimer = 0f; // Temporizador para controlar la duración del golpe

        //private float damageCooldown = 3.0f; // En segundos
        //private float damageCooldownTimer = 0f; // Temporizador para enfriamiento de daño
        public Boss() : base("Textures/Animations/Boss", new Vector2(800.0f, 300.0f))
        {
            //string p_texturePath = "Textures/Animations/Boss";

            //hit = CreateAnimation(p_texturePath + "/Hit/", 1, 0.5f, true);
            Random random = new Random();
            int x = random.Next(4, 7);
            speed = 1.0f * (float)x;
            //hitPoints = 3;
        }

        //private void StartHit()
        //{
        //    isHit = true;
        //    CurrentAnimation = hit;

        //}

        //private void EndHit()
        //{
        //    isHit = false;
        //    isMoving = true;
        //    CurrentAnimation = idle;

        //}

        //public bool CanTakeDamage()
        //{
        //    Engine.Debug($"damageCooldownTimer : {damageCooldownTimer}   -  damageCooldown : {damageCooldown}");
        //    return damageCooldownTimer >= damageCooldown;
        //}

        //public override void GetDamage(int damage)
        //{
        //    //hitPoints -= damage;
        //    //if (hitPoints < 0)
        //    //{
        //    //    Destroy();
        //    //}

        //    if (CanTakeDamage())
        //    {
        //        isMoving = false;
        //        life--;
        //        hitPoints -= damage;
        //        StartHit();
        //        damageCooldownTimer = 0f;
        //        Engine.Debug($" --------------    Vida del jefe : {hitPoints}");
        //    }
        //    if (hitPoints <= 0)
        //    {
        //        Die();
        //    }
        //}

        //public override void Update()
        //{
        //    if (!isAlive)
        //    {
        //        //COmentado para testear la pool de enemigos
        //        //GameManager.Instance.ChangeLevel(LevelType.Victory);

        //        return;
        //    }

        //    if (isHit)
        //    {
        //        hitTimer += Time.DeltaTime;
        //        if (hitTimer >= hitDuration)
        //        {
        //            EndHit();
        //        }
        //        else
        //        {
        //            CurrentAnimation.Update();
        //            return;
        //        }
        //    }


        //    FollowPlayer();


        //    if (isMoving)
        //    {
        //        Animation = walk; // Asegúrate de que 'walk' esté definida como tu animación de caminar
        //    }
        //    else
        //    {
        //        Animation = idle; // Cambiar a animación de inactividad si no se está moviendo
        //    }
        //    Animation.Update();
        //}
    }
}
