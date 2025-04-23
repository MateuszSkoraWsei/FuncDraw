using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ColorDialog = System.Windows.Forms.ColorDialog;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;
using Orientation = System.Windows.Controls.Orientation;
using Panel = System.Windows.Controls.Panel;
using Label = System.Windows.Controls.Label;


namespace FuncDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public int size = 10 ;
        public int elementCounter = 1;
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
            if (e.Delta > 0)
            {
                size += 1;
            }
            else
            {
                size -= 1;
                if (size < 1) size = 1;
            }
            GenerateGrid(sender, e);
        }
        public void GenerateGrid(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            for (int i = 0; i < MainGrid.ActualWidth ; i += size )
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
            for(int i = 0; i < MainGrid.ActualHeight; i += size)
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

            // storzenie osi x i y 
            int xCenter = (int) (Math.Floor(MainGrid.ActualWidth / size))/2 * size ;
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
                X1 = xCenter - size/2,
                Y1 = size,
                X2 = xCenter,
                Y2 = 0
            };
            Line yArrow2 = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                X1 = xCenter + size/2,
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


            //dodanie eventu do przycisku createBtn


            createBtn.Click += (s, e) =>
            {
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
                    Name = $"Square{elementCounter}",
                    Width = 20,
                    Height = 20,
                    Fill = new System.Windows.Media.SolidColorBrush(color),
                    Margin = new Thickness(5),
                };

                Label lblFuntion = new Label()
                {
                    Name = $"LabelFunction{elementCounter}",
                    Content = textBox.Text,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    Width = 100,
                    Height = 30,

                };

                Button menuButton = new Button()
                {
                    Name = $"MenuButton{elementCounter}",
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