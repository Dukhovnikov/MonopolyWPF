using System;
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
using System.Windows.Shapes;

namespace User
{
    /// <summary>
    /// Логика взаимодействия для Transaction.xaml
    /// </summary>
    public partial class Transaction : Window
    {
        public Transaction()
        {
            InitializeComponent();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
