
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
        private readonly List<Point> _points1;
        private readonly List<Point> _points2;
        private readonly Color _strokeColor;
        private readonly double _strokeWidth;
        
        public PolyLineDrower(Canvas canvas, List<Point> points1, List<Point> points2, Color strokeColor, double strokeWidth)
        {
            _canvas = canvas;
            _points1 = points1;
            _points2 = points2;
            _strokeColor = strokeColor;
            _strokeWidth = strokeWidth;
        }
        public  void DrawPolyLine( )
        {


            if (_points1.Count < 2 && _points2.Count < 2) return;

            // Rysowanie pierwszej polilinii, jeśli są punkty
            if (_points1.Count > 0)
            {
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = _points1[0]
                };

                PolyLineSegment lineSegment = new PolyLineSegment();
                for (int i = 1; i < _points1.Count; i++)
                {
                    lineSegment.Points.Add(_points1[i]);
                }

                pathFigure.Segments.Add(lineSegment);

                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry.Figures.Add(pathFigure);

                Path path = new Path
                {
                    Data = pathGeometry,
                    Stroke = new SolidColorBrush(_strokeColor),
                    StrokeThickness = _strokeWidth
                };

                _canvas.Children.Add(path);
            }

            // Rysowanie drugiej polilinii, jeśli są punkty
            if (_points2.Count > 0)
            {
                PathFigure pathFigure2 = new PathFigure
                {
                    StartPoint = _points2[0]
                };

                PolyLineSegment lineSegment2 = new PolyLineSegment();
                for (int i = 1; i < _points2.Count; i++)
                {
                    lineSegment2.Points.Add(_points2[i]);
                }

                pathFigure2.Segments.Add(lineSegment2);

                PathGeometry pathGeometry2 = new PathGeometry();
                pathGeometry2.Figures.Add(pathFigure2);

                Path path2 = new Path
                {
                    Data = pathGeometry2,
                    Stroke = new SolidColorBrush(_strokeColor),
                    StrokeThickness = _strokeWidth
                };

                _canvas.Children.Add(path2);
            }
        }
        
        
    }
}
