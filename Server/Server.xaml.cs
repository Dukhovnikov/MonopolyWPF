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
        List<WpfApplication1.UserData> Users = new List<WpfApplication1.UserData>(8);

        public ServerForm()
        {
            InitializeComponent();
            S1.NewMessage += S1_NewMessage;
            S1.ClientDisconnect += S1_ClientDisconnect;
            S1.UserConnected += S1_ClientConnect;
            SrvMsgConvertet.acceptDeal += SrvMsgConvertet_acceptDeal;
            SrvMsgConvertet.AskSelling += SrvMsgConvertet_AskSelling;
            SrvMsgConvertet.Auction += SrvMsgConvertet_Auction;
            SrvMsgConvertet.BayHouse += SrvMsgConvertet_BayHouse;
            SrvMsgConvertet.DepositUpdate += SrvMsgConvertet_DepositUpdate;
            SrvMsgConvertet.Error += SrvMsgConvertet_Error;
            SrvMsgConvertet.establish += SrvMsgConvertet_establish;
            SrvMsgConvertet.Rent += SrvMsgConvertet_Rent;
        }

        private void SrvMsgConvertet_Rent(byte ID, byte arg2)
        {
            //Users[ID].
        }

        private void SrvMsgConvertet_establish(byte ID, byte arg2)
        {
            throw new NotImplementedException();
        }

        private void SrvMsgConvertet_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        private void SrvMsgConvertet_DepositUpdate(byte ID, int arg2, string arg3)
        {
            throw new NotImplementedException();
        }

        private void SrvMsgConvertet_BayHouse(byte ID, byte arg2)
        {
            throw new NotImplementedException();
        }

        private void SrvMsgConvertet_Auction(byte ID, int arg2)
        {
            throw new NotImplementedException();
        }

        private void SrvMsgConvertet_AskSelling(byte ID, byte arg2)
        {
            throw new NotImplementedException();
        }

        private void SrvMsgConvertet_acceptDeal(byte ID, byte arg2, byte arg3, int arg4)
        {
            throw new NotImplementedException();
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
            SrvMsgConvertet.Parse(obj);
            
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
