using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace imifi_roguelike
{

    class Room
    {
        public List<Tile> tiles = new List<Tile>();
        public bool visible;
        public bool drawn;
        public int x;
        public int y;
        public List<Entity> enemies = new List<Entity>();
        ControlCollection Controls;

        public Room (int x_, int y_, ControlCollection controls)
        {
            Controls = controls;
            x = x_;
            y = y_;
            drawn = false;
        }

        public void GenerateFromGrid(string grid_path)
        {
            string[] lines = System.IO.File.ReadAllLines(grid_path);
            for (int i = 0; i < lines.Count(); i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '.')
                    {
                        tiles.Add(new Tile("wall1", 10*16*x + 16*j, 10*16*y + 16*i, Controls));
                    }
                    if (lines[i][j] == 'R')
                    {
                        enemies.Add(new Rat(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'B')
                    {
                        enemies.Add(new Horror(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'O')
                    {
                        enemies.Add(new Orc(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'U')
                    {
                        enemies.Add(new Uwuslime(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'T')
                    {
                        enemies.Add(new Troll(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'S')
                    {
                        enemies.Add(new Slime(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'D')
                    {
                        enemies.Add(new Demon(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'd')
                    {
                        enemies.Add(new Dragon(10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                    if (lines[i][j] == 'C')
                    {
                        tiles.Add(new Tile("chest", 10 * 16 * x + 16 * j, 10 * 16 * y + 16 * i, Controls));
                    }
                }
            }
        }
    }

    class Stage
    {

        public const int size_x = 6;
        public const int size_y = 3;
        public const int room_size_x = 10;
        public const int room_size_y = 10;
        public const string grid_folder = "grids/";
        public List<string> grids = new List<string>();
        public string name;
        public List<List<Room>> rooms = new List<List<Room>>();
        ControlCollection Controls;
        public List<Item> chest_items = new List<Item>();
        public List<Weapon> chest_weapons = new List<Weapon>();
        public Random rnd = new Random();
               
        public Stage(string name_, ControlCollection controls)
        {
            chest_weapons.Add(new Weapon("Катана", 50, "Вы режете врага катаной, нанося ", true));
            chest_weapons.Add(new Weapon("Экскалибур", 70, "Экскалибур создаёт луч концентрированного правосудия, нанося ", false));
            chest_weapons.Add(new Weapon("Двуручный меч", 35, "Вы с размаху бьёте врага двуручным мечом, нанося ", false));
            chest_weapons.Add(new Weapon("BFG-9000", 100, "Вы выстреливаете из BFG-9000, нанося ", false));
            chest_items.Add(new StrengthPotion());
            chest_items.Add(new WeaknessPotion());
            chest_items.Add(new HealingPotion());
            chest_items.Add(new LargeHealingPotion());
            chest_items.Add(new Watch());
            chest_items.Add(new Bomb());

            Controls = controls;
            name = name_;
            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(grid_folder + name);
            System.IO.FileInfo[] Files = d.GetFiles("*.txt");
            foreach (System.IO.FileInfo file in Files)
            {
                if (file.Name != "grid1.txt" && file.Name != "boss.txt") grids.Add(file.Name);
            }

            for (int i = 0; i <= size_y; i++)
            {
                rooms.Add(new List<Room>());
                for (int j = 0; j <= size_x; j++)
                {
                    rooms[i].Add(new Room(j, i, Controls));
                    rooms[i][j].visible = false;
                }
            }
            for (int i = 0; i <= size_y; i++)
            {
                for (int j = 0; j <= size_x; j++)
                {
                    if ((i != 2 || j != 0) && (i != 0 || j != size_x))
                    {
                        int value = rnd.Next(0, grids.Count());
                        rooms[i][j].GenerateFromGrid(grid_folder + name + "/" + grids[value]);
                        rooms[i][j].visible = false;
                    }
                }
            }
            rooms[2][0].GenerateFromGrid(grid_folder + "stage1/grid1.txt");
            rooms[2][0].visible = true;
            rooms[0][size_x].GenerateFromGrid(grid_folder + "stage1/boss.txt");
            rooms[0][size_x].visible = false;
            for (int i = 0; i <= size_y; i++)
            {
                for (int j = 0; j <= size_x; j++)
                {
                    if (i == 0)
                    {
                        rooms[i][j].tiles.Add(new Tile("wall1", 10 * 16 * j + 16 * 4, 10 * 16 * i + 16 * 0, Controls));
                    }
                    if (i == size_y)
                    {
                        rooms[i][j].tiles.Add(new Tile("wall1", 10 * 16 * j + 16 * 4, 10 * 16 * i + 16 * 9, Controls));
                    }
                    if (j == 0)
                    {
                        rooms[i][j].tiles.Add(new Tile("wall1", 10 * 16 * j + 16 * 0, 10 * 16 * i + 16 * 4, Controls));
                    }
                    if (j == size_x)
                    {
                        rooms[i][j].tiles.Add(new Tile("wall1", 10 * 16 * j + 16 * 9, 10 * 16 * i + 16 * 4, Controls));
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i <= size_y; i++)
            {
                for (int j = 0; j <= size_x; j++)
                {
                    if (rooms[i][j].visible && !rooms[i][j].drawn)
                    {
                        rooms[i][j].drawn = true;
                        foreach (Tile tile in rooms[i][j].tiles)
                        {
                            tile.Draw();
                        }
                    }
                }
            }
        }
    }
}
