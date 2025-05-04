using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;
namespace FuncDraw
{
    struct PointStruct
    {
        public double x;
        public double y;
        public PointStruct(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class PointsGenerator
    {
        
        public List<Point> points = new List<Point>();
        
        public void AddPoint(double x, double y)
        {
            points.Add(new Point(x, y));
        }

    }
}
