using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace aoc22
{
    internal class d24
    {
        public Dictionary<(int, int), Position> postitions = new();
        public void Readf()
        {
            var file = File.ReadAllLines("24.txt").Select(x => x.ToArray());
            var yLen = file.Count();
            var xLen = file.Select(x => x.Length).Max();
            for (var y = 0; y < yLen; y++)
                for (var x = 0; x < xLen; x++)
                    if (file.ElementAt(y)[x] == '#')
                        postitions.Add((x, y), new Wall(x, y, xLen, yLen, postitions));
                    else
                        postitions.Add((x, y), new Position(x, y, xLen, yLen, postitions));

            Walk();
        }

        private void Walk()
        {

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
        public Position(int x, int y, int xl, int yl, Dictionary<(int, int), Position> d) 
        {
            this.x = x;
            this.y = y;
            xLen = xl;
            yLen = yl;
            pos = d;    
        }

        public virtual void Add(string child)
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

        public virtual void Distribute()
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

    public class Wall : Position
    {
        public Wall(int x, int y, int xl, int yl, Dictionary<(int, int), Position> d) : base(x, y, xl, yl, d) { }

        public override void Add(string child)
        {
            throw new Exception();
        }

        public override void Distribute() { }
    }
}
