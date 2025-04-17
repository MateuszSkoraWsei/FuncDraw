using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FuncDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public int size = 10 ;
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
        }
        public void GenerateExpresionCreator(object sender, RoutedEventArgs e)
        {
            
            
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
                var parent = AddBtn.Parent as Panel;
                if (parent != null)
                {
                    parent.Children.Remove(AddBtn);
                }
                int index = ExpressionContainer.Children.IndexOf(border);
                ExpressionContainer.Children.Insert(index + 1, AddBtn);
                AddBtn.Visibility = Visibility.Visible;
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