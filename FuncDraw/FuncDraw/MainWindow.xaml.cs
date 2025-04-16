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
        public MainWindow()
        {
            InitializeComponent();
            ShowHideBtn.Click += ShowHideBtn_Click;

        }
        private void ShowHideBtn_Click(object sender, RoutedEventArgs e)
        {
           editor.Width = editor.Width == new GridLength(20) ?  new GridLength(200) : new GridLength(20);
        }

    }
}