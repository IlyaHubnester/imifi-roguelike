using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imifi_roguelike
{
    public static class Collisions {
        private class Point
        {
            public int x, y;
            public Point(int x_, int y_)
            {
                x = x_;
                y = y_;
            }
        }
        public static bool CheckCollision(Entity entity, Tile tile)
        {
            Point l1 = new Point(entity.x, entity.y);
            Point r1 = new Point(entity.x + 16, entity.y + 16);
            Point l2 = new Point(tile.x, tile.y);
            Point r2 = new Point(tile.x + 16, tile.y + 16);

            if (l1.x == l2.x && r1.x == r2.x && l1.y == l2.y && r1.y == r2.y) return true;
            return false;
        }
        public static bool CheckCollision(Entity entity1, Entity entity2)
        {
            Point l1 = new Point(entity1.x, entity1.y);
            Point r1 = new Point(entity1.x + 16, entity1.y + 16);
            Point l2 = new Point(entity2.x, entity2.y);
            Point r2 = new Point(entity2.x + 16, entity2.y + 16);

            if (l1.x == l2.x && r1.x == r2.x && l1.y == l2.y && r1.y == r2.y) return true;
            return false;
        }
    }
}
