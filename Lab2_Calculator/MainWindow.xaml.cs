using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Lab2_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void All_Click(object sender, RoutedEventArgs e)
        { 
            //Условие, чтобы числа не выходили за рамки
            if (Window_Result.Text.Length > 20)
            {

            }

            //Условие, чтобы не возникало чисел вида "088"
            else if (Window_Result.Text == "0")
            {
                Window_Result.Text = ((Button)sender).Content.ToString();
            }

            else
            {
                Window_Result.Text += ((Button)sender).Content;
            }
        }

        private void b0_Click(object sender, RoutedEventArgs e)
        {
            //Условие, чтобы числа не выходили за рамки
            if (Window_Result.Text.Length > 20)
            {

            }

            //Условие, чтобы не возникало чисел вида "00000000"
            else if (Window_Result.Text == "0")
            {
                Window_Result.Text = "0";
            }

            else
            {
                Window_Result.Text += ((Button)sender).Content;
            }
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            Window_Result.Text = "";
        }

        private void bResult_Click(object sender, RoutedEventArgs e)
        { string result="";
            
            //конструкция, чтобы не возникало ошибки при неверной введенной строке типа "3++"
            try
            {   if (Window_Result.Text.Length > 2)
                {
                    if (Window_Result.Text.Substring(Window_Result.Text.Length - 2, 2) == "/0")
                    {
                        result = "Нельзя делить на 0";
                    }
                    else
                    {
                        result = new DataTable().Compute(Window_Result.Text, null).ToString();
                        if (write_on.IsChecked == true) {
                            History.Items.Add(Window_Result.Text);
                        }
                        
                    }
                }
            //условие, чтобы при null или значении, не являющимся выражением выводился 0, а не ошибка
                else
                {
                    result = "0";
                }
                
            }
            catch(System.Data.SyntaxErrorException)
            {   
               
            }

            //Условие, чтобы числа не выходили за рамки
            if (result.Length > 20)
            {
                Window_Result.Text = "Слишком большое число";
            }

            //Условие, чтобы "," выглядела как "."
            else if (result.Contains(","))
            {
                result = result.Replace(",", ".");
                Window_Result.Text = result;
            }

            else { Window_Result.Text = result; }
        }

        private void visibility_on_Checked(object sender, RoutedEventArgs e)
        {
            History.Visibility = Visibility.Visible;
            write_on.Visibility = Visibility.Visible;
            b_clear_history.Visibility = Visibility.Visible;
            b_delete.Visibility = Visibility.Visible;

        }

        private void visibility_off_Checked(object sender, RoutedEventArgs e)
        {
            History.Visibility = Visibility.Hidden;
            write_on.Visibility = Visibility.Hidden;
            b_clear_history.Visibility = Visibility.Hidden;
            b_delete.Visibility = Visibility.Hidden;
            if (write_on.IsChecked == true)
            {
                write_on.IsChecked = false;
            }
        }

        private void b_clear_history_Click(object sender, RoutedEventArgs e)
        {
            History.Items.Clear();
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            if (History.Items.Count > 0) {
                History.Items.Remove(History.Items[History.Items.Count-1]);
            }
        }
    }
}
