using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;
using static imifi_roguelike.Collisions;

namespace imifi_roguelike
{
    class Horror : Entity
    {
        public Horror(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "horror", new Weapon("", 100, "Древний ужас проникает в ваш разум, нанося 100 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 1000;
            hp = 1000;
            give_xp = 1;
            boss = true;
        }
    }
}
