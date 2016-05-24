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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace User
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        IUserMessanger user = new UserMessanger();
        IUserMessangConverter userConvert = new UserMsgConverter();
        List<Thread> Threads = new List<Thread>(3);
        public UserForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.NewMsg += User_NewMsg;
            user.NewUDPMsg += User_NewMsg;
            user.Error += User_Error;
            userConvert.AuctionStart += UserConvert_AuctionStart;
            userConvert.OwnerEP += UserConvert_OwnerEP;
            userConvert.SellStrit += UserConvert_SellStrit;
            userConvert.SystemMsg += UserConvert_SystemMsg;
            userConvert.UpdateDeposit += UserConvert_UpdateDeposit;
            userConvert.UpdateStrits += UserConvert_UpdateStrits;
            userConvert.Error += UserConvert_Error;
        }

        private void UserConvert_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        private void User_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        private void UserConvert_UpdateStrits(byte[] obj)
        {
            Action act = () =>
            {
                listBox1.Items.Clear();
                foreach (var a in obj)
                    listBox1.Items.Add(a);
            };
            this.Dispatcher.Invoke(act);
        }

        private void UserConvert_SystemMsg(string obj)
        {
            MessageBox.Show(obj);
        }

        private void UserConvert_SellStrit(int arg1, byte arg2, string arg3)
        {
            if (MessageBox.Show(string.Format(" Предложение от: {0}\n Предложенная цена: {1}\n За улицу: {2}", arg3, arg1, arg2), "Предложена сделака:", MessageBoxButton.OKCancel, MessageBoxImage.Stop) == MessageBoxResult.OK)
                //тут надо написать отрпавку сообщения о продаже серверу
                MessageBox.Show("продано!");
        }

        private void UserConvert_OwnerEP(string obj)
        {
            throw new NotImplementedException();
        }

        private void UserConvert_AuctionStart(byte obj)
        {
            ///я хз как делать аукцион) по идее это должно быть подприложение (окно) где делают ставки...
            throw new NotImplementedException();
        }

        private void User_NewMsg(string obj)
        {
            Action act = () => listBox1.Items.Add(obj);
            this.Dispatcher.Invoke(act);
            userConvert.Parse(obj);
        }
        private void UserConvert_UpdateDeposit(int arg1, string arg2)
        {
            MessageBox.Show(arg2);
            Action act = () => label.Content = arg1.ToString();
            this.Dispatcher.Invoke(act);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                user.FindServer();
                if ((user as UserMessanger).ServerIP != null)
                {
                    MessageBox.Show(string.Format("Найден сервер: {0}", (user as UserMessanger).ServerIP));
                    user.ConnectToSRV(textBox.Text);
                    Thread th = new Thread(user.ListenTCP);
                    Thread th2 = new Thread(user.ListenUDP);
                    Threads.Add(th);
                    Threads.Add(th2);
                    th.IsBackground = true;
                    th2.IsBackground = true;
                    th.Start();
                    th2.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            (user as UserMessanger).Disconnect();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            user.SendToSRV(textBox1.Text);
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }
        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var form = new Transaction();
            form.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
                    }
    }
}
