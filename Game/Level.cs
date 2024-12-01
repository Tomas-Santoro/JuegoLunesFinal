using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum LevelType
    {
        Menu,
        Game,
        Defeat,
        Victory,
        Credits,
        GameB

    }
    public abstract class Level
    {
        protected Texture background; //Composicion, no puede existir el nivel sin la textura (background) 
        protected LevelType levelType;


        public LevelType LevelType => levelType;
        public Level(Texture background, LevelType levelType)  //Si un objeto Level es destruido, el objeto Texture asociado también será destruido.

        {
            this.background = background;
            this.levelType = levelType; //composicion  misma explicacion que el de arriba

        }

        public abstract void Update();
        public abstract void Render();
    }



   
}
