using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    class Client
    {
        /// <summary>
        /// Переменная обеспечивающая базовую функциональность приложения сокета.
        /// </summary>
        public Socket MainSocket;

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Бесконечно слушает сокет и генерирует события.
        /// </summary>
        public void Recive()
        {
            try
            {
                while (true)
                {
                    byte[] bufer = new byte[1024];
                    int bytesRec = MainSocket.Receive(bufer);
                    if (HaveMessage != null)
                        HaveMessage(Encoding.ASCII.GetString(bufer, 0, bytesRec), this);
                }
            }
            catch (Exception)
            {
                if (ClientDisconect != null)
                    ClientDisconect(this);
            }
        }

        /// <summary>
        /// Отправляем сообщение.
        /// </summary>
        public void Send(string msg)
        {
            byte[] bufer = Encoding.ASCII.GetBytes(msg);
            MainSocket.Send(bufer);
        }

        /// <summary>
        /// Конструктор, инициализирующий клиента связывая с заданным сокетом и присваивающий заданное имя.
        /// </summary>
        public Client(Socket ConnectedSocked, string UserName = "Ivan")
        {
            Name = UserName;
            MainSocket = ConnectedSocked;
        }

        /// <summary>
        /// Делегат, который реализует метод приема сообщений.
        /// </summary>
        public event Action<string, Client> HaveMessage;

        /// <summary>
        /// Делегат, реализующий метод, который отвечает за разрыв связи.
        /// </summary>
        public event Action<Client> ClientDisconect;
    }
}
