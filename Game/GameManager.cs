using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameManager
    {
        private static GameManager instance;
        private bool bossLevel = false;

        SoundPlayer soundPlayer = new SoundPlayer("Audio/backgroundMusic.wav");
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
        public LevelType CurrentLevelType { get => currentLevel.LevelType; }
        private GameManager()
        {
            ChangeLevel(LevelType.Menu);

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
                    bossLevel = false;
                    soundPlayer.PlayLooping();
                    break;
                case LevelType.Defeat:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/ScreenDefeat.png"), LevelType.Defeat);
                    soundPlayer.Stop();
                    break;
                case LevelType.Victory:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/ScreenVictory.png"), LevelType.Victory);
                    soundPlayer.Stop();
                    break;
                case LevelType.Credits:
                    currentLevel = new MenuLevel(Engine.GetTexture("Textures/Screens/CreditsScreen.png"), LevelType.Credits);
                    break;
                case LevelType.GameB:
                    currentLevel = new GameLevel(Engine.GetTexture("Textures/Screens/FondoNubes.png"), LevelType.GameB);
                    Engine.Debug("Nivel 2Game");
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
            if(Engine.GetKey(Keys.C)) 
            {
                ChangeLevel(LevelType.Credits);
            }
            currentLevel.Update();
        }

        public void OnEnemyDefeatedHandler()
        {
            Engine.Debug($"-------------- La cantidad de enemigos restantes luego de matar al último es {EnemyManager.Instance.quantity}");

            if (EnemyManager.Instance.quantity == 0 && bossLevel == true)
            {
                ChangeLevel(LevelType.Victory);
            }
            else if (EnemyManager.Instance.quantity == 0) 
            {
                ChangeLevel(LevelType.GameB);
                bossLevel = true;
                Engine.Debug("Estamos en el nivel dos vaaaaaamooooooooooooooooooooosssss");
            }
        }

        public void Render()
        {
            currentLevel.Render();
        }
    }

}
