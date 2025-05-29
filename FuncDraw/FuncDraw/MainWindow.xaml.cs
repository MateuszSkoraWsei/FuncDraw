using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using ColorDialog = System.Windows.Forms.ColorDialog;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;
using Orientation = System.Windows.Controls.Orientation;
using Panel = System.Windows.Controls.Panel;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Cursors = System.Windows.Input.Cursors;
using System.IO;

namespace FuncDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public Dictionary<string, object> Expressions = new Dictionary<string, object>();
        public Point przesuniecie = new Point(0,0);
        
        public double size = 25 ;
        public double scaleValue = 1;
        
        public int expressionCounter = 1;
        
        
        public MainWindow()
        {
            InitializeComponent();
            ShowHideBtn.Click += ShowHideBtn_Click;
            MainGrid.SizeChanged += GenerateGrid;
            AddBtn.Click += GenerateExpresionCreator;
            
            

        }
        
        private void ShowHideBtn_Click(object sender, RoutedEventArgs e)
        {
           editor.Width = editor.Width == new GridLength(20) ?  new GridLength(200) : new GridLength(20);
           
        }
        public void MainWindow_MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta <= 0 )
            {
                

                if(size == 45)
                {
                    size = 25;

                    scaleValue = (scaleValue.ToString().Contains("2")) ? scaleValue * 2.5 : scaleValue * 2;


                }
                else
                {
                    size += 5;
                }

            }
            else
            {
                
                if (size == 25)
                {
                    size = 45;
                    scaleValue = (scaleValue.ToString().Contains("5")) ? scaleValue / 2.5 : scaleValue / 2;


                }
                else
                {
                    size -= 5;
                }

                    

            }
            GenerateGrid(sender, e);
        }
        public void GenerateGrid(object sender, RoutedEventArgs e )
        {
            GridDrower grid = new GridDrower(MainGrid, size, scaleValue, przesuniecie);
            grid.DrawGrid();
            grid.DrawAxes();
            double xCenter = (int)(Math.Floor(MainGrid.ActualWidth / size)) / 2 * size;
            double yCenter = (int)(Math.Floor(MainGrid.ActualHeight / size)) / 2 * size;
            foreach(var expression in Expressions)
            {



                dynamic expressionData = expression.Value;
                string wyrazenie = expressionData.expresion;
                Color color = expressionData.color;
                string correctedExpression = wyrazenie.Split('=')[1];
                
                
                string output = wyrazenie.Split('=')[0].Trim();
                double skala = size / scaleValue;

                double x = (int)(MainGrid.ActualWidth) ;
                double y = (int)(MainGrid.ActualHeight) ;
                double bigger = x > y ? (x  +(przesuniecie.X < 0 ? przesuniecie.X * (-1) : przesuniecie.X)) : (y + +(przesuniecie.Y < 0 ? przesuniecie.Y * (-1) : przesuniecie.Y));
                // stworzneie pętli 
                double begining = Math.Ceiling(-  bigger  / size) * scaleValue - scaleValue * 5;
                double end = begining * (-1) + scaleValue * 5;
                

                PointsGenerator pointsGenerator = new PointsGenerator();
                if (correctedExpression.Contains("^"))
                {
                    for (double i = begining; i < end; i += (scaleValue/5))
                    {
                        // dodanie do listy
                        List<string> tokens = Tokenizer.Tokenize(correctedExpression);
                        CalculatePosition calculate = new CalculatePosition(i, i, tokens, output);
                        string XYposition = calculate.FindEquasion();
                        double X = double.Parse(XYposition.Split(":")[0]);
                        X = X * skala + xCenter + przesuniecie.X;
                        double Y = double.Parse(XYposition.Split(":")[1]);
                        Y =  yCenter - Y * skala + przesuniecie.Y;
                        pointsGenerator.AddPoint(X,Y);
                        
                    }
                }

                else
                {
                    for (double i = begining; i < end; i += (scaleValue))
                    {
                        // dodanie do listy
                        List<string> tokens = Tokenizer.Tokenize(correctedExpression);
                        CalculatePosition calculate = new CalculatePosition(i, i, tokens, output);
                        string XYposition = calculate.FindEquasion();
                        double X = double.Parse(XYposition.Split(":")[0]);
                        X = X * skala + xCenter + przesuniecie.X;
                        double Y = double.Parse(XYposition.Split(":")[1]);
                        Y = yCenter - Y * skala + przesuniecie.Y;
                        pointsGenerator.AddPoint(X, Y);
                    }
                }

                // utworzenie polyline i dodanie do canvas
                PolyLineDrower polyLineDrower = new PolyLineDrower(MainGrid , pointsGenerator.points , color , 2 );
                polyLineDrower.DrawPolyLine();



            }
        }
        public void GenerateExpresionCreator(object sender, RoutedEventArgs e)
        {
            exprCreator expressionCreator = new exprCreator( AddBtn , ExpressionContainer , expressionCounter);
            expressionCreator.ExpressionCreator(Expressions );
            expressionCounter++;
            GenerateGrid(sender,e);


        }
        public void testAdd(object sender, RoutedEventArgs e)
        {
           
            //tworzenie buttona

            Button test1 = new Button();
            test1.Content = $"Test";

            test1.Width = 50;
            test1.Height = 20;
            

            //dodanie do stackPanel
            ExpressionContainer.Children.Add(test1);
        }


        #region przesuwanie po wykresie
        public Point StartPosition;
        public Point EndPosition;
        public bool isDragging = false;
        private void Canvas_LeftBtn_Down(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MainGrid.Cursor = Cursors.Hand; 
                StartPosition = e.GetPosition(MainGrid);
                isDragging = true;
                MainGrid.CaptureMouse();
            }
            else
            {
                isDragging = false;
            }
        }
        private void Canvas_Mouse_Move(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                EndPosition = e.GetPosition(MainGrid);
                przesuniecie.X += EndPosition.X - StartPosition.X;
                przesuniecie.Y += EndPosition.Y - StartPosition.Y;
                StartPosition = EndPosition;
                GenerateGrid(sender, e);
            }

        }

        

        private void Canvas_LeftBtn_Up(object sender, MouseButtonEventArgs e)
        {
            if (isDragging){
                isDragging = false;
                
                //MessageBox.Show($"X: {przesuniecie.X}, Y: {przesuniecie.Y}");
                MainGrid.ReleaseMouseCapture();
                
            }
            
        }
        #endregion

        private void btnCentroj_Click(object sender, RoutedEventArgs e)
        {
            przesuniecie = new Point(0, 0);
            GenerateGrid(sender, e);
        }

        private void btnSkala_Click(object sender, RoutedEventArgs e)
        {
            scaleValue = 1;
            size = 25;
            GenerateGrid(sender, e);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save Expressions File",
                
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                // Przykład: zapis wszystkich wyrażeń
                var lines = new List<string>();
                foreach (var expression in Expressions)
                {
                    dynamic expressionData = expression.Value;
                    string wyrazenie = expressionData.expresion;
                    Color color = expressionData.color;
                    string name = expression.Key;
                    lines.Add($"{name};{wyrazenie};{color}");
                }
                File.WriteAllLines(filePath, lines);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Open Expressions File"
            };
            if(openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath);
                if (File.Exists(filePath))
                {
                    Expressions.Clear();
                    ExpressionContainer.Children.Clear();
                    string[] zawartość = File.ReadAllLines(filePath);
                    if (zawartość.Length == 0)
                    {
                        return;
                    }
                    foreach (string linia in zawartość)
                    {
                        string[] dane = linia.Split(';');
                        if (dane.Length == 3)
                        {
                            string name = dane[0];
                            string expresion = dane[1];
                            Color color = (Color)System.Windows.Media.ColorConverter.ConvertFromString(dane[2]);

                            Expressions.Add(name, new { expresion = expresion, color = color });
                            exprCreator expressionCreator = new exprCreator(AddBtn, ExpressionContainer, expressionCounter);
                            expressionCreator.ShowSaveExpression(name, expresion, color, Expressions);
                            expressionCounter = int.Parse(name.Substring(10));

                        }
                    }
                    ExpressionContainer.Children.Remove(AddBtn);
                    ExpressionContainer.Children.Add(AddBtn);
                    AddBtn.Visibility = Visibility.Visible;
                    if (Expressions.Count > 0)
                    {
                        // Pobierz największy numer z kluczy ExpressionN
                        var max = Expressions.Keys
                            .Select(k => {
                                if (k.StartsWith("Expression") && int.TryParse(k.Substring(10), out int n)) return n;
                                return 0;
                            })
                            .Max();
                        expressionCounter = max + 1;
                    }
                    else
                    {
                        expressionCounter = 1;
                    }
                    GenerateGrid(sender, e);

                }
            }
            
        }
    }
}