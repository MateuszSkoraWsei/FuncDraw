
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
            if (_points.Count < 2) return;

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = _points[0]
            };

            PolyBezierSegment bezierSegment = new PolyBezierSegment();
            for (int i = 1; i < _points.Count; i++)
            {
                bezierSegment.Points.Add(_points[i]);
            }

            PathGeometry pathGeometry = new PathGeometry();
            pathFigure.Segments.Add(bezierSegment);
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Data = pathGeometry,
                Stroke = new SolidColorBrush(_strokeColor),
                StrokeThickness = _strokeWidth
            };
            
            _canvas.Children.Add(path);
        }
    }
}
