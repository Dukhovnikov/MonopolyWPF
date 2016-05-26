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
        UserData Me;
        string UserName;
        Action<string> Log;


        public UserForm()
        {
            InitializeComponent();

            foreach (var item in Strits.strits)
            {
                ComboBoxItem temp = new ComboBoxItem();
                switch (item.Type)
                {
                    case Strit.Color.Коричневый:
                        temp.Background = Brushes.SandyBrown;
                        break;
                    case Strit.Color.Голубой:
                        temp.Background = Brushes.SkyBlue;
                        break;
                    case Strit.Color.Розовый:
                        temp.Background = Brushes.Plum;
                        break;
                    case Strit.Color.Оранжевый:
                        temp.Background = Brushes.Orange;
                        break;
                    case Strit.Color.Красный:
                        temp.Background = Brushes.OrangeRed;
                        break;
                    case Strit.Color.Желтый:
                        temp.Background = Brushes.Gold;
                        break;
                    case Strit.Color.Зеленый:
                        temp.Background = Brushes.MediumSeaGreen;
                        break;
                    case Strit.Color.Синий:
                        temp.Background = Brushes.RoyalBlue;
                        break;
                    case Strit.Color.ЖД:
                        temp.Background = Brushes.LightSlateGray;
                        break;
                    case Strit.Color.Электростанция:
                        temp.Background = Brushes.MintCream;
                        break;
                    case Strit.Color.Водопровод:
                        temp.Background = Brushes.MintCream;
                        break;
                    default:
                        break;
                }
                temp.Content = item.StritName;
                comboBox.Items.Add(temp);
            }
            house1.Visibility = Visibility.Hidden;
            house2.Visibility = Visibility.Hidden;
            house3.Visibility = Visibility.Hidden;
            house4.Visibility = Visibility.Hidden;
            hotel.Visibility = Visibility.Hidden;

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
            userConvert.RefreshHouse += UserConvert_RefreshHouse;
            Log = (S) =>
            {
                Action a = () =>
                {
                    listBox2.Items.Add(S);
                    listBox2.ScrollIntoView(listBox2.Items[listBox2.Items.Count - 1]);
                };
                Dispatcher.Invoke(a);
            };
        }

        private void UserConvert_RefreshHouse(byte arg1, byte arg2)
        {
            Strits.strits[arg1].HouseValue = arg2;
            Dispatcher.Invoke(() => listBox3.Items.Refresh());
            Dispatcher.Invoke(UpdateImgs);
        }

        private void UserConvert_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        private void User_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        /// <summary>
        /// Обновление списка улиц
        /// </summary>
        /// <param name="obj"></param>
        private void UserConvert_UpdateStrits(byte[] obj)
        {
            Action act = () =>
            {
                Me.StritNum = obj;
                listBox3.ItemsSource = Me.StritList;
            };
            this.Dispatcher.Invoke(act);

            //Action act = () =>
            //{
            //    Me.StritNum = obj;
            //    foreach (var item in Me.StritList)
            //    {
            //        ListBoxItem temp = new ListBoxItem();
            //        switch (item.Type)
            //        {
            //            case Strit.Color.Коричневый:
            //                temp.Background = Brushes.SandyBrown;
            //                break;
            //            case Strit.Color.Голубой:
            //                temp.Background = Brushes.SkyBlue;
            //                break;
            //            case Strit.Color.Розовый:
            //                temp.Background = Brushes.Plum;
            //                break;
            //            case Strit.Color.Оранжевый:
            //                temp.Background = Brushes.Orange;
            //                break;
            //            case Strit.Color.Красный:
            //                temp.Background = Brushes.OrangeRed;
            //                break;
            //            case Strit.Color.Желтый:
            //                temp.Background = Brushes.Gold;
            //                break;
            //            case Strit.Color.Зеленый:
            //                temp.Background = Brushes.MediumSeaGreen;
            //                break;
            //            case Strit.Color.Синий:
            //                temp.Background = Brushes.RoyalBlue;
            //                break;
            //            case Strit.Color.ЖД:
            //                temp.Background = Brushes.LightSlateGray;
            //                break;
            //            case Strit.Color.Электростанция:
            //                temp.Background = Brushes.MintCream;
            //                break;
            //            case Strit.Color.Водопровод:
            //                temp.Background = Brushes.MintCream;
            //                break;
            //            default:
            //                break;
            //        }
            //        temp.Content = item.StritName;
            //        listBox3.Items.Add(temp);
            //    }
            //};
            //this.Dispatcher.Invoke(act);
        }

        private void UserConvert_SystemMsg(string obj)
        {
            MessageBox.Show(obj);
            Log(obj);
        }

        /// <summary>
        /// Поступившее предложение от другого игрока
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        private void UserConvert_SellStrit(int arg1, byte arg2, string arg3)
        {
            string S = string.Format(" Предложение от: {0}\n Предложенная цена: {1}\n За улицу: {2}", arg3, arg1, Strits.strits[arg2]);
            if (MessageBox.Show(S, "Предложена сделака:", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                user.SendToSRV(string.Format("6:{0}:{1}:{2}", arg3, arg2, arg1));
                Log(S);
            }
        }


        /// <summary>
        /// Обработка сделки
        /// </summary>
        /// <param name="obj"></param>
        [STAThread]
        private void UserConvert_OwnerEP(string obj)
        {

            Thread th = new Thread((() =>
            {
                ///Ебал я в рот это окошко...
                Dispatcher.Invoke(() =>
                {
                    var form = new Transaction(user, UserName, obj, (byte)comboBox.SelectedIndex); form.ShowDialog(); form.Close();
                });
            }));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }

        private void UserConvert_AuctionStart(byte obj)
        {
            ///я хз как делать аукцион) по идее это должно быть подприложение (окно) где делают ставки...
            throw new NotImplementedException();
        }

        private void User_NewMsg(string obj)
        {
            userConvert.Parse(obj);
        }

        /// <summary>
        /// Обнавление баланса
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void UserConvert_UpdateDeposit(int arg1, string arg2)
        {
            MessageBox.Show(arg2);
            Action act = () => label.Content = arg1.ToString();
            this.Dispatcher.Invoke(act);
            Log(arg2);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
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
                        UserName = textBox.Text;
                        textBox.IsEnabled = false;
                        button5.IsEnabled = false;
                        Me = new UserData(UserName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            else
            {
                MessageBox.Show("Требуется задать имя игрока.", "Ошибка данных.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            (user as UserMessanger).Disconnect();
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //var form = new Transaction();
            //form.ShowDialog();
            user.SendToSRV("3:" + comboBox.SelectedIndex);
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        /// <summary>
        /// Отобразить информацию по улице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            UpdateImgs();
        }

        /// <summary>
        /// Обновить картинки домов
        /// </summary>
        void UpdateImgs()
        {
            if (listBox3.SelectedItem != null)
                switch ((listBox3.SelectedItem as Strit).HouseValue)
                {
                    case (0):
                        house1.Visibility = Visibility.Hidden;
                        house2.Visibility = Visibility.Hidden;
                        house3.Visibility = Visibility.Hidden;
                        house4.Visibility = Visibility.Hidden;
                        hotel.Visibility = Visibility.Hidden;
                        break;
                    case (1):
                        house1.Visibility = Visibility.Visible;
                        house2.Visibility = Visibility.Hidden;
                        house3.Visibility = Visibility.Hidden;
                        house4.Visibility = Visibility.Hidden;
                        hotel.Visibility = Visibility.Hidden;
                        break;
                    case (2):
                        house1.Visibility = Visibility.Visible;
                        house2.Visibility = Visibility.Visible;
                        house3.Visibility = Visibility.Hidden;
                        house4.Visibility = Visibility.Hidden;
                        hotel.Visibility = Visibility.Hidden;
                        break;
                    case (3):
                        house1.Visibility = Visibility.Visible;
                        house2.Visibility = Visibility.Visible;
                        house3.Visibility = Visibility.Visible;
                        house4.Visibility = Visibility.Hidden;
                        hotel.Visibility = Visibility.Hidden;
                        break;
                    case (4):
                        house1.Visibility = Visibility.Visible;
                        house2.Visibility = Visibility.Visible;
                        house3.Visibility = Visibility.Visible;
                        house4.Visibility = Visibility.Visible;
                        hotel.Visibility = Visibility.Hidden;
                        break;
                    case (5):
                        house1.Visibility = Visibility.Visible;
                        house2.Visibility = Visibility.Visible;
                        house3.Visibility = Visibility.Visible;
                        house4.Visibility = Visibility.Visible;
                        hotel.Visibility = Visibility.Visible;
                        break;
                }
        }

        /// <summary>
        /// Оплата ренты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            user.SendToSRV("5:" + comboBox.SelectedIndex);
            Log(string.Format("Оплата ренты на {0}", comboBox.SelectedItem));
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Покупка дома
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                int index = Strits.strits.IndexOf(Strits.strits.Where((a) => a.StritName == (listBox3.SelectedItem as Strit).StritName).ToArray()[0]);
                user.SendToSRV("2:" + index);
                Log(string.Format("Покупка дома на {0}", Strits.strits[index]));
            }
            else
                MessageBox.Show("Сначала необходимо выбрать улицу!");
        }
    }
}
