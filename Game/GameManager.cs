using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameManager
    {
        private static GameManager instance;
        //public Player player;

        //public static LevelType currentState = LevelType.Game;
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

            //player.OnDeathAction += OnDeathHandler;

        }

        public void ChangeLevel(LevelType levelType)
        {
            if (currentLevel != null)
            {
                currentLevel = null;
            }
            switch (levelType)
            {
                case LevelType.Menu:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/SplashScreen3.png"), LevelType.Menu);
                    break;
                case LevelType.Game:
                    currentLevel = new GameLevel(Engine.GetTexture("Textures/Screens/FondoNubes.png"), LevelType.Game);
                    break;
                case LevelType.Defeat:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/ScreenDefeat.png"), LevelType.Defeat);
                    break;
                case LevelType.Victory:
                    currentLevel = new GameLevel(Engine.GetTexture("Textures/Screens/ScreenVictory.png"), LevelType.Victory);
                    break;

            }
        }

        public void OnDeathHandler() {
            Engine.Debug("aaaaaaaaa");
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

}
