using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Button = System.Windows.Controls.Button;
using Brushes = System.Windows.Media.Brushes;
using TextBox = System.Windows.Controls.TextBox;
using Orientation = System.Windows.Controls.Orientation;
using System.Text.RegularExpressions;
using MessageBox = System.Windows.MessageBox;
using Label = System.Windows.Controls.Label;
using Panel = System.Windows.Controls.Panel;
using Color = System.Windows.Media.Color;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FuncDraw
{
    public class exprCreator
    {
        
        
        
        private Button _addBtn;
        private StackPanel _expressionContainer;
        private int _expressionCounter ;
        
        


        public exprCreator(  Button addBtn , StackPanel expressionContainer , int expressionCounter = 1 )
        {
            
           
            this._addBtn = addBtn;
            this._expressionContainer = expressionContainer;
            this._expressionCounter = expressionCounter;
            


        }
        public void ExpressionCreator(Dictionary<string, object> expressions )
        {
            System.Windows.Media.Color color = System.Windows.Media.Colors.Red;

            _addBtn.Visibility = Visibility.Collapsed;
            Border border = new Border();

            StackPanel stackPanel = new StackPanel()
            {
                Width = _expressionContainer.ActualWidth - 10,
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
                Name = $"b{_expressionCounter}",
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
            _expressionContainer.Children.Add(border);

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
            createBtn.Click += (s, e) =>
            {
                if(!expressions.ContainsKey($"Expression{createBtn.Name.Substring(1)}"))
                {
                    expressions.Add($"Expression{createBtn.Name.Substring(1)}", new { expresion = textBox.Text, color = color });
                }
                ShowExpression(createBtn, textBox, color, expressions, border);
            };
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
        int index = 0;
        public void ShowSaveExpression(string name, string expression , Color color , Dictionary<string, object> expressions)
        {
             _addBtn.Visibility = Visibility.Collapsed;
            StackPanel functionDisplay = new StackPanel()
            {
                Width = _expressionContainer.ActualWidth - 10,
                Height = 30,
                Background = Brushes.LightGray,
                Orientation = Orientation.Horizontal,
            };
            Shape square = new System.Windows.Shapes.Rectangle()
            {
                Name = $"Square{_expressionCounter}",
                Width = 20,
                Height = 20,
                Fill = new System.Windows.Media.SolidColorBrush(color),
                Margin = new Thickness(5),
            };

            Label lblFuntion = new Label()
            {
                Name = $"LabelFunction{_expressionCounter}",
                Content = expression,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Width = 100,
                Height = 30,

            };

            Button menuButton = new Button()
            {
                Name = $"MB{_expressionCounter}",
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
            _expressionContainer.Children.Add( functionDisplay);

            //dodanie eventu do przycisku menuButton
            _expressionCounter++;
            menuButton.Click += (s, e) =>
            {
                //dodanie menu kontekstowego
                ContextMenu contextMenu = new ContextMenu();
                MenuItem deleteItem = new MenuItem() { Header = "Delete", Icon = "X" };
                deleteItem.Click += (s, e) =>
                {
                    if (menuButton.Parent is StackPanel funcitonDisplay)
                    {

                        _expressionContainer.Children.Remove(funcitonDisplay);
                        expressions.Remove($"Expression{menuButton.Name.Substring(2)}");


                    }


                };
                MenuItem editItem = new MenuItem() { Header = "Edit", Icon = "e" };
                editItem.Click += (s, e) =>
                {
                    //dodanie edytora
                    var key = $"Expression{menuButton.Name.Substring(2)}";
                    dynamic expr = expressions[key];
                    int index = _expressionContainer.Children.IndexOf(functionDisplay);
                    _expressionContainer.Children.RemoveAt(index);
                    EditExpression(key, expr.expresion, expr.color, _expressionCounter, expressions, index);


                };
                contextMenu.Items.Add(deleteItem);
                contextMenu.Items.Add(editItem);
                menuButton.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            };

            //usunięcie / schowanie siebie ( border) 
            

            var parent = _addBtn.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(_addBtn);
            }

            _expressionContainer.Children.Insert(index + 1, _addBtn);
            _addBtn.Visibility = Visibility.Visible;
            index++;
        }
        public void ShowExpression(Button createBtn , TextBox textBox , Color color , Dictionary<string , object > expressions , Border border)
        {
            

            int index = _expressionContainer.Children.IndexOf(border);
            // stworzenie stackPanelu FunctionDisplay$ - wyświetlenie informacji o utworzonej funkcji  
            StackPanel functionDisplay = new StackPanel()
            {
                Width = _expressionContainer.ActualWidth - 10,
                Height = 30,
                Background = Brushes.LightGray,
                Orientation = Orientation.Horizontal,
            };
            Shape square = new System.Windows.Shapes.Rectangle()
            {
                Name = $"Square{_expressionCounter}",
                Width = 20,
                Height = 20,
                Fill = new System.Windows.Media.SolidColorBrush(color),
                Margin = new Thickness(5),
            };

            Label lblFuntion = new Label()
            {
                Name = $"LabelFunction{_expressionCounter}",
                Content = textBox.Text,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Width = 100,
                Height = 30,

            };

            Button menuButton = new Button()
            {
                Name = $"MB{_expressionCounter}",
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
            _expressionContainer.Children.Insert(index, functionDisplay);

            //dodanie eventu do przycisku menuButton
            _expressionCounter++;
            menuButton.Click += (s, e) =>
            {
                //dodanie menu kontekstowego
                ContextMenu contextMenu = new ContextMenu();
                MenuItem deleteItem = new MenuItem() { Header = "Delete", Icon = "X" };
                deleteItem.Click += (s, e) =>
                {
                    if (menuButton.Parent is StackPanel funcitonDisplay)
                    {

                        _expressionContainer.Children.Remove(funcitonDisplay);
                        expressions.Remove($"Expression{menuButton.Name.Substring(2)}");


                    }


                };
                MenuItem editItem = new MenuItem() { Header = "Edit", Icon = "e" };
                editItem.Click += (s, e) =>
                {
                    //dodanie edytora
                    var key = $"Expression{menuButton.Name.Substring(2)}";
                    dynamic expr = expressions[key];
                    int index = _expressionContainer.Children.IndexOf(functionDisplay);
                    _expressionContainer.Children.RemoveAt(index);
                    EditExpression(key, expr.expresion, expr.color, _expressionCounter, expressions, index);
                    

                };
                contextMenu.Items.Add(deleteItem);
                contextMenu.Items.Add(editItem);
                menuButton.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            };

            //usunięcie / schowanie siebie ( border) 
            border.Visibility = Visibility.Collapsed;

            var parent = _addBtn.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(_addBtn);
            }

            _expressionContainer.Children.Insert(index + 1, _addBtn);
            _addBtn.Visibility = Visibility.Visible;

        }
        public void EditExpression(string key,string expression , Color color , int counter, Dictionary<string,object> expressions , int index)
        {
            

            _addBtn.Visibility = Visibility.Collapsed;
            Border border = new Border();

            StackPanel stackPanel = new StackPanel()
            {
                Width = _expressionContainer.ActualWidth - 10,
                Height = 30,
                Background = Brushes.LightGray,
                Orientation = Orientation.Horizontal,

            };
            Button setColor = new Button()
            {
                Content = "",
                Background = new System.Windows.Media.SolidColorBrush(color),
                Width = 20,
                Height = 20,
                Margin = new Thickness(5),

            };
            TextBox textBox = new TextBox()
            {
                Width = 100,
                Height = 20,
                Margin = new Thickness(5),
                Text = expression
            };


            Button createBtn = new Button()
            {
                Name = $"b{_expressionCounter}",
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
            _expressionContainer.Children.Insert(index, border);

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
            createBtn.Click += (s, e) =>
            {
                expressions[key] = new { expresion = textBox.Text, color = color };
                ShowExpression(createBtn, textBox, color, expressions, border);
                border.Visibility = Visibility.Collapsed;
                _addBtn.Visibility = Visibility.Visible;
            };
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

    }
}
