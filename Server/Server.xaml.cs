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
namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ServerForm : Window
    {
        IServerMessanger S1 = new ServerMessanger();

        public ServerForm()
        {
            InitializeComponent();
            S1.NewMessage += S1_NewMessage;
            S1.ClientDisconnect += S1_ClientDisconnect;
            S1.UserConnected += S1_ClientConnect;
        }

        private void S1_ClientConnect(string obj)
        {
            Action Log = () => listBox.Items.Add(obj + " has been connected\n");
            Dispatcher.Invoke(Log);
            Action AddName = () => comboBox.Items.Add(obj);
            Dispatcher.Invoke(AddName);
        }

        private void S1_ClientDisconnect(string obj)
        {
            MessageBox.Show(string.Format("User {0} has been disconected", obj));
            Action Log = () => listBox.Items.Add(obj + " has been disconnected\n");
            Dispatcher.Invoke(Log);
            Action Clear = () => comboBox.Items.Remove("User " + obj);
            Dispatcher.Invoke(Clear);
        }

        private void S1_NewMessage(string obj)
        {
            Action Log = () => listBox.Items.Add(obj);
            Dispatcher.Invoke(Log);
            //тут обработка сообщения
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add("Server is ready!\n waiting users");
            new Thread(S1.UDPShown).Start();
            new Thread(S1.StartSRV).Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add("Start game");
            S1.ListenAll();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            S1.SendTo((byte)comboBox.SelectedIndex, textBox.Text);
        }
    }
}
