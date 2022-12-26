using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc22
{
    internal class d24
    {
        public Dictionary<(int, int)>
        public void Readf()
        {
            var file = File.ReadAllLines("24.txt").Select(x => x.ToArray());

        }
    }

    public class Position
    {
        public int x; 
        public int y;
        public int xLen;
        public int yLen;
        public Dictionary<(int, int), Position> pos { get; set; }
        public List<string> childs = new List<string>();
        public int added { get; set; } = 0;
        public Position(int x, int y, int xl, int yl) 
        {
            this.x = x;
            this.y = y;
            xLen = xl;
            yLen = yl;
        }

        public void Add(string child)
        {
            childs.Add(child);
            added++;
        }

        public Position GetNeigh(string dir)
        {
            switch (dir) 
            {
                case ">": return x == xLen - 2 ? pos[(1, y)] : pos[(x+1, y)];
                case "<": return x == 1 ? pos[(xLen-2, y)] : pos[(x-1, y)];
                case "^": return y == 1 ? pos[(x, yLen - 2)] : pos[(x, y - 1)];
                case "v": return y == yLen - 2 ? pos[(x, 1)] : pos[(x, y + 1)];
                default: throw new Exception();
            }
        }

        public void Distribute()
        {
            for (var i = 0; i < childs.Count - added; i++)
            {
                var c = childs[i];
                GetNeigh(c).Add(c); 
            }
            
            childs.RemoveRange(0, added);
            added = 0;
        }
    }
}
