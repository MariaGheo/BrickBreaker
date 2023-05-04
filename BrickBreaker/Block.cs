using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickBreaker
{
    public class Block
    {
        public int width = 80;
        public int height = 30;

        public int x;
        public int y; 
        public int hp;
        public Image image;

        public static Random rand = new Random();

        public Block(int _x, int _y, int _hp, Image _image)
        {
            x = _x;
            y = _y;
            hp = _hp;
            image = _image;
        }
    }
}
