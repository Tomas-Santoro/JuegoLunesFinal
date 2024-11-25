using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Renderer
    {
        private Texture texture;

        public Renderer() { 
        }

        public Texture Texture { get => texture; set => texture = value; }

        public void Render(TransformData transform) {
            Engine.Draw(texture, transform.Position.X, transform.Position.Y, transform.Scale.X, transform.Scale.Y, transform.Rotation,0,0);
        }
    }
}
