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
        private readonly double _scaleValue;
        private double _xCenter;
        private double _yCenter;

        public GridDrower(Canvas mainGrid, double GridSize, double ScaleValue)
        {
            _mainGrid = mainGrid;
            
            _gridSize = GridSize;
            _scaleValue = ScaleValue;
        }
        
        /// <summary>
        /// Draws a grid on the given grid with the given _gridSize
        /// </summary>
        
        public void DrawGrid()
        {
            double xCenter = (int)(Math.Floor(_mainGrid.ActualWidth / _gridSize)) / 2 * _gridSize;
             double yCenter = (int)(Math.Floor(_mainGrid.ActualHeight / _gridSize)) / 2 * _gridSize;
            int xnumLine = 0;
            int ynumLine = 0;
            _mainGrid.Children.Clear();
            #region background Grid
            for (double i = _gridSize; i <= xCenter + _gridSize; i += _gridSize)
            {
                xnumLine++;
                Line line = new Line();
                line.Stroke = Brushes.Black;
                if( xnumLine == 5)
                {
                    line.StrokeThickness = 0.3;
                    
                }
                else
                {
                    line.StrokeThickness = 0.1;
                }
                    line.X1 = xCenter +i;
                line.X2 = xCenter + i;
                line.Y1 = 0;
                line.Y2 = _mainGrid.ActualHeight;
                _mainGrid.Children.Add(line);
                Line line2 = new Line();
                line2.Stroke = Brushes.Black;
                if (xnumLine == 5)
                {
                    line2.StrokeThickness = 0.3;
                    xnumLine = 0;
                }
                else
                {
                    line2.StrokeThickness = 0.1;
                }
                line2.X1 = xCenter - i;
                line2.X2 = xCenter -i;
                line2.Y1 = 0;
                line2.Y2 = _mainGrid.ActualHeight;
                _mainGrid.Children.Add(line2);
            }
            for (double i = _gridSize; i <= yCenter +_gridSize; i += _gridSize)
            {
                ynumLine++;
                Line line = new Line();
                line.Stroke = Brushes.Black;
                if (ynumLine == 5)
                {
                    line.StrokeThickness = 0.3;
                    
                }
                else
                {
                    line.StrokeThickness = 0.1;
                }
                line.X1 = 0;
                line.X2 = _mainGrid.ActualWidth;
                line.Y1 = yCenter +i;
                line.Y2 = yCenter +i;
                _mainGrid.Children.Add(line);

                Line line2 = new Line();
                line2.Stroke = Brushes.Black;
                if (ynumLine == 5)
                {
                    line2.StrokeThickness = 0.3;
                    ynumLine = 0;
                }
                else
                {
                    line2.StrokeThickness = 0.1;
                }
                line2.X1 = 0;
                line2.X2 = _mainGrid.ActualWidth;
                line2.Y1 = yCenter -i;
                line2.Y2 = yCenter -i;
                _mainGrid.Children.Add(line2);
            }
            #endregion
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

            #region strzałki
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
            #endregion
            //dodanie opisów do osi x i y
            double tempScaleValue = _scaleValue;
            int numLine = 1;
            for (double i = xCenter; i < _mainGrid.ActualWidth - _gridSize; i += _gridSize)
            {
                
               
                TextBlock tb_Right = new TextBlock();
                TextBlock tb_Left = new TextBlock();


                if (numLine  % 6 == 0 )
                    {
                        tb_Right.Text = (0 + tempScaleValue - 1 ).ToString();
                        tb_Left.Text = (0 - tempScaleValue + 1).ToString();
                    numLine = 1;
                    }
                    else
                    {
                        tb_Right.Text = "";
                        tb_Left.Text = "";

                }

               


                tb_Right.FontSize = 12;
                
                tb_Right.Foreground = Brushes.Black;
                
                Canvas.SetLeft(tb_Right, i);
                Canvas.SetTop(tb_Right, yCenter + 5);
                _mainGrid.Children.Add(tb_Right);
                tb_Left.FontSize = 12;
                tb_Left.Foreground = Brushes.Black;
                
                Canvas.SetLeft(tb_Left, xCenter - ( i - xCenter ));
                Canvas.SetTop(tb_Left, yCenter + 5);
                _mainGrid.Children.Add(tb_Left);

                numLine++;
                tempScaleValue += _scaleValue;

            }
            tempScaleValue = _scaleValue;
            numLine = 1;
            for (double i = yCenter; i < _mainGrid.ActualHeight - _gridSize; i += _gridSize)
            {
                
                TextBlock tb_Top = new TextBlock();
                TextBlock tb_Bottom = new TextBlock();
                if (i - yCenter == 0)
                {
                    tb_Top.Text = " ";
                }
                else
                {
                    
                        if (numLine % 6 == 0)
                        {
                        tb_Top.Text = (0 + tempScaleValue - 1  ).ToString();
                        tb_Bottom.Text = (0 - tempScaleValue + 1).ToString();
                        numLine = 1;
                        }
                        else
                        {
                        tb_Top.Text = "";
                        }
                    
                }


                tb_Top.FontSize = 12;
                tb_Top.Foreground = Brushes.Black;
                tb_Bottom.FontSize = 12;
                tb_Bottom.Foreground = Brushes.Black;
                Canvas.SetLeft(tb_Top, xCenter + 5);
                Canvas.SetTop(tb_Top, i);
                Canvas.SetLeft(tb_Bottom, xCenter + 5);
                Canvas.SetTop(tb_Bottom, yCenter - (i - yCenter) );
                _mainGrid.Children.Add(tb_Top);
                _mainGrid.Children.Add(tb_Bottom);
                numLine++;
                tempScaleValue += _scaleValue;
            }

        }

    }
}
