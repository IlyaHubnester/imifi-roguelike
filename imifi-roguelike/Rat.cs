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
    class Rat : Entity
    {
        public Rat(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "rat", new Weapon("Когти", 5, "Крыса царапает вас когтями, нанося 5 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 15;
            hp = 15;
            give_xp = 5;
        }
    }

    class Orc : Entity
    {
        public Orc(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "orc", new Weapon("", 25, "Орк дубасит вас дубиной по голове, нанося 25 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 40;
            hp = 40;
            give_xp = 15;
        }
    }
    class Slime : Entity
    {
        public Slime(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "slime", new Weapon("", 10, "Слизень напрыгивает на вас, нанося 10 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 30;
            hp = 30;
            give_xp = 10;
        }
    }
    class Demon : Entity
    {
        public Demon(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "demon", new Weapon("", 40, "Демон поджигает вас адским пламенем, нанося 40 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 70;
            hp = 70;
            give_xp = 50;
        }
    }
    class Dragon : Entity
    {
        public Dragon(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "dragon", new Weapon("", 70, "Дракон кусает вас за палец, нанося 70 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 100;
            hp = 100;
            give_xp = 50;
        }
    }
    class Troll : Entity
    {
        public Troll(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "troll", new Weapon("", 25, "Тролль плюёт вам в лицо, нанося 25 урона.", false))
        {
            damage = weapon.damage;
            maxhp = 40;
            hp = 40;
            give_xp = 15;
        }
    }
    class Uwuslime : Entity
    {
        public Uwuslime(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "uwuslime", new Weapon("", 0, "Увуслайм обнимает вас.", false))
        {
            damage = weapon.damage;
            maxhp = 10000;
            hp = 10000;
            give_xp = 0;
        }
    }
}
