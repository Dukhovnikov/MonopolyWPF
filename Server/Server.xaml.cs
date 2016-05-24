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
using WpfApplication1;


namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ServerForm : Window
    {
        IServerMessanger S1 = new ServerMessanger();
        public static List<WpfApplication1.UserData> Users = new List<WpfApplication1.UserData>(8);

        Action<string> Log;

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
            //Метод для лога
            Log = (s) =>
            {
                Action a = () => listBox.Items.Add(s);
                Dispatcher.Invoke(a);
            };
            }

        /// <summary>
        /// Влатим ренту
        /// </summary>
        /// <param name="ID">плательщик</param>
        /// <param name="arg2">Улица</param>
        private void SrvMsgConvertet_Rent(byte ID, byte arg2)
        {
            try
            {
                //Проверяем есть ли владелец
                if (Strits.strits[arg2].Owner != null)
                {
                    ///Списываем с плательщика и записываем владельцу
                    Users[ID].reason = string.Format("Rent is {0}", Strits.strits[arg2].Rent[Strits.strits[arg2].HouseValue]);
                    Users[ID].Deposit -= Strits.strits[arg2].Rent[Strits.strits[arg2].HouseValue];
                    Strits.strits[arg2].Owner.reason = string.Format("Rent from user {0}, street {1}", Users[ID].UserName, Strits.strits[arg2]);
                    Strits.strits[arg2].Owner.Deposit += Strits.strits[arg2].Rent[Strits.strits[arg2].HouseValue];
                    Log(string.Format("Игрок {0} заплатил пользователю {1} ренту за улицу {2} ({3})", Users[ID], Strits.strits[arg2].Owner, Strits.strits[arg2].Rent));
                }
                else
                    S1.SendTo(ID, SrvMsgConvertet.Create(new string[] { SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "This strit hadn't owner!\nIt can be yours!" }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Закладывайся!
        /// </summary>
        /// <param name="ID">Вот ты</param>
        /// <param name="arg2">Вот здесь</param>
        private void SrvMsgConvertet_establish(byte ID, byte arg2)
        {
            try
            {
                ///проверяем свою ли улицу решил заложить
                if (Strits.strits[arg2].Owner == Users[ID])
                {
                    if (!Strits.strits[arg2].IsLaid)
                    {
                        //Закладываем
                        Strits.strits[arg2].IsLaid = true;
                        Users[ID].reason = "Street was establish";
                        Strits.strits[arg2].Owner.Deposit += Strits.strits[arg2].StreetPrice / 2;
                        Log(string.Format("Игрок {0} заложил улицу {1} (+{2})", Users[ID], Strits.strits[arg2], Strits.strits[arg2].StreetPrice / 2));

                    }
                    else
                    {
                        //Выкупаем
                        Strits.strits[arg2].IsLaid = true;
                        Users[ID].reason = "Street was deestablish";
                        Strits.strits[arg2].Owner.Deposit -= (int)(Strits.strits[arg2].StreetPrice / 1.8);
                        Log(string.Format("Игрок {0} выкупил улицу {1} (-{2})", Users[ID], Strits.strits[arg2], Strits.strits[arg2].StreetPrice / 1.8));

                    }
                }
                else
                    S1.SendTo(ID, SrvMsgConvertet.Create(new string[] { SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "This strit is not yours!" }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Полученно не корректное сообщение
        /// </summary>
        /// <param name="obj"></param>
        private void SrvMsgConvertet_Error(string obj)
        {
            MessageBox.Show(obj);
        }

        /// <summary>
        /// Кто то говорит что хочет поменять свой баланс
        /// </summary>
        /// <param name="ID">Игрок</param>
        /// <param name="arg2">Требуемый баланс</param>
        /// <param name="arg3">Причина</param>
        private void SrvMsgConvertet_DepositUpdate(byte ID, int arg2, string arg3)
        {
            //Спрашиваем легально ли это
            if (MessageBox.Show(string.Format("User {0} wonna change balance to {1} ({2})\n The rison is: {3}", Users[ID].UserName, arg2, arg2 - Users[ID].Deposit, arg3), "Deposit change", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //Меняем баланс
                Users[ID].reason = arg3;
                Users[ID].Deposit = arg2;
            }
            else
                //Посылаем нафиг
                S1.SendTo(ID, SrvMsgConvertet.Create(new string[] {
                    SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "Access deny!" }));
        }

        /// <summary>
        /// Покупка дома
        /// </summary>
        /// <param name="ID">Владелец</param>
        /// <param name="arg2">улица</param>
        private void SrvMsgConvertet_BayHouse(byte ID, byte arg2)
        {
            try
            {
                ///проверяем на своей ли улице
                if (Strits.strits[arg2].Owner == Users[ID])
                {
                    ///тут дико сложное условие проверки
                    var OneColor = (from str in Strits.strits
                                    where str.Type == Strits.strits[arg2].Type
                                    select str).ToList();
                    bool Good = true;
                    //ну для начала проверим на максимум
                    if (Strits.strits[arg2].HouseValue < 5)
                        foreach (var a in OneColor)
                        {
                            //Проверяем чтоб все улицы принадлежали игроку
                            if (a.Owner != Users[ID])
                                Good = false;
                            //Проверяем чтоб на всех удицах было домов не меньше чем на данной
                            if (a.HouseValue < Strits.strits[arg2].HouseValue)
                                Good = false;
                        }
                    else
                        S1.SendTo(ID, SrvMsgConvertet.Create(new string[] {
                            SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "Max house value!" }));
                    if (Good)
                    {
                        //Одобряем операцию
                        Strits.strits[arg2].HouseValue++;
                        Users[ID].reason = string.Format("House was bought");
                        Users[ID].Deposit -= Strits.strits[arg2].HousePrice;
                        Log(string.Format("Игрок {0} построил дом на улице {1} (-{2})", Users[ID], Strits.strits[arg2], Strits.strits[arg2].HousePrice));

                    }
                    else
                        S1.SendTo(ID, SrvMsgConvertet.Create(new string[] {
                            SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "You can't build house here!" }));
                }
                else
                    S1.SendTo(ID, SrvMsgConvertet.Create(new string[] {
                        SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "This strit is not yours!" }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Ставка на аукционе
        /// </summary>
        /// <param name="ID">Потльзователь</param>
        /// <param name="arg2">Индекс улицы</param>
        private void SrvMsgConvertet_Auction(byte ID, int arg2)
        {
            //Пока не будем реализовывать аукцион
            throw new NotImplementedException();
        }

        /// <summary>
        /// Запрос на покупку улицы
        /// </summary>
        /// <param name="ID">Игрок</param>
        /// <param name="arg2">Улица</param>
        private void SrvMsgConvertet_AskSelling(byte ID, byte arg2)
        {
            try
            {
                //Спрашиваем легально ли это
                if (MessageBox.Show(string.Format("Игрок {0} хочет купить улицу {1} ", Users[ID].UserName, Strits.strits[arg2]), "Покупка улицы", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    if (Strits.strits[arg2].Owner == null)
                    {
                        Strits.strits[arg2].Owner = Users[ID];
                        Users[ID].reason = string.Format("Strit {0} was bought", Strits.strits[arg2]);
                        Users[ID].Deposit -= Strits.strits[arg2].StreetPrice;
                        var a = Users[ID].StritNum != null ? Users[ID].StritNum.ToList() : new List<int>();
                        a.Add(arg2);
                        Users[ID].StritNum = a.ToArray();
                        Log(string.Format("Игрок {0} приобрел улицу {1} (-{2})", Users[ID], Strits.strits[arg2], Strits.strits[arg2].StreetPrice));

                    }
                    else
                    {
                        S1.SendTo(ID, SrvMsgConvertet.Create(new string[]
                        {SrvMsgConvertet.OutMsgType.OtherOwner.GetHashCode().ToString(), Strits.strits[arg2].Owner.getSocket() }));
                    }
                else
                    S1.SendTo(ID, SrvMsgConvertet.Create(new string[]
                                            {SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "Банк отказал вам в покупке!" }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Подтверждение сделки
        /// </summary>
        /// <param name="ID1"></param>
        /// <param name="ID2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        private void SrvMsgConvertet_acceptDeal(byte ID1, byte ID2, byte arg3, int arg4)
        {
            try
            {
                ///проверяем свою ли улицу решил заложить
                if (Strits.strits[arg3].Owner == Users[ID1])
                {
                    Users[ID1].StritNum = (Users[ID1].StritNum.ToList().Where((a) => a != arg3)).ToArray();
                    var t = Users[ID1].StritNum.ToList();
                    t.Add(arg3);
                    Users[ID2].StritNum = t.ToArray();
                    Users[ID1].reason = string.Format("Strit {0} was sell", Strits.strits[arg3]);
                    Users[ID2].reason = string.Format("Strit {0} was bought", Strits.strits[arg3]);
                    Users[ID1].Deposit += arg4;
                    Users[ID2].Deposit -= arg4;
                    Log(string.Format("Игрок {0} продал улицу {2} игроку {1} за {3})", Users[ID1], Users[ID2], Strits.strits[arg3], arg4));
                }
                else
                    S1.SendTo(ID1, SrvMsgConvertet.Create(new string[] { SrvMsgConvertet.OutMsgType.SystemMsg.GetHashCode().ToString(), "This strit is not yours!" }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void S1_ClientConnect(string obj, System.Net.IPEndPoint ep)
        {
            Log(string.Format("Игрок {0} Присоединился к игре", obj));
            Action AddName = () => comboBox.Items.Add(obj);
            Dispatcher.Invoke(AddName);
            Users.Add(new UserData(obj, ep));
            Action AddName2 = () => listBox_Copy.Items.Add(Users.Last());
            Dispatcher.Invoke(AddName2);
            Users.Last().OnDepositChange += ServerForm_OnDepositChange;
            Users.Last().OnStritsChange += ServerForm_OnStritsChange;
        }

        private void ServerForm_OnDepositChange(UserData sender)
        {
            S1.SendTo(sender.UserName, SrvMsgConvertet.Create(new string[] { SrvMsgConvertet.OutMsgType.DepositUpdate.GetHashCode().ToString(), sender.Deposit.ToString(), sender.reason }));
        }

        private void ServerForm_OnStritsChange(UserData sender)
        {
            string[] S = new string[] {
                SrvMsgConvertet.OutMsgType.StritsUpdate.GetHashCode().ToString()};
            var t = S.ToList();
            foreach (var a in sender.StritNum)
                t.Add(a.ToString());
            S1.SendTo(sender.UserName, SrvMsgConvertet.Create(t.ToArray()));
        }

        private void S1_ClientDisconnect(string obj)
        {
            MessageBox.Show(string.Format("Игрок {0} отключился от игры", obj));
             Log(string.Format("Игрок {0} отключился от игры", obj));
            Action Clear = () => comboBox.Items.Remove("User " + obj);
            Dispatcher.Invoke(Clear);
        }

        private void S1_NewMessage(string obj)
        {
            //Action Log = () => listBox.Items.Add(obj);
            //Dispatcher.Invoke(Log);
            //тут обработка сообщения
            SrvMsgConvertet.Parse(obj);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Log("Сервер включен! \nОжидается подключение игроков...");
            new Thread(S1.UDPShown).Start();
            new Thread(S1.StartSRV).Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Log("Игра начинается!");
            S1.ListenAll();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //S1.SendTo((byte)comboBox.SelectedIndex, textBox.Text);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
