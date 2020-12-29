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
    class Player : Entity
    {
        public int xp;
        public const int max_xp = 1000;
        public int level;
        public List<Weapon> weapon_inventory = new List<Weapon>();
        public int current_index;
        public int damage_modifier = 0;
        public List<Item> inventory = new List<Item>();
        public int current_item_index;
        public Player(int start_x, int start_y, ControlCollection controls) : base(start_x, start_y, controls, "clown", new Weapon("Кинжал", 10, "Вы втыкаете во врага кинжал, нанося ", true))
        {
            maxhp = 100;
            hp = 100;
            xp = 0;
            level = 1;
            damage = weapon.damage * level + damage_modifier;
            visible = true;
            weapon_inventory.Add(weapon);
            current_index = 0;
            current_item_index = 0;
        }

        public void GainXP(int gained_xp)
        {   
            if (xp < max_xp) xp += gained_xp;
            if (level == 1 && xp >= 15)
            {
                level++;
                maxhp += 25;
                hp += 25;
            }
            if (level == 2 && xp >= 50)
            {
                level++;
                maxhp += 75;
                hp += 75;
            }
            if (level == 3 && xp >= 100)
            {
                level++;
                maxhp += 100;
                hp += 100;
            }
            if (level == 4 && xp >= 300) 
            {
                level++;
                maxhp += 150;
                hp += 150;
            }
            if (level == 5 && xp >= 500)
            {
                level++;
                xp = 1000;
                maxhp += 150;
                hp += 150;
            }
            damage = weapon.damage * level + damage_modifier;
        }

        public void Heal(int heal_hp)
        {
            if (heal_hp + hp > maxhp) hp = maxhp;
            else hp += heal_hp;
        }

        public void ChangeWeapon(Weapon weapon_)
        {
            weapon = weapon_;
            damage = weapon.damage * level + damage_modifier;
        }

        public void ChangeDamageModifier(int new_modifier)
        {
            damage_modifier = new_modifier;
            damage = weapon.damage * level + damage_modifier;
        }

        public void TakeItem()
        {
            int val = GameLoop.current_stage.rnd.Next(0, 2);
            if (val == 0)
            {
                val = GameLoop.current_stage.rnd.Next(0, GameLoop.current_stage.chest_items.Count());
                inventory.Add(GameLoop.current_stage.chest_items[val]);
                GameLoop.log.Text += "Вы берёте из сундука " + GameLoop.current_stage.chest_items[val].name + "." + Environment.NewLine;
            }
            if (val == 1)
            {
                val = GameLoop.current_stage.rnd.Next(0, GameLoop.current_stage.chest_weapons.Count());
                weapon_inventory.Add(GameLoop.current_stage.chest_weapons[val]);
                GameLoop.log.Text += "Вы берёте из сундука " + GameLoop.current_stage.chest_weapons[val].name + "." + Environment.NewLine;
            }
        }

        public void Fight(Entity entity)
        {
            if (!dead && entity.sprite != "uwuslime")
            {
                entity.hp -= damage;
                GameLoop.log.Text += weapon.log + damage + " урона." + Environment.NewLine;

                if (!(weapon.fast == true && entity.hp <= 0))
                {
                    hp -= entity.damage;
                    GameLoop.log.Text += entity.weapon.log + Environment.NewLine;
                }
                if (entity.hp <= 0)
                {
                    GainXP(entity.give_xp);
                    if (entity.boss == true)
                    {
                        GameLoop.win_label.Visible = true;
                    }
                    GameLoop.log.Text += "Вы получаете " + entity.give_xp + " опыта." + Environment.NewLine;
                }
            }
            if (entity.sprite == "uwuslime")
            {
                GameLoop.log.Text += "Вы обнимаете увуслайма." + Environment.NewLine;
                GameLoop.log.Text += "Увуслайм обнимает вас." + Environment.NewLine;
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.E)
            {
                if (current_index < weapon_inventory.Count - 1)
                {
                    current_index++;
                    ChangeWeapon(weapon_inventory[current_index]);
                }
                else if (current_index == weapon_inventory.Count - 1)
                {
                    current_index = 0;
                    ChangeWeapon(weapon_inventory[current_index]);
                }
                
            }

            if (e.KeyCode == Keys.R)
            {
                if (current_item_index < inventory.Count - 1)
                {
                    current_item_index++;
                }
                else if (current_item_index == inventory.Count - 1)
                {
                    current_item_index = 0;
                }
            }

            if (e.KeyCode == Keys.Q)
            {
                if (inventory.Count > 0) {
                    inventory[current_item_index].Use();
                    inventory.RemoveAt(current_item_index);
                    if (current_item_index != 0) current_item_index -= 1;
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                x += 16;
                for (int i = 0; i <= Stage.size_y; i++)
                {
                    for (int j = 0; j <= Stage.size_x; j++)
                    {
                        foreach (Tile tile in GameLoop.current_stage.rooms[i][j].tiles)
                        {
                            if (Collisions.CheckCollision(this, tile))
                            {
                                if (tile.tile_name == "chest")
                                {
                                    tile.tile_name = "empty";
                                    tile.picture.Image = null;
                                    TakeItem();
                                }
                                if (tile.tile_name != "empty") x -= 16;
                            }
                        }
                        foreach (Entity enemy in GameLoop.current_stage.rooms[i][j].enemies)
                        {
                            if (CheckCollision(this, enemy) && !enemy.dead)
                            {
                                x -= 16;
                                Fight(enemy);
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                x -= 16;
                for (int i = 0; i <= Stage.size_y; i++)
                {
                    for (int j = 0; j <= Stage.size_x; j++)
                    {
                        foreach (Tile tile in GameLoop.current_stage.rooms[i][j].tiles)
                        {
                            if (Collisions.CheckCollision(this, tile))
                            {
                                if (tile.tile_name == "chest")
                                {
                                    tile.tile_name = "empty";
                                    tile.picture.Image = null;
                                    TakeItem();
                                }
                                if (tile.tile_name != "empty") x += 16;
                            }
                        }
                        foreach (Entity enemy in GameLoop.current_stage.rooms[i][j].enemies)
                        {
                            if (CheckCollision(this, enemy) && !enemy.dead)
                            {
                                x += 16;
                                Fight(enemy);
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                y += 16;
                for (int i = 0; i <= Stage.size_y; i++)
                {
                    for (int j = 0; j <= Stage.size_x; j++)
                    {
                        foreach (Tile tile in GameLoop.current_stage.rooms[i][j].tiles)
                        {
                            if (Collisions.CheckCollision(this, tile))
                            {
                                if (tile.tile_name == "chest")
                                {
                                    tile.tile_name = "empty";
                                    tile.picture.Image = null;
                                    TakeItem();
                                }
                                if (tile.tile_name != "empty") y -= 16;
                            }
                        }
                        foreach (Entity enemy in GameLoop.current_stage.rooms[i][j].enemies)
                        {
                            if (CheckCollision(this, enemy) && !enemy.dead)
                            {
                                y -= 16;
                                Fight(enemy);
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                y -= 16;
                for (int i = 0; i <= Stage.size_y; i++)
                {
                    for (int j = 0; j <= Stage.size_x; j++)
                    {
                        foreach (Tile tile in GameLoop.current_stage.rooms[i][j].tiles)
                        {
                            if (Collisions.CheckCollision(this, tile))
                            {
                                if (tile.tile_name == "chest")
                                {
                                    tile.tile_name = "empty";
                                    tile.picture.Image = null;
                                    TakeItem();
                                }
                                if (tile.tile_name != "empty") y += 16;
                            }
                        }
                        foreach (Entity enemy in GameLoop.current_stage.rooms[i][j].enemies)
                        {
                            if (CheckCollision(this, enemy) && !enemy.dead)
                            {
                                y += 16;
                                Fight(enemy);
                            }
                        }
                    }
                }
            }
        }

        new public void Update()
        {
            base.Update();
            for (int i = 0; i <= Stage.size_y; i++)
            {
                for (int j = 0; j <= Stage.size_x; j++)
                {
                    foreach (Tile tile in GameLoop.current_stage.rooms[i][j].tiles)
                    {
                        if (x + 16 == tile.x && x + 32 == tile.x + 16 && y + 16 == tile.y && y + 32 == tile.y + 16)
                        {
                            GameLoop.current_stage.rooms[i][j].visible = true;
                            foreach (Entity enemy in GameLoop.current_stage.rooms[i][j].enemies) enemy.Draw();
                        }
                    }
                }
            }
        }

    }
}
