
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace FuncDraw
{
    class PolyLineDrower
    {
        private readonly Canvas _canvas;
        private readonly List<Point> _points;
        private readonly Color _strokeColor;
        private readonly double _strokeWidth;
        
        public PolyLineDrower(Canvas canvas, List<Point> points, Color strokeColor, double strokeWidth)
        {
            _canvas = canvas;
            _points = points;
            _strokeColor = strokeColor;
            _strokeWidth = strokeWidth;
        }
        public  void DrawPolyLine( )
        {
            if(_points == null  || _points.Count == 0)
            {
                throw new ArgumentException("Points collection cannot be null or empty.");
            }
            Polyline polyline = new Polyline
            {
                Stroke = new SolidColorBrush(_strokeColor),
                StrokeThickness = _strokeWidth,
                
            };
            polyline.Points = [.. (IEnumerable<System.Windows.Point>)_points];
            _canvas.Children.Add(polyline);
        }
    }
}
