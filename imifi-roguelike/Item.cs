using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imifi_roguelike
{
    abstract class Item
    {
        public string name;

        public abstract void Use();
    }

    class StrengthPotion : Item
    {
        public StrengthPotion()
        {
            name = "Зелье силы";

        }
        public override void Use()
        {
            GameLoop.player.ChangeDamageModifier(GameLoop.player.damage_modifier + 10);
        }
    }
    class WeaknessPotion : Item
    {
        public WeaknessPotion()
        {
            name = "Зелье слабости";
        }

        public override void Use()
        {
            GameLoop.player.ChangeDamageModifier(GameLoop.player.damage_modifier - 10);
        }
    }
    class HealingPotion : Item
    {
        public HealingPotion()
        {
            name = "Зелье лечения";
        }

        public override void Use()
        {
            GameLoop.player.Heal(15);
        }
    }

    class LargeHealingPotion : Item
    {
        public LargeHealingPotion()
        {
            name = "Большое зелье лечения";
        }

        public override void Use()
        {
            GameLoop.player.Heal(50);
        }
    }
    class Bomb : Item
    {
        public Bomb()
        {
            name = "Бомба";
        }

        public override void Use()
        {
            GameLoop.log.Text += "Бомба взрывается у вас в руках." + Environment.NewLine;
            GameLoop.player.hp = 0;
        }
    }

    class Watch : Item
    {
        public Watch()
        {
            name = "Золотые часы";
        }

        public override void Use()
        {
            GameLoop.log.Text += "Вы смотрите на часы и понимаете, что тратите время зря." + Environment.NewLine;
        }
    }
}
