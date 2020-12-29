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
    class GameLoop
    {
        ControlCollection Controls;
        public static Stage current_stage;
        public static Player player;
        public const string tile_path = "tiles/";
        public static Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        public Label hp_label = new Label();
        public Label xp_label = new Label();
        public Label lvl_label = new Label();
        public Label weapon_label = new Label();
        public Label item_label = new Label();
        public static Label win_label = new Label();
        public static TextBox log = new TextBox();
        private System.Media.SoundPlayer Player = new System.Media.SoundPlayer();

        public GameLoop(ControlCollection controls)
        {
            Controls = controls;
            Initialize();     
        }

        public void CreateLabel(Label label, Point location)
        {
            label.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.ForeColor = System.Drawing.Color.White;
            label.Location = location;
            label.AutoSize = true;
        }

        public void CreateLabels()
        {
            CreateLabel(hp_label, new Point(100, 650));
            CreateLabel(xp_label, new Point(100, 680));
            CreateLabel(lvl_label, new Point(100, 710));
            CreateLabel(weapon_label, new Point(100, 740));
            CreateLabel(win_label, new Point(400, 450));
            CreateLabel(item_label, new Point(100, 770));
            win_label.Visible = false;
            win_label.Font = new System.Drawing.Font("Arial", 30.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            log.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            log.ForeColor = System.Drawing.Color.Black;
            log.ScrollBars = ScrollBars.Vertical;
            log.Location = new Point(500, 650);
            log.Size = new System.Drawing.Size(600, 150);
            log.Multiline = true;
            log.ReadOnly = true;
            log.Enter += new EventHandler(log_Enter);
            hp_label.KeyUp += new KeyEventHandler(KeyUp);
            Controls.Add(hp_label);
            Controls.Add(xp_label);
            Controls.Add(lvl_label);
            Controls.Add(weapon_label);
            Controls.Add(log);
            Controls.Add(win_label);
            Controls.Add(item_label);
            Controls.SetChildIndex(win_label, 0);

        }

        private void log_Enter(object sender, EventArgs e)
        {
            hp_label.Focus();
        }

        public void UpdateLabels()
        {
            hp_label.Text = "HP: " + player.hp + "/" + player.maxhp;
            xp_label.Text = "Опыт: " + player.xp;
            lvl_label.Text = "Уровень: " + player.level;
            weapon_label.Text = "Оружие: " + player.weapon.name;
            if (!player.inventory.Any())
            {
                item_label.Text = "В инвентаре нет предметов.";
            }
            else
            {
                item_label.Text = "Предмет: " + player.inventory[player.current_item_index].name;
                item_label.Size = new System.Drawing.Size(200, 200);
            }
            win_label.Text = "Вы победили!!!";
            log.SelectionStart = log.Text.Length;
            log.ScrollToCaret();
        }

        public void LoadImages()
        {
            bitmaps.Add("wall1", new Bitmap(tile_path + "wall1.png"));;
            bitmaps.Add("clown", new Bitmap(tile_path + "clown.png"));
            bitmaps.Add("rat", new Bitmap(tile_path + "rat.png"));
            bitmaps.Add("horror", new Bitmap(tile_path + "horror.png"));
            bitmaps.Add("orc", new Bitmap(tile_path + "orc.png"));
            bitmaps.Add("chest", new Bitmap(tile_path + "chest.png"));
            bitmaps.Add("dragon", new Bitmap(tile_path + "dragon.png"));
            bitmaps.Add("demon", new Bitmap(tile_path + "demon.png"));
            bitmaps.Add("slime", new Bitmap(tile_path + "slime.png"));
            bitmaps.Add("uwuslime", new Bitmap(tile_path + "uwuslime.png"));
            bitmaps.Add("troll", new Bitmap(tile_path + "troll.png"));
            bitmaps.Add("empty", new Bitmap(tile_path + "empty.png"));
        }

        async public void Initialize()
        {
            LoadImages();
            current_stage = new Stage("stage1", Controls);        
            player = new Player(32, 400, Controls);
            CreateLabels();
            player.Draw();
            this.Player.SoundLocation = "music/music.wav";
            this.Player.PlayLooping();
            while (true)
            {
                Update();
                Draw();
                await Task.Delay(10);           
            }
                        
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            player.OnKeyUp(e);
        }

        public void Draw()
        {
            current_stage.Draw();
        }

        public void Update()
        {
            
            player.Update();
            for (int i = 0; i <= Stage.size_y; i++)
            {
                for (int j = 0; j <= Stage.size_x; j++)
                {
                    foreach (Entity enemy in current_stage.rooms[i][j].enemies)
                    {
                        enemy.Update();
                    }
                }
            }
            UpdateLabels();
        }

    }
}
