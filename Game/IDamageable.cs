using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IDamageable
    {
        int HitPoints { get; }
        bool IsDestroyed { get; set; }

        void GetDamage(int damage);
        void Destroy();
    }
}
