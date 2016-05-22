using System;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// Интерфейс который отвечает фиг знает за, что. Сиднев - Лалка!
    /// </summary>
    interface IServerMessanger
    {
        /// <summary>
        /// Номер порта приложения.
        /// </summary>
        int port { get; }

        /// <summary>
        /// Список содержащий строки.
        /// </summary>
        List<string> Msgs { get; }

        void SendTo(byte ID, string msg);
        void ListenAll();
        void StartSRV();
        void UDPShown();

        /// <summary>
        /// Событие, отвечающее за новое сообщение.
        /// </summary>
        event Action<string> NewMessage;
        /// <summary>
        /// Событие, отвечающее за разрыв с сервером.
        /// </summary>
        event Action<string> ClientDisconnect;
        /// <summary>
        /// Событие, которое вызывается, когда пользователь пытается подключится к серверу.
        /// </summary>
        event Action<string> UserConnected;

    }
}
