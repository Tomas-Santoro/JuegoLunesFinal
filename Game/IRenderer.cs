using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IRenderer
    {

        Texture Texture { get; set; }


        float Width { get; set; }
        float Height { get; set; }


        void Draw(TransformData transform);

    }



}

