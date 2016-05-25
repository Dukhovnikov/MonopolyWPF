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
        byte strit;
        public Transaction()
        {
            InitializeComponent();

        }

        public Transaction(IUserMessanger user, string username, string OtherUser, byte strit)
        {
            InitializeComponent();
            this.user = user;
            this.OtherUser = OtherUser;
            this.username = username;
            this.strit = strit;
        }

        /// <summary>
        /// Выполняет удаление текста при щелчке мыши.
        /// </summary>
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        /// <summary>
        /// Закрытие формы.
        /// </summary>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Предлагает покупку улицы.
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            user.UDPSend("3:" + textBox.Text + ":" + strit + ":" + username, OtherUser);
            Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
