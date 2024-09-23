using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
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
}
