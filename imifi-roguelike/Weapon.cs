using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imifi_roguelike
{
    public class Weapon
    {
        public string name;
        public int damage;
        public string log;
        public bool fast;

        public Weapon(string name_, int damage_, string log_, bool fast_)
        {
            name = name_;
            damage = damage_;
            log = log_;
            fast = fast_;
        }
    }
}
