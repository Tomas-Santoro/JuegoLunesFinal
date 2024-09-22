using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game
{
    public delegate void TestDel(int a);
    #region Level
    public enum LevelType
    {
        Menu,
        Game
    }
    public abstract class Level
    {
        protected Texture background;
        protected LevelType levelType;

        public LevelType LevelType => levelType; 
        public Level(Texture background, LevelType levelType)
        {
            this.background = background;
            this.levelType = levelType;
        }

        public abstract void Update();
        public abstract void Render();
    }

    public class MenuLevel : Level
    {
        //private List<Button> buttons;
        public MenuLevel(Texture background, LevelType p_levelType) : base(background, p_levelType)
        {

        }

        public override void Update()
        {
        }

        public override void Render()
        {
            Engine.Draw(background);
        }
    }
    public class GameLevel : Level
    {
        public static Player player = new Player();  //REVISAR ESTO PARA UML 
        public static Enemy enemy = new Enemy();  //REVISAR ESTO PARA UML 

        private const int width = 27;//27
        private const int height = 16;//16
        private const int listlength = 17;
        private static int[,] tilemap = new int[width, height];
        private string[] tilelist = new string[listlength];
        //private List<Button> buttons;

        public GameLevel(Texture background, LevelType p_levelType) : base(background, p_levelType)
        {
            //Creation of tile map
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //este codigo pinta todo con bordes, pero pinta una linea feita
                    //creo que es un error de recorte
                    
                    if (i == 0)
                        if (j == 0)
                            tilemap[i, j] = 0;
                        else if (j == height - 1)
                            tilemap[i, j] = 9;
                        else tilemap[i, j] = 5;
                    else if (i == width - 1)
                        if (j == 0)
                            tilemap[i, j] = 2;
                        else if (j == height - 1)
                            tilemap[i, j] = 11;
                        else tilemap[i, j] = 7;
                    else if (j == 0)
                        tilemap[i, j] = 1;
                    else if (j == height - 1)
                        tilemap[i, j] = 10;
                    else tilemap[i, j] = 6;
                    



                    //codigo de prueba para ver algo 

                   /* if (i == 0)
                        if (j == 0)
                            tilemap[i, j] = -1;
                        else if (j == height - 1)
                            tilemap[i, j] = -1;
                        else tilemap[i, j] = -1;
                    else if (i == width - 1)
                        if (j == 0)
                            tilemap[i, j] = -1;
                        else if (j == height - 1)
                            tilemap[i, j] = -1;
                        else tilemap[i, j] = -1;
                    else if (j == 0)
                        tilemap[i, j] = -1;
                    else if (j == height - 1)
                        tilemap[i, j] = -1;
                    else if (i % 2 == 0 || j % 2 == 0)
                        tilemap[i, j] = 6;
                    else tilemap[i, j] = 6;//-1;
                    */
                }
            }

            //tilelist[0] = null;
            for (int i = 0; i < listlength; i++)
            {

                tilelist[i] = "Textures/Terrain/Grass/" + i + ".png";
            }

            player.OnChangedLife += OnLifeChangedHandler;
        }

        private void OnLifeChangedHandler(int life)
        {
            Engine.Debug($"vida del jugador: {life}");
        }
        public override void Update()
        {
            player.Update();
            enemy.Update();

            if (Engine.GetKey(Keys.SPACE))
            {
                player.Life --;           }
        }

        public override void Render()
        {
            Engine.Draw(background);

            drawMap();

            player.Draw();
            enemy.Draw();
        }

        private void drawMap()
        {

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tilemap[i, j] >= 0 && tilemap[i, j] != null)
                    {
                        Engine.Draw(tilelist[tilemap[i, j]], i * 64, j * 64, 1, 1, 0, 0, 0); // Usar 'x' e 'y' para la posición
                    }
                }
            }
        }

        public static bool CheckCollisions(float x, float y, float futureX, float futureY, float widthCharacter, float heightCharacter) 
        {
            bool result = false;

            int tileX = (int) futureX / 64;
            int tileY = (int) futureY / 64;

            if (tileX < 0 || tileY < 0 || tileX >= width || tileY >= height)
            {
                return true; // Si está fuera del mapa, se considera colisión
            }

            //aca se va a aplicar una logica mejor diferenciando los tipos de bloques
            if (tilemap [tileX, tileY] == -1)
                result = true;
            else
                result = false;

            return result;
        }
    }
    #endregion

    #region GameManager
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        private Level currentLevel;
        private GameManager()
        {
            ChangeLevel(LevelType.Menu);
        }
        public void ChangeLevel(LevelType levelType) 
        {
            if(currentLevel != null)
            {
                currentLevel = null;
            }
            switch(levelType)
            {
                case LevelType.Menu:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/SplashScreen.png"), LevelType.Menu);
                    break;
                case LevelType.Game:
                    currentLevel = new GameLevel(Engine.GetTexture("Textures/Screens/Level.png"), LevelType.Game);
                    break;
            }
        }
        public void Update()
        {
            if (Engine.GetKey(Keys.Q))
            {
               ChangeLevel(LevelType.Menu);
            }
            if (Engine.GetKey(Keys.E))
            {
                ChangeLevel(LevelType.Game);
            }
            currentLevel.Update();
        }
        public void Render()
        {
            currentLevel.Render();
        }
    }
    #endregion

    #region Player
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
        //private bool life = true;

        public float x;
        public float y;
        private float width = 1.0f;  // Ancho del jugador
        private float height = 1.0f; // Alto del jugador
        private int direcFlip = 1;


        public string texturePath;

        private Animation idle;
        private Animation walkX;
        private Animation attack;

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
            //idleTexture.Add(new Texture("Textures/Animations/Player/Idle/5.png"));

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


            attack = new Animation("Textures/Animations/Player/Attack/", attackTexture, 0.1f , true);

            currentAnimation = idle;

            //Posicion inicial (no encontre donde lo seteaban)
            x = 150;
            y = 150;
        }

        public void Attack()
        {
            currentAnimation = attack;
        }
        private void Kill()
        {
            isAlive = false;
            Engine.Debug("Estoy Muerto");
        }
        public void GetDamage()
        {

        }

        public void Update()
        {
            if (!isAlive)
            {
                return;
            }

            bool isMoving = false;
            direcFlip = 1;

            if (Engine.GetKey(Keys.J))
            {
                currentAnimation = attack;
            }
            else
            {
                // Lógica de movimiento
                if (Engine.GetKey(Keys.W)) // W
                {
                    if (!GameLevel.CheckCollisions(x, y, x, y - speed, width, height))
                    {
                        y -= speed;
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.S)) // S
                {
                    if (!GameLevel.CheckCollisions(x, y, x, y + speed + height, width, height))
                    {
                        y += speed;
                        isMoving = true;
                    }
                }
                if (Engine.GetKey(Keys.A)) // A
                {
                    if (!GameLevel.CheckCollisions(x, y, x - speed, y, width, height))
                    {
                        x -= speed;
                        isMoving = true;
                        direcFlip = -1;
                    }
                }
                if (Engine.GetKey(Keys.D)) // D
                {
                    if (!GameLevel.CheckCollisions(x, y, x + speed + width, y, width, height))
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
    #endregion


    #region Enemy
    public class Enemy
    {
        public bool isAlive = true;
        private int life = 3;

        private float x;
        private float y;
        private float speed = 3.0f;
        private float width = 1.0f;  // Ancho del enemigo
        private float height = 1.0f; // Alto del enemigo

        public string texturePath;

        private Animation test;
        private Animation idle;

        private Animation currentAnimation;

        public Enemy(string p_texturePath = "Textures/Enemy/Idle/0.png")
        {
            texturePath = p_texturePath;

            // Inicializo posición
            x = 1500; 
            y = 750;  

            List<Texture> pp = new List<Texture>();
            pp.Add(new Texture("Textures/Animations/Enemy/Idle/0.png"));
            pp.Add(new Texture("Textures/Animations/Enemy/Idle/1.png"));

            test = new Animation("Textures/Animations/Enemy/Idle/", pp, 1, true);
            idle = CreateAnimation("Textures/Knight/Idle/", 6, 1f, true);

            currentAnimation = test;
        }

        private void Kill()
        {
            isAlive = false;
            Engine.Debug("Estoy Muerto");
        }

        public void GetDamage()
        {
            if (!isAlive) return;

            life--;
            if (life <= 0) Kill();
        }

        public void Update()
        {
            if (!isAlive) return;

            FollowPlayer(); 
            currentAnimation.Update();
           
        }

        private void FollowPlayer()
        {
           
            float playerX = Program.player.x; 
            float playerY = Program.player.y;

          
            float directionX = playerX - x;
            float directionY = playerY - y;

            // Normalizar el vector de dirección para que tenga longitud 1
            float magnitude = (float)Math.Sqrt(directionX * directionX + directionY * directionY);

            // Solo se mueve si el enemigo no está ya en la misma posición que el jugador
            if (magnitude > 0) //si la distancia es mayor a cero se mueve. 
            {
                directionX /= magnitude;
                directionY /= magnitude;

                x += directionX * speed;
                y += directionY * speed;
            }
        }

        public void Draw()
        {
            if (!isAlive) return;

            var path = test.Id + (test.currentFrameIndex + 1) + ".png";

            
            Engine.Draw(path, (int)x, (int)y, 1, 1, 0, 0, 0);
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

    #endregion Enemy

    public class Program
        {
            public const int SCREEN_HEIGHT = 720;
            public const int SCREEN_WIDTH = 1280;

            public static Player player = new Player();
            public static Enemy enemy = new Enemy();

            private static void Main(string[] args)
            {
                Initialization();

                while (true)
                {
                    Time.CalculateDeltaTime();
                    Update();
                    Render();
                }
            }

            private static void Initialization()
            {
                Time.Initialize();
                Engine.Initialize("Paradigmas de programación", SCREEN_WIDTH, SCREEN_HEIGHT);
            }

            private static void Update()
            {
                player.Update();
                enemy.Update();
         
            if (CollisionsUtilities.IsBoxColliding(
                player.GetPosition(), player.GetSize(),
                enemy.GetPosition(), enemy.GetSize()))
            {
                // Colisión detectada: se puede reducir la vida del jugador, aplicar un efecto, etc.
                Engine.Debug("HIT");
             
            }
                GameManager.Instance.Update();
            }

            private static void Render()
            {
                Engine.Clear(); // Borra la pantalla
                //Textura Terreno
                GameManager.Instance.Render();

                Engine.Show(); // Muestra las imagenes dibujadas
            }
        }
    }
