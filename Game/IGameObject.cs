using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IGameObject
    {
        int Heal { get;}
        bool IsDestroyed { get; set; }

        void GetHeal(int heal);

        void Update();
        
        void Draw();
    }
}
