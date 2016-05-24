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
        string username;
        string OtherUser;
        IUserMessanger user;
        public Transaction()
        {
            InitializeComponent();

        }

        public Transaction(IUserMessanger user ,string username, string OtherUser)
        {
            InitializeComponent();
            this.user = user;
            this.OtherUser = OtherUser;
            this.username = username;
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            user.UDPSend("5:" + textBox.Text+":"+user, OtherUser);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
