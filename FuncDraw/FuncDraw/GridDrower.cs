using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace FuncDraw
{
    class GridDrower
    {
        private readonly Canvas _mainGrid ;
        private readonly double _gridSize;
       

        public GridDrower(Canvas mainGrid, double GridSize)
        {
            _mainGrid = mainGrid;
            
            _gridSize = GridSize;
        }
        /// <summary>
        /// Draws a grid on the given grid with the given _gridSize
        /// </summary>
        
        public void DrawGrid()
        {
            int numLine = 0;
            _mainGrid.Children.Clear();
            for (double i = 0; i < _mainGrid.ActualWidth; i += _gridSize)
            {
                numLine++;
                Line line = new Line();
                line.Stroke = Brushes.Black;
                if( numLine == 5)
                {
                    line.StrokeThickness = 0.5;
                    numLine = 0;
                }
                else
                {
                    line.StrokeThickness = 0.1;
                }
                    line.X1 = i;
                line.X2 = i;
                line.Y1 = 0;
                line.Y2 = _mainGrid.ActualHeight;
                _mainGrid.Children.Add(line);
            }
            for (double i = 0; i < _mainGrid.ActualHeight; i += _gridSize)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                if (numLine == 5)
                {
                    line.StrokeThickness = 0.5;
                    numLine = 0;
                }
                else
                {
                    line.StrokeThickness = 0.1;
                }
                line.X1 = 0;
                line.X2 = _mainGrid.ActualWidth;
                line.Y1 = i;
                line.Y2 = i;
                _mainGrid.Children.Add(line);
            }
        }
        /// <summary>
        /// Draws the axes on the given grid with the given _gridSize with little arrows at the end
        /// </summary>
        
        public void DrawAxes()
        {
            // storzenie osi x i y 
            double xCenter = (int)(Math.Floor(_mainGrid.ActualWidth / _gridSize)) / 2 * _gridSize;
            double yCenter = (int)(Math.Floor(_mainGrid.ActualHeight / _gridSize)) / 2 * _gridSize;
            Line xAxis = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = 0,
                Y1 = yCenter,
                X2 = _mainGrid.ActualWidth,
                Y2 = yCenter
            };
            //dodanie strzałki do osi x
            Line xArrow1 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = _mainGrid.ActualWidth - _gridSize,
                Y1 = yCenter - _gridSize / 2,
                X2 = _mainGrid.ActualWidth,
                Y2 = yCenter
            };
            Line xArrow2 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = _mainGrid.ActualWidth - _gridSize,
                Y1 = yCenter + _gridSize / 2,
                X2 = _mainGrid.ActualWidth,
                Y2 = yCenter
            };

            Line yAxis = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter,
                Y1 = 0,
                X2 = xCenter,
                Y2 = _mainGrid.ActualHeight
            };
            //dodanie strzałki do osi y
            Line yArrow1 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter - _gridSize / 2,
                Y1 = _gridSize,
                X2 = xCenter,
                Y2 = 0
            };
            Line yArrow2 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter + _gridSize / 2,
                Y1 = _gridSize,
                X2 = xCenter,
                Y2 = 0
            };

            _mainGrid.Children.Add(xAxis);
            _mainGrid.Children.Add(yAxis);
            _mainGrid.Children.Add(xArrow1);
            _mainGrid.Children.Add(xArrow2);
            _mainGrid.Children.Add(yArrow1);
            _mainGrid.Children.Add(yArrow2);
            //dodanie opisów do osi x i y
            
            for (double i = _gridSize; i < _mainGrid.ActualWidth - _gridSize; i += _gridSize)
            {
                
               
                TextBlock textBlock = new TextBlock();
                
                    if (i % 5 == 0)
                    {
                        textBlock.Text = (((double)i - (double)xCenter)/10).ToString();
                    }
                    else
                    {
                        textBlock.Text = "";
                    }
                
                

                textBlock.FontSize = 12;
                
                textBlock.Foreground = Brushes.Black;
                Canvas.SetLeft(textBlock, i);
                Canvas.SetTop(textBlock, yCenter + 5);
                _mainGrid.Children.Add(textBlock);
                
            }
            for (double i = _gridSize; i < _mainGrid.ActualHeight - _gridSize; i += _gridSize)
            {
                
                TextBlock textBlock = new TextBlock();
                if(i - yCenter == 0)
                {
                    textBlock.Text = " ";
                }
                else
                {
                    
                        if (i % 5 == 0)
                        {
                            textBlock.Text = (-1 * ((double)i - (double)yCenter) / 10).ToString();
                        }
                        else
                        {
                            textBlock.Text = "";
                        }
                    
                }


                textBlock.FontSize = 12;
                textBlock.Foreground = Brushes.Black;
                Canvas.SetLeft(textBlock, xCenter + 5);
                Canvas.SetTop(textBlock, i);
                _mainGrid.Children.Add(textBlock);
                
            }

        }

    }
}
