using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace FuncDraw
{
    class GridDrower
    {
        private readonly Grid _mainGrid ;
        private readonly int _gridSize;

        public GridDrower(Grid mainGrid, int gridSize)
        {
            _mainGrid = mainGrid;
            _gridSize = gridSize;
        }
        /// <summary>
        /// Draws a grid on the given grid with the given size
        /// </summary>
        /// <param name="MainGrid">localization where grid is draw</param>
        /// <param name="gridSize">size of grid</param>
        public void DrawGrid(Grid MainGrid, int gridSize)
        {
            MainGrid.Children.Clear();
            for (int i = 0; i < MainGrid.ActualWidth; i += gridSize)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.1;
                line.X1 = i;
                line.X2 = i;
                line.Y1 = 0;
                line.Y2 = MainGrid.ActualHeight;
                MainGrid.Children.Add(line);
            }
            for (int i = 0; i < MainGrid.ActualHeight; i += gridSize)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.1;
                line.X1 = 0;
                line.X2 = MainGrid.ActualWidth;
                line.Y1 = i;
                line.Y2 = i;
                MainGrid.Children.Add(line);
            }
        }
        /// <summary>
        /// Draws the axes on the given grid with the given size with little arrows at the end
        /// </summary>
        /// <param name="MainGrid">locatization where grid is draw</param>
        /// <param name="size">size of grid</param>
        public void DrawAxes(Grid MainGrid , int size)
        {
            // storzenie osi x i y 
            int xCenter = (int)(Math.Floor(MainGrid.ActualWidth / size)) / 2 * size;
            int yCenter = (int)(Math.Floor(MainGrid.ActualHeight / size)) / 2 * size;
            Line xAxis = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = 0,
                Y1 = yCenter,
                X2 = MainGrid.ActualWidth,
                Y2 = yCenter
            };
            //dodanie strzałki do osi x
            Line xArrow1 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = MainGrid.ActualWidth - size,
                Y1 = yCenter - size / 2,
                X2 = MainGrid.ActualWidth,
                Y2 = yCenter
            };
            Line xArrow2 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = MainGrid.ActualWidth - size,
                Y1 = yCenter + size / 2,
                X2 = MainGrid.ActualWidth,
                Y2 = yCenter
            };

            Line yAxis = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter,
                Y1 = 0,
                X2 = xCenter,
                Y2 = MainGrid.ActualHeight
            };
            //dodanie strzałki do osi y
            Line yArrow1 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter - size / 2,
                Y1 = size,
                X2 = xCenter,
                Y2 = 0
            };
            Line yArrow2 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter + size / 2,
                Y1 = size,
                X2 = xCenter,
                Y2 = 0
            };

            MainGrid.Children.Add(xAxis);
            MainGrid.Children.Add(yAxis);
            MainGrid.Children.Add(xArrow1);
            MainGrid.Children.Add(xArrow2);
            MainGrid.Children.Add(yArrow1);
            MainGrid.Children.Add(yArrow2);
        }

    }
}
