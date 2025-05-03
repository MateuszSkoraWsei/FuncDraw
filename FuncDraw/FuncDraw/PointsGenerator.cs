using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;
namespace FuncDraw
{
    class PointsGenerator
    {
        
        public List<Point> points = new List<Point>();
        
        public void AddPoint(int x, int y)
        {
            points.Add(new Point(x, y));
        }

    }
}
