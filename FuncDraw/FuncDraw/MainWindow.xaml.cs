﻿using System.Text;
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

namespace FuncDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, object> Expressions = new Dictionary<string, object>();
        
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
        public void GenerateGrid(object sender, RoutedEventArgs e)
        {
            GridDrower grid = new GridDrower(MainGrid, size, scaleValue);
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

                int x = (int)(MainGrid.ActualWidth);
                int y = (int)(MainGrid.ActualHeight);
                int bigger = x > y ? x : y;
                // stworzneie pętli 
                double begining = Math.Ceiling(-5 - (bigger / 2) / size) * scaleValue;
                double end = begining * (-1);
                

                PointsGenerator pointsGenerator = new PointsGenerator();
                if (correctedExpression.Contains("^"))
                {
                    for (double i = begining; i < end; i += (scaleValue/2))
                    {
                        // dodanie do listy
                        List<string> tokens = Tokenizer.Tokenize(correctedExpression);
                        CalculatePosition calculate = new CalculatePosition(i, i, tokens, output);
                        string XYposition = calculate.FindEquasion();
                        double X = double.Parse(XYposition.Split(":")[0]);
                        X = X * skala + xCenter;
                        double Y = double.Parse(XYposition.Split(":")[1]);
                        Y =  yCenter - Y * skala;
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
                        X = X * skala + xCenter;
                        double Y = double.Parse(XYposition.Split(":")[1]);
                        Y = yCenter - Y * skala;
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
            System.Windows.Media.Color color = System.Windows.Media.Colors.Red;

            AddBtn.Visibility = Visibility.Collapsed;
            Border border = new Border();

            StackPanel stackPanel = new StackPanel()
            {
                Width = ExpressionContainer.ActualWidth - 10 ,
                Height = 30,
                Background = Brushes.LightGray,
                Orientation = Orientation.Horizontal,

            };
            Button setColor = new Button()
            {
                Content = "",
                Background = Brushes.Red,
                Width = 20,
                Height = 20,
                Margin = new Thickness(5),

            };
            TextBox textBox = new TextBox()
            {
                Width = 100,
                Height = 20,
                Margin = new Thickness(5),
                
            };


            Button createBtn = new Button()
            {
                Name = $"b{expressionCounter}",
                Content = "✔",
                Width = 20,
                Height = 20,
                Margin = new Thickness(5),


            };
            

            // dodawanie elementów do siebie
            border.Child = stackPanel;
            stackPanel.Children.Add(setColor);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(createBtn);
            //dodawanie do stackPanel
            ExpressionContainer.Children.Add(border);

            // dodanie RegEx 

            textBox.PreviewTextInput += (s, e) =>
            {
                string pattern = @"^[xyXY().+\-^*=\/\d]+$";
                if (!Regex.IsMatch(e.Text, pattern))
                {
                    e.Handled = true; // zapobiega dalszemu przetwarzaniu zdarzenia
                }
                
                
            };
            textBox.LostFocus += (s, e) =>
            {
                //string pattern = @"^ *[xyXY] *= *(( *\( *-?\d+(\.\d+)? *\) *|-?\d+ *( *\.\d+ *)?|[xyXY]|[xyXY] *\^ *\d+)( *[+\-*\/] *( *\( *-? *\d+(\.\d+)? *\) *| *-? *\d+(\.\d+)? *| *[xyXY] *| *[xyXY] *\^ *\d+ *))*)*( *[+\-*\/] *\(-?\d+(\.\d+)?\))*$";
                string pattern = @"^\s*[xyXY]\s*=\s*(-?\s*)?((\d+(\.\d+)?|[xyXY]|\([^()]+\))(\s*\^?\s*\d*)?)(\s*[-+*\/]?\s*(-?\s*(\d+(\.\d+)?|[xyXY]|\([^()]+\))(\s*\^?\s*\d*)?))*\s*$";
                if (!Regex.IsMatch(textBox.Text, pattern))
                {
                    MessageBox.Show("Invalid expression format. Please use the format: x=expression or y=expression");

                    textBox.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        textBox.Focus();
                        
                    }), System.Windows.Threading.DispatcherPriority.Input);
                   
                }
            };

            #region createExpressions
            //dodanie eventu do przycisku createBtn


            createBtn.Click += (s, e) =>
            {
                
                Expressions.Add($"Expression{createBtn.Name.Substring(1)}", new { expresion = textBox.Text, color = color });
                
                int index = ExpressionContainer.Children.IndexOf(border);
                // stworzenie stackPanelu FunctionDisplay$ - wyświetlenie informacji o utworzonej funkcji  
                StackPanel functionDisplay = new StackPanel()
                {
                    Width = ExpressionContainer.ActualWidth - 10,
                    Height = 30,
                    Background = Brushes.LightGray,
                    Orientation = Orientation.Horizontal,
                };
                Shape square = new System.Windows.Shapes.Rectangle()
                {
                    Name = $"Square{expressionCounter}",
                    Width = 20,
                    Height = 20,
                    Fill = new System.Windows.Media.SolidColorBrush(color),
                    Margin = new Thickness(5),
                };

                Label lblFuntion = new Label()
                {
                    Name = $"LabelFunction{expressionCounter}",
                    Content = textBox.Text,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    Width = 100,
                    Height = 30,

                };

                Button menuButton = new Button()
                {
                    Name = $"MB{expressionCounter}",
                    Content = "...",
                    Width = 20,
                    Height = 20,
                    Margin = new Thickness(5),
                };
                //dodanie elementów do siebie
                functionDisplay.Children.Add(square);
                functionDisplay.Children.Add(lblFuntion);
                functionDisplay.Children.Add(menuButton);

                //dodanie do stackPanel
                ExpressionContainer.Children.Insert(index ,functionDisplay);

                //dodanie eventu do przycisku menuButton
                expressionCounter++;
                menuButton.Click += (s, e) =>
                {
                    //dodanie menu kontekstowego
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem deleteItem = new MenuItem() { Header = "Delete" , Icon = "X" };
                    deleteItem.Click += (s, e) =>
                    {
                        if( menuButton.Parent is StackPanel funcitonDisplay )
                        {
                           
                            ExpressionContainer.Children.Remove(funcitonDisplay);
                            Expressions.Remove($"Expression{menuButton.Name.Substring(2)}");
                            

                        }


                    };
                    MenuItem editItem = new MenuItem() { Header = "Edit" , Icon = "e"};
                    editItem.Click += (s, e) =>
                    {
                        //dodanie edytora
                        
                    };
                    contextMenu.Items.Add(deleteItem);
                    contextMenu.Items.Add(editItem);
                    menuButton.ContextMenu = contextMenu;
                    contextMenu.IsOpen = true;
                };

                //usunięcie / schowanie siebie ( border) 
                border.Visibility = Visibility.Collapsed;

                var parent = AddBtn.Parent as Panel;
                if (parent != null)
                {
                    parent.Children.Remove(AddBtn);
                }
                
                ExpressionContainer.Children.Insert(index + 1, AddBtn);
                AddBtn.Visibility = Visibility.Visible;
            };
            #endregion

            //dodawanie eventu do przycisku setColor
            setColor.Click += (s, e) =>
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    color = System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                    setColor.Background = new System.Windows.Media.SolidColorBrush(color);
                }
            };
            

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

    }
}